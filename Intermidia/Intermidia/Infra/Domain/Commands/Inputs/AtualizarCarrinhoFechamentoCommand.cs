using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class AtualizarCarrinhoFechamentoCommand : ICommand
    {
        public AtualizarCarrinhoFechamentoCommand()
        {

        }

        public AtualizarCarrinhoFechamentoCommand(CarrinhoFechamentoCommandResult fechamento)
        {
            CodCarrinho = fechamento.CodCarrinho;
            CodCondicaoPagamento = fechamento.CodCondicaoPagamento;
            CodTabelaPreco = fechamento.CodTabelaPreco;
            TipoFrete = fechamento.TipoFrete == "CIF" ? "C" : "FOB";
            CifFob = fechamento.TipoFrete == "CIF" ? "C" : "F";
            PercentualDesconto = fechamento.PercentualDesconto;
            DataEntrega = fechamento.DataEntrega;
            OrdemCompra = fechamento.OrdemCompra;
            Observacoes = fechamento.Observacao;
            Observacoes1 = fechamento.Observacao1;
            PrazoMedio = fechamento.PrazoMedio;
            PrazoAdicional = fechamento.PrazoAdicional;
            PedidoBonificado = fechamento.PedidoBonificado;
            CodTransportadora = fechamento.CodTransportadora;
            CodEvento = fechamento.CodEvento;
            CupomChave = fechamento.CupomChave;

            PercentualDesconto = fechamento.PercentualDesconto;
            PercentualDesconto1 = fechamento.PercentualDesconto1;
            PercentualDesconto2 = fechamento.PercentualDesconto2;
            PercentualDesconto3 = fechamento.PercentualDesconto3;

            PercentualComissaoRep = fechamento.PercentualComissaoRep;
            PercentualComissaoRep2 = fechamento.PercentualComissaoRep2;
        }

        public string CodCarrinho { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public string CodTabelaPreco { get; set; }
        public string TipoFrete { get; set; }
        public string CifFob { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal PercentualDesconto1 { get; set; }
        public decimal PercentualDesconto2 { get; set; }
        public decimal PercentualDesconto3 { get; set; }
        public decimal PercentualComissaoRep { get; set; }
        public decimal PercentualComissaoRep2 { get; set; }
        public DateTime DataEntrega { get; set; }
        public string OrdemCompra { get; set; }
        public string Observacoes { get; set; }
        public string Observacoes1 { get; set; }
        public decimal PrazoMedio { get; set; }
        public decimal PrazoAdicional { get; set; }
        public string PedidoBonificado { get; set; }
        public string CodTransportadora { get; set; }
        public string CodEvento { get; set; }
        public string CupomChave { get; set; }

        public void Validate()
        {

        }
    }



}
