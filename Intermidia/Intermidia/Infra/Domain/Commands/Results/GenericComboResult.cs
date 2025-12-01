using Intermidia.Intermidia.Infra.Domain.Entities;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class GenericComboResult : BaseMultiSelected
    {
        public string Nivel { get; set; }
        public string FiltroCatalogo { get; set; }
        public new string Codigo { get; set; }
        public new string Descricao { get; set; }
        public string QuebraPedido { get; set; }
        public string ValidaMultiplos { get; set; }
        public string ExibirNoDetalhe { get; set; }
        public string PermiteSelecaoMultipla { get; set; }
        public string SegmentaCliente { get; set; }
        public string ValidaMinimoPedido { get; set; }

        public decimal Desconto { get; set; }

        public string Padrao { get; set; }
        public string Tipo { get; set; }
        public DateTime? DataSelecao { get; set; }
    }

    public class ED_CollectionView_Generico
    {
        public String Tipo { get; set; }
        public String Codigo { get; set; }
        public String Descricao { get; set; }
    }

}
