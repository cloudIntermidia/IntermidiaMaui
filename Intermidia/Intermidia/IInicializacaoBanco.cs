namespace Intermidia.Intermidia
{
    public interface IInicializacaoBanco : IDisposable
    {
        Task<bool> BancoJaExiste();
        Task Init();
        Task DataInit();
        Task CriarTabelas();
        Task Atualizacoes();
        Task LimparDados();
        Task LimparFotos();
    }
}