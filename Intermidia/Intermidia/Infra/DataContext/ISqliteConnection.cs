using SQLite;

namespace Intermidia.Intermidia.Infra.DataContext
{
    public interface ISqliteConnection
    {
        SQLiteAsyncConnection DbConnectionAsync();
        Task<byte[]> GerarBackup(string nomeArquivo = null);
        void RestaurarBackup(string nomeArquivo);
    }
}
