namespace Intermidia.Pages.Mobilivendas.Sincronizacao
{
    public partial class SincronizacaoPage : ContentPage
    {
        public SincronizacaoPage()
        {
        }

        public SincronizacaoPage(SincronizacaoPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}