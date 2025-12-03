namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarCondicaoPagamentoCommand
    {
        public string CodPessoaVendedor { get; set; }
        public string CodMarca { get; set; }
        public string CodPessoaCliente { get; set; }
        public string CodPessoaRepresentante { get; set; }

        public string CodTabelaPreco { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public decimal PrazoMedio { get; set; }
        public decimal QtdParcelaMax { get; set; }
        public string TipoVenda { get; set; }

        public string WherePermissao { get; set; }

        public string WhereLikeNome { get; set; }

    }


}
