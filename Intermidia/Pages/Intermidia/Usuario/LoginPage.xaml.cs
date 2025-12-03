namespace Intermidia.Pages.Intermidia.Usuario
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
        }

        public LoginPage(LoginPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}