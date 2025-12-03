using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class IdiomaRepository : IIdiomaRepository
    {
        private readonly SQLiteAsyncConnection _sqlAsyncConnection;

        public IdiomaRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }

        public async Task<List<IdiomaCommandResult>> GetIdiomaList()
        {
            string sql = ManagerQuery.MakeSql("PRO_IDIOMA_GET", "Query", null);
            var result = await _sqlAsyncConnection.QueryAsync<IdiomaCommandResult>(sql);
            return result;

        }

    }
}
