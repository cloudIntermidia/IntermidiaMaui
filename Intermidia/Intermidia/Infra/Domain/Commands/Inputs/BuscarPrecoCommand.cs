namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarPrecoCommand
    {
        public BuscarPrecoCommand()
        {
        }

        public BuscarPrecoCommand(string codTabelaPreco, string codProduto)
        {
            CodTabelaPreco = codTabelaPreco;
            CodProduto = codProduto;
        }

        public string CodTabelaPreco { get; set; }
        public string CodProduto { get; set; }
    }

}
