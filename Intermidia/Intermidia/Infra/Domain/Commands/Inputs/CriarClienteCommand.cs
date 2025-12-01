using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class CriarClienteCommand : ICommand
    {
        public CriarClienteCommand(UsuarioCommandResult usuario, ClienteCommandResult cliente)
        {
            Usuario = usuario;
            Cliente = cliente;
        }

        public UsuarioCommandResult Usuario { get; private set; }
        public ClienteCommandResult Cliente { get; private set; }
        public string CodSituacaoCliente { get; set; }


        public void Validate()
        {

        }
    }


}
