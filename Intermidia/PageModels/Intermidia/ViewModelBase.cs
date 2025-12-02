using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Intermidia.PageModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected IPageDialogService DialogService { get; private set; }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _estaOperando;
        public bool EstaOperando
        {
            get { return _estaOperando; }
            set { SetProperty(ref _estaOperando, value); }
        }

        public string AppName
        {
           // get { return App.Current.Properties["AppName"] as string; }
            get { return ""; }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public ViewModelBase()
        {

        }
        public ViewModelBase(INavigationService navigationService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}