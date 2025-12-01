using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarGradesItemCommand
    {
        public string CodCarrinho { get; set; }
        public decimal CodItemCarrinho { get; set; }
        public string CodItemPedido { get; set; }
        public BuscarGradesItemCommand(string codcarrinho, decimal codItemCarrinho)
        {
            CodItemCarrinho = codItemCarrinho;
            CodCarrinho = codcarrinho;
        }

        public BuscarGradesItemCommand(string codcarrinho, string codItem)
        {
            CodItemPedido = codItem;
            CodCarrinho = codcarrinho;
        }

    }


}
