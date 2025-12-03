using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        protected readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public MarcaRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }
        public async Task<SelecaoMarcaCommandResult> BuscarMarca(BuscarMarcaCommand command)
        {
            var result = await BuscarMarcas(command);
            return result.FirstOrDefault();
        }

        public async Task<List<SelecaoMarcaCommandResult>> BuscarMarcas(BuscarMarcaCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_SELECAO_MARCAS_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<SelecaoMarcaCommandResult>(sql);
            return result;
        }
    }
}
