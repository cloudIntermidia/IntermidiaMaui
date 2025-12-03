using Intermidia.Intermidia.Infra.Domain;
using Microsoft.Maui.Controls;
using Prism.Navigation;
using Prism.Services;
using static Android.OS.Build;

namespace Intermidia.Pages.Mobilivendas.Shared
{
    public partial class CabecalhoView : ContentView
    {
        private readonly INavigationService _navigation;
        private readonly IPageDialogService _dialog;

        public CabecalhoView()
        {
            InitializeComponent();

            usuario.Text = "Usuário: " + Session.USUARIO_LOGADO?.Nome;
            versao.Text = Session.VERSAO_SISTEMA;

            //_navigation = Application.Current.Properties["NavigationService"] as INavigationService;

            //BindingContext = new CabecalhoViewModel(_navigation, _dialog);

            DesabilitarNavegacao();
        }

        private void DesabilitarNavegacao()
        {
            if (Session.USUARIO_LOGADO?.CodTipoPessoa == "6")
            {
                //stackAtendimento.IsVisible = false;
            }
        }
    }
}
