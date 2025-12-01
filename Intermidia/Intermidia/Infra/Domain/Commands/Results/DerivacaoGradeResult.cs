namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class DerivacaoGradeResult : BaseViewModel
    {
        private string _codDerivacao;
        private string _codGrade;
        private string _codCaixa;
        private string _tipoGrade;
        private decimal _ordem;
        private decimal _codItemCarrinho;
        private decimal _codGradeItemCarrinho;
        private int _qtd;
        private int _qtdMultipla;
        private int _qtdNaGrade;
        private int _qtdTotal;
        private int _qtdEntregue;
        private int _qtdCancelado;
        private int _qtdAEntregar;
        private int _qtdDevolvido;
        private int _tamanhoCaixa;
        private int _estoque;
        private string _qtdString;
        private string _codProduto;
        private string _codItemProntaEntrega;
        // private string _codProdutoSku;
        private string _codBarra;
        private string _codAtributo;
        private bool _isSelected;
        private bool _isBlocked;

        public string CodItemProntaEntrega
        {
            get { return _codItemProntaEntrega; }
            set { SetProperty(ref _codItemProntaEntrega, value); }
        }

        public string CodAtributo
        {
            get { return _codAtributo; }
            set { SetProperty(ref _codAtributo, value); }
        }

        private string _codDeposito;
        public string CodDeposito
        {
            get { return _codDeposito; }
            set { SetProperty(ref _codDeposito, value); }
        }

        public string CodDerivacao
        {
            get { return _codDerivacao; }
            set { SetProperty(ref _codDerivacao, value); }
        }

        public string CodGrade
        {
            get { return _codGrade; }
            set { SetProperty(ref _codGrade, value); }
        }

        public string CodCaixa
        {
            get { return _codCaixa; }
            set { SetProperty(ref _codCaixa, value); }
        }

        public string TipoGrade
        {
            get { return _tipoGrade; }
            set { SetProperty(ref _tipoGrade, value); }
        }

        public decimal Ordem
        {
            get { return _ordem; }
            set { SetProperty(ref _ordem, value); }
        }

        public decimal CodItemCarrinho
        {
            get { return _codItemCarrinho; }
            set { SetProperty(ref _codItemCarrinho, value); }
        }
        public decimal CodGradeItemCarrinho
        {
            get { return _codGradeItemCarrinho; }
            set { SetProperty(ref _codGradeItemCarrinho, value); }
        }

        public int TamanhoCaixa
        {
            get { return _tamanhoCaixa; }
            set
            {
                SetProperty(ref _tamanhoCaixa, value);
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

        public int Qtd
        {
            get { return _qtd; }
            set
            {
                SetProperty(ref _qtd, value);
                OnPropertyChanged("QtdString");
            }
        }

        public int QtdMultipla
        {
            get { return _qtdMultipla; }
            set
            {
                SetProperty(ref _qtdMultipla, value);
            }
        }

        public int QtdNaGrade
        {
            get { return _qtdNaGrade; }
            set
            {
                SetProperty(ref _qtdNaGrade, value);
            }
        }

        public int QtdTotal
        {
            get { return _qtdTotal; }
            set
            {
                SetProperty(ref _qtdTotal, value);
            }
        }

        public int QtdEntregue
        {
            get { return _qtdEntregue; }
            set
            {
                SetProperty(ref _qtdEntregue, value);
            }
        }

        public int QtdCancelado
        {
            get { return _qtdCancelado; }
            set
            {
                SetProperty(ref _qtdCancelado, value);
            }
        }

        public int QtdAEntregar
        {
            get { return _qtdAEntregar; }
            set
            {
                SetProperty(ref _qtdAEntregar, value);
            }
        }

        public int QtdDevolvido
        {
            get { return _qtdDevolvido; }
            set
            {
                SetProperty(ref _qtdDevolvido, value);
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }


        public bool IsBlocked
        {
            get { return _isBlocked; }
            set { SetProperty(ref _isBlocked, value); }
        }

        public string CodProduto
        {
            get { return _codProduto; }
            set { SetProperty(ref _codProduto, value); }
        }

        //public string CodProdutoSku
        //{
        //    get { return _codProdutoSku; }
        // set { SetProperty(ref _codProdutoSku, value); }
        //}

        public string CodBarra
        {
            get { return _codBarra; }
            set { SetProperty(ref _codBarra, value); }
        }

        public string QtdString
        {
            get { return Qtd == 0 ? string.Empty : Qtd.ToString(); }
            set
            {
                int val = 0;
                if (!string.IsNullOrEmpty(value) && int.TryParse(value, out val))
                {
                    Qtd = val;
                    SetProperty(ref _qtdString, value);
                }
                else
                {
                    Qtd = 0;
                    SetProperty(ref _qtdString, "0");
                }
            }
        }

        private int _quantidadeCaixa;
        public int QuantidadeCaixa
        {
            get { return _quantidadeCaixa; }
            set { SetProperty(ref _quantidadeCaixa, value); }
        }

        private int _quantidadePack;
        public int QuantidadePack
        {
            get { return _quantidadePack; }
            set { SetProperty(ref _quantidadePack, value); }
        }

    }

}
