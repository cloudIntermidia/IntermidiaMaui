namespace Intermidia.Pages.Intermidia.Sincronizacao
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