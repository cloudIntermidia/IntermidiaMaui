namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class GestaoClienteCommandResult : BaseViewModel
    {
        private string _codPessoaCliente;
        private string _razaoSocial;
        private decimal _valorTotalAtual;
        private decimal _qtdPecasAtual;
        private decimal _qtdParesAtual;
        private decimal _qtdTotalAtual;
        private decimal _valorTotalAnterior;
        private decimal _qtdPecasAnterior;
        private decimal _qtdParesAnterior;
        private decimal _qtdTotalAnterior;
        private decimal _percentual;
        private decimal _bloqueado;
        private decimal _venda1a90dd;
        private decimal _venda91a120dd;
        private decimal _venda121a150dd;
        private decimal _pedidoAberto;
        private decimal _pendencia;
        private string _uf;
        private string _descricao;
        private decimal _midiaAtual;
        private decimal _catalogoAtual;
        private string _email;
        private string _cidade;
        private string _codPessoaRepresentante;
        private string _codSituacaoCliente;
        private decimal _qtdPedido;
        private string _codPessoaPreposto;
        private string _cep;
        private string _endereco;
        private bool _clienteChecado;
        private string _latitude;
        private string _longitude;

        public string CodPessoaCliente { get => _codPessoaCliente; set => SetProperty(ref _codPessoaCliente, value); }
        public string RazaoSocial { get => _razaoSocial; set => SetProperty(ref _razaoSocial, value); }
        public decimal ValorTotalAtual { get => _valorTotalAtual; set => SetProperty(ref _valorTotalAtual, value); }
        public decimal QtdPecasAtual { get => _qtdPecasAtual; set => SetProperty(ref _qtdPecasAtual, value); }
        public decimal QtdParesAtual { get => _qtdParesAtual; set => SetProperty(ref _qtdParesAtual, value); }
        public decimal QtdTotalAtual { get => _qtdTotalAtual; set => SetProperty(ref _qtdTotalAtual, value); }
        public decimal ValorTotalAnterior { get => _valorTotalAnterior; set => SetProperty(ref _valorTotalAnterior, value); }
        public decimal QtdPecasAnterior { get => _qtdPecasAnterior; set => SetProperty(ref _qtdPecasAnterior, value); }
        public decimal QtdParesAnterior { get => _qtdParesAnterior; set => SetProperty(ref _qtdParesAnterior, value); }
        public decimal QtdTotalAnterior { get => _qtdTotalAnterior; set => SetProperty(ref _qtdTotalAnterior, value); }
        public decimal Percentual { get => _percentual; set => SetProperty(ref _percentual, value); }
        public decimal Bloqueado { get => _bloqueado; set => SetProperty(ref _bloqueado, value); }
        public decimal Venda1a90dd { get => _venda1a90dd; set => SetProperty(ref _venda1a90dd, value); }
        public decimal Venda91a120dd { get => _venda91a120dd; set => SetProperty(ref _venda91a120dd, value); }
        public decimal Venda121a150dd { get => _venda121a150dd; set => SetProperty(ref _venda121a150dd, value); }
        public decimal PedidoAberto { get => _pedidoAberto; set => SetProperty(ref _pedidoAberto, value); }
        public decimal Pendencia { get => _pendencia; set => SetProperty(ref _pendencia, value); }
        public string Cep { get => _cep; set => SetProperty(ref _cep, value); }
        public string Endereco { get => _endereco; set => SetProperty(ref _endereco, value); }
        public string Uf { get => _uf; set => SetProperty(ref _uf, value); }
        public string Descricao { get => _descricao; set => SetProperty(ref _descricao, value); }
        public decimal MidiaAtual { get => _midiaAtual; set => SetProperty(ref _midiaAtual, value); }
        public decimal CatalogoAtual { get => _catalogoAtual; set => SetProperty(ref _catalogoAtual, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string Cidade { get => _cidade; set => SetProperty(ref _cidade, value); }
        public string CodPessoaRepresentante { get => _codPessoaRepresentante; set => SetProperty(ref _codPessoaRepresentante, value); }
        public string CodSituacaoCliente { get => _codSituacaoCliente; set => SetProperty(ref _codSituacaoCliente, value); }
        public decimal QtdPedido { get => _qtdPedido; set => SetProperty(ref _qtdPedido, value); }
        public string CodPessoaPreposto { get => _codPessoaPreposto; set => SetProperty(ref _codPessoaPreposto, value); }
        public bool ClienteChecado { get => _clienteChecado; set => SetProperty(ref _clienteChecado, value); }
        public string Latitude { get => _latitude; set => SetProperty(ref _latitude, value); }
        public string Longitude { get => _longitude; set => SetProperty(ref _longitude, value); }
    }

}
