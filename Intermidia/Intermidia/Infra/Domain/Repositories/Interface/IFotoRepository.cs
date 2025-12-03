using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface IFotoRepository
    {
        //Task<List<SelecaoMarcaCommandResult>> BuscarFotoMarcas(UsuarioCommandResult usuario);
        Task<List<TBT_FOTO_RELATORIO>> BuscarFotosRelatorio(BuscarFotoRelatorioCommand command);
        Task<string> BuscarFoto(string codProdutoFoto);
        Task<string> BuscarFotoMini(string codProdutoFoto);
        Task<string> BuscarFotoMarca(string codProdutoFoto, string codMarca);
        Task<List<string>> BuscarFotosExtras(string codProduto);
    }
}
