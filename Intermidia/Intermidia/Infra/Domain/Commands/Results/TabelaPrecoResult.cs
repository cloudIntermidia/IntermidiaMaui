namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class TabelaPrecoResult : GenericComboResult
    {
        public string CodTabelaPreco { get; set; }
        public string DescricaoDetalhada { get; set; }
        public string Moeda { get; set; }
        public int QtdParcela { get; set; }
        public int IndPromocional { get; set; }
        public bool IndAtivo { get; set; }
        public DateTime? FinalVigencia { get; set; }
        public DateTime? InicioVigencia { get; set; }
        public TabelaPrecoResult()
        {
        }
    }
}
