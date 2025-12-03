using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Entities;
using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public class FotoRepository : IFotoRepository
    {
        private readonly SQLiteAsyncConnection _sqlAsyncConnection;
        public FotoRepository(ISqliteConnection context)
        {
            _sqlAsyncConnection = context.DbConnectionAsync();
        }

        public async Task<List<TBT_FOTO_RELATORIO>> BuscarFotosRelatorio(BuscarFotoRelatorioCommand command)
        {
            var sql = $"SELECT Imagem FROM TBT_FOTO_RELATORIO WHERE 1 = 1 AND Tipo = '{command.Tipo}' AND CodMarca = '{command.CodMarca}' ";
            if (command.Codigo != null)
                sql += $"AND Codigo like '{command.Codigo}_0' ";

            //if (command.Codigo != null)
            //    sql += $"AND CodColecao like '%{command.}_%' ";

            sql += "ORDER BY ORDEM;";

            var fotos = await _sqlAsyncConnection.QueryAsync<TBT_FOTO_RELATORIO>(sql);
            return fotos;
        }

        public async Task<string> BuscarFoto(string codProdutoFoto)
        {
            var foto = await _sqlAsyncConnection.ExecuteScalarAsync<string>
                ($"SELECT Imagem FROM TBT_FOTO_PRODUTO WHERE CodProdutoFoto = '{codProdutoFoto}' ORDER BY ID DESC;");
            return foto;
        }

        public async Task<string> BuscarFotoMini(string codProdutoFoto)
        {
            var foto = await _sqlAsyncConnection.ExecuteScalarAsync<string>
                ($"SELECT Imagem FROM TBT_FOTO_PRODUTO_MINI WHERE CodProdutoFoto = '{codProdutoFoto}' ORDER BY ID DESC;");
            if (!string.IsNullOrEmpty(foto))
            {
                return foto;
            }

            return await BuscarFoto(codProdutoFoto);
        }

        public async Task<string> BuscarFotoMarca(string codProdutoFoto, string codMarca)
        {
            try
            {
                if (codMarca != null)
                {
                    var foto = await _sqlAsyncConnection.FindAsync<TBT_FOTO_MARCA>(x => x.CodFoto == codProdutoFoto && x.CodMarca == codMarca);
                    return foto?.Imagem;
                }
                else
                {
                    var foto = await _sqlAsyncConnection.FindAsync<TBT_FOTO_MARCA>(x => x.CodFoto == codProdutoFoto);
                    return foto?.Imagem;
                }
            }
            catch (SQLiteException e)
            {
                throw e;
            }
        }

        public async Task<List<string>> BuscarFotosExtras(string CodProdutoFoto)
        {
            //            var fotos = await _sqlAsyncConnection.Table<TBT_FOTO_PRODUTO_EXTRA>().Where(x => x.CodProdutoFoto.StartsWith(CodProdutoFoto)).ToListAsync();
            //var listFotos = fotos.Count == 0 ? new List<string>() : fotos.OrderBy(x => x.CodProdutoFoto).Select(x => x.Imagem).ToList();

            var fotos = await _sqlAsyncConnection.Table<TBT_FOTO_PRODUTO_EXTRA>().Where(x => x.CodProdutoFoto.StartsWith(CodProdutoFoto)).ToListAsync();
            var listFotos = fotos.Count == 0 ? new List<string>() : fotos.OrderBy(x => x.CodProdutoFoto).Select(x => x.Imagem).ToList();
            return listFotos;
        }

        //public async Task<List<SelecaoMarcaCommandResult>> BuscarFotoMarcas(UsuarioCommandResult usuario)
        //{
        //    string sql = ManagerQuery.MakeSql("PRO_SELECAO_MARCAS_GET", "Query", new { CodPessoa = usuario?.CodPessoa });
        //    Debug.Write(sql);
        //    var result = await _sqlAsyncConnection.QueryAsync<SelecaoMarcaCommandResult>(sql);
        //    return result;
        //}

    }
}
