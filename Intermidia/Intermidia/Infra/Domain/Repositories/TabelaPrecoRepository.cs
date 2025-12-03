using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class TabelaPrecoRepository : ITabelaPrecoRepository
    {
        protected readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public TabelaPrecoRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }

        public virtual async Task<TabelaPrecoResult> BuscarTabelaDePreco(BuscarTabelaPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TABELA_PRECO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TabelaPrecoResult>(sql);
            return result?.FirstOrDefault();
        }
        public virtual async Task<List<TabelaPrecoResult>> BuscarTabelasDePreco(BuscarTabelaPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TABELA_PRECO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TabelaPrecoResult>(sql);
            return result;
        }

        public virtual async Task<List<TabelaPrecoResult>> BuscarTabelasPorCode(BuscarTabelaPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TABELA_PRECO_CODE_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TabelaPrecoResult>(sql);
            return result;
        }

        public virtual async Task<List<TabelaPrecoIndiceResult>> BuscarIndicesTabelaDePreco(BuscarTabelaPrecoIndiceCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TABELA_PRECO_INDICE_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TabelaPrecoIndiceResult>(sql);
            return result;
        }

        public virtual async Task<decimal> BuscarComissaoItem(BuscarItemTabelaPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_COMISSAO_ITEM_TABELA_PRECO_GET", "Query", command);
            var result = await _sqlAsyncConnection.ExecuteScalarAsync<decimal>(sql);
            return result;
        }

        public virtual async Task<TabelaPrecoResult> BuscarTabelaDePrecoPadrao(BuscarTabelaPrecoCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_TABELA_PRECO_PADRAO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<TabelaPrecoResult>(sql);
            return result?.FirstOrDefault();
        }

        public void Dispose()
        {
            if (_sqlAsyncConnection != null)
            {
                _sqlAsyncConnection.GetConnection().Dispose();
            }
        }
    }
}
