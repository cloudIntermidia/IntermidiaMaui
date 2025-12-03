using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public UsuarioRepository(ISqliteConnection context)
        {
            try
            {
                _sqlAsyncConnection = context.DbConnectionAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

        }

        public async Task<UsuarioCommandResult> BuscarUsuario(AutenticarUsuarioCommand command)
        {
            string sql = ManagerQuery.MakeSql("PRO_USUARIO_GET", "Query", command);
            var result = await _sqlAsyncConnection.QueryAsync<UsuarioCommandResult>(sql);
            return result.FirstOrDefault();
        }

        public async Task<UsuarioCommandResult> GetLoginUsuario(string CodUsuario)
        {
            string sql = "SELECT U.Login AS Login FROM TBT_USUARIO U WHERE CODUSUARIO = '" + CodUsuario + "'";
            var result = await _sqlAsyncConnection.QueryAsync<UsuarioCommandResult>(sql);
            return result.FirstOrDefault();
        }

        public async Task<List<TBT_REGRA_USUARIO_CLIENTE>> BuscarRegraCliente(string codUsuario)
        {
            //string sql = ManagerQuery.MakeSql("PRO_ESTOQUES_PRODUTO_GET", "Query", command);
            string sql = $"SELECT DISTINCT TpRegra, CodCliente, CodRepresentante FROM TBT_REGRA_USUARIO_CLIENTE WHERE CodRepresentante = '{codUsuario}';";
            var result = await _sqlAsyncConnection.QueryAsync<TBT_REGRA_USUARIO_CLIENTE>(sql);
            return result;
        }

        public async Task<List<TBT_REGRA_USUARIO_MARCA>> BuscarRegraMarca(string codUsuario)
        {
            //string sql = ManagerQuery.MakeSql("PRO_ESTOQUES_PRODUTO_GET", "Query", command);
            string sql = $"SELECT DISTINCT TpRegra, CodMarca, CodRepresentante FROM TBT_REGRA_USUARIO_MARCA WHERE CodRepresentante = '{codUsuario}';";
            var result = await _sqlAsyncConnection.QueryAsync<TBT_REGRA_USUARIO_MARCA>(sql);
            return result;
        }

        public async Task<List<TBT_REGRA_USUARIO_PRODUTO>> BuscarRegraProduto(string codUsuario)
        {
            string sql = $"SELECT DISTINCT TpRegra, CodProduto, CodRepresentante FROM TBT_REGRA_USUARIO_PRODUTO WHERE CodRepresentante = '{codUsuario}';";
            var result = await _sqlAsyncConnection.QueryAsync<TBT_REGRA_USUARIO_PRODUTO>(sql);
            return result;
        }

        //public async Task<List<TBT_NIVEL_ATRIBUTO>> BuscarEmpresaDisponivel()
        //{
        //    string sql = $"SELECT CodAtributo, Descricao FROM TBT_NIVEL_ATRIBUTO WHERE CodNivel = 1 ;";
        //    var result = await _sqlAsyncConnection.QueryAsync<TBT_NIVEL_ATRIBUTO>(sql);
        //    return result;
        //}

        public void Dispose()
        {
            if (_sqlAsyncConnection != null)
            {
                _sqlAsyncConnection.GetConnection().Dispose();
            }
        }
    }
}
