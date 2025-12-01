using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public interface IParametroSincronizacaoRepository : IDisposable //: IRepositoryBase<PARAMETRO_SINCRONIZACAO>
    {
        Task<string> BuscarValorParametro(string codParametro);
        Task<int> SalvarParametro(string codParametro, string valor);
        Task<TBT_SINCRONIZACAO_USUARIO> BuscarUltimaDataSincUsuario(string codUsuario);
        Task<int> SalvarSincronizacaoUsuario(string CodUsuario, string DataSincronizacao);
        Task LimpaHistoricoCarrinho();
    }
}
