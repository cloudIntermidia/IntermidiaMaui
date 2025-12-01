namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class AtendimentoCommandResult
    {
        public string CodAtendimento { get; set; }
        public string CodPessoaCliente { get; set; }
        public string CodGrupoCliente { get; set; }
        public string CodPessoaRepresentante { get; set; }
        public decimal CodUsuario { get; set; }
        public string CodMarca { get; set; }
        public string CodInstalacao { get; set; }
        public string Descricao { get; set; }
        public decimal IndAberto { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public string CodTabelaPreco { get; set; }
        public decimal ItensEmAtendimento { get; set; } = -1;
        public decimal PrazoMedio { get; set; }
        public decimal PrazoAdicional { get; set; }
        public string PedidoBonificado { get; set; }
        public string Tipo { get; set; }
        public string TipoFrete { get; set; }
        public string CodEvento { get; set; }
        public decimal IndiceCoeficiente { get; set; }
        public decimal Markup { get; set; }
        public int IndPrivateLabel { get; set; }

        public decimal PercentualDesconto1 { get; set; }

        public string ConfiguracaoAtendimento { get; set; }

        public string Descricao_Desconto
        {
            get
            {
                string retorno = Descricao;

                if (PercentualDesconto1 > 0)
                {
                    retorno = string.Format("{0} {1}", retorno, new FuncaoGenerica().FormataPercentual(PercentualDesconto1));
                }

                return retorno;
            }
        }

        public string Bloquear { get; set; }
        public Boolean BloqueadoVendaPrazo
        {
            get
            {
                Boolean retorno = false;

                if (!String.IsNullOrEmpty(this.Bloquear) && this.Bloquear.Equals("S"))
                {
                    retorno = true;
                }

                return retorno;
            }
        }

    }

}
