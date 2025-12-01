using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class CriarAtendimentoCommand
    {
        public CriarAtendimentoCommand(string codPessoaCliente)
        {
            this.CodPessoaCliente = codPessoaCliente;
        }

        public CriarAtendimentoCommand()
        {

        }
        public CriarAtendimentoCommand(
                string codPessoaCliente, decimal codUsuario, string codMarca,
                string codInstalacao, string descricao, decimal indAberto,
                string codCondicaoPagamento, string tipoCliente,
                string codTabelaPreco, string codTransportadora,
                string tipoFrete, decimal prazoMedio, decimal prazoAdicional,
                string pedidoBonificado, string codEvento, decimal percentualDesconto1,
                int indPrivateLabel)
        {
            CodPessoaCliente = codPessoaCliente;
            CodUsuario = codUsuario;
            CodMarca = codMarca;
            CodInstalacao = codInstalacao;
            Descricao = descricao;
            IndAberto = indAberto;
            CodCondicaoPagamento = codCondicaoPagamento;
            TipoCliente = tipoCliente;
            CodTabelaPreco = codTabelaPreco;
            CodTransportadora = codTransportadora;
            TipoFrete = tipoFrete;
            PrazoMedio = prazoMedio;
            PrazoAdicional = prazoAdicional;
            PedidoBonificado = pedidoBonificado;
            CodEvento = codEvento;
            PercentualDesconto1 = percentualDesconto1;
            IndPrivateLabel = indPrivateLabel;
        }

        public string CodAtendimento { get; set; }
        public string ConfiguracaoAtendimento { get; set; }
        public string CodPessoaCliente { get; set; }
        public decimal CodUsuario { get; set; }
        public string CodMarca { get; set; }
        public string CodInstalacao { get; set; }
        public string Descricao { get; set; }
        public decimal IndAberto { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public string TipoCliente { get; set; }
        public string CodTabelaPreco { get; set; }
        public string CodTransportadora { get; set; }
        public string TipoFrete { get; set; }
        public decimal PrazoMedio { get; set; }
        public decimal PrazoAdicional { get; set; }
        public string PedidoBonificado { get; set; }
        public string CodEvento { get; set; }
        public string Controle { get; set; }
        public string TipoVenda { get; set; }
        public decimal PercentualDesconto1 { get; set; }
        public int IndPrivateLabel { get; set; }
        //public string Bloquear { get; set; }
    }



}
