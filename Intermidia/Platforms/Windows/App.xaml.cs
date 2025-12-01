// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using Intermidia.Pages.Intermidia.Usuario;

namespace Intermidia.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    }



    //public partial class App : MauiWinUIApplication
    //{
    //    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    //    protected override Window CreateWindow(IActivationState activationState)
    //    {
    //        return new Window(new LoginPage()); // Substitua HomePage pela sua classe de página inicial
    //    }
    //}
}
