namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class RelacionarClienteCommand
    {
        public RelacionarClienteCommand(string codPessoaCliente, string codMarca, string codPessoaRepresentante, string codPessoaVendedor)
        {
            CodPessoaCliente = codPessoaCliente;
            CodMarca = codMarca;
            CodPessoaRepresentante = codPessoaRepresentante;
            CodPessoaVendedor = codPessoaVendedor;
        }

        public string CodPessoaCliente { get; set; }
        public string CodMarca { get; private set; }
        public string CodPessoaRepresentante { get; private set; }
        public string CodPessoaVendedor { get; private set; }
        public int IndBloqueioPedido { get; set; }
    }


}
