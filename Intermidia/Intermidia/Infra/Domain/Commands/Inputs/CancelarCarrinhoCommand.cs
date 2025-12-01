using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class CancelarCarrinhoCommand : ICommand
    {
        public string CodCarrinho { get; set; }
        public string CodAtendimento { get; set; }
        public string CodSituacaoPedido { get; private set; }

        public CancelarCarrinhoCommand(string codCarrinho, string codAtendimento, string codSituacaoPedido = "7")
        {
            CodAtendimento = codAtendimento;
            CodCarrinho = codCarrinho;
            CodSituacaoPedido = codSituacaoPedido;
        }

        public void Validate()
        {
        }
    }

}
