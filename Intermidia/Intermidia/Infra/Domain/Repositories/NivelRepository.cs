using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class NivelRepository : INivelRepository
    {
        private readonly SQLiteAsyncConnection _sqlConnection;
        public NivelRepository(ISqliteConnection context)
        {
            _sqlConnection = context.DbConnectionAsync();
        }

        public virtual async Task<List<NivelResult>> GetConfiguracaoNivelSortido()
        {
            string sql = ManagerQuery.MakeSql("PRO_CONFIGURACAO_NIVEL_SORTIDO_GET", "Query", null);
            var result = await _sqlConnection.QueryAsync<NivelResult>(sql);
            return result;

        }

        public virtual async Task<List<NivelResult>> GetConfiguracaoNivel()
        {
            string sql = ManagerQuery.MakeSql("PRO_CONFIGURACAO_NIVEL_GET", "Query", null);
            var result = await _sqlConnection.QueryAsync<NivelResult>(sql);
            return result;

        }

        public virtual async Task<List<NivelResult>> GetConfiguracaoNivelGeracaoCatalogo()
        {
            string sql = ManagerQuery.MakeSql("PRO_CONFIGURACAO_NIVEL_GERAR_CATALOGO_GET", "Query", null);
            var result = await _sqlConnection.QueryAsync<NivelResult>(sql);
            return result;

        }

        public virtual async Task<List<NivelAtributoResult>> GetNivel(BuscarNivelCommand command, List<NivelAtributoResult> filtros)
        {
            string sqlTodosProdutos = "SELECT pla.CodProduto from TBT_PRODUTO p " +
                                      "JOIN TBT_NIVEL_PRODUTO pla on p.CodProduto = pla.CodProduto AND pla.IndAtivo = 1 ";
            string sql = "SELECT DISTINCT PLA.CodNivel AS CodNivel, PLA.CodNivel AS Codigo, PNA.CodAtributo, PNA.Descricao AS Descricao, N.Ordem As Ordem FROM TBT_PRODUTO P " +
                         "INNER JOIN TBT_NIVEL_PRODUTO PLA on P.CodProduto = PLA.CodProduto AND PLA.IndAtivo = 1 " +
                         "INNER JOIN TBT_NIVEL N on PLA.CodNivel = N.CodNivel " +
                         "INNER JOIN TBT_NIVEL_ATRIBUTO PNA on PNA.CodAtributo = PLA.CodAtributo AND N.CodNivel = PNA.CodNivel " +
                         $"WHERE PLA.CodNivel = '{command.CodNivel}' " +
                         $"AND P.CodMarca = '{command.CodMarca}' " +
                         //"AND p.VendaBloqueada = 0 ";
                         " AND EXISTS ( " +
                            " SELECT * FROM TBT_ITEM_PRONTA_ENTREGA " +
                            " WHERE TBT_ITEM_PRONTA_ENTREGA.codProduto = P.CodProduto ) "
                            ;

            if (filtros?.Count > 0 && filtros.Any(x => x.Codigo != command.CodNivel))
            {
                string niveisWhere = string.Empty;
                string subSelect = string.Empty;
                string UltimoNivel = string.Empty;
                string niveisWhereTemp = string.Empty;
                foreach (var filtro in filtros.Where(x => x.Codigo != command.CodNivel))
                {
                    int indiceColecao = filtros.IndexOf(filtro);
                    if (indiceColecao != 0 && indiceColecao < filtros.Count && !string.IsNullOrEmpty(UltimoNivel))
                    {
                        if (UltimoNivel == filtro.Codigo)
                        {
                            niveisWhereTemp += " UNION ";
                        }
                        else
                        {
                            subSelect = $"SELECT B.CodProduto FROM ( {niveisWhereTemp} ) B INTERSECT ";
                            niveisWhereTemp = null;
                        }
                    }

                    niveisWhereTemp += $"{sqlTodosProdutos} WHERE CodNivel = '{filtro.Codigo}' AND CodAtributo = '{filtro.CodAtributo}' ";
                    if (!string.IsNullOrEmpty(subSelect))
                    {
                        niveisWhere += $"{subSelect} ";
                        subSelect = null;
                    }
                    UltimoNivel = filtro.Codigo;
                }


                if (!string.IsNullOrEmpty(niveisWhereTemp))
                {
                    niveisWhere += $"SELECT B.CodProduto FROM ( {niveisWhereTemp} ) B ";
                }

                string where = $"AND PLA.CodProduto IN ( SELECT A.Codproduto FROM ( {niveisWhere} ) A ) ";
                sql += where;
            }

            sql += "ORDER BY PNA.Descricao;";
            var result = await _sqlConnection.QueryAsync<NivelAtributoResult>(sql);
            return result;
        }

        public virtual async Task<List<NivelResult>> GetNiveisQuebra()
        {
            string sqlNiveisQuebra = "SELECT * FROM TBT_NIVEL WHERE QuebraPedido = '1';";
            List<NivelResult> niveisDeQuebra = await _sqlConnection.QueryAsync<NivelResult>(sqlNiveisQuebra);
            return niveisDeQuebra;
        }

        public virtual async Task<List<NivelProdutoResult>> GetNiveisProduto(string codProduto)
        {
            string sqlNiveisProdutoQuebra = $"SELECT DISTINCT N.CodNivel, N.Descricao AS Nivel, NP.CodProduto, NA.CodAtributo, NA.Descricao AS Atributo FROM TBT_NIVEL_PRODUTO NP " +
                                            $"INNER JOIN TBT_NIVEL_ATRIBUTO NA ON NP.CodAtributo = NA.CodAtributo AND NP.CodNivel = NA.CodNivel " +
                                            $"INNER JOIN TBT_NIVEL N ON N.CodNivel = NA.CodNivel " +
                                            $"WHERE CodProduto = '{codProduto}' " +
                                            $"AND NP.IndAtivo = 1;";
            List<NivelProdutoResult> niveisDoProduto = await _sqlConnection.QueryAsync<NivelProdutoResult>(sqlNiveisProdutoQuebra);
            return niveisDoProduto;
        }

        public virtual async Task<List<NivelProdutoResult>> GetNiveisProduto(string codProduto, string codNiveis)
        {
            string sqlNiveisProdutoQuebra = $"SELECT * FROM TBT_NIVEL_PRODUTO WHERE CodProduto = '{codProduto}' AND CodNivel IN ({codNiveis});";
            List<NivelProdutoResult> niveisDoProduto = await _sqlConnection.QueryAsync<NivelProdutoResult>(sqlNiveisProdutoQuebra);
            return niveisDoProduto;
        }

        public virtual async Task<List<NivelProdutoResult>> GetNiveisDeQuebraProduto(string codProduto)
        {
            ////Buscar todos os niveis de quebra.
            List<NivelResult> niveisDeQuebra = await GetNiveisQuebra();
            if (niveisDeQuebra.Count == 0)
                return new List<NivelProdutoResult>();

            string niveis = string.Empty;
            foreach (var item in niveisDeQuebra)
                niveis += $"'{item.CodNivel}',";

            niveis = niveis.Substring(0, niveis.Length - 1);

            string sqlNiveisProdutoQuebra = $"SELECT * FROM TBT_NIVEL_PRODUTO WHERE CodProduto = '{codProduto}' AND CodNivel IN ({niveis});";
            List<NivelProdutoResult> niveisDoProduto = await _sqlConnection.QueryAsync<NivelProdutoResult>(sqlNiveisProdutoQuebra);
            return niveisDoProduto;
        }

        public virtual async Task<List<NivelDescontoCommandResult>> GetDescontos(BuscarNivelDescontoCommand command)
        {
            string sql = $"SELECT ND.CodNivel,ND.CodAtributo,NA.Descricao,IFNULL(ND.Desconto, 0) AS Desconto,ND.CodPessoaCliente FROM TBT_NIVEL N " +
                $"INNER JOIN TBT_NIVEL_ATRIBUTO NA ON N.CodNivel = NA.CodNivel " +
                $"INNER JOIN TBT_NIVEL_DESCONTO ND ON NA.CodNivel = ND.CodNivel AND NA.CodAtributo = ND.CodAtributo " +
                $"WHERE N.DefineDesconto = 1 " +
                $"AND ND.IndAtivo = 1 " +
                $"AND EXISTS (SELECT 1 FROM TBT_PRODUTO " +
                $"INNER JOIN TBT_NIVEL_PRODUTO ON TBT_NIVEL_PRODUTO.CodProduto = TBT_PRODUTO.CodProduto " +
                $"AND TBT_PRODUTO.CodMarca = '{command.CodMarca}' ";

            if (!string.IsNullOrEmpty(command.CodProduto) && command.CodNivel == null)
            {
                sql += $"AND TBT_PRODUTO.CodProduto = '{command.CodProduto}' " +
                       $"AND TBT_NIVEL_PRODUTO.CodNivel = NA.CodNivel AND TBT_NIVEL_PRODUTO.CodAtributo = NA.CodAtributo ";
            }

            sql += $") ";

            if (command.CodNivel != null)
                sql += $"AND ND.CodNivel = '{command.CodNivel}' ";
            if (command.CodAtributo != null)
                sql += $"AND ND.CodAtributo = '{command.CodAtributo}' ";
            if (command.CodPessoaCliente != null)
                sql += $"AND ND.CodPessoaCliente = '{command.CodPessoaCliente}' ";

            var dados = await _sqlConnection.QueryAsync<NivelDescontoCommandResult>(sql);
            return dados;
        }

        public virtual async Task<List<NivelAtributoResult>> GetNiveisSegmentacao()
        {
            string sql = "SELECT NA.CodNivel,NA.CodAtributo FROM TBT_NIVEL N " +
                         "INNER JOIN TBT_NIVEL_ATRIBUTO NA ON N.CodNivel = NA.CodNivel " +
                         "WHERE SegmentaCliente = '1';";
            List<NivelAtributoResult> niveis = await _sqlConnection.QueryAsync<NivelAtributoResult>(sql);
            return niveis;
        }

        public virtual async Task<List<NivelAtributoResult>> GetNiveisSegmentacaoDoCliente(string codPessoaCliente)
        {
            string sql = "SELECT NA.CodNivel,NA.CodAtributo FROM TBT_NIVEL N " +
                         "INNER JOIN TBT_NIVEL_ATRIBUTO NA ON N.CodNivel = NA.CodNivel " +
                         $"INNER JOIN TBT_PESSOA_NIVEL NC ON NA.CodNivel = NC.CodNivel AND NA.CodAtributo = NC.CodAtributo AND NC.CodPessoa = '{codPessoaCliente}' " +
                         "WHERE N.SegmentaCliente = '1';";
            List<NivelAtributoResult> niveis = await _sqlConnection.QueryAsync<NivelAtributoResult>(sql);
            return niveis;
        }


        public void Dispose()
        {
            if (_sqlConnection != null)
            {
                _sqlConnection.GetConnection().Dispose();
            }
        }

        public async Task<string> GetCodNivelDesconto(string v)
        {
            string sql = $"SELECT CodNivel FROM TBT_NIVEL WHERE Titulo = '{v}';";
            var CodNivel = await _sqlConnection.QueryAsync<NivelResult>(sql);
            return CodNivel.First().CodNivel;
        }
    }
}
