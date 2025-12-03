using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface IMarcaRepository
    {
        Task<List<SelecaoMarcaCommandResult>> BuscarMarcas(BuscarMarcaCommand command);
        Task<SelecaoMarcaCommandResult> BuscarMarca(BuscarMarcaCommand command);
    }
}
