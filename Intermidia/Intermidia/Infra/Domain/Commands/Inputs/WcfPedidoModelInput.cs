using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class WcfPedidoModelInput
    {
        public PEDIDOVENDA PEDIDOVENDA { get; set; }
        public String FLG_AMBIENTE { get; set; }
    }

    public class PEDIDOVENDA
    {
        public object Carrinho { get; set; }
        public object Itens { get; set; }
    }


}
