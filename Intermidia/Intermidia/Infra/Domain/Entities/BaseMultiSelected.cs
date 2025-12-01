

//using Flunt.Notifications;

namespace Intermidia.Intermidia.Infra.Domain.Entities
{
    public abstract class BaseMultiSelected : BaseViewModel
    {
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { SetProperty(ref codigo, value); }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set { SetProperty(ref descricao, value); }
        }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
    }

}
