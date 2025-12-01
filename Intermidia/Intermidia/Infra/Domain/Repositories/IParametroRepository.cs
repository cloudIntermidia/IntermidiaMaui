using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public interface IParametroRepository
    {
        Task<string> BuscarValorParametro(string codParametro);
        Task<string> GetParametroMarca(string codMarca, string codParametro);
    }
}
