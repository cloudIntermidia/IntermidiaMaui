using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class CarrinhoFechamentoCommandResult : BaseViewModel
    {
        private string _codCarrinho;
        private string _codCondicaoPagamento;
        private string _condicaoPagamento;
        private string _tipoFrete;
        private decimal _percentualDesconto;
        private decimal _percentualDesconto1;
        private decimal _percentualDesconto2;
        private decimal _percentualDesconto3;
        private decimal _percentualComissaoRep;
        private decimal _percentualComissaoRep2;
        private string _percentualDescontoString;
        private string _percentualDesconto1String;
        private string _percentualDesconto2String;
        private string _percentualDesconto3String;
        private string _ordemCompra;
        private string _observacao;
        private string _observacao1;
        private decimal _prazoMedio;
        private decimal _prazoAdicional;
        private string _codTabelaPreco;
        private string _tabelaPreco;
        private string _pedidoBonificado;
        private string _codTransportadora;
        private string _razaoSocial;
        private string _codPessoaCliente;
        private DateTime _dataEntrega;
        private bool _abateComissao;
        private string _codEvento;
        private string _codMetodoPagamento;
        private string _metodoPagamento;
        private string _codTipoPedido;
        private string _tipoPedido;
        private string _codTipoPrazo;
        private string _tipoPrazo;
        private string _email;
        private string _indNecessitaAprovacao;
        private int _indPrivateLabel;
        private bool _isPrivateLabel;

        private string _cupomChave;
        public string CupomChave { get => _cupomChave; set => SetProperty(ref _cupomChave, value); }

        private string _controle;
        private ICondicaoPagamentoRepository condicaoPagamentoRepository1;
        private object condicaoPagamentoRepository2;
        public Task<TabelaPrecoResult> condicaoPagamento;

        public CarrinhoFechamentoCommandResult(ICondicaoPagamentoRepository condicaoPagamentoRepository1, object condicaoPagamentoRepository2)
        {
            this.condicaoPagamentoRepository1 = condicaoPagamentoRepository1;
            this.condicaoPagamentoRepository2 = condicaoPagamentoRepository2;
        }

        public CarrinhoFechamentoCommandResult()
        {
        }

        public string Controle { get => _controle; set => SetProperty(ref _controle, value); }
        public string RazaoSocial { get => _razaoSocial; set => SetProperty(ref _razaoSocial, value); }
        public string CodPessoaCliente { get => _codPessoaCliente; set => SetProperty(ref _codPessoaCliente, value); }
        public string CodCarrinho { get => _codCarrinho; set => SetProperty(ref _codCarrinho, value); }
        public string CodCondicaoPagamento { get => _codCondicaoPagamento; set => SetProperty(ref _codCondicaoPagamento, value); }
        public string CondicaoPagamento { get => _condicaoPagamento; set => SetProperty(ref _condicaoPagamento, value); }
        public string TipoFrete { get => _tipoFrete; set => SetProperty(ref _tipoFrete, value); }
        public string OrdemCompra { get => _ordemCompra; set => SetProperty(ref _ordemCompra, value); }
        public string Observacao { get => _observacao; set => SetProperty(ref _observacao, value); }
        public string Observacao1 { get => _observacao1; set => SetProperty(ref _observacao1, value); }
        public decimal PrazoMedio { get => _prazoMedio; set => SetProperty(ref _prazoMedio, value); }
        public decimal PrazoAdicional { get => _prazoAdicional; set => SetProperty(ref _prazoAdicional, value); }
        public string CodTabelaPreco { get => _codTabelaPreco; set => SetProperty(ref _codTabelaPreco, value); }
        public string TabelaPreco { get => _tabelaPreco; set => SetProperty(ref _tabelaPreco, value); }
        public decimal PercentualDesconto { get => _percentualDesconto; set => SetProperty(ref _percentualDesconto, value); }
        public decimal PercentualDesconto1 { get => _percentualDesconto1; set => SetProperty(ref _percentualDesconto1, value); }
        public decimal PercentualDesconto2 { get => _percentualDesconto2; set => SetProperty(ref _percentualDesconto2, value); }
        public decimal PercentualDesconto3 { get => _percentualDesconto3; set => SetProperty(ref _percentualDesconto3, value); }
        public decimal PercentualComissaoRep { get => _percentualComissaoRep; set => SetProperty(ref _percentualComissaoRep, value); }
        public decimal PercentualComissaoRep2 { get => _percentualComissaoRep2; set => SetProperty(ref _percentualComissaoRep2, value); }
        public string PercentualDescontoString { get => _percentualDescontoString; set => SetProperty(ref _percentualDescontoString, value); }
        public string PercentualDesconto1String { get => _percentualDesconto1String; set => SetProperty(ref _percentualDesconto1String, value); }
        public string PercentualDesconto2String { get => _percentualDesconto2String; set => SetProperty(ref _percentualDesconto2String, value); }
        public string PercentualDesconto3String { get => _percentualDesconto3String; set => SetProperty(ref _percentualDesconto3String, value); }
        public string PedidoBonificado { get => _pedidoBonificado; set => SetProperty(ref _pedidoBonificado, value); }
        public string CodTransportadora { get => _codTransportadora; set => SetProperty(ref _codTransportadora, value); }
        public bool AbateComissao { get => _abateComissao; set => SetProperty(ref _abateComissao, value); }
        public DateTime DataEntrega { get => _dataEntrega; set => SetProperty(ref _dataEntrega, value); }
        public string CodEvento { get => _codEvento; set => SetProperty(ref _codEvento, value); }
        public string CodTipoPedido { get => _codTipoPedido; set => SetProperty(ref _codTipoPedido, value); }
        public string TipoPedido { get => _tipoPedido; set => SetProperty(ref _tipoPedido, value); }
        public string CodTipoPrazo { get => _codTipoPrazo; set => SetProperty(ref _codTipoPrazo, value); }
        public string TipoPrazo { get => _tipoPrazo; set => SetProperty(ref _tipoPrazo, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public int IndPrivateLabel { get => _indPrivateLabel; set => SetProperty(ref _indPrivateLabel, value); }
        public bool IsPrivateLabel { get => _isPrivateLabel; set => SetProperty(ref _isPrivateLabel, value); }

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

        public string IndNecessitaAprovacao
        {
            get { return _indNecessitaAprovacao; }
            set { SetProperty(ref _indNecessitaAprovacao, value); }
        }
    }

}
