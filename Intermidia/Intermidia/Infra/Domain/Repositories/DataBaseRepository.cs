using Intermidia.Intermidia.Infra.DataContext;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class DataBaseRepository
    {
        protected readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public DataBaseRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }

        public async Task<string> GetString(string tabela, string campo, string whereValue)
        {
            string sql = $"SELECT {campo} FROM {tabela} where {whereValue};";
            var dado = await _sqlAsyncConnection.ExecuteScalarAsync<string>(sql);
            return dado;
        }

        public async Task<decimal> GetDecimal(string tabela, string campo, Dictionary<string, object> camposWhere)
        {
            string whereValue = string.Empty;
            foreach (var item in camposWhere)
            {
                if (item.Value != null)
                {
                    if (item.Value is string)
                    {
                        whereValue += $"{item.Key} = '{item.Value.ToString()}' AND ";
                    }
                    else if (item.Value is decimal)
                    {
                        whereValue += $"{item.Key} = {item.Value.ToString().Replace(",", ".")} AND ";
                    }
                }
            }

            whereValue = whereValue.Substring(0, whereValue.Length - 4);

            string sql = $"SELECT IFNULL((SELECT {campo} FROM {tabela} where {whereValue}),0);";
            var dado = await _sqlAsyncConnection.ExecuteScalarAsync<decimal>(sql);
            return dado;
        }

        public async Task<int> ExecutaUpdate(string tabela, List<string> columnsName, List<string> camposWhere, object parameters)
        {
            string sqlUpdate = ManagerQuery.MakeUpdate(columnsName, tabela, camposWhere, parameters);
            int rows = await _sqlAsyncConnection.ExecuteAsync(sqlUpdate);
            return rows;
        }

        public async Task<int> ExecutaUpdateWithScript(string script)
        {
            int rows = await _sqlAsyncConnection.ExecuteAsync(script);
            return rows;
        }

        public async Task<List<SqliteTableInfoCommandResult>> BuscarInfoTabela(string tabela)
        {
            var camposItem = (await _sqlAsyncConnection.QueryAsync<SqliteTableInfoCommandResult>($"PRAGMA table_info({tabela});")).ToList();
            return camposItem;
        }

        /// <summary>
        /// Metodo retorna uma estrutura 'TableInfo' 
        /// com os valores 'ColumnValue' e 'ColumnName'
        /// </summary>
        /// <param name="tabela">Nome da tabela no banco</param>
        /// <param name="whereColumns"> Filtro do registro </param>
        /// <returns></returns>
        public async Task<List<TableInfo>> BuscarDadosTabela(string tabela, List<TableInfo> whereColumns)
        {
            var columnsName = (await BuscarInfoTabela(tabela)).Select(x => x.name);

            List<TableInfo> lstTableInfo = new List<TableInfo>();
            string where = string.Empty;
            foreach (var campo in whereColumns)
                where += $"{campo.ColumnName} = '{campo.ColumnValue}' AND ";

            where = where.Substring(0, where.Length - 4);

            string sqlCampos = string.Empty;
            foreach (var item in columnsName)
            {
                sqlCampos += $"UNION SELECT '{item}' AS ColumnName, {item} AS ColumnValue FROM {tabela} WHERE {where} ";
            }

            sqlCampos = sqlCampos.Substring(6);
            var dados = await _sqlAsyncConnection.QueryAsync<TableInfo>(sqlCampos);
            return dados;

        }

        /// <summary>
        /// Retorna um object 
        /// </summary>
        /// <param name="tabela">Nome da tabela no banco</param>
        /// <param name="campo">Campos da tabela com o devido mapeamento para o objeto</param>
        /// <param name="whereValue">Filtro do para trazer somente o registro desejado</param>
        /// <returns>object</returns>
        //public async Task<object> BuscarDados(string tabela, string campo, string whereValue)
        //{
        //    string sql = $"SELECT {campo} FROM {tabela} where {whereValue} LIMIT 1;";
        //    var dado = await _sqlAsyncConnection.QueryAsync<object>(sql);
        //    return dado.FirstOrDefault();
        //}
    }
}
