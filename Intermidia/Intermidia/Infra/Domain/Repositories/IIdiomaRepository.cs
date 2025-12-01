using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public interface IIdiomaRepository
    {
        Task<List<IdiomaCommandResult>> GetIdiomaList();

    }
}
