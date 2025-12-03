namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class TabelaPrecoIndiceResult
    {
        public string CodTabelaPreco { get; set; }
        public string IndPromocional { get; set; }
        public decimal Coeficiente { get; set; }
        public decimal Markup { get; set; }
        public decimal PercentualComissao { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoSugestao { get; set; }
    }
}
