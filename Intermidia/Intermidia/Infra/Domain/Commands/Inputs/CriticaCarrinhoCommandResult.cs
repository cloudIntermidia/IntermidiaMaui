namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class CriticaCarrinhoCommandResult : BaseViewModel
    {
        private string _codCarrinho;
        public string CodCarrinho
        {
            get { return _codCarrinho; }
            set { SetProperty(ref _codCarrinho, value); }
        }

        private decimal _seqCritica;
        public decimal SeqCritica
        {
            get { return _seqCritica; }
            set { SetProperty(ref _seqCritica, value); }
        }

        private string _critica;
        public string Critica
        {
            get { return _critica; }
            set { SetProperty(ref _critica, value); }
        }

        private string _codItemCarrinho;
        public string CodItemCarrinho
        {
            get { return _codItemCarrinho; }
            set { SetProperty(ref _codItemCarrinho, value); }
        }

        private string _codGradeItemCarrinho;
        public string CodGradeItemCarrinho
        {
            get { return _codGradeItemCarrinho; }
            set { SetProperty(ref _codGradeItemCarrinho, value); }
        }

    }



}
