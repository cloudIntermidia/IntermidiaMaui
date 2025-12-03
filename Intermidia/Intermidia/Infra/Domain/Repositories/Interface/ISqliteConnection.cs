using SQLite;

namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface ISqliteConnection
    {
        SQLiteAsyncConnection DbConnectionAsync();
        Task<byte[]> GerarBackup(string nomeArquivo = null);
        void RestaurarBackup(string nomeArquivo);
    }
}
