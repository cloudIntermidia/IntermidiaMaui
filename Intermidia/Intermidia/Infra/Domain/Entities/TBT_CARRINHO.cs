using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using System;

namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_CARRINHO : Entity
    {
        public TBT_CARRINHO()
        {

        }
        public TBT_CARRINHO(string codCarrinho, AtendimentoCommandResult atendimento, UsuarioCommandResult usuario, CarrinhoCommandResult carrinhoOrigem, string codTipoPedido = null)
        {
            //copia ou desmembramento
            if (carrinhoOrigem != null)
            {
                CodCarrinho = codCarrinho;
                CodMarca = usuario.CodMarca;
                CodPessoaCliente = atendimento.CodPessoaCliente;
                CodPessoaRepresentante = usuario.CodPessoa;
                CodPessoaPreposto = usuario.CodPessoa;
                CodSituacaoPedido = "1";
                CodTipoPedido = codTipoPedido; // codTipoPedido;
                CodUsuario = (int)usuario.CodUsuario;
                CodAtendimento = atendimento.CodAtendimento;
                DataEmissao = DateTime.Now;
                DataEntrega = carrinhoOrigem.DataEntrega;
                CodInstalacao = usuario.CodInstalacao;
                CtrlDataOperacao = DateTime.Now;
                PedidoMae = carrinhoOrigem.PedidoMae;
                CodCondicaoPagamento = atendimento.CodCondicaoPagamento;
                CodTabelaPreco = atendimento.CodTabelaPreco;
                PrazoMedio = atendimento.PrazoMedio;
                PrazoAdicional = atendimento.PrazoAdicional;
                PedidoBonificado = atendimento.PedidoBonificado;
            }
            else
            {
                CodCarrinho = codCarrinho;
                CodMarca = usuario.CodMarca;
                CodPessoaCliente = atendimento.CodPessoaCliente;
                CodPessoaRepresentante = usuario.CodPessoa;
                CodPessoaPreposto = usuario.CodPessoa;
                CodSituacaoPedido = "1";
                CodTipoPedido = codTipoPedido; // codTipoPedido;
                CodUsuario = (int)usuario.CodUsuario;
                CodAtendimento = atendimento.CodAtendimento;
                DataEmissao = DateTime.Now;
                DataEntrega = null;
                CodInstalacao = usuario.CodInstalacao;
                CtrlDataOperacao = DateTime.Now;

                CodCondicaoPagamento = atendimento.CodCondicaoPagamento;
                CodTabelaPreco = atendimento.CodTabelaPreco;
                PrazoMedio = atendimento.PrazoMedio;
                PrazoAdicional = atendimento.PrazoAdicional;
                PedidoBonificado = atendimento.PedidoBonificado;
                PercentualDesconto1 = atendimento.PercentualDesconto1;
            }

        }

        public string CodCarrinho { get; set; }
        public string CodMarca { get; set; }
        public string CodPessoaCliente { get; set; }
        public string CodPessoaRepresentante { get; set; }
        public string CodPessoaPreposto { get; set; }
        public string CodSituacaoPedido { get; set; }
        public string CodTipoPedido { get; set; }
        public int CodUsuario { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public string CodTabelaPreco { get; set; }
        public string CodAtendimento { get; set; }
        public string CodInstrucao { get; set; }
        public Nullable<System.DateTime> DataEmissao { get; set; }
        public Nullable<System.DateTime> DataEntrega { get; set; }
        public Nullable<int> QtdTotal { get; set; }
        public Nullable<decimal> ValorTotal { get; set; }
        public Nullable<decimal> ValorTotalLiquido { get; set; }
        public string OrdemCompra { get; set; }
        public string Observacoes { get; set; }
        public string Observacoes1 { get; set; }
        public string CodPedido { get; set; }
        public string AceitaFaturamentoAntecipado { get; set; }
        public string CifFob { get; set; }
        public string CodDeposito { get; set; }
        public string CodPedidoOrigem { get; set; }
        public string CodInstalacao { get; set; }
        public DateTime? CtrlDataOperacao { get; set; }
        public decimal PercentualDesconto { get; set; }
        public decimal PercentualDesconto1 { get; set; }
        public decimal PercentualDesconto2 { get; set; }
        public decimal PercentualDesconto3 { get; set; }
        public decimal PrazoMedio { get; set; }
        public decimal PrazoAdicional { get; set; }
        public string PedidoBonificado { get; set; }
        public string CodPoliticaComercial { get; set; }
        public string Email { get; set; }
        public string TipoPedido { get; set; }
        public string TipoPrazo { get; set; }
        public string AceitaAdicionalFrete { get; set; }
        public decimal ID { get; set; }
        public decimal IndBloqueado { get; set; }
        public decimal IdServidor { get; set; }
        public string PedidoMae { get; set; }
        public int IndPedidoMae { get; set; }

        public Boolean MaterialPDV { get; set; }
    }
}
