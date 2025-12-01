namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class AutenticarUsuarioCommand
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public AutenticarUsuarioCommand(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

    }

}
