using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class CondicaoPagamentoRepository : ICondicaoPagamentoRepository
    {

        protected readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public CondicaoPagamentoRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }

        public virtual async Task<TabelaPrecoResult> BuscarCondicaoPagamento(BuscarCondicaoPagamentoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CONDICAO_PAGAMENTO_FIND", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TabelaPrecoResult>(sql);
            return result.FirstOrDefault();
        }

        public virtual async Task<List<GenericComboResult>> BuscarCondicoesParaFechamento(BuscarCondicaoPagamentoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CONDICAO_PAGAMENTO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }
        public virtual async Task<List<GenericComboResult>> BuscarCondicoesParaFechamento(BuscarCondicaoPagamentoCommand command, string codCarrinho)
        {
            if (string.IsNullOrEmpty(command.CodCondicaoPagamento))
            {
                command.CodCondicaoPagamento = "-1";
            }
            string sql = $"SELECT CP.CodCondicaoPagamento AS Codigo, CP.Descricao AS Descricao, CP.DescontoMaximo AS Desconto " +
                                            $"FROM TBT_CONDICAO_PAGAMENTO CP " +
                                            $"INNER JOIN TBT_CONDICAO_PAGAMENTO_MARCA CPM ON CP.CodCondicaoPagamento = CPM.CodCondicaoPagamento " +
                                            $"INNER JOIN TBT_CARRINHO_NIVEL CN ON CPM.CodMarca = CN.CodAtributo " +
                                            $"WHERE CP.IndAtivo = 1 " +
                                            $"AND EXISTS (SELECT 1 FROM TBT_CARRINHO_NIVEL CN2 WHERE CN2.CodCarrinho = '{codCarrinho}' AND cn.CodAtributo = CN2.CodAtributo) " +
                                            $"AND ('{command.CodCondicaoPagamento}' = '-1' OR  CP.CodCondicaoPagamento = '{command.CodCondicaoPagamento}')" +
                                            $"AND (-1 = -1 OR CP.PrazoMedio <= -1 ) " +
                                            $"AND ('-1' = '-1' OR EXISTS ( SELECT 1 FROM TBT_TABELA_PRECO_COND_PGTO TPCP WHERE TPCP.CodTabelaPreco = '-1'	AND TPCP.CodCondicaoPagamento = CP.CodCondicaoPagamento)) " +
                                            $"GROUP BY CP.Descricao " +
                                            $"ORDER BY CP.Descricao;";
            List<GenericComboResult> niveisDoProduto = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return niveisDoProduto;
        }

        public virtual async Task<List<GenericComboResult>> BuscarCondicaoParaFechamentoPadrao()
        {
            string sql = ManagerQuery.MakeSql("PRO_CONDICAO_PAGAMENTO_PADRAO_GET", "Query", null);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<List<GenericComboResult>> BuscarPrazosMedio(BuscarCondicaoPagamentoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_PRAZO_MEDIO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);
            return result;
        }

        public virtual async Task<GenericComboResult> BuscarCondicoesParaCliente(BuscarCondicaoPagamentoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_CONDICAO_PAGAMENTO_CLIENTE", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<GenericComboResult>(sql);

            if (result.Count > 0)
            {
                return result[0];
            }
            return null;
        }

        public virtual async Task<GenericComboResult> BuscarCondicaoPadrao(BuscarCondicaoPagamentoCommand command)
        {
            return null;
        }

    }
}
