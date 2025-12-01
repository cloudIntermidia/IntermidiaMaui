using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using System.Windows.Input;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class CopiarCarrinhoCommand : ICommand
    {
        public string CodCarrinhoOrigem { get; set; }
        public string PedidoMae { get; set; }
        public List<ItemCommandResult> Itens { get; set; }
        public string CodAtendimento { get; set; }
        public string CodPessoaCliente { get; set; }
        public UsuarioCommandResult Usuario { get; set; }
        public CriarAtendimentoCommand NovoAtendimento { get; set; }
        public DateTime? DataEntrega { get; set; }
        public DateTime? DataFaturamento { get; set; }

        public string CodTipoPedido { get; set; }
        public string CodPoliticaComercial { get; set; }
        public string CodTabelaPreco { get; set; }
        public decimal PercentualDesconto { get; set; }
        public string CodCondicaoPagamento { get; set; }

        public CopiarCarrinhoCommand()
        {
            Itens = new List<ItemCommandResult>();
        }

        public void Validate()
        {
        }
    }



}
