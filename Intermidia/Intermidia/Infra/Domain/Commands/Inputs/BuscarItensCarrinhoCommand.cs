namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarItensCarrinhoCommand
    {
        public BuscarItensCarrinhoCommand()
        {

        }
        public BuscarItensCarrinhoCommand(string codCarrinho, string codTabelaPreco = null)
        {
            CodCarrinho = codCarrinho;
            CodTabelaPreco = codTabelaPreco;
        }

        public string CodCarrinho { get; set; }
        public string CodProduto { get; set; }
        public string CodGrade { get; set; }
        public string CodAtendimento { get; set; }
        public string CodSituacaoPedido { get; set; }
        public string CodTipoPedido { get; set; }
        /// <summary>
        /// Propriedade utilizada para trazer somente itens do carrinho 
        /// que sera alterado.
        /// </summary>
        public string ValidaBloqueado { get; set; }
        public string CodTabelaPreco { get; set; }
    }


}
