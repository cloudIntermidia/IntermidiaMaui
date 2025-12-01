namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class EnderecoCommandResult : BaseViewModel, ICloneable
    {
        public EnderecoCommandResult()
        {

        }

        public EnderecoCommandResult(decimal indPrincipal, decimal indCobranca)
        {
            IndPrincipal = indPrincipal;
            IndCobranca = indCobranca;
        }

        private string _endereco;
        private string _numero;
        private string _complemento;
        private string _cidade;
        private string _uf;
        private string _bairro;
        private string _cep;
        private string _telefone;
        private string _fax;
        private string _codPessoaCliente;
        private decimal _seqEndereco;
        private decimal _indPrincipal;
        private decimal _indCobranca;
        private decimal _indComercial;
        private decimal _indResidencial;
        private decimal _indEntrega;
        private decimal _indContato;

        public string Endereco { get => _endereco; set => SetProperty(ref _endereco, value); }
        public string Numero { get => _numero; set => SetProperty(ref _numero, value); }
        public string Complemento { get => _complemento; set => SetProperty(ref _complemento, value); }
        public string Cidade { get => _cidade; set => SetProperty(ref _cidade, value); }
        public string UF { get => _uf; set => SetProperty(ref _uf, value); }
        public string Bairro { get => _bairro; set => SetProperty(ref _bairro, value); }
        public string CEP { get => _cep; set => SetProperty(ref _cep, value); }
        public string Telefone { get => _telefone; set => SetProperty(ref _telefone, value); }
        public string Fax { get => _fax; set => SetProperty(ref _fax, value); }
        public string CodPessoaCliente { get => _codPessoaCliente; set => SetProperty(ref _codPessoaCliente, value); }
        public decimal SeqEndereco { get => _seqEndereco; set => SetProperty(ref _seqEndereco, value); }
        public decimal IndPrincipal { get => _indPrincipal; set => SetProperty(ref _indPrincipal, value); }
        public decimal IndCobranca { get => _indCobranca; set => SetProperty(ref _indCobranca, value); }
        public decimal IndComercial { get => _indComercial; private set => SetProperty(ref _indComercial, value); }
        public decimal IndResidencial { get => _indResidencial; private set => SetProperty(ref _indResidencial, value); }
        public decimal IndEntrega { get => _indEntrega; private set => SetProperty(ref _indEntrega, value); }
        public decimal IndContato { get => _indContato; private set => SetProperty(ref _indContato, value); }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

}
