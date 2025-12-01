using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;
using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public interface ISqliteConnection
    {
        SQLiteAsyncConnection DbConnectionAsync();
        Task<byte[]> GerarBackup(string nomeArquivo = null);
        void RestaurarBackup(string nomeArquivo);
    }
}
