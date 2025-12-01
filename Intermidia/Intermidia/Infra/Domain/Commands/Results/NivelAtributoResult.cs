namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class NivelAtributoResult : GenericComboResult
    {
        public string CodNivel { get; set; }
        public string CodAtributo { get; set; }
        public int Ordem { get; set; }
    }
}
