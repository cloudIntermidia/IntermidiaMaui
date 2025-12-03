using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        protected readonly SQLiteAsyncConnection _sqlAsyncConnection;
        protected static readonly AsyncLock asyncLock = new AsyncLock();
        protected readonly INivelRepository _nivelRepository;
        private readonly IProdutoRepository _produtoRepository;
        protected readonly ITabelaPrecoRepository _tabelaPrecoRepository;
        public CarrinhoRepository(ISqliteConnection context, INivelRepository nivelRepository, ITabelaPrecoRepository tabelaPrecoRepository, IProdutoRepository produtoRepository)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
            _nivelRepository = nivelRepository;
            _tabelaPrecoRepository = tabelaPrecoRepository;
            _produtoRepository = produtoRepository;
        }

        public virtual async Task<CarrinhoCommandResult> FindCarrinho(BuscarCarrinhoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CARRINHO_GET", "Query", command);
            var carrinho = await _sqlAsyncConnection.QueryAsync<CarrinhoCommandResult>(sql);
            return carrinho.FirstOrDefault();
        }

        public virtual async Task<List<CarrinhoCommandResult>> GetCarrinhos(BuscarCarrinhoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CARRINHO_GET", "Query", command);
            var carrinhos = await _sqlAsyncConnection.QueryAsync<CarrinhoCommandResult>(sql);
            if (carrinhos?.Count > 0)
            {
                foreach (var car in carrinhos)
                {
                    car.Itens = await BuscarItensCarrinho(new BuscarItensCarrinhoCommand(car.CodCarrinho, car.CodTabelaPreco));
                    foreach (var item in car.Itens)
                    {
                        var grades = await BuscarGradesDoItem(new BuscarGradesItemCommand(car.CodCarrinho, item.CodItemCarrinho));
                        foreach (var grade in grades)
                        {
                            item.Grades.Add(grade);
                        }
                    }
                }
            }

            return carrinhos;
        }

        public virtual async Task<ObservableCollection<ItemCommandResult>> BuscarItensCarrinho(BuscarItensCarrinhoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_ITEM_CARRINHO_GET", "Query", command);
            var itens = await _sqlAsyncConnection.QueryAsync<ItemCommandResult>(sql);
            return new ObservableCollection<ItemCommandResult>(itens);
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarGradesDoItem(BuscarGradesItemCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_GRADE_ITEM_CARRINHO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<ItemCommandResult> BuscarItemCarrinho(BuscarItensCarrinhoCommand command)
        {
            var itens = await BuscarItensCarrinho(command);
            return itens.FirstOrDefault();
        }

        public virtual async Task<CarrinhoCommandResult> Criar(
            TBT_CARRINHO carrinho, List<TBT_ITEM_CARRINHO> itens,
            List<TBT_GRADE_ITEM_CARRINHO> grades, List<TBT_CARRINHO_NIVEL> niveisCarrinho)
        {
            try
            {
                string sql = string.Empty;

                #region Tabela de Preço 

                if (itens != null && itens.Count > 0)
                {
                    var valorTabelaPreco = await _sqlAsyncConnection.ExecuteScalarAsync<string>($"SELECT DISTINCT CodTabelaPreco " +
                                                                                                $"FROM TBT_CLIENTE_TABELA_PRECO " +
                                                                                                $"WHERE CodCliente = '{carrinho.CodPessoaCliente}' AND " +
                                                                                                $"CodEmpresa = (SELECT CodEmpresa FROM TBT_PRODUTO WHERE CodProduto = '{itens[0].CodProduto}') LIMIT 1;");
                    if (!string.IsNullOrEmpty(valorTabelaPreco))
                        carrinho.CodTabelaPreco = valorTabelaPreco;
                }

                #endregion Tabela de Preço 

                var camposCarrinho = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_CARRINHO);")).Select(x => x.name).ToList();
                var camposItem = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_ITEM_CARRINHO);")).Select(x => x.name).ToList();
                using (await asyncLock.LockAsync())
                {
                    _sqlAsyncConnection.GetConnection().BeginTransaction();

                    //int rowsCarrinho = await _sqlAsyncConnection.InsertAsync(carrinho);
                    sql = ManagerQuery.MakeInsertOrReplace(camposCarrinho, nameof(TBT_CARRINHO), carrinho);
                    int rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                    foreach (var item in itens)
                    {
                        sql = ManagerQuery.MakeInsertOrReplace(camposItem, nameof(TBT_ITEM_CARRINHO), item);
                        rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                    }
                    int rowsGradesItens = await _sqlAsyncConnection.InsertAllAsync(grades);
                    int rowsniveisCarrinho = await _sqlAsyncConnection.InsertAllAsync(niveisCarrinho);

                    _sqlAsyncConnection.GetConnection().Commit();

                    //await AtualizaQtdCarrinho(carrinho.CodCarrinho);

                    Atualiza_Desconto_Itens(carrinho);

                    return await FindCarrinho(new BuscarCarrinhoCommand(carrinho.CodCarrinho, carrinho.CodAtendimento, "1", carrinho.CodUsuario, carrinho.CodPessoaCliente, carrinho.CodTipoPedido, carrinho.CodMarca));
                }
            }
            catch (SQLiteException sqliteException)
            {
                if (sqliteException.Result == SQLite3.Result.Busy ||
                    sqliteException.Result == SQLite3.Result.Constraint)
                {
                    return await Criar(carrinho, itens, grades, niveisCarrinho);
                }

                throw sqliteException;
            }
            catch (Exception ex)
            {
                _sqlAsyncConnection.GetConnection().Rollback();
                throw ex;
            }
        }

        public virtual async Task<CarrinhoCommandResult> Atualizar(TBT_CARRINHO carrinho, List<TBT_ITEM_CARRINHO> itens, List<TBT_GRADE_ITEM_CARRINHO> grades)
        {
            try
            {
                List<TBT_ITEM_CARRINHO> itensNoCarrinho = await _sqlAsyncConnection.Table<TBT_ITEM_CARRINHO>().Where(x => x.CodCarrinho == carrinho.CodCarrinho).ToListAsync();
                List<TBT_GRADE_ITEM_CARRINHO> gradesNoCarrinho = await _sqlAsyncConnection.Table<TBT_GRADE_ITEM_CARRINHO>().Where(x => x.CodCarrinho == carrinho.CodCarrinho).ToListAsync();

                var camposItem = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_ITEM_CARRINHO);")).Select(x => x.name).ToList();
                var camposGrade = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_GRADE_ITEM_CARRINHO);")).Select(x => x.name).ToList();
                string sql = string.Empty;

                foreach (var item in itens)
                {
                    item.QtdCaixa = item.QtdCaixa;
                    item.QtdTotal = item.Grades.Sum(x => x.Qtd);
                    using (await asyncLock.LockAsync())
                    {
                        int rowsItens = 0;
                        if (itensNoCarrinho.Any(x => x.CodProduto == item.CodProduto && x.CodDeposito == item.CodDeposito && x.CodGrade == item.CodGrade && x.CodKit == item.CodKit))
                        {
                            item.CodItemCarrinho = itensNoCarrinho.Where(x => x.CodProduto == item.CodProduto && x.CodDeposito == item.CodDeposito && x.CodGrade == item.CodGrade).Select(x => x.CodItemCarrinho).FirstOrDefault();
                            sql = ManagerQuery.MakeUpdate(camposItem, nameof(TBT_ITEM_CARRINHO), new List<string>() { "CodCarrinho", "CodItemCarrinho" }, item);
                            rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                        }
                        else
                        {
                            item.CodItemCarrinho = await GerarNovoCodItemCarrinho(carrinho);
                            sql = ManagerQuery.MakeInsertOrReplace(camposItem, nameof(TBT_ITEM_CARRINHO), item);
                            rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                        }
                    }

                    using (await asyncLock.LockAsync())
                    {
                        foreach (var grade in item.Grades)
                        {
                            int rowsGradesItens = 0;
                            grade.CodItemCarrinho = item.CodItemCarrinho;
                            var gradesDoItem = gradesNoCarrinho.Where(x => x.CodItemCarrinho == item.CodItemCarrinho).ToList();
                            if (gradesDoItem != null && gradesDoItem.Any(x => x.CodDerivacao == grade.CodDerivacao))
                            {
                                grade.CodGradeItemCarrinho = gradesDoItem.Where(x => x.CodDerivacao == grade.CodDerivacao).Select(x => x.CodGradeItemCarrinho).FirstOrDefault();
                                sql = ManagerQuery.MakeUpdate(camposGrade, nameof(TBT_GRADE_ITEM_CARRINHO), new List<string>() { "CodCarrinho", "CodItemCarrinho", "CodGradeItemCarrinho" }, grade);
                                rowsGradesItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                            }
                            else
                            {
                                grade.CodGradeItemCarrinho = await GerarNovoCodGradeItemCarrinho(item);
                                rowsGradesItens = await _sqlAsyncConnection.InsertAsync(grade);
                            }
                        }
                    }
                }

                Atualiza_Desconto_Itens(carrinho);

                return await FindCarrinho(new BuscarCarrinhoCommand(carrinho.CodCarrinho, carrinho.CodAtendimento, "1", carrinho.CodUsuario, carrinho.CodPessoaCliente, carrinho.CodTipoPedido, carrinho.CodMarca));
            }
            catch (SQLiteException sqliteException)
            {
                if (sqliteException.Result == SQLite3.Result.Busy ||
                    sqliteException.Result == SQLite3.Result.Constraint)
                {
                    return await Atualizar(carrinho, itens, grades);
                }

                throw;
            }
            catch (Exception ex)
            {
                Debug.Write("CarrinhoRepository => Atualizar Carrinho");
                Debug.Write(ex.Message);
            }

            return null;
        }

        //        private async void Atualiza_Desconto_Itens(TBT_CARRINHO carrinho, List<TBT_ITEM_CARRINHO> itens, List<TBT_GRADE_ITEM_CARRINHO> grades)
        private async void Atualiza_Desconto_Itens(TBT_CARRINHO carrinho)
        {
            string sql = string.Empty;
            //sql = ManagerQuery.MakeSql("CARRINHO_UPDATE_QTD", "Function", carrinho);
            //var statements = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (var statement in statements)
            //{
            //    using (await asyncLock.LockAsync())
            //    {
            //        int rowsAffected = await _sqlAsyncConnection.ExecuteAsync(statement);
            //    }
            //}

            sql = ManagerQuery.MakeSql("DELETE_ITENS_ZERADOS", "Function", carrinho);
            var statements = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var statement in statements)
            {
                using (await asyncLock.LockAsync())
                {
                    int rowsAffected = await _sqlAsyncConnection.ExecuteAsync(statement);
                }
            }

            sql = ManagerQuery.MakeSql("CARRINHO_UPDATE_QTD", "Function", carrinho);
            statements = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var statement in statements)
            {
                using (await asyncLock.LockAsync())
                {
                    int rowsAffected = await _sqlAsyncConnection.ExecuteAsync(statement);
                }
            }

            ////Aplica descontos de itens.
            //foreach (var item in itens)
            //{
            //    sql = ManagerQuery.MakeSql("ITEM_CARRINHO_UPDATE", "Function", item);
            //    statements = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var statement in statements)
            //    {
            //        using (await asyncLock.LockAsync())
            //        {
            //            int rowsAffected = await _sqlAsyncConnection.ExecuteAsync(statement);
            //        }
            //    }
            //}

            List<TBT_ITEM_CARRINHO> listaItensCarrinho = await _sqlAsyncConnection.Table<TBT_ITEM_CARRINHO>().Where(x => x.CodCarrinho == carrinho.CodCarrinho).ToListAsync();

            //Aplica descontos de itens.
            foreach (var item in listaItensCarrinho)
            {
                sql = ManagerQuery.MakeSql("ITEM_CARRINHO_UPDATE", "Function", item);
                statements = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var statement in statements)
                {
                    using (await asyncLock.LockAsync())
                    {
                        int rowsAffected = await _sqlAsyncConnection.ExecuteAsync(statement);
                    }
                }
            }
        }

        public virtual async Task<string> GerarNovoCodCarrinho(UsuarioCommandResult command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CARRINHO_GET_LASTCOD", "Query", command);
            int cod = await _sqlAsyncConnection.ExecuteScalarAsync<int>(sql);
            return $"{command.CodUsuario}.{command.CodInstalacao}.{cod}";
        }

        public virtual async Task<int> GerarNovoCodItemCarrinho(TBT_CARRINHO carrinho)
        {
            var item = await _sqlAsyncConnection.Table<TBT_ITEM_CARRINHO>()
                                                .Where(o => o.CodCarrinho == carrinho.CodCarrinho)
                                                .OrderByDescending(o => o.CodItemCarrinho)
                                                .FirstOrDefaultAsync();

            return item == null ? 1 : (item.CodItemCarrinho + 1);
        }


        public virtual async Task<int> GerarNovoCodGradeItemCarrinho(TBT_ITEM_CARRINHO item)
        {
            var grade = await _sqlAsyncConnection.Table<TBT_GRADE_ITEM_CARRINHO>()
                                                    .Where(o => o.CodCarrinho == item.CodCarrinho && o.CodItemCarrinho == item.CodItemCarrinho)
                                                    .OrderByDescending(o => o.CodGradeItemCarrinho)
                                                    .FirstOrDefaultAsync();

            int codGradeItemCarrinho = grade == null ? 1 : (grade.CodGradeItemCarrinho + 1);
            return codGradeItemCarrinho;
        }

        public virtual async Task<string> QuebrarPedido(QuebrarPedidoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CARRINHO_QUEBRA", "Query", command);
            var carrinhos = await _sqlAsyncConnection.QueryAsync<TBT_CARRINHO>(sql);

            if (carrinhos?.Count == 0)
                return null;

            //Buscar todos os atributos de cada nivel de quebra.
            List<NivelProdutoResult> niveisDoProduto = await _nivelRepository.GetNiveisDeQuebraProduto(command.CodProduto);
            string codCarrinho = null;

            Int16 numContador = 0;

            foreach (var carrinho in carrinhos)
            {
                numContador = 0;
                codCarrinho = carrinho.CodCarrinho;
                foreach (var nivelQuebraDoProduto in niveisDoProduto)
                {
                    numContador++;
                    string carrinhoAuxiliar = null;
                    string sqlQuebraCarrinho = ManagerQuery.MakeSql("PRO_CARRINHO_NIVEL", "Query", new { CodCarrinho = carrinho.CodCarrinho, CodNivel = nivelQuebraDoProduto.CodNivel, nivelQuebraDoProduto.CodAtributo });
                    carrinhoAuxiliar = await _sqlAsyncConnection.ExecuteScalarAsync<string>(sqlQuebraCarrinho);

                    // Se um dos níveis não estiverem no carrinho pula para o próximo
                    if (!string.IsNullOrEmpty(carrinhoAuxiliar))
                    {
                        codCarrinho = carrinhoAuxiliar;
                    }
                    else
                    {
                        //break;
                        if (numContador == niveisDoProduto.Count)
                        {
                            codCarrinho = String.Empty;
                        }
                        //else
                        //{
                        //    break;
                        //}
                    }

                }

                // Se achou um carrinho compatível retorna ele mesmo.
                if (!string.IsNullOrEmpty(codCarrinho))
                    break;
            }

            return codCarrinho;
        }

        public virtual async Task<bool> AtualizaQtdCarrinho(string codCarrinho)
        {
            int rowsAffected = 0;
            try
            {
                string sql = ManagerQuery.MakeSql("CARRINHO_UPDATE_QTD", "Function", new { CodCarrinho = codCarrinho });
                var statements = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var statement in statements)
                {
                    rowsAffected = await _sqlAsyncConnection.ExecuteAsync(statement);
                }
            }
            catch (SQLiteException sqliteException)
            {
                if (sqliteException.Result == SQLite3.Result.Busy)
                {
                    await AtualizaQtdCarrinho(codCarrinho);
                }

                throw;
            }
            catch (Exception ex)
            {
                Debug.Write("CarrinhoRepository => Atualizar QTD Carrinho");
                Debug.Write(ex.Message);
            }

            return rowsAffected > 0;
        }

        public virtual async Task<bool> ApagaItensZerados(string codCarrinho)
        {
            int rowsAffected = 0;
            try
            {
                string sql = ManagerQuery.MakeSql("DELETE_ITENS_ZERADOS", "Function", new { CodCarrinho = codCarrinho });
                var statements = sql.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var statement in statements)
                {
                    rowsAffected = await _sqlAsyncConnection.ExecuteAsync(statement);
                }
            }
            catch (SQLiteException sqliteException)
            {
                if (sqliteException.Result == SQLite3.Result.Busy)
                {
                    await AtualizaQtdCarrinho(codCarrinho);
                }

                throw;
            }
            catch (Exception ex)
            {
                Debug.Write("CarrinhoRepository => Atualizar QTD Carrinho");
                Debug.Write(ex.Message);
            }


            return rowsAffected > 0;
        }

        public virtual async Task<bool> Cancelar(CancelarCarrinhoCommand command)
        {
            string sql = ManagerQuery.MakeSql("CARRINHO_CANCELAR", "CRUD", command);
            var carrinhos = await _sqlAsyncConnection.ExecuteAsync(sql);
            return carrinhos > 0;
        }

        public virtual async Task<bool> SalvarFechamento(AtualizarCarrinhoFechamentoCommand command)
        {
            var camposCarrinho = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_CARRINHO);")).Select(x => x.name).ToList();
            string sql = ManagerQuery.MakeUpdate(camposCarrinho, nameof(TBT_CARRINHO), new List<string>() { "CodCarrinho" }, command);
            var carrinhos = await _sqlAsyncConnection.ExecuteAsync(sql);

            await AtualizaQtdCarrinho(command.CodCarrinho);
            return carrinhos > 0;
        }

        public virtual async Task<bool> DesmembrarItens(string codCarrinhoDestino, List<TBT_ITEM_CARRINHO> itens)
        {
            try
            {
                var itensCarrinhoDestino = await _sqlAsyncConnection.Table<TBT_ITEM_CARRINHO>().Where(x => x.CodCarrinho == codCarrinhoDestino).ToListAsync();
                List<TBT_GRADE_ITEM_CARRINHO> gradesNoCarrinho = await _sqlAsyncConnection.Table<TBT_GRADE_ITEM_CARRINHO>().Where(x => x.CodCarrinho == codCarrinhoDestino).ToListAsync();

                string sql = string.Empty;
                int rowsItens = 0;
                int rowsGradesItens = 0;
                var camposCarrinho = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_CARRINHO);")).Select(x => x.name).ToList();
                var camposItem = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_ITEM_CARRINHO);")).Select(x => x.name).ToList();
                var camposGrade = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_GRADE_ITEM_CARRINHO);")).Select(x => x.name).ToList();

                _sqlAsyncConnection.GetConnection().BeginTransaction();

                foreach (var item in itens)
                {
                    var itemExiste = itensCarrinhoDestino.Where(x => x.CodProduto == item.CodProduto).FirstOrDefault();
                    if (itemExiste != null)
                    {
                        item.QtdCaixa = item.Grades.Sum(x => x.Qtd);
                        item.QtdTotal = item.Grades.Sum(x => x.Qtd);
                        using (await asyncLock.LockAsync())
                        {
                            rowsItens = 0;
                            item.CodItemCarrinho = itensCarrinhoDestino.Where(x => x.CodProduto == item.CodProduto && x.CodDeposito == item.CodDeposito && x.CodGrade == item.CodGrade).Select(x => x.CodItemCarrinho).FirstOrDefault();
                            sql = ManagerQuery.MakeUpdate(camposItem, nameof(TBT_ITEM_CARRINHO), new List<string>() { "CodCarrinho", "CodItemCarrinho" }, item);
                            rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);


                            foreach (var grade in item.Grades)
                            {
                                rowsGradesItens = 0;
                                grade.CodItemCarrinho = item.CodItemCarrinho;
                                var gradesDoItem = gradesNoCarrinho.Where(x => x.CodItemCarrinho == item.CodItemCarrinho && x.CodDerivacao == grade.CodDerivacao).FirstOrDefault();
                                if (gradesDoItem != null)
                                {
                                    grade.CodGradeItemCarrinho = gradesDoItem.CodGradeItemCarrinho;
                                    sql = ManagerQuery.MakeUpdate(camposGrade, nameof(TBT_GRADE_ITEM_CARRINHO), new List<string>() { "CodCarrinho", "CodItemCarrinho", "CodGradeItemCarrinho" }, grade);
                                    rowsGradesItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                                }
                                else
                                {
                                    grade.CodGradeItemCarrinho = await GerarNovoCodGradeItemCarrinho(item);
                                    rowsGradesItens = await _sqlAsyncConnection.InsertAsync(grade);
                                }
                            }
                        }
                    }
                    else
                    {
                        item.CodItemCarrinho = await GerarNovoCodItemCarrinho(new TBT_CARRINHO() { CodCarrinho = codCarrinhoDestino });
                        sql = ManagerQuery.MakeInsertOrReplace(camposItem, nameof(TBT_ITEM_CARRINHO), item);
                        rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);

                        item.Grades.ForEach(x => x.CodItemCarrinho = item.CodItemCarrinho);
                        rowsGradesItens = await _sqlAsyncConnection.InsertAllAsync(item.Grades);
                    }
                }

                _sqlAsyncConnection.GetConnection().Commit();

                await AtualizaQtdCarrinho(codCarrinhoDestino);
                return true;
            }
            catch (SQLiteException sqliteException)
            {
                if (sqliteException.Result == SQLite3.Result.Busy ||
                    sqliteException.Result == SQLite3.Result.Constraint)
                {
                    return await DesmembrarItens(codCarrinhoDestino, itens);
                }

                throw;
            }
            catch (Exception ex)
            {
                Debug.Write("CarrinhoRepository => Desmembrar itens.");
                Debug.Write(ex.Message);
            }

            return false;
        }

        public virtual async Task<WcfPedidoModelInput> BuscarCarrinhoParaTransmissao(string codCarrinho, string codSuframa = "", Boolean materialPDV = false)
        {
            var carrinho = await _sqlAsyncConnection.FindAsync<TBT_CARRINHO>(x => x.CodCarrinho == codCarrinho);
            var itens = await _sqlAsyncConnection.Table<TBT_ITEM_CARRINHO>().Where(x => x.CodCarrinho == codCarrinho).ToListAsync();
            var grades = await _sqlAsyncConnection.Table<TBT_GRADE_ITEM_CARRINHO>().Where(x => x.CodCarrinho == codCarrinho).ToListAsync();

            if (carrinho.CodPessoaCliente.Contains("."))
            {
                var codPessoaClienteERP = await _sqlAsyncConnection.ExecuteScalarAsync<string>($"SELECT CodPessoaERP FROM TBT_PESSOA WHERE CodPessoa = '{carrinho.CodPessoaCliente}' LIMIT 1;");
                if (!string.IsNullOrEmpty(codPessoaClienteERP))
                    carrinho.CodPessoaCliente = codPessoaClienteERP;

            }

            WcfPedidoModelInput retorno = new WcfPedidoModelInput();


            foreach (var item in itens)
            {
                item.Grades = new List<TBT_GRADE_ITEM_CARRINHO>();
                var lstGrades = grades.Where(x => x.CodCarrinho == item.CodCarrinho && x.CodItemCarrinho == item.CodItemCarrinho).ToList();
                if (lstGrades != null)
                {
                    item.Grades.AddRange(lstGrades);
                }
            }

            carrinho.MaterialPDV = materialPDV;

            PEDIDOVENDA pedido = new PEDIDOVENDA();
            pedido.Carrinho = carrinho;
            pedido.Itens = itens;

            retorno.PEDIDOVENDA = pedido;
            return retorno;
        }

        public virtual async Task<CarrinhoCommandResult> BuscarPedidoCopia(string codPedido)
        {
            var carrinho = new CarrinhoCommandResult();

            var sql = "SELECT  " +
                        " Observacoes, " +
                        " OrdemCompra, " +
                        " AceitaFaturamentoAntecipado, " +
                        " CodCondicaoPagamento, " +
                        " PercentualDesconto, " +
                        " CodTransportadora " +
                        " FROM TBT_PEDIDO " +
                        $" WHERE CodPedido = '{codPedido}' ";

            var carrinhoResult = await _sqlAsyncConnection.QueryAsync<CarrinhoCommandResult>(sql);

            if (carrinhoResult.Count > 0)
            {
                carrinho = carrinhoResult.FirstOrDefault();

                sql = "SELECT  " +
                " CodProduto, " +
                " QtdTotal " +
                " FROM TBT_ITEM_PEDIDO " +
                $" WHERE CodPedido = '{codPedido}' ";

                var itens = await _sqlAsyncConnection.QueryAsync<ItemCommandResult>(sql);

                foreach (var item in itens)
                {
                    carrinho.Itens.Add(item);
                }
            }

            return carrinho;
        }

        //        public virtual async Task<bool> AtualizarPedidoImplantado(string codPedido, string codSituacaoPedido, string codCarrinho)
        public virtual async Task<bool> AtualizarPedidoImplantado(string codPedido, string codSituacaoPedido, string codCarrinho, Decimal valorFinalComImposto)
        {
            //            string sql = ManagerQuery.MakeSql("CARRINHO_IMPLANTAR", "CRUD", new { CodPedido = codPedido, CodSituacaoPedido = codSituacaoPedido, CodCarrinho = codCarrinho });
            string sql = ManagerQuery.MakeSql("CARRINHO_IMPLANTAR", "CRUD", new { CodPedido = codPedido, CodSituacaoPedido = codSituacaoPedido, CodCarrinho = codCarrinho, ValorFinalComImposto = valorFinalComImposto });
            var carrinhos = await _sqlAsyncConnection.ExecuteAsync(sql);
            return carrinhos > 0;
        }

        public virtual async Task<bool> AlterarSituacao(string codCarrinho, string codSituacaoPedido)
        {
            string sql = $"UPDATE TBT_CARRINHO SET CodSituacaoPedido = '{codSituacaoPedido}' where CodCarrinho = '{codCarrinho}';";
            var carrinhos = await _sqlAsyncConnection.ExecuteAsync(sql);
            return carrinhos > 0;
        }

        public virtual async Task<bool> AlterarPedidoDestino(string codPedidoDestino, string codCarrinhoAgrupado)
        {
            string sql = $"UPDATE TBT_CARRINHO SET CodPedidoDestino = '{codPedidoDestino}' where CodCarrinho = '{codCarrinhoAgrupado}';";
            var carrinhos = await _sqlAsyncConnection.ExecuteAsync(sql);
            return carrinhos > 0;
        }

        public virtual async Task<bool> AlterarAtendimento(string codCarrinho, string codAtendimento)
        {
            string sql = $"UPDATE TBT_CARRINHO SET CodAtendimento = '{codAtendimento}' where CodCarrinho = '{codCarrinho}';";
            var carrinhos = await _sqlAsyncConnection.ExecuteAsync(sql);
            return carrinhos > 0;
        }

        //        public virtual async Task<List<string>> ValidacoesDoCarrinho(string codCarrinho)
        public virtual async Task<List<string>> ValidacoesDoCarrinho(string codCarrinho, Boolean validaTipoFrete)

        {
            await Task.Delay(1);
            return new List<string>();
        }

        public virtual async Task AlterarClienteDoCarrinho(string codCarrinho, string codPessoaCliente)
        {
            string sql = $"UPDATE TBT_CARRINHO SET CodPessoaCliente = '{codPessoaCliente}' WHERE CodCarrinho = '{codCarrinho}';";
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync(sql);
        }

        public virtual async Task<List<ItemCommandResult>> GetListItensResumo(CarrinhoCommandResult carrinho, string codNivelAgrupamento)
        {
            string sql = ManagerQuery.MakeSql("PRO_ITENS_RESUMO_GET", "Query", new
            {
                CodPessoaRepresentante = carrinho.CodPessoaRepresentante,
                CodCarrinho = carrinho.CodCarrinho,
                CodPedido = carrinho.CodPedido,
                CodNivelAgrupamento = codNivelAgrupamento
            });
            var oLista = await _sqlAsyncConnection.QueryAsync<ItemCommandResult>(sql);
            return oLista;
        }

        public virtual async Task AlterarItensCarrinho(string codCarrinho, string CodTabelaPreco, decimal valorUnitario, decimal PercDesc1, decimal PercDesc2, string codAtributo)
        {
            string sql = $"UPDATE TBT_ITEM_CARRINHO " +
                         $"SET CodTabelaPreco = '{CodTabelaPreco}' , " +
                         $"ValorUnitario = '{valorUnitario.ToString().Replace(",", ".")}' , " +
                         $"PercDesc1 = '{PercDesc1}' " +
                         $"WHERE EXISTS (" +
                         $"SELECT * FROM TBT_NIVEL_PRODUTO WHERE (TBT_ITEM_CARRINHO.CodProduto = TBT_NIVEL_PRODUTO.CodProduto AND TBT_NIVEL_PRODUTO.CodNivel = 6) " +
                         $"AND ( TBT_NIVEL_PRODUTO.CodAtributo='{codAtributo}' AND TBT_ITEM_CARRINHO.CodCarrinho = '{codCarrinho}'))" +
                         $"AND NOT EXISTS (SELECT 1 FROM TBT_PRODUTO_RELACIONADO " +
                         $"WHERE TBT_PRODUTO_RELACIONADO.CodProduto = TBT_ITEM_CARRINHO.CodProduto OR " +
                         $"TBT_PRODUTO_RELACIONADO.CodProdutoRelacionado = TBT_ITEM_CARRINHO.CodProduto)" +
                         $";";
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync(sql);

            // Atualiza itens de conjunto para o preco da tabela
            sql = $"UPDATE TBT_ITEM_CARRINHO " +
                  $"SET " +
                  $"ValorUnitario = (SELECT Preco FROM TBT_ITEM_TABELA_PRECO WHERE TBT_ITEM_TABELA_PRECO.CodProduto = TBT_ITEM_CARRINHO.CodProduto AND TBT_ITEM_TABELA_PRECO.CodTabelaPreco = '{CodTabelaPreco}') , " +
                  $"PercDesc1 = '{PercDesc1}' " +
                  $"WHERE EXISTS (" +
                  $"SELECT * FROM TBT_NIVEL_PRODUTO WHERE (TBT_ITEM_CARRINHO.CodProduto = TBT_NIVEL_PRODUTO.CodProduto AND TBT_NIVEL_PRODUTO.CodNivel = 6) " +
                  $"AND ( TBT_NIVEL_PRODUTO.CodAtributo='{codAtributo}' AND TBT_ITEM_CARRINHO.CodCarrinho = '{codCarrinho}'))" +
                  $"AND EXISTS (SELECT 1 FROM TBT_PRODUTO_RELACIONADO " +
                  $"WHERE TBT_PRODUTO_RELACIONADO.CodProduto = TBT_ITEM_CARRINHO.CodProduto OR " +
                  $"TBT_PRODUTO_RELACIONADO.CodProdutoRelacionado = TBT_ITEM_CARRINHO.CodProduto)" +
                  $";";

            rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync(sql);

            await AtualizaQtdCarrinho(codCarrinho);
        }

        public virtual async Task<bool> Copiar(CopiarCarrinhoCommand command)
        {
            try
            {
                string sql = string.Empty;
                string codCarrinhoNovo = await GerarNovoCodCarrinho(command.Usuario);


                TBT_CARRINHO carrinho = await _sqlAsyncConnection.GetAsync<TBT_CARRINHO>(x => x.CodCarrinho == command.CodCarrinhoOrigem);
                List<TBT_ITEM_CARRINHO> itens = await _sqlAsyncConnection.Table<TBT_ITEM_CARRINHO>().Where(x => x.CodCarrinho == command.CodCarrinhoOrigem).ToListAsync();
                List<TBT_GRADE_ITEM_CARRINHO> grades = await _sqlAsyncConnection.Table<TBT_GRADE_ITEM_CARRINHO>().Where(x => x.CodCarrinho == command.CodCarrinhoOrigem).ToListAsync();
                List<TBT_CARRINHO_NIVEL> niveisCarrinho = await _sqlAsyncConnection.Table<TBT_CARRINHO_NIVEL>().Where(x => x.CodCarrinho == command.CodCarrinhoOrigem).ToListAsync();

                carrinho.CodCarrinho = codCarrinhoNovo;
                carrinho.CodAtendimento = command.CodAtendimento;
                carrinho.CodSituacaoPedido = "1";
                carrinho.CodPessoaCliente = command.CodPessoaCliente;
                itens.ForEach(x => x.CodCarrinho = codCarrinhoNovo);
                grades.ForEach(x => x.CodCarrinho = codCarrinhoNovo);
                niveisCarrinho.ForEach(x => x.CodCarrinho = codCarrinhoNovo);


                var camposCarrinho = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_CARRINHO);")).Select(x => x.name).ToList();
                var camposItem = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_ITEM_CARRINHO);")).Select(x => x.name).ToList();
                using (await asyncLock.LockAsync())
                {
                    _sqlAsyncConnection.GetConnection().BeginTransaction();

                    sql = ManagerQuery.MakeInsertOrReplace(camposCarrinho, nameof(TBT_CARRINHO), carrinho);
                    int rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                    foreach (var item in itens)
                    {
                        sql = ManagerQuery.MakeInsertOrReplace(camposItem, nameof(TBT_ITEM_CARRINHO), item);
                        rowsItens = await _sqlAsyncConnection.ExecuteAsync(sql);
                    }
                    int rowsGradesItens = await _sqlAsyncConnection.InsertAllAsync(grades);
                    int rowsniveisCarrinho = await _sqlAsyncConnection.InsertAllAsync(niveisCarrinho);

                    _sqlAsyncConnection.GetConnection().Commit();

                    await AtualizaQtdCarrinho(carrinho.CodCarrinho);
                    return true;
                }
            }
            catch (SQLiteException sqliteException)
            {
                if (sqliteException.Result == SQLite3.Result.Busy ||
                    sqliteException.Result == SQLite3.Result.Constraint)
                {
                    return await Copiar(command);
                }

                throw sqliteException;
            }
            catch (Exception ex)
            {
                _sqlAsyncConnection.GetConnection().Rollback();
                throw ex;
            }
        }

        public virtual async Task BloquearCarrinhos(List<CarrinhoCommandResult> carrinhos)
        {
            string sql = string.Empty;
            decimal indBloqueado = 0;
            foreach (var carrinho in carrinhos)
            {
                indBloqueado = carrinho.IndBloqueado == 0 ? 1 : 0;
                sql = $"UPDATE TBT_CARRINHO SET IndBloqueado = {indBloqueado} WHERE CodCarrinho = '{carrinho.CodCarrinho}';";

                await _sqlAsyncConnection.ExecuteAsync(sql);
            }

        }

        public virtual async Task<List<CriticaCarrinhoCommandResult>> BuscarCriticas(string codCarrinho)
        {
            string sql = ManagerQuery.MakeSql("PRO_CRITICA_CARRINHO_GET", "Query", new { CodCarrinho = codCarrinho });
            var criticas = await _sqlAsyncConnection.QueryAsync<CriticaCarrinhoCommandResult>(sql);
            return criticas;
        }

        public virtual async Task<bool> AtualizarDataFaturamento(string codCarrinho)
        {
            DateTime? maiorDataItem = await BuscarMaiorDataDoItem(codCarrinho);
            var dataCarrinhoString = await _sqlAsyncConnection.ExecuteScalarAsync<string>($"select dataFaturamento from TBT_CARRINHO WHERE CodCarrinho = '{codCarrinho}';");
            DateTime? dataCarrinho = string.IsNullOrEmpty(dataCarrinhoString) ? (DateTime?)null : DateTime.Parse(dataCarrinhoString);

            if (maiorDataItem.HasValue &&
                (!dataCarrinho.HasValue || dataCarrinho.Value < maiorDataItem))
            {
                string dateString = maiorDataItem.Value.ToString("yyyy-MM-ddTHH:mm:ss");
                int rows = await _sqlAsyncConnection.ExecuteAsync($"UPDATE TBT_CARRINHO SET dataFaturamento = '{dateString}' where CodCarrinho = '{codCarrinho}';");
                return rows > 0;
            }

            return true;
        }

        public virtual async Task<DateTime> BuscarMaiorDataDoItem(string codCarrinho)
        {
            var maiorDataItemString = await _sqlAsyncConnection.ExecuteScalarAsync<string>($"SELECT MAX(DataEntrega) FROM TBT_ITEM_CARRINHO WHERE CodCarrinho = '{codCarrinho}';");
            DateTime maiorDataItem = string.IsNullOrEmpty(maiorDataItemString) ? DateTime.Today : DateTime.Parse(maiorDataItemString);

            DateTime dta = maiorDataItem;
            string dtaStr = dta.ToString("dd/MM/yyyy HH:mm:ss");

            DateTime myDate = DateTime.ParseExact(dtaStr, "dd/MM/yyyy HH:mm:ss",
                           System.Globalization.CultureInfo.InvariantCulture);

            //myDate = myDate.AddDays(1);

            return myDate;
        }

        public virtual async Task<ObservableCollection<DerivacaoGradeResult>> GetListItensGradeResumo(string codCarrinho)
        {
            string sql = ManagerQuery.MakeSql("PRO_GRADE_ITENS_RESUMO_GET", "Query", new { CodCarrinho = codCarrinho });
            var oLista = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return new ObservableCollection<DerivacaoGradeResult>(oLista);
        }

        public virtual async Task ReordenarItens(string codCarrinho)
        {
            ObservableCollection<ItemCommandResult> itens2 = await BuscarItensCarrinho(new BuscarItensCarrinhoCommand(codCarrinho, null));
            int seq = 900;
            foreach (var item in itens2)
            {
                int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_ITEM_CARRINHO SET CodItemCarrinho = ? WHERE CodCarrinho = ? AND CodItemCarrinho = ?", seq, item.CodCarrinho, item.CodItemCarrinho);

                rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_GRADE_ITEM_CARRINHO SET CodItemCarrinho = ? WHERE CodCarrinho = ? AND CodItemCarrinho = ?", seq, item.CodCarrinho, item.CodItemCarrinho);
                seq++;
            }

            ObservableCollection<ItemCommandResult> itens3 = await BuscarItensCarrinho(new BuscarItensCarrinhoCommand(codCarrinho, null));
            seq = 1;
            foreach (var item in itens3)
            {
                int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_ITEM_CARRINHO SET CodItemCarrinho = ? WHERE CodCarrinho = ? AND CodItemCarrinho = ?", seq, item.CodCarrinho, item.CodItemCarrinho);

                rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_GRADE_ITEM_CARRINHO SET CodItemCarrinho = ? WHERE CodCarrinho = ? AND CodItemCarrinho = ?", seq, item.CodCarrinho, item.CodItemCarrinho);
                seq++;
            }
        }


        public virtual async Task ExcluirItens(List<TBT_ITEM_CARRINHO> itens)
        {
            foreach (var item in itens)
            {
                await _sqlAsyncConnection.ExecuteAsync("DELETE FROM TBT_GRADE_ITEM_CARRINHO WHERE CodCarrinho = ? AND CodItemCarrinho = ?", item.CodCarrinho, item.CodItemCarrinho);
                await _sqlAsyncConnection.ExecuteAsync("DELETE FROM TBT_ITEM_CARRINHO WHERE CodCarrinho = ? AND CodItemCarrinho = ?", item.CodCarrinho, item.CodItemCarrinho);
            }

            var itensCount = await _sqlAsyncConnection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM TBT_ITEM_CARRINHO WHERE CodCarrinho = ?", itens[0].CodCarrinho);
            if (itensCount == 0)
                await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_CARRINHO SET CodSituacaoPedido = '7' WHERE CodCarrinho = ?", itens[0].CodCarrinho);

            await AtualizaQtdCarrinho(itens[0].CodCarrinho);
        }

        public virtual async Task CadastraCarrinhoHistorico(string codCarrinho)
        {
            var contemCarrinho = await _sqlAsyncConnection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM TBT_HISTORICO_CARRINHO WHERE CodCarrinho = ?", codCarrinho);
            if (contemCarrinho == 0)
            {
                await _sqlAsyncConnection.ExecuteAsync("INSERT INTO TBT_HISTORICO_CARRINHO (CodCarrinho) VALUES (?) ", codCarrinho);
            }
        }

        protected async Task<int> DeleteItem(decimal codItemCarrinho)
        {
            string sql = $"DELETE FROM TBT_ITEM_CARRINHO WHERE CodItemCarrinho = {codItemCarrinho}";
            int rows = await _sqlAsyncConnection.ExecuteAsync(sql);
            return rows;
        }

        protected async Task<int> DeleteGradesDoItem(string codCarrinho, decimal codItemCarrinho)
        {
            string sql = $"DELETE FROM TBT_GRADE_ITEM_CARRINHO WHERE CodCarrinho = '{codCarrinho}' AND CodItemCarrinho = {codItemCarrinho}";
            int rows = await _sqlAsyncConnection.ExecuteAsync(sql);
            return rows;
        }

        protected async Task<int> DeleteGradeItem(decimal codItemCarrinho, string codDerivacao)
        {
            string sql = $"DELETE FROM TBT_GRADE_ITEM_CARRINHO WHERE CodItemCarrinho = {codItemCarrinho} AND CodDerivacao = '{codDerivacao}';";
            int rows = await _sqlAsyncConnection.ExecuteAsync(sql);
            return rows;
        }



    }
}
