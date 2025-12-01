using Intermidia.Models;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class ModeloCommandResult : BaseViewModel, ICommandResult, ICloneable
    {
        public System.Windows.Input.ICommand AdicionarItemCommand { get; set; }

        private TipoExibicao _tipoExibicao;
        public TipoExibicao TipoExibicao
        {
            get { return _tipoExibicao; }
            set
            {
                SetProperty(ref _tipoExibicao, value);
            }
        }
        private string _imprimirCatalogo;
        public string ImprimirCatalogo
        {
            get { return _imprimirCatalogo; }
            set { SetProperty(ref _imprimirCatalogo, value); }
        }

        private string _segmento;
        public string Segmento
        {
            get { return _segmento; }
            set { SetProperty(ref _segmento, value); }
        }

        private string _whereNiveis;
        public string WhereNiveis
        {
            get { return _whereNiveis; }
            set { SetProperty(ref _whereNiveis, value); }
        }

        private string _genero;
        public string Genero
        {
            get { return _genero; }
            set { SetProperty(ref _genero, value); }
        }

        private string _tipoLayoutImpressao;
        public string TipoLayoutImpressao
        {
            get { return _tipoLayoutImpressao; }
            set { SetProperty(ref _tipoLayoutImpressao, value); }
        }

        private bool _exibirCheckSortido;
        public bool ExibirCheckSortido
        {
            get { return _exibirCheckSortido; }
            set { SetProperty(ref _exibirCheckSortido, value); }
        }

        private bool _exibirPreco;
        public bool ExibirPreco
        {
            get { return _exibirPreco; }
            set { SetProperty(ref _exibirPreco, value); }
        }

        private decimal _qtdMultipla;
        public decimal QtdMultipla
        {
            get { return _qtdMultipla; }
            set { SetProperty(ref _qtdMultipla, value); }
        }

        private string _qtdMultiplaEncaixotamento;
        public string QtdMultiplaEncaixotamento
        {
            get { return _qtdMultiplaEncaixotamento; }
            set { SetProperty(ref _qtdMultiplaEncaixotamento, value); }
        }

        private string _tamanhoInicial;
        public string TamanhoInicial
        {
            get { return _tamanhoInicial; }
            set { SetProperty(ref _tamanhoInicial, value); }
        }

        private string _tamanhoFinal;
        public string TamanhoFinal
        {
            get { return _tamanhoFinal; }
            set { SetProperty(ref _tamanhoFinal, value); }
        }

        private string _corTraduzida;
        public string CorTraduzida
        {
            get { return _corTraduzida; }
            set { SetProperty(ref _corTraduzida, value); }
        }

        private string _categoria;
        public string Categoria
        {
            get { return _categoria; }
            set { SetProperty(ref _categoria, value); }
        }

        private int _itemENovaCor;
        public int ItemENovaCor
        {
            get { return _itemENovaCor; }
            set { SetProperty(ref _itemENovaCor, value); }
        }

        private int _itemENovoEstilo;
        public int ItemENovoEstilo
        {
            get { return _itemENovoEstilo; }
            set { SetProperty(ref _itemENovoEstilo, value); }
        }

        private string _codGrade;
        private string _codMarca;
        private string _codProdutoFoto;
        private string _codModeloFoto;
        private string _descricao;
        private string _peso;
        private string _dimensoes;
        private string _marca;
        private string _descricaoCor;
        private string _descricaoProdutoModelo;
        private string _codModelo;
        private string _codProdutoModelo;
        private string _codTipoPedido;
        private string _origem;
        private string _codTabelaPreco;
        private decimal _precoDaTabela;
        private decimal _precoVenda;
        private decimal _precoSugestao;
        private decimal _markup;
        private decimal _percentualDesconto;
        private decimal _markupTabela;
        private string _imagem;
        private string _codAtendimento;
        private decimal _itensEmAtendimento;
        private string _composicao;
        private string _composicaoCabedal;
        private string _composicaoSolado;
        private string _descricaoCompleta;
        private DateTime _dataInicioVendas;
        private DateTime _dataEntrega;
        private bool _itemSortidoChecado;
        private bool _itemChecado;
        private bool _itemNovo;
        private string _codeEAN;
        public int _itemChecadoInt;
        public int _itemEstaNoCarrinho;
        public int _itemEPromocao;
        public int _itemEKit;
        public int _itemEProntaEntrega;
        public int _estoque;
        private string _grades;

        private string _codCaixa;
        private string _codDeposito;
        private string _codigoProduto;
        private string _codigoBarra;

        public string CodCaixa
        {
            get { return _codCaixa; }
            set
            {
                SetProperty(ref _codCaixa, value);
            }
        }

        public string CodDeposito
        {
            get { return _codDeposito; }
            set
            {
                SetProperty(ref _codDeposito, value);
            }
        }

        public bool ItemSortidoChecado
        {
            get { return _itemSortidoChecado; }
            set
            {
                SetProperty(ref _itemSortidoChecado, value);
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


        public bool ItemNovo
        {
            get { return _itemNovo; }
            set
            {
                SetProperty(ref _itemNovo, value);
            }
        }

        public int ItemEstaNoCarrinho
        {
            get { return _itemEstaNoCarrinho; }
            set
            {
                SetProperty(ref _itemEstaNoCarrinho, value);
            }
        }


        public int Estoque
        {
            get { return _estoque; }
            set
            {
                SetProperty(ref _estoque, value);
            }
        }

        private int _itemEImportado;
        public int ItemEImportado
        {
            get { return _itemEImportado; }
            set
            {
                SetProperty(ref _itemEImportado, value);
            }
        }

        public int ItemEPromocao
        {
            get { return _itemEPromocao; }
            set
            {
                SetProperty(ref _itemEPromocao, value);
            }
        }

        public int ItemEKit
        {
            get { return _itemEKit; }
            set
            {
                SetProperty(ref _itemEKit, value);
            }
        }

        public int ItemEProntaEntrega
        {
            get { return _itemEProntaEntrega; }
            set
            {
                SetProperty(ref _itemEProntaEntrega, value);
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

        public DateTime DataEntrega
        {
            get { return _dataEntrega; }
            set
            {
                SetProperty(ref _dataEntrega, value);
            }
        }

        public DateTime DataInicioVendas
        {
            get { return _dataInicioVendas; }
            set
            {
                SetProperty(ref _dataInicioVendas, value);
            }
        }


        public string Grades
        {
            get { return _grades; }
            set
            {
                SetProperty(ref _grades, value);
            }
        }

        public string CodGrade
        {
            get { return _codGrade; }
            set
            {
                SetProperty(ref _codGrade, value);
            }
        }

        public string Origem
        {
            get { return _origem; }
            set
            {
                SetProperty(ref _origem, value);
            }
        }

        public string CodMarca
        {
            get { return _codMarca; }
            set
            {
                SetProperty(ref _codMarca, value);
            }
        }
        public string CodProdutoFoto
        {
            get { return _codProdutoFoto; }
            set
            {
                SetProperty(ref _codProdutoFoto, value);
            }
        }

        public string CodModeloFoto
        {
            get { return _codModeloFoto; }
            set
            {
                SetProperty(ref _codModeloFoto, value);
            }
        }
        public string DescricaoProdutoModelo
        {
            //get { return _descricaoProdutoModelo; }
            get
            {
                String retorno = String.Empty;

                if (!String.IsNullOrEmpty(_descricaoProdutoModelo))
                    retorno = _descricaoProdutoModelo.Trim();

                return retorno;
            }
            set
            {
                SetProperty(ref _descricaoProdutoModelo, value);
            }
        }

        private string _descricaoReduzida;
        public string DescricaoReduzida
        {
            get { return _descricaoReduzida; }
            set
            {
                SetProperty(ref _descricaoReduzida, value);
            }
        }

        private string _descricaoMaterial;
        public string DescricaoMaterial
        {
            get { return _descricaoMaterial; }
            set
            {
                SetProperty(ref _descricaoMaterial, value);
            }
        }

        public string Descricao
        {
            get { return _descricao; }
            set
            {
                SetProperty(ref _descricao, value);
            }
        }

        public string Peso
        {
            get { return _peso; }
            set
            {
                SetProperty(ref _peso, value);
            }
        }

        public string Dimensoes
        {
            get { return _dimensoes; }
            set
            {
                SetProperty(ref _dimensoes, value);
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

        public string Marca
        {
            get { return _marca; }
            set
            {
                SetProperty(ref _marca, value);
            }
        }

        public string DescricaoCompleta
        {
            get { return _descricaoCompleta; }
            set
            {
                SetProperty(ref _descricaoCompleta, value);
            }
        }

        public string CodeEAN
        {
            get { return _codeEAN; }
            set
            {
                SetProperty(ref _codeEAN, value);
            }
        }

        public string CodProdutoModelo
        {
            get { return _codProdutoModelo; }
            set
            {
                SetProperty(ref _codProdutoModelo, value);
            }
        }

        public string CodModelo
        {
            get { return _codModelo; }
            set
            {
                SetProperty(ref _codModelo, value);
            }
        }
        public string CodTipoPedido
        {
            get { return _codTipoPedido; }
            set
            {
                SetProperty(ref _codTipoPedido, value);
            }
        }

        public string CodTabelaPreco
        {
            get { return _codTabelaPreco; }
            set
            {
                SetProperty(ref _codTabelaPreco, value);
            }
        }
        public decimal PrecoSugestao
        {
            get { return _precoSugestao; }
            set
            {
                SetProperty(ref _precoSugestao, value);
            }
        }

        public decimal PrecoDaTabela
        {
            get { return _precoDaTabela; }
            set
            {
                SetProperty(ref _precoDaTabela, value);
            }
        }

        public decimal PrecoVenda
        {
            get
            {
                decimal retorno = _precoVenda;

                if (PrecoVenda_Kit != null && PrecoVenda_Kit > 0)
                {
                    retorno = PrecoVenda_Kit;
                }
                else
                {
                    if (PrecoVenda_ClienteEmpresa != null && PrecoVenda_ClienteEmpresa > 0)
                    {
                        retorno = PrecoVenda_ClienteEmpresa;
                    }
                    else
                    {
                        if (PrecoVenda_ClienteEmpresa_Acrescimo != null && PrecoVenda_ClienteEmpresa_Acrescimo > 0)
                        {
                            retorno += PrecoVenda_ClienteEmpresa_Acrescimo;
                        }
                    }
                }

                return retorno;
            }
            set
            {
                SetProperty(ref _precoVenda, value);
            }
        }

        public Boolean Considera_Semax_Kit
        {
            get
            {
                Boolean retorno = false;

                //SEMAX e KIT
                if (!String.IsNullOrEmpty(this.CodEmpresa) &&
                    this.CodEmpresa.Equals("8") &&
                    this.QtdProdutoComposicaoKit > 0)
                {
                    retorno = true;
                }

                return retorno;
            }
        }


        public String PrecoVenda_ExibicaoCatalogo
        {
            get
            {
                decimal Valor = this.PrecoVenda;
                String texto = String.Empty;

                if (Considera_Semax_Kit)
                {
                    texto = "Valor Unitário = ";
                    //Valor = Valor / this.QtdProdutoComposicaoKit;
                    Valor = this.PrecoVenda_Kit_Diferente;
                    Valor = Math.Round(Convert.ToDecimal(Valor), 2);
                }

                String retorno = String.Concat(texto, new FuncaoGenerica().FormataMoeda(Valor));

                return retorno;
            }
        }


        public decimal PercentualDesconto
        {
            get { return _percentualDesconto; }
            set
            {
                SetProperty(ref _percentualDesconto, value);
            }
        }

        public decimal MarkupTabela
        {
            get { return _markupTabela; }
            set
            {
                SetProperty(ref _markupTabela, value);
            }
        }

        public decimal Markup
        {
            get { return _markup; }
            set
            {
                SetProperty(ref _markup, value);
            }
        }

        public string Imagem
        {
            get { return _imagem; }
            set
            {
                SetProperty(ref _imagem, value);
            }
        }

        private object _imagemStream;

        public object ImagemStream
        {
            get { return _imagemStream; }
            set
            {
                SetProperty(ref _imagemStream, value);
            }
        }


        public string CodAtendimento
        {
            get { return _codAtendimento; }
            set
            {
                SetProperty(ref _codAtendimento, value);
            }
        }
        public decimal ItensEmAtendimento
        {
            get { return _itensEmAtendimento; }
            set
            {
                SetProperty(ref _itensEmAtendimento, value);
            }
        }
        public string Composicao
        {
            get { return _composicao; }
            set
            {
                SetProperty(ref _composicao, value);
            }
        }

        public string ComposicaoCabedal
        {
            get { return _composicaoCabedal; }
            set
            {
                SetProperty(ref _composicaoCabedal, value);
            }
        }

        public string ComposicaoSolado
        {
            get { return _composicaoSolado; }
            set
            {
                SetProperty(ref _composicaoSolado, value);
            }
        }

        public string CodProduto
        {
            get { return _codigoProduto; }
            set { SetProperty(ref _codigoProduto, value); }
        }

        public string CodBarra
        {
            get { return _codigoBarra; }
            set { SetProperty(ref _codigoBarra, value); }
        }

        public ModeloCommandResult()
        {
            ItensEmAtendimento = -1;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        private bool _prontaEntrega;
        public bool ProntaEntrega
        {
            get { return _prontaEntrega; }
            set { SetProperty(ref _prontaEntrega, value); }
        }

        private bool _alertaAmarelo;
        public bool AlertaAmarelo
        {
            get { return _alertaAmarelo; }
            set { SetProperty(ref _alertaAmarelo, value); }
        }


        private int _indProntaEntrega;
        public int IndProntaEntrega
        {
            get { return _indProntaEntrega; }
            set { SetProperty(ref _indProntaEntrega, value); }
        }

        private string _descricaoMedida;
        public string DescricaoMedida
        {
            get { return _descricaoMedida; }
            set { SetProperty(ref _descricaoMedida, value); }
        }

        private string _descricaoEspecTecnica;
        public string DescricaoEspecTecnica
        {
            get { return _descricaoEspecTecnica; }
            set { SetProperty(ref _descricaoEspecTecnica, value); }
        }

        private decimal _precoVenda_ClienteEmpresa;
        public decimal PrecoVenda_ClienteEmpresa
        {
            get { return _precoVenda_ClienteEmpresa; }
            set
            {
                SetProperty(ref _precoVenda_ClienteEmpresa, value);
            }
        }

        private decimal _precoVenda_ClienteEmpresa_Acrescimo;
        public decimal PrecoVenda_ClienteEmpresa_Acrescimo
        {
            get { return _precoVenda_ClienteEmpresa_Acrescimo; }
            set
            {
                SetProperty(ref _precoVenda_ClienteEmpresa_Acrescimo, value);
            }
        }

        private decimal _precoVenda_Kit;
        public decimal PrecoVenda_Kit
        {
            get { return _precoVenda_Kit; }
            set
            {
                SetProperty(ref _precoVenda_Kit, value);
            }
        }

        private decimal _precoVenda_Kit_Diferente;
        public decimal PrecoVenda_Kit_Diferente
        {
            get { return _precoVenda_Kit_Diferente; }
            set
            {
                SetProperty(ref _precoVenda_Kit_Diferente, value);
            }
        }

        public int _estoqueExibicao;
        public int EstoqueExibicao
        {
            get { return _estoqueExibicao; }
            set
            {
                SetProperty(ref _estoqueExibicao, value);
            }
        }


        public int _numQtdCarrinho;
        public int NumQtdCarrinho
        {
            get { return _numQtdCarrinho; }
            set
            {
                SetProperty(ref _numQtdCarrinho, value);
            }
        }

        public string NumQtdCarrinho_Exibicao
        {
            get
            {
                String retorno = String.Empty;

                if (NumQtdCarrinho > 0)
                {
                    retorno = String.Format("({0})", NumQtdCarrinho);
                }

                return retorno;
            }
        }

        #region Controle Foto

        public struct _ED_Foto
        {
            public String Imagem { get; set; }
            public Object ImagemStream { get; set; }
        };

        private List<_ED_Foto> _listaFotoCompleto;
        public List<_ED_Foto> ListaFotoCompleto
        {
            get { return _listaFotoCompleto; }
            set
            {
                SetProperty(ref _listaFotoCompleto, value);
            }
        }

        private String _statusFoto;
        public String StatusFoto
        {
            get { return _statusFoto; }
            set { SetProperty(ref _statusFoto, value); }
        }

        private String _statusFotoZoom;
        public String StatusFotoZoom
        {
            get { return _statusFotoZoom; }
            set { SetProperty(ref _statusFotoZoom, value); }
        }

        public Int32 numFoto_Qtd_Total { get; set; }
        public Int32 numFoto_Corrente { get; set; }

        private bool _exibeBotao_FotoAnterior;
        public bool ExibeBotao_FotoAnterior
        {
            get { return _exibeBotao_FotoAnterior; }
            set { SetProperty(ref _exibeBotao_FotoAnterior, value); }
        }

        private bool _exibeBotao_FotoProxima;
        public bool ExibeBotao_FotoProxima
        {
            get { return _exibeBotao_FotoProxima; }
            set { SetProperty(ref _exibeBotao_FotoProxima, value); }
        }

        private object _fotoVisualizacaoCorrente;
        public object FotoVisualizacaoCorrente
        {
            get { return _fotoVisualizacaoCorrente; }
            set
            {
                SetProperty(ref _fotoVisualizacaoCorrente, value);
            }
        }

        #region Zoom

        private object _fotoZoomVisualizacaoCorrente;
        public object FotoZoomVisualizacaoCorrente
        {
            get { return _fotoZoomVisualizacaoCorrente; }
            set
            {
                SetProperty(ref _fotoZoomVisualizacaoCorrente, value);
            }
        }

        public Int32 numFoto_Zoom_Corrente { get; set; }

        private bool _exibeBotao_FotoZoomAnterior;
        public bool ExibeBotao_FotoZoomAnterior
        {
            get { return _exibeBotao_FotoZoomAnterior; }
            set { SetProperty(ref _exibeBotao_FotoZoomAnterior, value); }
        }

        private bool _exibeBotao_FotoZoomProxima;
        public bool ExibeBotao_FotoZoomProxima
        {
            get { return _exibeBotao_FotoZoomProxima; }
            set { SetProperty(ref _exibeBotao_FotoZoomProxima, value); }
        }

        #endregion Zoom

        #endregion Controle Foto

        private DateTime? _dataDisponibilidade;
        public DateTime? DataDisponibilidade
        {
            get { return _dataDisponibilidade; }
            set
            {
                SetProperty(ref _dataDisponibilidade, value);
            }
        }

        private Boolean _exibeProntaEntrega_Vermelho;
        public Boolean ExibeProntaEntrega_Vermelho
        {
            get { return _exibeProntaEntrega_Vermelho; }
            set { SetProperty(ref _exibeProntaEntrega_Vermelho, value); }
        }

        private Boolean _exibeProntaEntrega_Amarelo;
        public Boolean ExibeProntaEntrega_Amarelo
        {
            get { return _exibeProntaEntrega_Amarelo; }
            set { SetProperty(ref _exibeProntaEntrega_Amarelo, value); }
        }

        private Int16 _alertaEstoque;
        public Int16 AlertaEstoque
        {
            get { return _alertaEstoque; }
            set { SetProperty(ref _alertaEstoque, value); }
        }

        private Int16 _indMaterialPDV;
        public Int16 IndMaterialPDV
        {
            get { return _indMaterialPDV; }
            set { SetProperty(ref _indMaterialPDV, value); }
        }


        private string _codEmpresa;
        public string CodEmpresa
        {
            get { return _codEmpresa; }
            set
            {
                SetProperty(ref _codEmpresa, value);
            }
        }

        private int _qtdProdutoComposicaoKit;
        public int QtdProdutoComposicaoKit
        {
            get { return _qtdProdutoComposicaoKit; }
            set { SetProperty(ref _qtdProdutoComposicaoKit, value); }
        }
    }

    public class ProdutoKit_CommandResult : BaseViewModel, ICommandResult
    {
        public ProdutoKit_CommandResult()
        {
        }

        private int _quantidade;
        public int Quantidade
        {
            get { return _quantidade; }
            set { SetProperty(ref _quantidade, value); }
        }

        private string _codProduto;
        public string CodProduto
        {
            get { return _codProduto; }
            set
            {
                SetProperty(ref _codProduto, value);
            }
        }

        private decimal _preco;
        public decimal Preco
        {
            //get { return _preco; }
            get
            {
                //return _precoVenda;

                decimal retorno = _preco;

                //if (PrecoVenda_ClienteEmpresa != null && PrecoVenda_ClienteEmpresa > 0)
                //{
                //    retorno = PrecoVenda_ClienteEmpresa;
                //}
                if (PrecoVenda_ClienteEmpresa != null && PrecoVenda_ClienteEmpresa > 0)
                {
                    retorno = PrecoVenda_ClienteEmpresa;
                }
                else
                {
                    if (PrecoVenda_ClienteEmpresa_Acrescimo != null && PrecoVenda_ClienteEmpresa_Acrescimo > 0)
                    {
                        retorno += PrecoVenda_ClienteEmpresa_Acrescimo;
                    }
                }

                return retorno;
            }
            set
            {
                SetProperty(ref _preco, value);
            }
        }

        private decimal _precoVenda_ClienteEmpresa;
        public decimal PrecoVenda_ClienteEmpresa
        {
            get { return _precoVenda_ClienteEmpresa; }
            set
            {
                SetProperty(ref _precoVenda_ClienteEmpresa, value);
            }
        }

        private decimal _precoVenda_ClienteEmpresa_Acrescimo;
        public decimal PrecoVenda_ClienteEmpresa_Acrescimo
        {
            get { return _precoVenda_ClienteEmpresa_Acrescimo; }
            set
            {
                SetProperty(ref _precoVenda_ClienteEmpresa_Acrescimo, value);
            }
        }

        public decimal PrecoTotal
        {
            get
            {
                return Preco * Quantidade;
            }
        }
    }

}
