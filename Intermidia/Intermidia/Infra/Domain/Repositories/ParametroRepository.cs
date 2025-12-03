using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class ParametroRepository : IParametroRepository
    {
        private readonly SQLiteAsyncConnection _sqlConnection;
        public ParametroRepository(ISqliteConnection context)
        {
            _sqlConnection = context.DbConnectionAsync();
        }

        public async Task<string> BuscarValorParametro(string codParametro)
        {
            var param = await BuscarParametro(codParametro);
            return param == null ? string.Empty : param.Valor;
        }

        private async Task<TBT_PARAMETRO> BuscarParametro(string codParametro)
        {
            return await _sqlConnection.FindAsync<TBT_PARAMETRO>(x => x.CodParametro == codParametro);
        }

        public async Task<string> GetParametroMarca(string codMarca, string codParametro)
        {
            string sql = ManagerQuery.MakeSql("PRO_PARAMETRO_MARCA_GET", "Query", new { CodMarca = codMarca, CodParametro = codParametro });
            var result = await _sqlConnection.QueryAsync<ParametroMarcaCommandResult>(sql);
            if (result.Count > 0)
                return result[0].ValorParametroMarca;

            return null;
        }
    }
}
