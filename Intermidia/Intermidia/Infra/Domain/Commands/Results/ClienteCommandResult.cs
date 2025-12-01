namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class ClienteCommandResult : BaseViewModel, ICloneable
    {
        public ClienteCommandResult()
        {
            EnderecoPrincipal = new EnderecoCommandResult(1, 0);
            //EnderecoCobranca = new EnderecoCommandResult(0,1);
            DataCadastro = DateTime.Now;
        }

        private string _codPessoaCliente;
        private string _razaoSocial;
        private string _nomeFantasia;
        private string _cNPJ;
        private string _codSituacaoCliente;
        private string _codGrupoCliente;
        private string _codCategoriaCliente;
        private string _contato;
        private string _telefone1;
        private string _telefone2;
        private string _email;
        private string _emailNFE;
        private string _inscricaoEstadual;
        private string _codPessoaAFV;
        private string _codPessoaERP;
        private string _cidade;
        private string _uF;
        private string _ultimoPedido;
        private string _codTabelaPreco;
        private string _latitude;
        private string _longitude;
        private string _observacao;
        private string _representante;
        private string _tipoVenda;
        private string _redeCliente;
        private string _enderecoPrincipalCompleto;
        private int _indBloqueioPedido;
        private int _indBloqueioFaturamento;
        private int _indBloqueioExpedicao;
        private decimal _limiteCredito;
        private DateTime _dataCadastro;
        private EnderecoCommandResult _enderecoPrincipal;
        //private EnderecoCommandResult _enderecoCobranca;
        private UsuarioCommandResult _usuario;
        private DateTime? _datafundacao;
        private string _emailcobranca;
        private string _emailComercial;
        private string _ramoAtividade;
        private string _suframa;
        private string _clasificacaoComercial;
        private string _parecer;
        private string _grupo;
        private string _prazo;
        private string _banco;
        private string _agencia;
        private string _conta;
        private decimal? _percDescFrete;
        private string _limite;
        private string _codCondicaoPagamento;

        private string _segmento;
        private string _codigoSegmento;

        public string Segmento
        {
            get => _segmento;
            set => SetProperty(ref _segmento, value);
        }

        public string CodCondicaoPagamento
        {
            get => _codCondicaoPagamento;
            set => SetProperty(ref _codCondicaoPagamento, value);
        }

        public string CodigoSegmento
        {
            get => _codigoSegmento;
            set => SetProperty(ref _codigoSegmento, value);
        }

        //public string FantasiaRazaoSocial
        //{
        //    get => NomeFantasia.Trim() + " - "+RazaoSocial.Trim() ;
        //}

        public string Limite
        {
            get => _limite;
            set => SetProperty(ref _limite, value);
        }

        public decimal? PercDescFrete
        {
            get => _percDescFrete;
            set => SetProperty(ref _percDescFrete, value);
        }
        private string _perfil;

        public string Perfil
        {
            get => _perfil;
            set => SetProperty(ref _perfil, value);
        }

        public DateTime? Datafundacao
        {
            get => _datafundacao;
            set => SetProperty(ref _datafundacao, value);
        }

        public string Emailcobranca
        {
            get => _emailcobranca;
            set => SetProperty(ref _emailcobranca, value);
        }

        public string EmailComercial
        {
            get => _emailComercial;
            set => SetProperty(ref _emailComercial, value);
        }

        public string RamoAtividade
        {
            get => _ramoAtividade;
            set => SetProperty(ref _ramoAtividade, value);
        }

        public string Suframa
        {
            get => _suframa;
            set => SetProperty(ref _suframa, value);
        }

        public string ClasificacaoComercial
        {
            get => _clasificacaoComercial;
            set => SetProperty(ref _clasificacaoComercial, value);
        }

        public string Parecer
        {
            get => _parecer;
            set => SetProperty(ref _parecer, value);
        }

        public string Grupo
        {
            get => _grupo;
            set => SetProperty(ref _grupo, value);
        }

        public string Prazo
        {
            get => _prazo;
            set => SetProperty(ref _prazo, value);
        }

        public string Banco
        {
            get => _banco;
            set => SetProperty(ref _banco, value);
        }

        public string Agencia
        {
            get => _agencia;
            set => SetProperty(ref _agencia, value);
        }

        public string Conta
        {
            get => _conta;
            set => SetProperty(ref _conta, value);
        }




        private DateTime DataCadastro { get => _dataCadastro; set => SetProperty(ref _dataCadastro, value); }
        public string CodPessoaCliente { get => _codPessoaCliente; set => SetProperty(ref _codPessoaCliente, value); }
        public string Representante { get => _representante; set => SetProperty(ref _representante, value); }
        public string RazaoSocial { get => _razaoSocial; set => SetProperty(ref _razaoSocial, value); }
        public string NomeFantasia { get => _nomeFantasia; set => SetProperty(ref _nomeFantasia, value); }
        public string CNPJ { get => _cNPJ; set => SetProperty(ref _cNPJ, value); }
        public string CodSituacaoCliente { get => _codSituacaoCliente; set => SetProperty(ref _codSituacaoCliente, value); }
        public string CodGrupoCliente { get => _codGrupoCliente; set => SetProperty(ref _codGrupoCliente, value); }
        public string CodCategoriaCliente { get => _codCategoriaCliente; set => SetProperty(ref _codCategoriaCliente, value); }
        public string Contato { get => _contato; set => SetProperty(ref _contato, value); }
        public string Telefone1 { get => _telefone1; set => SetProperty(ref _telefone1, value); }
        public string Telefone2 { get => _telefone2; set => SetProperty(ref _telefone2, value); }
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        public string EmailNFE { get => _emailNFE; set => SetProperty(ref _emailNFE, value); }
        public string InscricaoEstadual { get => _inscricaoEstadual; set => SetProperty(ref _inscricaoEstadual, value); }
        public string CodPessoaAFV { get => _codPessoaAFV; set => SetProperty(ref _codPessoaAFV, value); }
        public string CodPessoaERP { get => _codPessoaERP; set => SetProperty(ref _codPessoaERP, value); }
        public string Cidade { get => _cidade; set => SetProperty(ref _cidade, value); }
        public string UF { get => _uF; set => SetProperty(ref _uF, value); }
        public string UltimoPedido { get => _ultimoPedido; set => SetProperty(ref _ultimoPedido, value); }
        public string CodTabelaPreco { get => _codTabelaPreco; set => SetProperty(ref _codTabelaPreco, value); }
        public string Latitude { get => _latitude; set => SetProperty(ref _latitude, value); }
        public string Longitude { get => _longitude; set => SetProperty(ref _longitude, value); }
        public string Observacao { get => _observacao; set => SetProperty(ref _observacao, value); }
        public string TipoVenda { get => _tipoVenda; set => SetProperty(ref _tipoVenda, value); }
        public string EnderecoPrincipalCompleto { get => _enderecoPrincipalCompleto; set => SetProperty(ref _enderecoPrincipalCompleto, value); }
        public int IndBloqueioPedido { get => _indBloqueioPedido; set => SetProperty(ref _indBloqueioPedido, value); }
        public int IndBloqueioFaturamento { get => _indBloqueioFaturamento; set => SetProperty(ref _indBloqueioFaturamento, value); }
        public int IndBloqueioExpedicao { get => _indBloqueioExpedicao; set => SetProperty(ref _indBloqueioExpedicao, value); }
        public decimal LimiteCredito { get => _limiteCredito; set => SetProperty(ref _limiteCredito, value); }
        public EnderecoCommandResult EnderecoPrincipal { get => _enderecoPrincipal; set => SetProperty(ref _enderecoPrincipal, value); }
        //public EnderecoCommandResult EnderecoCobranca { get => _enderecoCobranca; set => SetProperty(ref _enderecoCobranca, value); }

        public UsuarioCommandResult Usuario { get => _usuario; set => SetProperty(ref _usuario, value); }
        public string Apelido { get { return $"{RazaoSocial} - {CNPJ}"; } }
        public string Localidade { get { return $"{Cidade} - {UF}"; } }

        public string RedeCliente { get => _redeCliente; set => SetProperty(ref _redeCliente, value); }

        public ICommand InfoCommand { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
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

        public string ClienteLogradouro_Numero_Complemento
        {
            get
            {
                String endereco = String.Empty;

                if (!String.IsNullOrEmpty(this.ClienteLogradouro))
                    endereco = this.ClienteLogradouro.Trim();

                if (!String.IsNullOrEmpty(this.ClienteNumero))
                    endereco = String.Concat(endereco, " - Nº: ", this.ClienteNumero.Trim());

                if (!String.IsNullOrEmpty(this.ClienteComplemento))
                    endereco = String.Concat(endereco, " - Complemento: ", this.ClienteComplemento.Trim());

                return endereco;
            }
        }

        #endregion Endereco


        private Boolean _controleCNPJ;
        public Boolean ControleCNPJ
        {
            get { return _controleCNPJ; }
            set { SetProperty(ref _controleCNPJ, value); }
        }

        private Boolean _controleCPF;
        public Boolean ControleCPF
        {
            get { return _controleCPF; }
            set { SetProperty(ref _controleCPF, value); }
        }

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


        //public string CNPJ_CPF
        //{
        //    get
        //    {
        //        String retorno = CNPJ;

        //        if (!String.IsNullOrEmpty(CPF))
        //            retorno = CPF;

        //        return retorno;
        //    }
        //}

        //public Boolean VerificaPessoaFisica
        //{
        //    get {
        //        Boolean retorno = false;

        //        String valor = new FuncaoGenerica().FormataCNPJ(CNPJ);

        //        if (!String.IsNullOrEmpty(valor))
        //        {
        //            if (valor.Length == 11)
        //                retorno = true;
        //        }

        //        return retorno;
        //    }
        //}

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

        public Boolean VerificaPessoaFisica
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

        private Boolean _pessoaFisica;
        public Boolean PessoaFisica
        {
            get { return _pessoaFisica; }
            set { SetProperty(ref _pessoaFisica, value); }
        }

        public List<_ED_Empresa_TabelaPreco> Lista_Empresa_TabelaPreco { get; set; }

        #region Struct

        public struct _ED_Empresa_TabelaPreco
        {
            public String CodEmpresa { get; set; }
            public String CodTabelaPreco { get; set; }
        };

        #endregion Struct

        private string _bloquear;
        public string Bloquear
        {
            get { return _bloquear; }
            set { SetProperty(ref _bloquear, value); }
        }

        public Boolean BloqueadoVendaPrazo
        {
            get
            {
                Boolean retorno = false;

                if (!String.IsNullOrEmpty(this.Bloquear) && this.Bloquear.Equals("S"))
                {
                    retorno = true;
                }

                return retorno;
            }
        }

    }

}
