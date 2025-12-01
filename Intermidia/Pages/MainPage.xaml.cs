using Intermidia.Models;
using Intermidia.PageModels;

namespace Intermidia.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(SincronizacaoPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}