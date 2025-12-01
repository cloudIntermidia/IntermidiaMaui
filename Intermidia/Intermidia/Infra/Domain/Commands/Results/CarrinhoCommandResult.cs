using System.Collections.ObjectModel;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public partial class CarrinhoCommandResult : BaseViewModel, ICommandResult
    {
        private string _codCarrinho;
        private string _codPedido;
        private string _origem;
        private string _ordemCompra;
        private string _codPessoaCliente;
        private string _codPessoaRepresentante;
        private string _codPessoaPreposto;
        private string _codCondicaoPagamento;
        private string _condicaoPagamento;
        private string _codTipoPedido;
        private string _tipoPedido;
        private string _tipoVenda;
        private string _codSituacaoPedido;
        private string _codAtendimento;
        private decimal _codUsuario;
        private decimal _codInstalacao;
        private string _codMarca;
        private string _marca;
        private string _email;
        private string _representante;
        private string _preposto;
        private string _codColecao;
        private string _observacoes;
        private string _observacoes1;
        private string _cifFob;
        private string _nomeFantasia;
        private string _razaoSocial;
        private string _situacao;
        private string _cNPJ;
        private string _grupoCliente;
        private DateTime _dataEmissao;
        private DateTime? _dataEntrega;
        private DateTime? _dataFaturamento;
        private decimal _qtdTotal;
        private decimal _valorTotal;
        private decimal _valorTotalLiquido;
        private decimal _indBloqueado;
        private string _percentualDescontoStrg;
        private string _percentualDesconto;
        private decimal? _percentualDesconto1;
        private decimal? _percentualDesconto2;
        private decimal? _percentualDesconto3;
        private decimal? _percentualDesconto4;
        private decimal? _percentualDesconto5;
        private decimal? _percentualComissaoRep;
        private decimal? _aceitaFaturamentoParcial;
        private decimal? _aceitaFaturamentoAntecipado;
        private string _operacaoFiscal;
        private string _pedidoCliente;
        private string _pedidoBonificado;
        private string _periodoPcp;
        private string _semanaFaturamento;
        private string _numeroEntrega;
        private DateTime? _entrega;
        private DateTime? _limite;
        private DateTime? _dataParaTransferencia;
        private EnderecoCommandResult _endereco;
        private ObservableCollection<ItemCommandResult> _itens;
        private string _codEvento;
        private string _codTabelaPreco;
        private string _tabelaPreco;
        private string _codPoliticaComercial;
        private string _politicaComercial;
        private string _codMetodoPagamento;
        private string _metodoPagamento;
        private string _codDeposito;
        private decimal _prazoMedio;
        private decimal _prazoAdicional;
        private string _unidadeFaturamento;
        private string _cupomChave;
        private int _indPrivateLabel;
        public int IndPrivateLabel
        {
            get { return _indPrivateLabel; }
            set { SetProperty(ref _indPrivateLabel, value); }
        }

        private int _indPedidoMae;
        public int IndPedidoMae
        {
            get { return _indPedidoMae; }
            set { SetProperty(ref _indPedidoMae, value); }
        }

        private string _pedidoMae;
        public string PedidoMae
        {
            get { return _pedidoMae; }
            set { SetProperty(ref _pedidoMae, value); }
        }


        private List<ItemCommandResult> resumoOrder;
        public List<ItemCommandResult> ResumoOrder { get => resumoOrder; set => SetProperty(ref resumoOrder, value); }
        public string CupomChave { get => _cupomChave; set => SetProperty(ref _cupomChave, value); }

        private int _idServidor;
        public int IdServidor
        {
            get { return _idServidor; }
            set { SetProperty(ref _idServidor, value); }
        }

        private string _notaFiscal;
        public string NotaFiscal
        {
            get { return _notaFiscal; }
            set { SetProperty(ref _notaFiscal, value); }
        }

        private string _codPedidoRepresentante;
        public string CodPedidoRepresentante
        {
            get { return _codPedidoRepresentante; }
            set { SetProperty(ref _codPedidoRepresentante, value); }
        }

        private bool _carrinhoChecado;
        public bool CarrinhoChecado
        {
            get { return _carrinhoChecado; }
            set { SetProperty(ref _carrinhoChecado, value); }
        }

        public string Origem
        {
            get { return _origem; }
            set { SetProperty(ref _origem, value); }
        }

        public decimal IndBloqueado
        {
            get { return _indBloqueado; }
            set { SetProperty(ref _indBloqueado, value); }
        }

        public decimal? PercentualComissaoRep
        {
            get { return _percentualComissaoRep; }
            set { SetProperty(ref _percentualComissaoRep, value); }
        }

        public string PercentualDesconto
        {
            get { return _percentualDesconto; }
            set { SetProperty(ref _percentualDesconto, value); }
        }

        public decimal? AceitaFaturamentoParcial
        {
            get { return _aceitaFaturamentoParcial; }
            set { SetProperty(ref _aceitaFaturamentoParcial, value); }
        }

        public decimal? AceitaFaturamentoAntecipado
        {
            get { return _aceitaFaturamentoAntecipado; }
            set { SetProperty(ref _aceitaFaturamentoAntecipado, value); }
        }

        public decimal? PercentualDesconto1
        {
            get { return _percentualDesconto1; }
            set { SetProperty(ref _percentualDesconto1, value); }
        }

        public decimal? PercentualDesconto2
        {
            get { return _percentualDesconto2; }
            set { SetProperty(ref _percentualDesconto2, value); }
        }

        public decimal? PercentualDesconto3
        {
            get { return _percentualDesconto3; }
            set { SetProperty(ref _percentualDesconto3, value); }
        }

        public decimal? PercentualDesconto4
        {
            get { return _percentualDesconto4; }
            set { SetProperty(ref _percentualDesconto4, value); }
        }

        public decimal? PercentualDesconto5
        {
            get { return _percentualDesconto5; }
            set { SetProperty(ref _percentualDesconto5, value); }
        }

        public string OperacaoFiscal
        {
            get { return _operacaoFiscal; }
            set { SetProperty(ref _operacaoFiscal, value); }
        }

        public string PedidoCliente
        {
            get { return _pedidoCliente; }
            set { SetProperty(ref _pedidoCliente, value); }
        }

        public string CodCarrinho
        {
            get { return _codCarrinho; }
            set { SetProperty(ref _codCarrinho, value); }
        }

        public string OrdemCompra
        {
            get { return _ordemCompra; }
            set { SetProperty(ref _ordemCompra, value); }
        }

        public string CodPessoaCliente
        {
            get { return _codPessoaCliente; }
            set { SetProperty(ref _codPessoaCliente, value); }
        }

        public string CodPessoaPreposto
        {
            get { return _codPessoaPreposto; }
            set { SetProperty(ref _codPessoaPreposto, value); }
        }

        public string CodPessoaRepresentante
        {
            get { return _codPessoaRepresentante; }
            set { SetProperty(ref _codPessoaRepresentante, value); }
        }

        public string CodCondicaoPagamento
        {
            get { return _codCondicaoPagamento; }
            set { SetProperty(ref _codCondicaoPagamento, value); }
        }

        public string CondicaoPagamento
        {
            get { return _condicaoPagamento; }
            set { SetProperty(ref _condicaoPagamento, value); }
        }

        public string CodMetodoPagamento
        {
            get { return _codMetodoPagamento; }
            set { SetProperty(ref _codMetodoPagamento, value); }
        }

        public string MetodoPagamento
        {
            get { return _metodoPagamento; }
            set { SetProperty(ref _metodoPagamento, value); }
        }

        public string TipoVenda
        {
            get { return _tipoVenda; }
            set { SetProperty(ref _tipoVenda, value); }
        }

        public string TipoPedido
        {
            get { return _tipoPedido; }
            set { SetProperty(ref _tipoPedido, value); }
        }

        public string CodTipoPedido
        {
            get { return _codTipoPedido; }
            set { SetProperty(ref _codTipoPedido, value); }
        }

        public string CodSituacaoPedido
        {
            get { return _codSituacaoPedido; }
            set { SetProperty(ref _codSituacaoPedido, value); }
        }

        public string CodAtendimento
        {
            get { return _codAtendimento; }
            set { SetProperty(ref _codAtendimento, value); }
        }

        public decimal CodUsuario
        {
            get { return _codUsuario; }
            set { SetProperty(ref _codUsuario, value); }
        }

        public decimal CodInstalacao
        {
            get { return _codInstalacao; }
            set { SetProperty(ref _codInstalacao, value); }
        }

        public string CodMarca
        {
            get { return _codMarca; }
            set { SetProperty(ref _codMarca, value); }
        }

        public string Marca
        {
            get { return _marca; }
            set { SetProperty(ref _marca, value); }
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Representante
        {
            get { return _representante; }
            set { SetProperty(ref _representante, value); }
        }

        public string Preposto
        {
            get { return _preposto; }
            set { SetProperty(ref _preposto, value); }
        }

        public string CodColecao
        {
            get { return _codColecao; }
            set { SetProperty(ref _codColecao, value); }
        }

        public string Observacoes
        {
            get { return _observacoes; }
            set { SetProperty(ref _observacoes, value); }
        }

        public string Observacoes1
        {
            get { return _observacoes1; }
            set { SetProperty(ref _observacoes1, value); }
        }

        public string CifFob
        {
            get { return _cifFob; }
            set { SetProperty(ref _cifFob, value); }
        }

        public string NomeFantasia
        {
            get { return _nomeFantasia; }
            set { SetProperty(ref _nomeFantasia, value); }
        }

        public string RazaoSocial
        {
            get { return _razaoSocial; }
            set { SetProperty(ref _razaoSocial, value); }
        }

        public string Situacao
        {
            get { return _situacao; }
            set { SetProperty(ref _situacao, value); }
        }

        public string CNPJ
        {
            get { return _cNPJ; }
            set { SetProperty(ref _cNPJ, value); }
        }

        public string PercentualDescontoStrg
        {
            get { return _percentualDescontoStrg; }
            set { SetProperty(ref _percentualDescontoStrg, value); }
        }

        public string GrupoCliente
        {
            get { return _grupoCliente; }
            set { SetProperty(ref _grupoCliente, value); }
        }

        public DateTime DataEmissao
        {
            get { return _dataEmissao; }
            set { SetProperty(ref _dataEmissao, value); }
        }

        public DateTime? DataEntrega
        {
            get { return _dataEntrega; }
            set { SetProperty(ref _dataEntrega, value); }
        }

        public DateTime? DataFaturamento
        {
            get { return _dataFaturamento; }
            set { SetProperty(ref _dataFaturamento, value); }
        }

        public decimal QtdTotal
        {
            get { return _qtdTotal; }
            set { SetProperty(ref _qtdTotal, value); }
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

        public ObservableCollection<ItemCommandResult> Itens
        {
            get { return _itens; }
            set { SetProperty(ref _itens, value); }
        }

        public string DescontoUI
        {
            get
            {

                string result = "";
                if (PercentualDesconto != null && PercentualDesconto.IsNotEmpty() && PercentualDesconto != "0")
                {
                    result = PercentualDesconto.Replace("+0.0000", "");
                }
                else
                {
                    if (PercentualDesconto1 > 0)
                    {
                        if (result.Length > 0)
                        {
                            result = result + "+" + PercentualDesconto1.ToString();
                        }
                        else
                        {
                            result = PercentualDesconto1.ToString();
                        }
                    }

                    if (PercentualDesconto2 > 0)
                    {
                        if (result.Length > 0)
                        {
                            result = result + "+" + PercentualDesconto2.ToString();
                        }
                        else
                        {
                            result = PercentualDesconto2.ToString();
                        }
                    }

                    if (PercentualDesconto3 > 0)
                    {
                        if (result.Length > 0)
                        {
                            result = result + "+" + PercentualDesconto3.ToString();
                        }
                        else
                        {
                            result = PercentualDesconto3.ToString();
                        }
                    }

                    if (PercentualDesconto4 > 0)
                    {
                        if (result.Length > 0)
                        {
                            result = result + "+" + PercentualDesconto4.ToString();
                        }
                        else
                        {
                            result = PercentualDesconto4.ToString();
                        }
                    }

                    if (PercentualDesconto5 > 0)
                    {
                        if (result.Length > 0)
                        {
                            result = result + "+" + PercentualDesconto5.ToString();
                        }
                        else
                        {
                            result = PercentualDesconto5.ToString();
                        }
                    }
                }
                return result;
            }
        }

        public string CodDeposito
        {
            get { return _codDeposito; }
            set { SetProperty(ref _codDeposito, value); }
        }
        public EnderecoCommandResult Endereco { get => _endereco; set => SetProperty(ref _endereco, value); }
        public string CodPedido { get => _codPedido; set => SetProperty(ref _codPedido, value); }
        public string PedidoBonificado { get => _pedidoBonificado; set => SetProperty(ref _pedidoBonificado, value); }
        public string PeriodoPcp { get => _periodoPcp; set => SetProperty(ref _periodoPcp, value); }
        public string NumeroEntrega { get => _numeroEntrega; set => SetProperty(ref _numeroEntrega, value); }
        public DateTime? Entrega { get => _entrega; set => SetProperty(ref _entrega, value); }
        public DateTime? Limite { get => _limite; set => SetProperty(ref _limite, value); }
        public DateTime? DataParaTransferencia { get => _dataParaTransferencia; set => SetProperty(ref _dataParaTransferencia, value); }
        public string SemanaFaturamento { get => _semanaFaturamento; set => SetProperty(ref _semanaFaturamento, value); }
        public string CodEvento { get => _codEvento; set => SetProperty(ref _codEvento, value); }
        public string CodTabelaPreco { get => _codTabelaPreco; set => SetProperty(ref _codTabelaPreco, value); }
        public string TabelaPreco { get => _tabelaPreco; set => SetProperty(ref _tabelaPreco, value); }
        public string CodPoliticaComercial { get => _codPoliticaComercial; set => SetProperty(ref _codPoliticaComercial, value); }
        public string PoliticaComercial { get => _politicaComercial; set => SetProperty(ref _politicaComercial, value); }
        public decimal PrazoMedio { get => _prazoMedio; set => SetProperty(ref _prazoMedio, value); }
        public decimal PrazoAdicional { get => _prazoAdicional; set => SetProperty(ref _prazoAdicional, value); }
        public string UnidadeFaturamento { get => _unidadeFaturamento; set => SetProperty(ref _unidadeFaturamento, value); }

        private string _codSuframa;
        public string CodSuframa
        {
            get { return _codSuframa; }
            set { SetProperty(ref _codSuframa, value); }
        }

        public string CodCarrinho_CodSuframa
        {
            get
            {
                String retorno = CodCarrinho;

                if (!String.IsNullOrEmpty(CodSuframa))
                {
                    retorno = String.Concat(retorno, "#", CodSuframa);
                }

                return retorno;
            }
        }

        public CarrinhoCommandResult()
        {
            Itens = new ObservableCollection<ItemCommandResult>();
            ResumoOrder = new List<ItemCommandResult>();
        }

        private string _inscricaoEstadual;
        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set { SetProperty(ref _inscricaoEstadual, value); }
        }

        private string _transportadora;
        public string Transportadora
        {
            get { return _transportadora; }
            set { SetProperty(ref _transportadora, value); }
        }

        #region Endereco

        private string _clienteLogradouro;
        public string ClienteLogradouro
        {
            get { return _clienteLogradouro; }
            set { SetProperty(ref _clienteLogradouro, value); }
        }

        private string _clienteNumero;
        public string ClienteNumero
        {
            get { return _clienteNumero; }
            set { SetProperty(ref _clienteNumero, value); }
        }

        private string _clienteComplemento;
        public string ClienteComplemento
        {
            get { return _clienteComplemento; }
            set { SetProperty(ref _clienteComplemento, value); }
        }

        private string _clienteFax;
        public string ClienteFax
        {
            get { return _clienteFax; }
            set { SetProperty(ref _clienteFax, value); }
        }

        private string _clienteCEP;
        public string ClienteCEP
        {
            get { return _clienteCEP; }
            set { SetProperty(ref _clienteCEP, value); }
        }

        public string ClienteCEP_Formatado
        {
            get
            {
                return new FuncaoGenerica().FormataCEP(ClienteCEP);
            }
        }

        private string _clienteUF;
        public string ClienteUF
        {
            get { return _clienteUF; }
            set { SetProperty(ref _clienteUF, value); }
        }

        private string _clienteCidade;
        public string ClienteCidade
        {
            get { return _clienteCidade; }
            set { SetProperty(ref _clienteCidade, value); }
        }

        private string _clienteTelefone;
        public string ClienteTelefone
        {
            get { return _clienteTelefone; }
            set { SetProperty(ref _clienteTelefone, value); }
        }

        private string _clienteBairro;
        public string ClienteBairro
        {
            get { return _clienteBairro; }
            set { SetProperty(ref _clienteBairro, value); }
        }

        private string _redePrincipal;
        public string RedePrincipal
        {
            get { return _redePrincipal; }
            set { SetProperty(ref _redePrincipal, value); }
        }

        #endregion Endereco

        private decimal _valorFinalComImposto;
        public decimal ValorFinalComImposto
        {
            get { return _valorFinalComImposto; }
            set { SetProperty(ref _valorFinalComImposto, value); }
        }

        private string _nomeRepresentante;
        public string NomeRepresentante
        {
            get { return _nomeRepresentante; }
            set { SetProperty(ref _nomeRepresentante, value); }
        }

        public Boolean ConsideraProdutoKit { get; set; }

        private ObservableCollection<ItemCommandResult> _listaProduto_Completa;
        public ObservableCollection<ItemCommandResult> ListaProduto_Completa
        {
            get { return _listaProduto_Completa; }
            set { SetProperty(ref _listaProduto_Completa, value); }
        }

        #region CNPJ | CPF

        private String _CPF;
        public String CPF
        {
            //get { return _CPF; }
            get
            {
                String retorno = _CPF;

                if (String.IsNullOrEmpty(retorno))
                {
                    if (!String.IsNullOrEmpty(CNPJ))
                    {
                        retorno = CNPJ;
                    }

                }

                return retorno;
            }
            set { SetProperty(ref _CPF, value); }
        }

        public string CPF_ComMascara
        {
            get
            {
                String retorno = CPF;

                retorno = new FuncaoGenerica().FormataCPF(retorno);

                return retorno;
            }
        }

        public string CNPJ_ComMascara
        {
            get
            {
                String retorno = CNPJ;

                retorno = new FuncaoGenerica().FormataCNPJ(retorno);

                return retorno;
            }
        }

        public Boolean ExibeCNPJ
        {
            get
            {
                Boolean retorno = false;

                String valor = new FuncaoGenerica().Remove_Formatacao_CNPJ_CPF(CNPJ);
                if (!String.IsNullOrEmpty(valor))
                {
                    if (valor.Length == 14)
                        retorno = true;
                }

                return retorno;
            }
        }

        public Boolean ExibeCPF
        {
            get
            {
                Boolean retorno = false;

                String valor = new FuncaoGenerica().Remove_Formatacao_CNPJ_CPF(CNPJ);
                if (!String.IsNullOrEmpty(valor))
                {
                    if (valor.Length == 11)
                        retorno = true;
                }

                return retorno;
            }
        }

        public string CNPJ_CPF_Exibicao
        {
            get
            {
                String retorno = String.Empty;

                if (ExibeCNPJ)
                    retorno = $"CNPJ: {CNPJ_ComMascara}";
                else if (ExibeCPF)
                    retorno = $"CPF: {CPF_ComMascara}";

                return retorno;
            }
        }

        #endregion CNPJ | CPF

        private string _notaFiscal_Pedido;
        public string NotaFiscal_Pedido
        {
            get { return _notaFiscal_Pedido; }
            set { SetProperty(ref _notaFiscal_Pedido, value); }
        }

        private decimal _qtdTotalEntregue;
        public decimal QtdTotalEntregue
        {
            get { return _qtdTotalEntregue; }
            set { SetProperty(ref _qtdTotalEntregue, value); }
        }

        private decimal _qtdTotalEntregar;
        public decimal QtdTotalEntregar
        {
            get { return _qtdTotalEntregar; }
            set { SetProperty(ref _qtdTotalEntregar, value); }
        }

        public Boolean MaterialPDV
        {
            get
            {
                Boolean retorno = false;

                if (this.Itens != null && this.Itens.Count > 0)
                {
                    foreach (ItemCommandResult oED in this.Itens)
                    {
                        if (oED.IndMaterialPDV > 0)
                        {
                            retorno = true;
                            break;
                        }
                    }
                }


                return retorno;
            }
        }
    }

}
