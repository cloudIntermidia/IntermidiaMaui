using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using SQLite;
using System.Text;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        protected readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public ClienteRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }

        public virtual async Task<ClienteCommandResult> BuscarCliente(BuscarClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CLIENTES_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<ClienteCommandResult>(sql);
            return result.FirstOrDefault();
        }

        public virtual async Task<ClienteCommandResult> BuscarClientePorCode(BuscarClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CLIENTE_CODE_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<ClienteCommandResult>(sql);
            return result.FirstOrDefault();
        }

        public virtual async Task<ClienteCommandResult> BuscarClienteIntegrado(string cnpj)
        {
            string cnpjSemFormatacao = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            string sql = $"SELECT CodPessoaCliente FROM TBT_CLIENTE WHERE (CNPJ = '{cnpj}' OR CNPJ = '{cnpjSemFormatacao}') AND (CodPessoaCliente NOT LIKE '%.%' OR EXISTS ( SELECT * FROM TBT_PESSOA P WHERE P.CodPessoa = TBT_CLIENTE.CodPessoaCliente AND P.CodPessoaERP IS NOT NULL)) ";
            var result = await _sqlAsyncConnection.QueryAsync<ClienteCommandResult>(sql);
            return result.FirstOrDefault();
        }

        private String RetornaFiltro_Pesquisa(String filtro)
        {
            String retorno = String.Empty;

            if (!String.IsNullOrEmpty(filtro))
            {
                var arrayFiltro = filtro.Split(' ').ToList();

                String whereTemp = String.Empty;

                Int16 numCont = 0;

                foreach (var item in arrayFiltro)
                {
                    numCont++;

                    whereTemp += " ( ";
                    whereTemp += $"    '-1' = '{item}' OR ";
                    whereTemp += $"    ( ";
                    whereTemp += $"        c.RazaoSocial like '%{item}%'  OR ";
                    whereTemp += $"        c.NomeFantasia like '%{item}%' OR ";
                    whereTemp += $"        c.CodGrupoCliente like '%{item}%' OR ";
                    //whereTemp += $"        c.Cnpj like '%{item}%' OR ";
                    whereTemp += $"        REPLACE(REPLACE(REPLACE(c.CNPJ, '-', ''), '/', ''), '.', '') like '%{item}%' OR ";
                    whereTemp += $"        c.CodPessoaCliente like '%{item}%' OR ";
                    whereTemp += $"        p.CodPessoaERP like '%{item}%' OR ";
                    whereTemp += $"        E.UF like '%{item}%' OR ";
                    whereTemp += $"        E.Endereco like '%{item}%' OR ";
                    whereTemp += $"        E.Complemento like '%{item}%' OR ";
                    whereTemp += $"        E.Cidade like '%{item}%' ";
                    whereTemp += "    ) ";
                    whereTemp += " ) ";

                    if (numCont < arrayFiltro.Count)
                    {
                        whereTemp += " AND ";
                    }
                }

                retorno += " AND ";
                retorno += " ( ";
                retorno += whereTemp;
                retorno += " ) ";
            }

            return retorno;
        }

        public virtual async Task<List<ClienteCommandResult>> BuscarClientes(BuscarClienteCommand command)
        {
            string sql;
            if (command.FiltroPesquisa == null)
                command.FiltroPesquisa = "";

            command.WhereFiltroLike = RetornaFiltro_Pesquisa(command.FiltroPesquisa);
            command.WherePermissao = new FuncaoGenerica().Retorna_Where_Permissao(FuncaoGenerica.TipoRegra.Cliente, "c.CodPessoaCliente");

            sql = ManagerQuery.MakeSql("PRO_CLIENTES_GET", "Query", command);

            var result = await _sqlAsyncConnection.QueryAsync<ClienteCommandResult>(sql);
            return result;
        }


        public virtual async Task<List<ClienteCommandResult>> BuscarTodosClientes(BuscarClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CLIENTES_ALL_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<ClienteCommandResult>(sql).ConfigureAwait(false);
            return result;
        }

        public virtual async Task<List<GestaoClienteCommandResult>> BuscarGestaoClientes(BuscarGestaoClienteCommand command)
        {
            //Atualiza a data de Filtro.
            command = AtualizaFiltroData(command);

            string sql = ManagerQuery.MakeSql("PRO_GESTAO_CLIENTES_GET", "Query", command);

            var result = await _sqlAsyncConnection.QueryAsync<GestaoClienteCommandResult>(sql);
            return result;
        }

        public virtual async Task<List<GestaoClienteCommandResult>> BuscarGestaoClientesAll(BuscarGestaoClienteCommand command)
        {
            //Atualiza a data de Filtro.
            command = AtualizaFiltroData(command);

            string sql = ManagerQuery.MakeSql("PRO_GESTAO_CLIENTES_ALL_GET", "Query", command);

            var result = await _sqlAsyncConnection.QueryAsync<GestaoClienteCommandResult>(sql);
            return result;
        }

        public BuscarGestaoClienteCommand AtualizaFiltroData(BuscarGestaoClienteCommand command)
        {
            //Padrão é data emissão
            command.FMDataEmissao = "FM.DataEmissao";

            if (command.TipoData == "Emissão")
            {
                command.FMAtualInicial = (DateTime)command.DataInicial;
                command.FMAtualFinal = (DateTime)command.DataFinal;

                command.FMAnteriorInicial = ((DateTime)command.DataInicial).AddYears(-1);
                command.FMAnteriorFinal = ((DateTime)command.DataFinal).AddYears(-1);

                command.FMDataEmissao = "FM.DataEmissao";

            }
            else if (command.TipoData == "Entrega")
            {
                command.FMAtualInicial = (DateTime)command.DataInicial;
                command.FMAtualFinal = (DateTime)command.DataFinal;

                command.FMAnteriorInicial = ((DateTime)command.DataInicial).AddYears(-1);
                command.FMAnteriorFinal = ((DateTime)command.DataFinal).AddYears(-1);

                command.FMDataEmissao = "FM.DataEntrega";
            }
            return command;
        }

        public virtual async Task<List<GenericComboResult>> BuscarVendedores(BuscarGestaoClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("COMBO_VENDEDORES_GESTAO", "Query.Filtros", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarAllVendedores(BuscarGestaoClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("COMBO_ALL_VENDEDORES_GESTAO", "Query.Filtros", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarGrupoDeClientes(BuscarGestaoClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("COMBO_GRUPO_CLIENTES_GESTAO", "Query.Filtros", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarClientes(UsuarioCommandResult command)
        {
            string sql = ManagerQuery.MakeSql("CLIENTES_COMBO_PEDIDOS", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }
        public virtual async Task<List<GenericComboResult>> BuscarClientes(BuscarGestaoClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("COMBO_CLIENTES_GESTAO", "Query.Filtros", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<string> BuscarRepresentantePorCliente(ClienteCommandResult command)
        {
            string sql = ManagerQuery.MakeSql("PRO_REPRESENTANTE_GET", "Query", command);
            var result = await _sqlAsyncConnection.ExecuteScalarAsync<string>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarTipoFeedback(BuscarTipoFeedback command)
        {
            string sql = ManagerQuery.MakeSql("COMBO_TIPO_FEEDBACK", "Query.Filtros", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarUFs(BuscarGestaoClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("COMBO_UF_GESTAO", "Query.Filtros", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<EnderecoCommandResult> BuscarEnderecoPrincipal(string codPessoaCliente)
        {
            var endereco = await _sqlAsyncConnection.FindAsync<TBT_ENDERECO_CLIENTE>(x => x.CodPessoaCliente == codPessoaCliente && x.IndPrincipal == 1);
            if (endereco == null)
            {
                return null;
            }

            return new EnderecoCommandResult()
            {
                Endereco = endereco.Endereco,
                Bairro = endereco.Bairro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                Cidade = endereco.Cidade,
                UF = endereco.UF,
                CEP = endereco.CEP,
                Telefone = endereco.Telefone,
                Fax = endereco.Telefone
            };

        }
        public virtual async Task<EnderecoCommandResult> BuscarEnderecoCobranca(string codPessoaCliente)
        {
            var endereco = await _sqlAsyncConnection.FindAsync<TBT_ENDERECO_CLIENTE>(x => x.CodPessoaCliente == codPessoaCliente && x.IndCobranca == 1);
            if (endereco == null)
            {
                return null;
            }

            return new EnderecoCommandResult()
            {
                Endereco = endereco.Endereco,
                Bairro = endereco.Bairro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                Cidade = endereco.Cidade,
                UF = endereco.UF,
                CEP = endereco.CEP,
                Telefone = endereco.Telefone,
                Fax = endereco.Fax
            };

        }
        public virtual async Task<bool> CriarCliente(CriarClienteCommand command)
        {
            int rows = 0;
            try
            {
                _sqlAsyncConnection.GetConnection().BeginTransaction();
                var camposPessoa = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_PESSOA);")).Select(x => x.name).ToList();
                var camposCliente = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_CLIENTE);")).Select(x => x.name).ToList();
                var camposMarcaCliente = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_MARCA_CLIENTE);")).Select(x => x.name).ToList();
                var camposEnderecoCliente = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_ENDERECO_CLIENTE);")).Select(x => x.name).ToList();

                #region Codigo da Cidade

                if (!String.IsNullOrEmpty(command.Cliente.EnderecoPrincipal.Cidade))
                {
                    String sqlConsulta = $"SELECT DISTINCT CodMunicipio " +
                                            $"FROM TBT_MUNICIPIO " +
                                            $"WHERE NomeMunicipio = '{command.Cliente.EnderecoPrincipal.Cidade}' AND Uf = '{command.Cliente.EnderecoPrincipal.UF}'  ;";


                    var codMunicipio = await _sqlAsyncConnection.ExecuteScalarAsync<string>(sqlConsulta);
                    if (!string.IsNullOrEmpty(codMunicipio))
                        command.Cliente.EnderecoPrincipal.Cidade = codMunicipio;
                }

                #endregion Codigo da Cidade

                #region Trata CPF + CNPJ

                if (String.IsNullOrEmpty(command.Cliente.CNPJ) && !String.IsNullOrEmpty(command.Cliente.CPF))
                {
                    command.Cliente.CNPJ = command.Cliente.CPF;
                }

                #endregion Trata CPF + CNPJ

                command.Cliente.CodSituacaoCliente = "50";
                var commandPessoa = new CriarPessoaCommand(command.Cliente.CodPessoaCliente, command.Cliente.RazaoSocial, "4", null, command.Cliente.Email, command.Cliente.CodPessoaCliente, null, 0, 1, command.Usuario.CodUsuario, command.Usuario.CodInstalacao);
                var commandCliente = command.Cliente;
                var commandMarcaCliente = new RelacionarClienteCommand(command.Cliente.CodPessoaCliente, command.Usuario.CodMarca, command.Usuario.CodPessoa, command.Usuario.CodPessoa);
                var commandEnderecoPrincipalCliente = command.Cliente.EnderecoPrincipal;
                //var commandEnderecoCobrancaCliente = command.Cliente.EnderecoCobranca;

                commandEnderecoPrincipalCliente.SeqEndereco = 1;
                //commandEnderecoCobrancaCliente.SeqEndereco = 2;


                string sqlInsertPessoa = string.Empty;
                string sqlInsertCliente = string.Empty;
                string sqlInsertMarcaCliente = string.Empty;
                string sqlInsertEnderecoPrincipalCliente = string.Empty;
                //string sqlInsertEnderecoCobrancaCliente = string.Empty;

                string sqlNovoCodPessoa = $"SELECT '{command.Usuario.CodUsuario}.' || '{command.Usuario.CodInstalacao}.' || (IFNULL(MAX(substr(CodPessoa, length('{command.Usuario.CodUsuario}') + length('{command.Usuario.CodInstalacao}') + 3)) + 1, 1)) as CodPessoaCliente FROM TBT_PESSOA  WHERE CodPessoa like ('{command.Usuario.CodUsuario}' || '.' || '{command.Usuario.CodInstalacao}' || '.' || '%');";
                ClienteCommandResult result = (_sqlAsyncConnection.GetConnection().Query<ClienteCommandResult>(sqlNovoCodPessoa)).FirstOrDefault();
                //commandMarcaCliente.CodPessoaCliente = command.Cliente.EnderecoCobranca.CodPessoaCliente = command.Cliente.EnderecoPrincipal.CodPessoaCliente = commandPessoa.CodPessoa = commandCliente.CodPessoaCliente = result.CodPessoaCliente;
                commandMarcaCliente.CodPessoaCliente = command.Cliente.EnderecoPrincipal.CodPessoaCliente = commandPessoa.CodPessoa = commandCliente.CodPessoaCliente = result.CodPessoaCliente;

                sqlInsertPessoa = ManagerQuery.MakeInsertOrReplace(camposPessoa, "TBT_PESSOA", commandPessoa);
                sqlInsertCliente = ManagerQuery.MakeInsertOrReplace(camposCliente, "TBT_CLIENTE", commandCliente);
                sqlInsertMarcaCliente = ManagerQuery.MakeInsertOrReplace(camposMarcaCliente, "TBT_MARCA_CLIENTE", commandMarcaCliente);
                sqlInsertEnderecoPrincipalCliente = ManagerQuery.MakeInsertOrReplace(camposEnderecoCliente, "TBT_ENDERECO_CLIENTE", commandEnderecoPrincipalCliente);
                //sqlInsertEnderecoCobrancaCliente = ManagerQuery.MakeInsertOrReplace(camposEnderecoCliente, "TBT_ENDERECO_CLIENTE", commandEnderecoCobrancaCliente);

                //Trata SQL pra CPF
                if (!String.IsNullOrEmpty(command.Cliente.CNPJ) && !String.IsNullOrEmpty(command.Cliente.CPF))
                {
                    sqlInsertCliente = sqlInsertCliente.Replace(command.Cliente.CNPJ, command.Cliente.CPF);
                    command.Cliente.CNPJ = String.Empty;
                }

                rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertPessoa);
                rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertCliente);
                rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertMarcaCliente);
                rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertEnderecoPrincipalCliente);
                //rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertEnderecoCobrancaCliente);

                #region Empresa | Tabela de Preço

                String sqlGenerico = String.Empty;
                String nomeTabelaGenerico = "TBT_CLIENTE_EMPRESA_TABELA_PRECO_ENVIAR_ERP";

                var camposTBT_CLIENTE_ENVIAR_ERP = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>($"PRAGMA table_info({nomeTabelaGenerico});")).Select(x => x.name).ToList();

                if (command.Cliente.Lista_Empresa_TabelaPreco != null && command.Cliente.Lista_Empresa_TabelaPreco.Count > 0)
                {
                    foreach (_ED_Empresa_TabelaPreco oED in command.Cliente.Lista_Empresa_TabelaPreco)
                    {
                        CriarClienteEnviarERPCommand oCriarClienteEnviarERPCommand = new CriarClienteEnviarERPCommand();
                        oCriarClienteEnviarERPCommand.CodEmpresa = oED.CodEmpresa;
                        oCriarClienteEnviarERPCommand.CodTabelaPreco = oED.CodTabelaPreco;
                        oCriarClienteEnviarERPCommand.CodCliente = command.Cliente.CodPessoaCliente;

                        sqlGenerico = ManagerQuery.MakeInsertOrReplace(camposTBT_CLIENTE_ENVIAR_ERP, nomeTabelaGenerico, oCriarClienteEnviarERPCommand);
                        rows = _sqlAsyncConnection.GetConnection().Execute(sqlGenerico);

                        #region Tabela TBT_CLIENTE_TABELA_PRECO

                        String nomeTabelaGenerico2 = "TBT_CLIENTE_TABELA_PRECO";
                        var camposTBT_CLIENTE_TABELA_PRECO = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>($"PRAGMA table_info({nomeTabelaGenerico2});")).Select(x => x.name).ToList();
                        CriarClienteTabelaPrecoCommand oCriarClienteTabelaPrecoCommand = new CriarClienteTabelaPrecoCommand();
                        oCriarClienteTabelaPrecoCommand.CodEmpresa = oED.CodEmpresa;
                        oCriarClienteTabelaPrecoCommand.CodTabelaPreco = oED.CodTabelaPreco;
                        oCriarClienteTabelaPrecoCommand.CodCliente = command.Cliente.CodPessoaCliente;
                        oCriarClienteTabelaPrecoCommand.ID = 1;
                        oCriarClienteTabelaPrecoCommand.CtrlDataOperacao = DateTime.Now;
                        sqlGenerico = ManagerQuery.MakeInsertOrReplace(camposTBT_CLIENTE_TABELA_PRECO, nomeTabelaGenerico2, oCriarClienteTabelaPrecoCommand);
                        rows = _sqlAsyncConnection.GetConnection().Execute(sqlGenerico);

                        #endregion Tabela TBT_CLIENTE_TABELA_PRECO

                    }
                }

                #endregion Empresa | Tabela de Preço

                _sqlAsyncConnection.GetConnection().Commit();
            }
            catch (Exception ex)
            {
                _sqlAsyncConnection.GetConnection().Rollback();
                throw ex;
            }

            return rows > 0;
        }


        public virtual async Task<bool> AtualizarCliente(CriarClienteCommand command, IParametroSincronizacaoRepository parametroSincronizacaoRepository)
        {
            int rows = 0;
            try
            {
                _sqlAsyncConnection.GetConnection().BeginTransaction();
                var camposPessoa = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_PESSOA);")).Select(x => x.name).ToList();
                var camposCliente = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_CLIENTE);")).Select(x => x.name).ToList();
                var camposEnderecoCliente = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_ENDERECO_CLIENTE);")).Select(x => x.name).ToList();
                var camposMarcaCliente = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>("PRAGMA table_info(TBT_MARCA_CLIENTE);")).Select(x => x.name).ToList();
                command.CodSituacaoCliente = "50";
                var commandPessoa = new CriarPessoaCommand(command.Cliente.CodPessoaCliente, command.Cliente.RazaoSocial, "4", null, command.Cliente.Email, command.Cliente.CodPessoaCliente, null, 0, 1, command.Usuario.CodUsuario, command.Usuario.CodInstalacao);
                var commandCliente = command.Cliente;
                var commandMarcaCliente = new RelacionarClienteCommand(command.Cliente.CodPessoaCliente, command.Usuario.CodMarca, command.Usuario.CodPessoa, command.Usuario.CodPessoa);
                var commandEnderecoPrincipalCliente = command.Cliente.EnderecoPrincipal;
                commandEnderecoPrincipalCliente.IndPrincipal = 1;
                commandEnderecoPrincipalCliente.IndCobranca = 0;
                //var commandEnderecoCobrancaCliente = command.Cliente.EnderecoCobranca;
                //commandEnderecoCobrancaCliente.IndPrincipal = 0;
                //commandEnderecoCobrancaCliente.IndCobranca = 1;

                //commandEnderecoCobrancaCliente.CodPessoaCliente = commandEnderecoPrincipalCliente.CodPessoaCliente = commandCliente.CodPessoaCliente;
                //commandEnderecoCobrancaCliente.SeqEndereco = 1;

                string sqlInsertPessoa = string.Empty;
                string sqlInsertCliente = string.Empty;
                string sqlInsertMarcaCliente = string.Empty;
                string sqlInsertEnderecoPrincipalCliente = string.Empty;
                //string sqlInsertEnderecoCobrancaCliente = string.Empty;

                //  string sqlNovoCodPessoa = $"SELECT '{command.Usuario.CodUsuario}.' || '{command.Usuario.CodInstalacao}.' || IFNULL(MAX(substr(CodPessoa, length('{command.Usuario.CodUsuario}') + length('{command.Usuario.CodInstalacao}') + 3)) + 1, 0) + 1 as CodPessoaCliente FROM TBT_PESSOA  WHERE CodPessoa like ('{command.Usuario.CodUsuario}' || '.' || '{command.Usuario.CodInstalacao}' || '.' || '%');";
                //   ClienteCommandResult result = (_sqlAsyncConnection.GetConnection().Query<ClienteCommandResult>(sqlNovoCodPessoa)).FirstOrDefault();
                // commandMarcaCliente.CodPessoaCliente = command.Cliente.EnderecoCobranca.CodPessoaCliente = command.Cliente.EnderecoPrincipal.CodPessoaCliente = commandPessoa.CodPessoa = commandCliente.CodPessoaCliente = result.CodPessoaCliente;

                sqlInsertPessoa = ManagerQuery.MakeUpdate(camposPessoa, "TBT_PESSOA", new List<string>() { "CodPessoa" }, commandPessoa);
                sqlInsertCliente = ManagerQuery.MakeUpdate(camposCliente, "TBT_CLIENTE", new List<string>() { "CodPessoaCliente" }, commandCliente);
                //sqlInsertMarcaCliente = ManagerQuery.MakeUpdate(camposMarcaCliente, "TBT_MARCA_CLIENTE", new List<string>() { "CodPessoa" }, commandMarcaCliente);
                sqlInsertEnderecoPrincipalCliente = ManagerQuery.MakeUpdate(camposEnderecoCliente, "TBT_ENDERECO_CLIENTE", new List<string>() { "CodPessoaCliente", "IndPrincipal" }, commandEnderecoPrincipalCliente);

                //var temEnderecoCobranca = (await _sqlAsyncConnection.ExecuteScalarAsync<int>
                //    ($"SELECT COUNT(*) FROM TBT_ENDERECO_CLIENTE WHERE CodPessoaCliente = '{commandEnderecoCobrancaCliente.CodPessoaCliente}' AND IndCobranca = 1;")) >= 1;

                //if (temEnderecoCobranca)
                //    sqlInsertEnderecoCobrancaCliente = ManagerQuery.MakeUpdate(camposEnderecoCliente, "TBT_ENDERECO_CLIENTE", new List<string>() { "CodPessoaCliente", "IndPrincipal", "IndCobranca" }, commandEnderecoCobrancaCliente);
                //else
                //    sqlInsertEnderecoCobrancaCliente = ManagerQuery.MakeInsertOrReplace(camposEnderecoCliente, "TBT_ENDERECO_CLIENTE", commandEnderecoCobrancaCliente);

                rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertPessoa);
                rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertCliente);
                // rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertMarcaCliente);
                rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertEnderecoPrincipalCliente);
                //rows = _sqlAsyncConnection.GetConnection().Execute(sqlInsertEnderecoCobrancaCliente);


                _sqlAsyncConnection.GetConnection().Commit();
            }
            catch (Exception ex)
            {
                _sqlAsyncConnection.GetConnection().Rollback();
                throw ex;
            }

            return rows > 0;
        }


        public virtual async Task<string> CriarPessoa(CriarPessoaCommand command)
        {
            string script = ManagerQuery.MakeSql("PESSOA_INSERT", "CRUD", command);
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync(script);
            //return rowsAffecteds > 0;
            var retorno = await _sqlAsyncConnection.ExecuteScalarAsync<String>("SELECT CodPessoa FROM TBT_PESSOA ORDER BY ID DESC LIMIT 1;");
            return retorno;
        }

        public virtual async Task<bool> CriarFeedbackCliente(CriarFeedbackClienteCommand command)
        {
            string script = ManagerQuery.MakeSql("FEEDBACK_CLIENTE_INSERT", "CRUD", command);
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync(script);
            return true;
        }
        public virtual async Task<bool> RelacionarClienteComRepresentante(RelacionarClienteCommand command)
        {
            string script = ManagerQuery.MakeSql("MARCA_CLIENTE_INSERT", "Query", command);
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync(script);
            return rowsAffecteds > 0;
        }
        public virtual async Task<bool> AdicionarEndereco(EnderecoCommandResult command)
        {
            string script = ManagerQuery.MakeSql("ENDERECO_CLIENTE_INSERT", "Query", command);
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync(script);
            return rowsAffecteds > 0;
        }
        public virtual async Task<WcfClienteModelInput> BuscarClienteParaTransmissao(string codPessoaCliente)
        {
            WcfClienteModelInput model = new WcfClienteModelInput();
            model.Cliente = await _sqlAsyncConnection.FindAsync<TBT_CLIENTE>(x => x.CodPessoaCliente == codPessoaCliente);
            model.Pessoa = await _sqlAsyncConnection.FindAsync<TBT_PESSOA>(x => x.CodPessoa == codPessoaCliente);
            model.MarcaCliente = await _sqlAsyncConnection.Table<TBT_MARCA_CLIENTE>().Where(x => x.CodPessoaCliente == codPessoaCliente).ToListAsync();

            #region Endereços

            StringBuilder sbSelect = new StringBuilder();
            sbSelect.Append("SELECT ");
            sbSelect.Append("   ENDC.SeqEndereco, ENDC.UF, ENDC.Cidade, ENDC.Endereco, ENDC.Numero, ");
            sbSelect.Append("   ENDC.Complemento, ENDC.Bairro, ENDC.Telefone, ENDC.Fax, ENDC.CEP, ENDC.IndCobranca, ENDC.IndPrincipal, ");
            sbSelect.Append("   MUNC.CodMunicipio ");
            sbSelect.Append(" FROM TBT_ENDERECO_CLIENTE ENDC ");
            sbSelect.Append(" INNER JOIN TBT_MUNICIPIO MUNC ON ENDC.UF = MUNC.UF AND ENDC.Cidade = MUNC.CodMunicipio ");
            sbSelect.Append($" WHERE ENDC.CodPessoaCliente = '{codPessoaCliente}' ");

            model.Enderecos = await _sqlAsyncConnection.QueryAsync<TBT_ENDERECO_CLIENTE>(sbSelect.ToString());

            #endregion Endereços

            #region Empresa | Tabela de Preço

            sbSelect = new StringBuilder();
            sbSelect.Append("SELECT ");
            sbSelect.Append("   TAB.CodCliente, TAB.CodEmpresa, TAB.CodTabelaPreco ");
            sbSelect.Append(" FROM TBT_CLIENTE_EMPRESA_TABELA_PRECO_ENVIAR_ERP TAB ");
            sbSelect.Append($" WHERE TAB.CodCliente = '{codPessoaCliente}' ");

            model.Lista_Empresa_TabelaPreco = await _sqlAsyncConnection.QueryAsync<TBT_CLIENTE_EMPRESA_TABELA_PRECO_ENVIAR_ERP>(sbSelect.ToString());

            #endregion Empresa | Tabela de Preço

            return model;
        }
        public virtual async Task<bool> CnpjJaExiste(string cnpj)
        {
            var cliente = await _sqlAsyncConnection.FindAsync<TBT_CLIENTE>(x => x.CNPJ == cnpj);
            return cliente != null;
        }
        public virtual async Task<bool> AtualizarClienteIntegrado(string codPessoaCliente, string codPessoaErp)
        {
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_PESSOA SET CodPessoaERP = ? WHERE CodPessoa = ?", codPessoaErp, codPessoaCliente);
            return rowsAffecteds > 0;
        }

        public virtual async Task<bool> AtualizarEnviarERP(string codPessoaCliente, string codPessoaErp)
        {
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_CLIENTE_EMPRESA_TABELA_PRECO_ENVIAR_ERP SET CodCliente = ? WHERE CodCliente = ?", codPessoaErp, codPessoaCliente);
            return rowsAffecteds > 0;
        }

        public virtual async Task<bool> AtualizarClienteTabelaPreco(string codPessoaCliente, string codPessoaErp)
        {
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_CLIENTE_TABELA_PRECO SET CodCliente = ? WHERE CodCliente = ?", codPessoaErp, codPessoaCliente);
            return rowsAffecteds > 0;
        }

        public virtual async Task<bool> AtualizarClienteNoCarrinho(string codCarrinho, string codPessoaErp)
        {
            int rowsAffecteds = await _sqlAsyncConnection.ExecuteAsync("UPDATE TBT_CARRINHO SET CodPessoaCliente = ? WHERE CodCarrinho = ?", codPessoaErp, codCarrinho);
            return rowsAffecteds > 0;
        }

        public virtual async Task<List<GenericComboResult>> BuscarUFCadastro()
        {
            string sql = ManagerQuery.MakeSql("PRO_UF_COMBO_CADASTRO", "Query", null);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }
        public virtual async Task<List<GenericComboResult>> BuscarMunicipios(string uf)
        {
            string sql = ManagerQuery.MakeSql("PRO_MUNICIPIO_COMBO_CADASTRO", "Query", new { UF = uf });
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarSegmentacaoCadastro()
        {
            string sql = ManagerQuery.MakeSql("PRO_SEGMENTACAO_CADASTRO_CLIENTE_GET", "Query", null);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarTipoFeedback()
        {
            string sql = ManagerQuery.MakeSql("PRO_TIPO_FEEDBACK_GET", "Query", null);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<bool> VerificaClienteFeira(BuscarClienteCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_VALIDA_CLIENTE_FEIRA_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<ClienteCommandResult>(sql).ConfigureAwait(false);
            bool clienteFeira = false;
            if (result == null || result.Count == 0)
            {
                clienteFeira = true;
            }
            return clienteFeira;
        }

        public virtual async Task<decimal> BuscarMinimoFretePorCliente(string codPessoaCliente)
        {
            decimal valorMinimoFrete = await _sqlAsyncConnection.ExecuteScalarAsync<decimal>(
                "SELECT " +
                "   CASE WHEN ValorMinimoFrete IS NULL THEN 0 ELSE ValorMinimoFrete END AS ValorMinimoFrete " +
                " FROM TBT_CLIENTE" +
                " WHERE CodPessoaCliente = ? ", codPessoaCliente);
            return valorMinimoFrete;
        }

        public virtual async Task<string> BuscarTransportadoraPorCliente(string codPessoaCliente)
        {
            string valorMinimoFrete = await _sqlAsyncConnection.ExecuteScalarAsync<string>(
                "SELECT " +
                "   CodTransportadora " +
                " FROM TBT_MARCA_CLIENTE" +
                " WHERE CodPessoaCliente = ? ", codPessoaCliente);
            return valorMinimoFrete;
        }

        public virtual async Task<decimal> BuscarMinimoRegiao(string codPessoaCliente)
        {
            decimal valorMinimoRegiao = await _sqlAsyncConnection.ExecuteScalarAsync<decimal>(
                " SELECT ValorMinimo " +
                " FROM TBT_VALOR_MINIMO_REGIAO R " +
                " JOIN TBT_ENDERECO_CLIENTE EC ON EC.UF = R.UF " +
                " AND EC.IndPrincipal = 1 " +
                " WHERE EC.CodPessoaCliente =  ? ", codPessoaCliente);

            return valorMinimoRegiao;
        }

        public virtual async Task<List<ED_CollectionView_Generico>> ListarEmpresa_TabelaPreco_Disponivel()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("SELECT TAB.Tipo, TAB.Codigo, TAB.Descricao FROM ");
            sbSql.Append("( ");
            sbSql.Append("  SELECT DISTINCT 'E' AS Tipo, CodAtributo AS Codigo, Descricao FROM TBT_NIVEL_ATRIBUTO WHERE CodNivel = 1 ");
            sbSql.Append("  UNION ");
            sbSql.Append("  SELECT DISTINCT 'T' AS Tipo, CodTabelaPreco AS Codigo, 'Tabela de Preço ' || CodTabelaPreco AS Descricao FROM TBT_CLIENTE_TABELA_PRECO ");
            sbSql.Append(") TAB ");
            sbSql.Append("ORDER BY TAB.Tipo, TAB.Descricao; ");

            var result = await _sqlAsyncConnection.QueryAsync<ED_CollectionView_Generico>(sbSql.ToString());
            return result;
        }
    }

}
