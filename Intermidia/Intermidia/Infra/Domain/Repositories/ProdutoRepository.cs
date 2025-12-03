using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        protected readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public ProdutoRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }

        public virtual async Task<decimal> GetPreco(BuscarItemTabelaPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TABELAS_PRODUTO_GET", "Query", command);
            var produto = (await _sqlAsyncConnection.QueryAsync<ModeloCommandResult>(sql)).FirstOrDefault();
            return produto != null ? produto.PrecoVenda : 0;
        }

        public virtual async Task<string> GetCodTabelaPrecoMaisProxByProdutoAndPreco(BuscarItemTabelaPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TABELAS_PRODUTO_GET", "Query", command);
            var precos = await _sqlAsyncConnection.QueryAsync<ModeloCommandResult>(sql);

            decimal precoMaior = 0;
            string codTabelaPreco = "0";
            if (precos != null)
            {
                foreach (var item in precos)
                {
                    if (item.PrecoVenda >= precoMaior && Math.Round(item.PrecoVenda, 2, MidpointRounding.AwayFromZero) <= command.Valor)
                    {
                        precoMaior = item.PrecoVenda;
                        codTabelaPreco = item.CodTabelaPreco;
                    }
                }

                return codTabelaPreco;
            }
            else
                return codTabelaPreco;
        }

        public virtual async Task<List<ItemCommandResult>> BuscarProdutosDoModelo(ModeloCommandResult command, AtendimentoCommandResult atendimento, string estoque)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTOS_MODELO_GET", "Query", new
            {
                CodModelo = command.CodModelo,
                CodProdutoModelo = command.CodProdutoModelo,
                CodTabelaPreco = atendimento?.CodTabelaPreco,
                CodAtendimento = atendimento?.CodAtendimento,
                ItensEmAtendimento = atendimento == null ? -1 : atendimento.ItensEmAtendimento
            });
            var result = await _sqlAsyncConnection.QueryAsync<ItemCommandResult>(sql);
            if (result != null)
            {
                foreach (var prod in result)
                {
                    var lstGrades = await BuscarGradesDoProduto(new BuscarGradesProdutoCommand(atendimento?.CodAtendimento, prod.CodProduto, null, null, null, null, null, atendimento?.CodTabelaPreco));
                    foreach (var grade in lstGrades)
                    {
                        grade.CodProduto = prod.CodProduto;
                        prod.Grades.Add(grade);
                    }

                    prod.QtdTotal = prod.Grades.Sum(x => x.Qtd);
                }
            }

            return result;
        }

        public virtual async Task<ItemCommandResult> BuscarProduto(string CodProduto)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTO_GET", "Query", new
            {
                CodProduto = CodProduto,
            });
            var result = await _sqlAsyncConnection.QueryAsync<ItemCommandResult>(sql);

            return result.FirstOrDefault();
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarGradesDoProduto(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_GRADES_PRODUTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarGradesDoProdutoProjecao(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_GRADES_PRODUTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarGradesDoProdutoPE(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_GRADES_PRODUTO_PE_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoProduto(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TAMANHOS_POSSIVEIS_PRODUTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoProdutoProjecao(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TAMANHOS_POSSIVEIS_PRODUTO_PROJECAO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoProdutoPE(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TAMANHOS_POSSIVEIS_PRODUTO_PE_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<List<DerivacaoGradeResult>> BuscarTamanhosPossiveisDoKit(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TAMANHOS_POSSIVEIS_KIT_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result;
        }

        public virtual async Task<TabelaPrecoIndiceResult> BuscarPrecoDaTabela(BuscarPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CALCULO_PRECO_GET", "Query", command);
            var result = (await _sqlAsyncConnection.QueryAsync<TabelaPrecoIndiceResult>(sql)).FirstOrDefault();
            return result;
        }

        public virtual async Task<ItemCommandResult> BuscarQtdItemNoAtendimento(BuscarItemAtendimentoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_ITEM_PRODUTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<ItemCommandResult>(sql);
            return result.FirstOrDefault(); ;
        }

        public virtual async Task<bool> ProdutoEstaNaSegmentacao(string codProduto, string codSegmentacao)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTO_SEGMENTO_GET", "Query", new { CodProduto = codProduto, CodSegmento = codSegmentacao });
            var result = await _sqlAsyncConnection.QueryAsync<DerivacaoGradeResult>(sql);
            return result != null;
        }

        public virtual async Task<List<TecnologiaCommandResult>> BuscarTecnologiasDoProduto(BuscarTecnologiaCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTO_TECNOLOGIA_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TecnologiaCommandResult>(sql);
            return result;
        }



        public virtual async Task<List<TecnologiaCommandResult>> BuscarTecnologiasDoModelo(BuscarTecnologiaCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTO_TECNOLOGIA_MODELO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TecnologiaCommandResult>(sql);
            return result;
        }

        public virtual async Task<List<MaterialCommandResult>> BuscarMateriaisDoProduto(BuscarMaterialCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTO_MATERIAL_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<MaterialCommandResult>(sql);
            return result;
        }

        public virtual async Task<List<string>> BuscarInformacoesDoDetalhe(string codProduto)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTO_INFO_DETALHE_GET", "Query", new { CodProduto = codProduto });
            var result = await _sqlAsyncConnection.QueryAsync<ItemCommandResult>(sql);
            return result.Select(x => x.Descricao).ToList();
        }

        public virtual async Task<string> BuscarDescontoMaximo(string codProduto)
        {
            string sql = $"SELECT DescontoMaximo FROM TBT_PRODUTO WHERE CodProduto = '{codProduto}';";
            var value = await _sqlAsyncConnection.ExecuteScalarAsync<string>(sql);
            return value;

        }

        public virtual async Task<List<EstoqueResult>> BuscarEstoques(BuscarEstoquesCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_ESTOQUES_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<EstoqueResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarEstoqueDisponiveisDoProduto(BuscarEstoquesCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_ESTOQUES_PRODUTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<EstoqueResult>> BuscarEstoquesGradeFechada(BuscarEstoquesCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_ESTOQUES_GRADE_FECHADA_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<EstoqueResult>(sql);
            return result;
        }

        public virtual async Task<List<EstoqueResult>> BuscarEstoqueKit(BuscarEstoquesCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_ESTOQUES_KIT_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<EstoqueResult>(sql);
            return result;
        }


        public virtual async Task<string> GetString(string codProduto, string columnName)
        {
            string sql = $"SELECT {columnName} FROM TBT_PRODUTO WHERE CodProduto = '{codProduto}';";
            var value = await _sqlAsyncConnection.ExecuteScalarAsync<string>(sql);
            return value;
        }

        public virtual async Task<string> GetCodBarraDerivacao(string codProduto, string codDerivacao)
        {
            string sql = $"SELECT CodBarra FROM TBT_ITEM_PRONTA_ENTREGA WHERE CodProduto = '{codProduto}' AND CodGrade = '{codDerivacao}';";
            var value = await _sqlAsyncConnection.ExecuteScalarAsync<string>(sql);
            return value;
        }

        public virtual async Task<List<GenericComboResult>> BuscarGradesDisponiveisDoProduto(BuscarGradesProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_GRADES_DISPONIVEIS_PRODUTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarCDProduto(BuscarCDProdutoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CD_PRODUTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<string>> BuscarProdutosRelacionados(string codProduto)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRODUTOS_RELACIONADOS_GET", "Query", new { CodProduto = codProduto });
            var result = await _sqlAsyncConnection.QueryAsync<ProdutoRelacionadoCommandResult>(sql);
            return result.Select(x => x.CodProdutoRelacionado).ToList();
        }

        public void Dispose()
        {
        }

    }
}
