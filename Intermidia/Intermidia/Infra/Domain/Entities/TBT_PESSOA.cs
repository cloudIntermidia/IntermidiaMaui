namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_PESSOA : Entity
    {
        public string CodPessoa { get; set; }
        public string Nome { get; set; }
        public string CodTipoPessoa { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public Nullable<decimal> PercentualDesconto { get; set; }
        public int IndAtivo { get; set; }
        public Nullable<decimal> Comissao { get; set; }
        public string Email { get; set; }
        public string CodPessoaERP { get; set; }
        public string CodPessoaAFV { get; set; }
        public string CodTabelaPreco { get; set; }
        public string CodPessoaTransportadora { get; set; }
        public string CodPessoaRedespacho { get; set; }
        public string Telefone1 { get; set; }
        public decimal ID { get; set; }
        public Nullable<System.DateTime> CtrlDataOperacao { get; set; }
    }

}
