using SQLite;
namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class ParametroSincronizacaoRepository : IParametroSincronizacaoRepository
    {
        private readonly SQLiteAsyncConnection _sqlConnection;
        public ParametroSincronizacaoRepository(ISqliteConnection context)
        {
            _sqlConnection = context.DbConnectionAsync();
        }

        public async Task<string> BuscarValorParametro(string codParametro)
        {
            var param = await BuscarParametro(codParametro);
            return param == null ? string.Empty : param.Valor;
        }

        public async Task<int> SalvarParametro(string codParametro, string valor)
        {
            var param = await BuscarParametro(codParametro);
            if (param != null)
                param.AlterarValor(valor);
            else
                param = new AFV_PARAMETRO_SINCRONIZACAO(codParametro, valor);

            if (param.Valid)
            {
                int rows = await _sqlConnection.InsertOrReplaceAsync(param);
                return rows;
            }

            return 0;
        }

        private async Task<AFV_PARAMETRO_SINCRONIZACAO> BuscarParametro(string codParametro)
        {
            var result = await _sqlConnection.FindAsync<AFV_PARAMETRO_SINCRONIZACAO>(x => x.CodParametro == codParametro);
            return result;
        }

        public async Task<TBT_SINCRONIZACAO_USUARIO> BuscarUltimaDataSincUsuario(string codUsuario)
        {
            var result = await _sqlConnection.FindAsync<TBT_SINCRONIZACAO_USUARIO>(x => x.CodUsuario == codUsuario);
            return result;
        }

        public async Task<int> SalvarSincronizacaoUsuario(string codUsuario, string dataSinc)
        {
            //await _sqlConnection.ExecuteAsync("DELETE FROM TBT_SINCRONIZACAO_USUARIO");

            int rows = await _sqlConnection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM TBT_SINCRONIZACAO_USUARIO WHERE CodUsuario = '{codUsuario}' ");

            if (rows == 0)
            {
                rows = await _sqlConnection.ExecuteAsync($"INSERT INTO TBT_SINCRONIZACAO_USUARIO(CodUsuario, DataUltimaSincronizacao) VALUES ('{codUsuario}', '{dataSinc.ToString()}') ");
            }
            else
            {
                rows = await _sqlConnection.ExecuteAsync($"UPDATE TBT_SINCRONIZACAO_USUARIO  SET DataUltimaSincronizacao = '{dataSinc.ToString()}' WHERE  CodUsuario = '{codUsuario}' ");
            }

            return rows;
        }

        public async Task LimpaHistoricoCarrinho()
        {
            int rows = await _sqlConnection.ExecuteAsync($" DELETE FROM TBT_HISTORICO_CARRINHO; ");
        }

        public void Dispose()
        {

        }
    }
}
