namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class WcfValidarCupomInput
    {
        public string Chave { get; set; }
        public string CodCarrinho { get; set; }
        public string CodPessoaCliente { get; set; }
        public string CodUsuario { get; set; }

        public WcfValidarCupomInput(string chaveCupom, string codCarrinho, string codPessoaCliente, string codUsuario)
        {
            Chave = chaveCupom;
            CodCarrinho = codCarrinho;
            CodPessoaCliente = codPessoaCliente;
            CodUsuario = codUsuario;
        }
    }


}
