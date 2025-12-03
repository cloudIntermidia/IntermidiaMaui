using Intermidia.Intermidia.Infra.Domain.Commands.Results;

namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public class TBT_ITEM_CARRINHO : Entity
    {
        public TBT_ITEM_CARRINHO()
        {
        }
        public TBT_ITEM_CARRINHO(string codCarrinho, decimal codItemCarrinho, ItemCommandResult produto, ModeloCommandResult modelo = null)
        {
            CodCarrinho = codCarrinho;
            CodItemCarrinho = (int)codItemCarrinho;
            CodProduto = produto.CodProduto;
            CodGrade = produto.CodGrade;
            QtdNaCaixa = (int)produto.QtdNaCaixa;
            QtdCaixa = (int)produto.QtdCaixa;
            QtdTotal = (int)produto.QtdTotal;
            ValorUnitario = produto.ValorUnitario;
            ValorUnitarioLiquido = produto.ValorUnitarioLiquido;
            CtrlDataOperacao = DateTime.Now;
            CodDeposito = produto.CodDeposito;
            CodCaixa = produto.CodCaixa;
            DataEntrega = produto.Entrega ?? produto.DataEntrega;
            DataEstoque = produto.DataEstoque;
            Markup = produto.Markup;
            MarkupTabela = produto.MarkupTabela;
            Comissao = produto.Comissao;
            CodTabelaPreco = produto.CodTabelaPreco;
            CodItemProntaEntrega = produto.CodItemProntaEntrega;
            Tipo = produto.Tipo;
            TipoGradeIncluida = produto.TipoGradeIncluida;
            PercDesc = produto.PercDesc;
            PercDesc1 = produto.PercDesc1;
            PercDesc2 = produto.PercDesc2;
            CodKit = produto.CodKit;
            UsaDescontoItem = produto.UsaDescontoItem;
            PedidoMae = produto.PedidoMae;
            CodKit = produto.CodKit;
            FatorKit = Convert.ToInt32(produto.FatorKit);
            CodLocalPadrao = produto.CodLocalPadrao;
        }

        public string CodCarrinho { get; set; }
        public int CodItemCarrinho { get; set; }
        public string CodProduto { get; set; }
        public Nullable<System.DateTime> DataEntrega { get; set; }
        public Nullable<System.DateTime> DataEstoque { get; set; }
        public string CodDeposito { get; set; }
        public string CodGrade { get; set; }
        public string Tipo { get; set; }
        public string TipoGradeIncluida { get; set; }
        public string CodTabelaPreco { get; set; }
        public Nullable<int> QtdNaCaixa { get; set; }
        public Nullable<int> QtdCaixa { get; set; }
        public Nullable<int> QtdTotal { get; set; }
        public Nullable<decimal> ValorUnitario { get; set; }
        public Nullable<decimal> ValorUnitarioLiquido { get; set; }
        public Nullable<decimal> PercDesc { get; set; }
        public Nullable<decimal> PercDesc1 { get; set; }
        public Nullable<decimal> PercDesc2 { get; set; }
        public Nullable<decimal> PercDesc3 { get; set; }
        public Nullable<decimal> PercDesc4 { get; set; }
        public int UsaDescontoItem { get; set; }
        public Nullable<decimal> CodInstalacao { get; set; }
        public string CodItemProntaEntrega { get; set; }
        public string CodCaixa { get; set; }
        public string CodKit { get; set; }
        public decimal ID { get; set; }
        public decimal Excluido { get; set; }
        public virtual List<TBT_GRADE_ITEM_CARRINHO> Grades { get; set; }
        public Nullable<System.DateTime> CtrlDataOperacao { get; set; }
        public Nullable<decimal> Comissao { get; set; }
        public Nullable<decimal> Markup { get; set; }
        public Nullable<decimal> MarkupTabela { get; set; }
        public string PedidoMae { get; set; }
        public Nullable<int> FatorKit { get; set; }
        public string CodLocalPadrao { get; set; }
        public List<TBT_ITEM_CARRINHO_KIT> ListaProdutoKIT { get; set; }
        public string CodEmpresa { get; set; }
    }

    public class TBT_ITEM_CARRINHO_KIT : Entity
    {
        public TBT_ITEM_CARRINHO_KIT()
        {
        }

        public string CodProduto { get; set; }
        public int Quantidade { get; set; }
        public Nullable<decimal> Preco { get; set; }
        public Nullable<decimal> PrecoVenda_ClienteEmpresa { get; set; }
    }

}
