namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class QuebrarPedidoCommand
    {
        public QuebrarPedidoCommand(string codAtendimento, string codPessoaCliente,
            string codPessoaRepresentante, string codMarca, string codProduto,
            string codTipoPedido, string codPoliticaComercial = null, string codTabelaPreco = null,
            DateTime? dataEntrega = null, string codDeposito = null)
        {
            CodAtendimento = codAtendimento;
            CodPessoaCliente = codPessoaCliente;
            CodPessoaRepresentante = codPessoaRepresentante;
            CodMarca = codMarca;
            CodProduto = codProduto;
            CodTipoPedido = codTipoPedido;
            CodPoliticaComercial = codPoliticaComercial;
            CodTabelaPreco = codTabelaPreco;
            DataEntrega = dataEntrega;
            CodDeposito = codDeposito;
        }

        public string CodAtendimento { get; private set; }
        public string CodPessoaCliente { get; private set; }
        public string CodPessoaRepresentante { get; private set; }
        public string CodMarca { get; private set; }
        public string CodProduto { get; private set; }
        public string CodTipoPedido { get; private set; }
        public string CodPoliticaComercial { get; private set; }
        public string CodTabelaPreco { get; private set; }
        public string CodDeposito { get; set; }
        public DateTime? DataEntrega { get; set; }
    }



}
