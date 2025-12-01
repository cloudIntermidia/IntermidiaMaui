using Intermidia.Intermidia.Infra.Domain.Entities;
using System.Collections.ObjectModel;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class ItemCommandResult : BaseViewModel, ICommandResult, ICloneable
    {
        private string _codItemProntaEntrega;
        private string _codDerivacao;
        private string _codAtributo;
        private string _codCarrinho;
        private decimal _codItemCarrinho;
        private string _codItemPedido;
        private string _codMarca;
        private string _marca;
        private string _codGrade;
        private string _codDeposito;
        private string _codBarra;
        private string _codKit;
        private DateTime? _entrega;
        private DateTime? _dataEstoque;
        private DateTime _dataEntrega;
        private DateTime _dataLimite;
        private string _codCategoria;
        private string _codColecao;
        private string _codTabelaPreco;
        private string _codModelo;
        private string _codProdutoUI;
        private string _codProduto;
        private string _codProdutoFoto;
        private string _referencia;
        private string _descricao;
        private string _descricaoKit;
        private string _descricaoReduzida;
        private string _descricaoCompleta;
        private string _descricaoModelo;
        private string _notaFiscal;
        private string _serie;
        private string _filial;
        private string _imagem;
        private string _site;
        private string _tipo;
        private string _tipoGradeIncluida;
        private decimal _qtdTotal;
        private decimal _qtdCaixa;
        private decimal _qtdNaCaixa;
        private decimal _qtd;
        private decimal _qtdDisponivel;
        private decimal _qtdSolicitada;
        private decimal _qtdUI;
        private decimal _qtdEstoque;
        private decimal _qtdEstoqueTotal;
        private decimal _desconto;
        private decimal _precoSugestao;
        private decimal _precoCusto;
        private decimal _precoCustoComDesconto;
        private decimal _valorUnitario;
        private decimal _valorUnitarioLiquido;
        private decimal _valorTotal;
        private decimal _valorTotalLiquido;
        private decimal _markup;
        private decimal _markupTabela;
        private decimal _comissao;
        private decimal _percDesc;
        private decimal _percDesc1;
        private decimal _percDesc2;
        private bool _itemChecado;
        private bool _itemBloqueado;
        private bool _itemEmEdicao;
        public int _itemChecadoInt;
        public int _indPPK;
        public int _usaDescontoItem;
        private string _codCor;
        private string _codCaixa;
        private string _descricaoMaterial;
        private ObservableCollection<DerivacaoGradeResult> _grades;
        private string _descricaoCor;

        private decimal _qtdSaldo;

        public bool _indProblema;
        public bool IndProblema
        {
            get { return _indProblema; }
            set
            {
                SetProperty(ref _indProblema, value);
            }
        }

        public decimal _fatorkit;
        public decimal FatorKit
        {
            get { return _fatorkit; }
            set
            {
                SetProperty(ref _fatorkit, value);
            }
        }

        public int _itemEKit;
        public int ItemEKit
        {
            get { return _itemEKit; }
            set
            {
                SetProperty(ref _itemEKit, value);
            }
        }

        public int _excluir;
        public int Excluir
        {
            get { return _excluir; }
            set
            {
                SetProperty(ref _excluir, value);
            }
        }

        public string _pedidoMae;
        public string PedidoMae
        {
            get { return _pedidoMae; }
            set
            {
                SetProperty(ref _pedidoMae, value);
            }
        }

        public decimal QtdSaldo
        {
            get { return _qtdSaldo; }
            set
            {
                SetProperty(ref _qtdSaldo, value);
            }
        }

        public string CodItemProntaEntrega
        {
            get { return _codItemProntaEntrega; }
            set
            {
                SetProperty(ref _codItemProntaEntrega, value);
            }
        }

        public string DescricaoReduzida
        {
            get { return _descricaoReduzida; }
            set
            {
                SetProperty(ref _descricaoReduzida, value);
            }
        }

        public string CodCaixa
        {
            get { return _codCaixa; }
            set
            {
                SetProperty(ref _codCaixa, value);
            }
        }

        public DateTime DataLimite
        {
            get => _dataLimite;
            set
            {
                SetProperty(ref _dataLimite, value);
            }
        }

        public DateTime DataEntrega
        {
            get => _dataEntrega;
            set
            {
                SetProperty(ref _dataEntrega, value);
            }
        }

        public bool ItemEmEdicao
        {
            get { return _itemEmEdicao; }
            set
            {
                SetProperty(ref _itemEmEdicao, value);
            }
        }

        public bool ItemBloqueado
        {
            get { return _itemBloqueado; }
            set
            {
                SetProperty(ref _itemBloqueado, value);
            }
        }

        public decimal PercDesc
        {
            get { return _percDesc; }
            set
            {
                SetProperty(ref _percDesc, value);
            }
        }

        public decimal PercDesc1
        {
            get { return _percDesc1; }
            set
            {
                SetProperty(ref _percDesc1, value);
            }
        }

        public string CodAtributo
        {
            get { return _codAtributo; }
            set
            {
                SetProperty(ref _codAtributo, value);
            }
        }

        public string CodDerivacao
        {
            get { return _codDerivacao; }
            set
            {
                SetProperty(ref _codDerivacao, value);
            }
        }

        public bool ItemChecado
        {
            get { return _itemChecado; }
            set
            {
                SetProperty(ref _itemChecado, value);
            }
        }

        public decimal PercDesc2
        {
            get { return _percDesc2; }
            set
            {
                SetProperty(ref _percDesc2, value);
            }
        }

        public int ItemChecadoInt
        {
            get { return _itemChecadoInt; }
            set
            {
                ItemChecado = value == 1 ? true : false;
                SetProperty(ref _itemChecadoInt, value);
            }
        }

        public int UsaDescontoItem
        {
            get { return _usaDescontoItem; }
            set
            {
                SetProperty(ref _usaDescontoItem, value);
            }
        }

        public int IndPPK
        {
            get { return _indPPK; }
            set
            {
                SetProperty(ref _indPPK, value);
            }
        }

        public string CodCategoria
        {
            get { return _codCategoria; }
            set { SetProperty(ref _codCategoria, value); }
        }

        public string CodBarra
        {
            get { return _codBarra; }
            set { SetProperty(ref _codBarra, value); }
        }

        public string CodKit
        {
            get { return _codKit; }
            set { SetProperty(ref _codKit, value); }
        }

        public string CodColecao
        {
            get { return _codColecao; }
            set { SetProperty(ref _codColecao, value); }
        }

        public string CodDeposito
        {
            get { return _codDeposito; }
            set { SetProperty(ref _codDeposito, value); }
        }

        public DateTime? DataEstoque
        {
            get { return _dataEstoque; }
            set { SetProperty(ref _dataEstoque, value); }
        }

        public DateTime? Entrega
        {
            get { return _entrega; }
            set { SetProperty(ref _entrega, value); }
        }

        public string CodGrade
        {
            get { return _codGrade; }
            set { SetProperty(ref _codGrade, value); }
        }

        public string CodTabelaPreco
        {
            get { return _codTabelaPreco; }
            set { SetProperty(ref _codTabelaPreco, value); }
        }

        public string CodCarrinho
        {
            get { return _codCarrinho; }
            set { SetProperty(ref _codCarrinho, value); }
        }

        public decimal CodItemCarrinho
        {
            get { return _codItemCarrinho; }
            set { SetProperty(ref _codItemCarrinho, value); }
        }

        public string CodItemPedido
        {
            get { return _codItemPedido; }
            set { SetProperty(ref _codItemPedido, value); }
        }

        public string CodMarca
        {
            get { return _codMarca; }
            set { SetProperty(ref _codMarca, value); }
        }

        public string Marca
        {
            get { return _codMarca; }
            set { SetProperty(ref _codMarca, value); }
        }

        public string CodModelo
        {
            get { return _codModelo; }
            set { SetProperty(ref _codModelo, value); }
        }

        public string CodProduto
        {
            get { return _codProduto; }
            set { SetProperty(ref _codProduto, value); }
        }

        public string CodProdutoUI
        {
            get { return _codProdutoUI; }
            set { SetProperty(ref _codProdutoUI, value); }
        }


        public string CodProdutoFoto
        {
            get { return _codProdutoFoto; }
            set { SetProperty(ref _codProdutoFoto, value); }
        }

        public string Referencia
        {
            get { return _referencia; }
            set { SetProperty(ref _referencia, value); }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { SetProperty(ref _descricao, value); }
        }

        public string DescricaoKit
        {
            get { return _descricaoKit; }
            set { SetProperty(ref _descricaoKit, value); }
        }

        public string DescricaoCompleta
        {
            get { return _descricaoCompleta; }
            set { SetProperty(ref _descricaoCompleta, value); }
        }

        public string DescricaoModelo
        {
            get { return _descricaoModelo; }
            set { SetProperty(ref _descricaoModelo, value); }
        }

        public string Imagem
        {
            get { return _imagem; }
            set { SetProperty(ref _imagem, value); }
        }
        public string NotaFiscal
        {
            get { return _notaFiscal; }
            set { SetProperty(ref _notaFiscal, value); }
        }

        public string Serie
        {
            get { return _serie; }
            set { SetProperty(ref _serie, value); }
        }

        public string Filial
        {
            get { return _filial; }
            set { SetProperty(ref _filial, value); }
        }

        public decimal QtdTotal
        {
            get { return _qtdTotal; }
            set { SetProperty(ref _qtdTotal, value); }
        }

        public decimal QtdDisponivel
        {
            get { return _qtdDisponivel; }
            set { SetProperty(ref _qtdDisponivel, value); }
        }

        public decimal QtdSolicitada
        {
            get { return _qtdSolicitada; }
            set { SetProperty(ref _qtdSolicitada, value); }
        }

        public decimal QtdEstoque
        {
            get { return _qtdEstoque; }
            set { SetProperty(ref _qtdEstoque, value); }
        }

        public decimal QtdEstoqueTotal
        {
            get { return _qtdEstoqueTotal; }
            set { SetProperty(ref _qtdEstoqueTotal, value); }
        }

        public decimal Qtd
        {
            get { return _qtd; }
            set { SetProperty(ref _qtd, value); }
        }

        public decimal QtdUI
        {
            get { return _qtdUI; }
            set { SetProperty(ref _qtdUI, value); }
        }

        public decimal QtdNaCaixa
        {
            get { return _qtdNaCaixa; }
            set { SetProperty(ref _qtdNaCaixa, value); }
        }

        public decimal QtdCaixa
        {
            get { return _qtdCaixa; }
            set { SetProperty(ref _qtdCaixa, value); }
        }

        public decimal Desconto
        {
            get { return _desconto; }
            set { SetProperty(ref _desconto, value); }
        }

        public decimal PrecoSugestao
        {
            get { return _precoSugestao; }
            set { SetProperty(ref _precoSugestao, value); }
        }

        public decimal PrecoCusto
        {
            get { return _precoCusto; }
            set { SetProperty(ref _precoCusto, value); }
        }

        public decimal PrecoCustoComDesconto
        {
            get { return _precoCustoComDesconto; }
            set { SetProperty(ref _precoCustoComDesconto, value); }
        }

        public decimal ValorUnitario
        {
            get { return _valorUnitario; }
            set { SetProperty(ref _valorUnitario, value); }
        }

        public decimal ValorUnitarioLiquido
        {
            get { return _valorUnitarioLiquido; }
            set { SetProperty(ref _valorUnitarioLiquido, value); }
        }

        public decimal ValorTotal
        {
            get { return _valorTotal; }
            set { SetProperty(ref _valorTotal, value); }
        }

        public decimal ValorTotalLiquido
        {
            get { return _valorTotalLiquido; }
            set { SetProperty(ref _valorTotalLiquido, value); }
        }

        public decimal Markup
        {
            get { return _markup; }
            set { SetProperty(ref _markup, value); }
        }

        public decimal MarkupTabela
        {
            get { return _markupTabela; }
            set { SetProperty(ref _markupTabela, value); }
        }

        public decimal Comissao
        {
            get { return _comissao; }
            set { SetProperty(ref _comissao, value); }
        }

        private string _cor;
        public string Cor
        {
            get { return _cor; }
            set { SetProperty(ref _cor, value); }
        }

        private string _valorUnitarioStr;
        public string ValorUnitarioStr
        {
            get { return _valorUnitarioStr; }
            set { SetProperty(ref _valorUnitarioStr, value); }
        }

        private string _descontoPercentualStr;
        public string DescontoPercentualStr
        {
            get { return _descontoPercentualStr; }
            set { SetProperty(ref _descontoPercentualStr, value); }
        }

        private string _descontoValorStr;
        public string DescontoValorStr
        {
            get { return _descontoValorStr; }
            set { SetProperty(ref _descontoValorStr, value); }
        }

        public string Site
        {
            get { return _site; }
            set { SetProperty(ref _site, value); }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { SetProperty(ref _tipo, value); }
        }

        public string TipoGradeIncluida
        {
            get { return _tipoGradeIncluida; }
            set { SetProperty(ref _tipoGradeIncluida, value); }
        }

        public string CodCor
        {
            get { return _codCor; }
            set
            {
                SetProperty(ref _codCor, value);
            }
        }

        private string _descComposta;
        public string DescComposta
        {
            get
            {
                string chain = "";

                if (!string.IsNullOrEmpty(_codBarra))

                    chain = _codBarra;

                if (!string.IsNullOrEmpty(_descricaoModelo))

                    if (!string.IsNullOrEmpty(chain))
                        chain = chain + " - " + _descricaoModelo;
                    else
                    {
                        chain = _descricaoModelo;

                    }


                if (!string.IsNullOrEmpty(_descricaoCompleta))

                    if (!string.IsNullOrEmpty(chain))
                        chain = chain + " - " + _descricaoCompleta;
                    else
                    {
                        chain = _descricaoCompleta;
                    }
                return chain;
                ;
            }

        }

        public ObservableCollection<DerivacaoGradeResult> Grades
        {
            get { return _grades; }
            set { SetProperty(ref _grades, value); }
        }

        public ItemCommandResult()
        {
            Grades = new ObservableCollection<DerivacaoGradeResult>();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public string DescricaoMaterial
        {
            get { return _descricaoMaterial; }
            set
            {
                SetProperty(ref _descricaoMaterial, value);
            }
        }

        public string DescricaoCor
        {
            get { return _descricaoCor; }
            set
            {
                SetProperty(ref _descricaoCor, value);
            }
        }


        private string _codLocalPadrao;
        public string CodLocalPadrao
        {
            get { return _codLocalPadrao; }
            set { SetProperty(ref _codLocalPadrao, value); }
        }

        private string _descricaoComplemento;
        public string DescricaoComplemento
        {
            get { return _descricaoComplemento; }
            set { SetProperty(ref _descricaoComplemento, value); }
        }

        private string _qtdProdutoCaixa;
        public string QtdProdutoCaixa
        {
            get { return _qtdProdutoCaixa; }
            set { SetProperty(ref _qtdProdutoCaixa, value); }
        }

        public String Calcula_Caixa
        {
            get
            {
                String retorno = String.Empty;

                if (!String.IsNullOrEmpty(QtdProdutoCaixa))
                {
                    Decimal numQuantidade = (this.Grades.Sum(x => x.Qtd)) * this.QtdCaixa;

                    Decimal numTotal = numQuantidade / Convert.ToDecimal(this.QtdProdutoCaixa);

                    if (numTotal > 0)
                    {
                        retorno = Convert.ToString(Math.Round(numTotal, 2));
                    }
                }

                return retorno;
            }
        }

        public String Calcula_Caixa_Inteiro
        {
            get
            {
                String retorno = String.Empty;

                if (!String.IsNullOrEmpty(QtdProdutoCaixa))
                {
                    Decimal numQuantidade = (this.Grades.Sum(x => x.Qtd)) * this.QtdCaixa;

                    Decimal numTotal = numQuantidade / Convert.ToDecimal(this.QtdProdutoCaixa);

                    if (numTotal > 0)
                    {
                        retorno = Convert.ToString(numTotal);
                    }
                }

                return retorno;
            }
        }


        private List<TBT_ITEM_CARRINHO_KIT> _listaProdutoKIT;
        public List<TBT_ITEM_CARRINHO_KIT> ListaProdutoKIT
        {
            get { return _listaProdutoKIT; }
            set { SetProperty(ref _listaProdutoKIT, value); }
        }

        public string CodItemCarrinho_Texto
        {
            get
            {
                String retorno = String.Empty;

                if (CodItemCarrinho > 0)
                {
                    retorno = Convert.ToString(CodItemCarrinho);
                }

                return retorno;
            }
        }

        private Boolean _produtoComposicaoKIT;
        public Boolean ProdutoComposicaoKIT
        {
            get { return _produtoComposicaoKIT; }
            set { SetProperty(ref _produtoComposicaoKIT, value); }
        }

        public Boolean ExibeOpcaoSelecao
        {
            get
            {
                Boolean retorno = true;

                if (ProdutoComposicaoKIT)
                {
                    retorno = false;
                }

                return retorno;
            }
        }

        public string PercDesc_Texto
        {
            get
            {
                String retorno = String.Empty;

                if (!ProdutoComposicaoKIT)
                {
                    if (PercDesc > 0)
                    {
                        retorno = Convert.ToString(PercDesc);

                        if (retorno.Contains(","))
                            retorno = retorno.Replace(",", ".");
                    }
                }

                return retorno;
            }
        }

        private DateTime? _dataDisponibilidade;
        public DateTime? DataDisponibilidade
        {
            get { return _dataDisponibilidade; }
            set
            {
                SetProperty(ref _dataDisponibilidade, value);
            }
        }

        private decimal _produtoDescontoMaximo;
        public decimal ProdutoDescontoMaximo
        {
            get { return _produtoDescontoMaximo; }
            set
            {
                SetProperty(ref _produtoDescontoMaximo, value);
            }
        }

        private decimal _produtoDescontoCarrinho;
        public decimal ProdutoDescontoCarrinho
        {
            get { return _produtoDescontoCarrinho; }
            set
            {
                SetProperty(ref _produtoDescontoCarrinho, value);
            }
        }

        private Int16 _indMaterialPDV;
        public Int16 IndMaterialPDV
        {
            get { return _indMaterialPDV; }
            set { SetProperty(ref _indMaterialPDV, value); }
        }


        private decimal _qtdPendente;
        public decimal QtdPendente
        {
            get { return _qtdPendente; }
            set { SetProperty(ref _qtdPendente, value); }
        }

        private string _descricaoSemCodProduto;
        public string DescricaoSemCodProduto
        {
            get { return _descricaoSemCodProduto; }
            set { SetProperty(ref _descricaoSemCodProduto, value); }
        }

        #region Pack

        private string _qtdProdutoPack;
        public string QtdProdutoPack
        {
            get { return _qtdProdutoPack; }
            set { SetProperty(ref _qtdProdutoPack, value); }
        }

        //public String Calcula_Pack
        //{
        //    get
        //    {
        //        String retorno = String.Empty;

        //        if (!String.IsNullOrEmpty(QtdProdutoPack))
        //        {
        //            Decimal numQuantidade = (this.Grades.Sum(x => x.Qtd)) * this.QtdCaixa;

        //            Decimal numTotal = numQuantidade / Convert.ToDecimal(this.QtdProdutoCaixa);

        //            if (numTotal > 0)
        //            {
        //                retorno = Convert.ToString(Math.Round(numTotal, 2));
        //            }
        //        }

        //        return retorno;
        //    }
        //}

        //public String Calcula_Caixa_Inteiro
        //{
        //    get
        //    {
        //        String retorno = String.Empty;

        //        if (!String.IsNullOrEmpty(QtdProdutoCaixa))
        //        {
        //            Decimal numQuantidade = (this.Grades.Sum(x => x.Qtd)) * this.QtdCaixa;

        //            Decimal numTotal = numQuantidade / Convert.ToDecimal(this.QtdProdutoCaixa);

        //            if (numTotal > 0)
        //            {
        //                retorno = Convert.ToString(numTotal);
        //            }
        //        }

        //        return retorno;
        //    }
        //}


        #endregion Pack

        private Boolean _exibicaoPDF;
        public Boolean ExibicaoPDF
        {
            get { return _exibicaoPDF; }
            set { SetProperty(ref _exibicaoPDF, value); }
        }

    }

}
