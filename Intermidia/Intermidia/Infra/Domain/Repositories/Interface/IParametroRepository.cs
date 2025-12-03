namespace Intermidia.Intermidia.Infra.Domain.Repositories.Interface
{
    public interface IParametroRepository
    {
        Task<string> BuscarValorParametro(string codParametro);
        Task<string> GetParametroMarca(string codMarca, string codParametro);
    }
}
