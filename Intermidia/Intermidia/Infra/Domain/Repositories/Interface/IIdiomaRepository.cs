using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface IIdiomaRepository
    {
        Task<List<IdiomaCommandResult>> GetIdiomaList();

    }
}
