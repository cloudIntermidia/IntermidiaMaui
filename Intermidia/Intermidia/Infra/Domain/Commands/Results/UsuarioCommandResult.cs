namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class UsuarioCommandResult : AutenticarUsuarioCommand
    {
        public string Login { get; set; }
        public decimal ID { get; set; }
        public decimal CodUsuario { get; set; }
        public string CodMarca { get; set; }
        public string CodPessoa { get; set; }
        public string Nome { get; set; }
        public string CodTipoPessoa { get; set; }
        public string CodTabelaPreco { get; set; }
        public string CodInstalacao { get; set; }
        public string Email { get; set; }
        public string TipoVendedor { get; set; }

        public TpVendedor VerificaTipoVendedor
        {
            get
            {
                TpVendedor retorno = TpVendedor.Representante;

                switch (TipoVendedor)
                {
                    case "G":
                        retorno = TpVendedor.Gerente;
                        break;
                    default:
                        retorno = TpVendedor.Representante;
                        break;
                }

                return retorno;
            }
        }

        public enum TpVendedor
        {
            Representante,
            Gerente
        }
    }

}
