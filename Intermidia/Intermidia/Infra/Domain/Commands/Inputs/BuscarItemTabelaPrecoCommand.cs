namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarItemTabelaPrecoCommand
    {
        public string CodTabelaPreco { get; set; }
        public string CodProduto { get; set; }
        public decimal Valor { get; set; }
        public string CodPessoa { get; set; }
    }


}
