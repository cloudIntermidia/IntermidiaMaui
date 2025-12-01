using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Repositories
{
    public interface IUsuarioRepository : IDisposable
    {
        Task<UsuarioCommandResult> BuscarUsuario(AutenticarUsuarioCommand command);
        Task<UsuarioCommandResult> GetLoginUsuario(string CodUsuario);
        Task<List<TBT_REGRA_USUARIO_CLIENTE>> BuscarRegraCliente(string codUsuario);
        Task<List<TBT_REGRA_USUARIO_MARCA>> BuscarRegraMarca(string codUsuario);
        //Task<List<TBT_NIVEL_ATRIBUTO>> BuscarEmpresaDisponivel();
        Task<List<TBT_REGRA_USUARIO_PRODUTO>> BuscarRegraProduto(string codUsuario);

    }
}
