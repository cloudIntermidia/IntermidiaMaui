using Acr.UserDialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Intermidia.Intermidia;
using Intermidia.Intermidia.Infra.Domain;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Repositories;
using Intermidia.Models;
using Intermidia.Services.Xamarim;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Prism.Navigation;
using CommonServiceLocator;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Plugin.Connectivity;

namespace Intermidia.PageModels
{
    public partial class LoginPageModel : ObservableObject, IProjectTaskPageModel
    {
        #region "Propriedades"
        private string _login;
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }

        private string _msgResources;
        public string MsgResources
        {
            get { return _msgResources; }
            set { SetProperty(ref _msgResources, value); }
        }


        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set { SetProperty(ref _senha, value); }
        }

        private string versaoSistema;
        public string VersaoSistema
        {
            get { return versaoSistema; }
            set { SetProperty(ref versaoSistema, value); }
        }


        private string ambiente;
        public string Ambiente
        {
            get { return ambiente; }
            set { SetProperty(ref ambiente, value); }
        }


        private bool _multilanguage;
        public bool Multilanguage
        {
            get { return _multilanguage; }
            set { SetProperty(ref _multilanguage, value); }
        }


        private ImageSource _fundoLogin;
        public ImageSource FundoLogin
        {
            get { return _fundoLogin; }
            set { SetProperty(ref _fundoLogin, value); }
        }

        private ImageSource _logoCliente;
        public ImageSource LogoCliente
        {
            get { return _logoCliente; }
            set { SetProperty(ref _logoCliente, value); }
        }


        private List<IdiomaCommandResult> _listaIdiomas;
        public List<IdiomaCommandResult> ListaIdiomas
        {
            get { return _listaIdiomas; }
            set { SetProperty(ref _listaIdiomas, value); }
        }


        private IdiomaCommandResult _idioma;
        public IdiomaCommandResult Idioma
        {
            get { return _idioma; }
            set { SetProperty(ref _idioma, value); }
        }

        #endregion

        #region "Commands"
        public ICommand LoginCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand AmbienteCommand { get; set; }
        #endregion

        #region "Repositorios"
        private IInicializacaoBanco _initializer;
        private IMarcaRepository _marcaRepository;
        private IUsuarioRepository _usuarioRepository;
        private IParametroSincronizacaoRepository _parametroSincronizacaRepository;
        private IParametroRepository _parametroRepository;
        private IDeviceInformation _deviceInformation;
        private IFotoRepository _fotoRepository;
        private IIdiomaRepository _idiomaRepository;
        private ISqliteConnection _iSqliteConnection;
        private readonly ITranslateExtension _translateExtension;

        #endregion


        private bool _isNavigatedTo;
        private bool _dataLoaded;
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ModalErrorHandler _errorHandler;
        private readonly SeedDataService _seedDataService;

        [ObservableProperty]
        private List<CategoryChartData> _todoCategoryData = [];

        [ObservableProperty]
        private List<Brush> _todoCategoryColors = [];

        [ObservableProperty]
        private List<ProjectTask> _tasks = [];

        [ObservableProperty]
        private List<Project> _projects = [];

        [ObservableProperty]
        bool _isBusy;

        [ObservableProperty]
        bool _isRefreshing;

        [ObservableProperty]
        private string _today = DateTime.Now.ToString("dddd, MMM d");

        public bool HasCompletedTasks
            => Tasks?.Any(t => t.IsCompleted) ?? false;

        //public LoginPageModel(SeedDataService seedDataService, ProjectRepository projectRepository,
        //    TaskRepository taskRepository, CategoryRepository categoryRepository, ModalErrorHandler errorHandler)
        //{
        //    _projectRepository = projectRepository;
        //    _taskRepository = taskRepository;
        //    _categoryRepository = categoryRepository;
        //    _errorHandler = errorHandler;
        //    _seedDataService = seedDataService;
        //}

        public LoginPageModel()
        {
        }

        //public LoginPageModel()
        //{
        //}

        //public LoginPageModel(INavigationService navigationService, IPageDialogService dialogService,
        //                         IDeviceInformation deviceInformation, IIdiomaRepository idiomaRepository, ITranslateExtension translateExtension, ISqliteConnection iSqliteConnection
        //                         )
        //   : base(navigationService, dialogService)

        public LoginPageModel(IDeviceInformation deviceInformation, IIdiomaRepository idiomaRepository, ITranslateExtension translateExtension, ISqliteConnection iSqliteConnection)
        {
            _deviceInformation = deviceInformation;
            _idiomaRepository = idiomaRepository;
            _translateExtension = translateExtension;
            LoginCommand = new Command(Autenticar);
            SettingsCommand = new Command(Settings);
            AmbienteCommand = new Command(TrocarAmbiente);
            ListaIdiomas = new List<IdiomaCommandResult>();
            Login = Utils.Settings.Login;
            Session.Idioma = Utils.Settings.Culture;
            Multilanguage = false;
            //Login = "R400112";
            //Senha = "0580341291";

            if (string.IsNullOrEmpty(Utils.Settings.DownloadUrl)
                || string.IsNullOrEmpty(Utils.Settings.UploadUrl)
                )
            {
                Utils.Settings.DownloadUrl = ParametrosSistema.DOWNLOADURL + "_" + Utils.Settings.Ambiente;
                Utils.Settings.UploadUrl = ParametrosSistema.UPLOADURL + "_" + Utils.Settings.Ambiente;
            }

            _iSqliteConnection = iSqliteConnection;

        }


        private async Task LoadData()
        {
            //try
            //{
            //    IsBusy = true;

            //    Projects = await _projectRepository.ListAsync();

            //    var chartData = new List<CategoryChartData>();
            //    var chartColors = new List<Brush>();

            //    var categories = await _categoryRepository.ListAsync();
            //    foreach (var category in categories)
            //    {
            //        chartColors.Add(category.ColorBrush);

            //        var ps = Projects.Where(p => p.CategoryID == category.ID).ToList();
            //        int tasksCount = ps.SelectMany(p => p.Tasks).Count();

            //        chartData.Add(new(category.Title, tasksCount));
            //    }

            //    TodoCategoryData = chartData;
            //    TodoCategoryColors = chartColors;

            //    Tasks = await _taskRepository.ListAsync();
            //}
            //finally
            //{
            //    IsBusy = false;
            //    OnPropertyChanged(nameof(HasCompletedTasks));
            //}
        }

        private async Task InitData(SeedDataService seedDataService)
        {
            bool isSeeded = Preferences.Default.ContainsKey("is_seeded");

            if (!isSeeded)
            {
                await seedDataService.LoadSeedDataAsync();
            }

            Preferences.Default.Set("is_seeded", true);
            await Refresh();
        }


        [RelayCommand]
        private async Task Refresh()
        {
            try
            {
                IsRefreshing = true;
                await LoadData();
            }
            catch (Exception e)
            {
                _errorHandler.HandleError(e);
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private void NavigatedTo() =>
            _isNavigatedTo = true;

        [RelayCommand]
        private void NavigatedFrom() =>
            _isNavigatedTo = false;

        [RelayCommand]
        private async Task Appearing()
        {
            if (!_dataLoaded)
            {
                await InitData(_seedDataService);
                _dataLoaded = true;
                await Refresh();
            }
            // This means we are being navigated to
            else if (!_isNavigatedTo)
            {
                await Refresh();
            }
        }

        [RelayCommand]
        private Task TaskCompleted(ProjectTask task)
        {
            OnPropertyChanged(nameof(HasCompletedTasks));
            return _taskRepository.SaveItemAsync(task);
        }

        [RelayCommand]
        private Task AddTask()
            => Shell.Current.GoToAsync($"task");

        [RelayCommand]
        private Task NavigateToProject(Project project)
            => Shell.Current.GoToAsync($"project?id={project.ID}");

        [RelayCommand]
        private Task NavigateToTask(ProjectTask task)
            => Shell.Current.GoToAsync($"task?id={task.ID}");

        [RelayCommand]
        private async Task CleanTasks()
        {
            var completedTasks = Tasks.Where(t => t.IsCompleted).ToList();
            foreach (var task in completedTasks)
            {
                await _taskRepository.DeleteItemAsync(task);
                Tasks.Remove(task);
            }

            OnPropertyChanged(nameof(HasCompletedTasks));
            Tasks = new(Tasks);
            await AppShell.DisplayToastAsync("All cleaned up!");
        }

        #region "Metodos"
        private async void Autenticar()
        {
            try
            {
                if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Senha))
                {
                    MsgResources = _translateExtension.GetMessage("LoginPageVMMessageUsuarioOuSenhaInvalido");
                    await UserDialogs.Instance.AlertAsync(MsgResources, AppName);
                    return;
                }

                if (Idioma != null)
                {
                    Utils.Settings.Culture = Idioma.Culture;
                    Session.Idioma = Idioma.Culture;
                }
                else
                {
                    Utils.Settings.Culture = "pt";
                    Session.Idioma = "pt";
                }

                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageAutenticando");
                UserDialogs.Instance.ShowLoading(MsgResources);
                var user = await _usuarioRepository.BuscarUsuario(new AutenticarUsuarioCommand(Login, Senha));
                if (user != null)
                {
                    var sincUsuario = await _parametroSincronizacaRepository.BuscarUltimaDataSincUsuario(user.CodUsuario.ToString());
                    Utils.Settings.Login = user.Login;

                    //verifica se o usuario que esta entrando é o mesmo ou um novo
                    if (sincUsuario != null)
                    {
                        string usuarioLogado = await _parametroSincronizacaRepository.BuscarValorParametro("CODUSUARIO");
                        if (usuarioLogado == sincUsuario.CodUsuario)
                        {

                            Session.USUARIO_LOGADO = user;
                            if (Idioma != null)
                                Session.Idioma = Idioma.Culture;
                            else
                            {
                                Session.Idioma = "pt";
                            }

                            #region Regra (Permissão/Restrição)

                            Session_Generica.Instance.LIST_REGRA_CLIENTE = null;
                            Session_Generica.Instance.LIST_REGRA_MARCA = null;
                            Session_Generica.Instance.LIST_REGRA_PRODUTO = null;
                            //Session_Generica.Instance.LIST_EMPRESA_DISPONIVEL = null;

                            if (user.VerificaTipoVendedor == UsuarioCommandResult.TpVendedor.Representante)
                            {
                                Session_Generica.Instance.LIST_REGRA_CLIENTE = await _usuarioRepository.BuscarRegraCliente(user.CodUsuario.ToString());
                                Session_Generica.Instance.LIST_REGRA_MARCA = await _usuarioRepository.BuscarRegraMarca(user.CodUsuario.ToString());
                                Session_Generica.Instance.LIST_REGRA_PRODUTO = await _usuarioRepository.BuscarRegraProduto(user.CodUsuario.ToString());
                            }

                            //Session_Generica.Instance.LIST_EMPRESA_DISPONIVEL = await _usuarioRepository.BuscarEmpresaDisponivel();

                            #endregion Regra (Permissão/Restrição)

                            var usuarioPrecisaSincronizacao = await UsuarioPrecisaSincronizar();
                            UserDialogs.Instance.HideLoading();
                            if (usuarioPrecisaSincronizacao)
                            {
                                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageTempoSemSincronizar");
                                await UserDialogs.Instance.AlertAsync(MsgResources + "\n", AppName);
                                //await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage", System.UriKind.Relative), null, true);
                                await new FuncaoGenerica_Direcionamento().Acessar(FuncaoGenerica_Direcionamento.Link.Sincronizacao, NavigationService);
                                return;
                            }
                            await IrParaCatalogo(user);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            //Salva o novo usuario e manda sincronizar
                            Session.USUARIO_LOGADO = user;
                            //await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage", System.UriKind.Relative), null, true);
                            await new FuncaoGenerica_Direcionamento().Acessar(FuncaoGenerica_Direcionamento.Link.Sincronizacao, NavigationService);
                        }
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        //Salva o novo usuario e manda sincronizar
                        Session.USUARIO_LOGADO = user;
                        //await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage", System.UriKind.Relative), null, true);
                        await new FuncaoGenerica_Direcionamento().Acessar(FuncaoGenerica_Direcionamento.Link.Sincronizacao, NavigationService);
                    }
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    //Valida se tem versão nova.
                    bool temVersaoNova = await UpdateUtility.AtualizarSeNecessario(_parametroSincronizacaRepository, _parametroRepository);
                    if (!temVersaoNova)
                        await BuscarUsuarioNoServidor();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(ex.InnerException != null ? ex.InnerException.Message : ex.Message, AppName);
            }
        }
        public async Task InitializeBD()
        {
            try
            {
                _usuarioRepository = ServiceLocator.Current.GetInstance<IUsuarioRepository>();
                _marcaRepository = ServiceLocator.Current.GetInstance<IMarcaRepository>();
                _parametroRepository = ServiceLocator.Current.GetInstance<IParametroRepository>();
                _parametroSincronizacaRepository = ServiceLocator.Current.GetInstance<IParametroSincronizacaoRepository>();
                _fotoRepository = ServiceLocator.Current.GetInstance<IFotoRepository>();
                _initializer = ServiceLocator.Current.GetInstance<IInicializacaoBanco>();
                if (!await _initializer.BancoJaExiste())
                    await _initializer.Init();

                await _initializer.Atualizacoes();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async void CarregaIdiomas()
        {
            ListaIdiomas = await _idiomaRepository.GetIdiomaList();
            Idioma = ListaIdiomas.FirstOrDefault(t => t.Culture == Utils.Settings.Culture);

        }

        public async Task RequestPermissions()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage);

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Storage))
                        status = results[Permission.Storage];
                }

                if (status != PermissionStatus.Granted)
                {
                    MsgResources = _translateExtension.GetMessage("LoginPageVMMessagePerrmitirAcessoStorage");
                    throw new Exception(MsgResources);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Load()
        {
            try
            {
                if (Device.RuntimePlatform == Device.Android)
                    await RequestPermissions();

                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageInicializandoBanco");
                UserDialogs.Instance.ShowLoading(MsgResources);
                await InitializeBD();

                VersaoSistema = Session.VERSAO_SISTEMA = await ServiceUtility.BuscarVersaoLocal(_parametroSincronizacaRepository);
                Ambiente = Utils.Settings.Ambiente == "P" ? "Produção" : "Teste";
                CarregaIdiomas();

                //var logoCliente = await _fotoRepository.BuscarFotoMarca("Logo", Session.USUARIO_LOGADO.CodMarca);
                //LogoCliente = logoCliente == null ? ImageSource.FromFile("logo_cliente.png") : ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(logoCliente)));

                LogoCliente = await new FuncaoGenerica_Imagem().RetornaImagem(_fotoRepository, FuncaoGenerica_Imagem.TipoImagem.Nulo);

                var multilingua = await _parametroRepository.BuscarValorParametro("MULTILANGUAGE");

                if (multilingua == "S")
                {
                    Multilanguage = true;
                }

                if (Session.UsaApenasCorFundoLogin)
                {
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                //var image = await _fotoRepository.BuscarFotoMarca("Fundo_Login", Session.USUARIO_LOGADO.CodMarca);
                //FundoLogin = image == null ? ImageSource.FromFile("login_background.png") : ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(image)));
                //                FundoLogin = await new FuncaoGenerica_Imagem().RetornaImagem(_fotoRepository, FuncaoGenerica_Imagem.TipoImagem.LogoFundoLogin);
                FundoLogin = await new FuncaoGenerica_Imagem().RetornaImagem(_fotoRepository, FuncaoGenerica_Imagem.TipoImagem.FundoIntermidia);

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(ex.Message);
            }
            UserDialogs.Instance.HideLoading();
        }



        private async Task<bool> UsuarioPrecisaSincronizar()
        {
            bool usuarioPrecisaSincronizacao = await ServiceUtility.UsuarioPrecisaSincronizar(_parametroRepository, _parametroSincronizacaRepository);
            return usuarioPrecisaSincronizacao;
        }



        private async Task IrParaCatalogo(UsuarioCommandResult user)
        {
            NavigationParameters parametros = new NavigationParameters();
            parametros.Add("usuario", user);
            await SessionInit(user);
            try
            {
                //string telainicial = string.IsNullOrEmpty(Session.TELA_INICIAL) ? "CatalogoPage" : Session.TELA_INICIAL;
                //await NavigationService.NavigateAsync(new System.Uri($"{Session.URI_BUNDLE}/{telainicial}", System.UriKind.Absolute), parametros);
                await new FuncaoGenerica_Direcionamento().Acessar(FuncaoGenerica_Direcionamento.Link.Catalogo, NavigationService, parametros);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private async Task SessionInit(UsuarioCommandResult user)
        {
            var paramMarkupPadrao = await _parametroRepository.BuscarValorParametro(ParametrosSistema.MARKUPPADRAO);
            decimal markupPadrao = 2.2M;
            Session.USUARIO_LOGADO = user;
            //Session.CATALOGO_VIEW_MODEL = null;
            Session.ATENDIMENTO_ATUAL = null;
            Session.MarkupPadrao = markupPadrao;
            //Session_Catalogo.NumPagina_PesquisaCatologo = 0;
            //Session_Catalogo.PesquisaVoltaCatalogo = false;
            //Session_Catalogo.NumLinha_PesquisaCatologo = 0;
            //Session_Catalogo.NumSegundo_PesquisaCatologo = 0;
            new FuncaoGenerica_Sessao().DefineConfigInicialCatagodo();

            Session.MARCA = await _marcaRepository.BuscarMarca(new BuscarMarcaCommand() { CodMarca = user.CodMarca });

            if (!string.IsNullOrEmpty(paramMarkupPadrao) && decimal.TryParse(paramMarkupPadrao, out markupPadrao))
            {
                Session.MarkupPadrao = decimal.Parse(paramMarkupPadrao);
            }
        }

        private async Task BuscarUsuarioNoServidor()
        {
            int codUsuario = await ServiceUtility.AutenticarNoServidor(_parametroSincronizacaRepository, Login, Senha);
            if (codUsuario != 0)
            {
                Utils.Settings.Login = Login;
                int newCodInstalacao = await ServiceUtility.GetCodInstalacao(_parametroSincronizacaRepository, codUsuario, _deviceInformation.GetIPAddress(), _deviceInformation.GetDeviceName(), Device.Idiom.ToString());
                await _parametroSincronizacaRepository.SalvarParametro(ParametrosSistema.CODINSTALACAO, newCodInstalacao.ToString());
                await _parametroSincronizacaRepository.SalvarParametro(ParametrosSistema.CODUSUARIO, codUsuario.ToString());
                await _parametroSincronizacaRepository.SalvarParametro(ParametrosSistema.DATAULTIMAATUALIZACAO, "2000-01-01T00:00:00");
                Session.USUARIO_LOGADO = new UsuarioCommandResult() { CodUsuario = codUsuario };
                //await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage", System.UriKind.Relative), null, true);
                await new FuncaoGenerica_Direcionamento().Acessar(FuncaoGenerica_Direcionamento.Link.Sincronizacao, NavigationService);
            }
            else
            {
                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageUsuarioNaoEncontrado");
                await UserDialogs.Instance.AlertAsync(MsgResources, AppName, "OK");
            }
        }
        #endregion

        #region Funções administrativas
        private async void Settings(object obj)
        {

            string ambiente = Utils.Settings.Ambiente == "P" ? "Ambiente Atual: Produção" : "Ambiente Atual: Teste";
            //string[] options = new string[] { "Limpar Fotos", "Limpar Dados" }; //, "Sincronizar"
            string[] options = new string[] { "Limpar Fotos", "Limpar Dados", "Gerar backup" }; //, "Sincronizar"
            var result = await UserDialogs.Instance.ActionSheetAsync("Selecione", ambiente, "Cancelar", null, options);
            switch (result)
            {
                case "Limpar Fotos":
                    var confirmLimpaFoto = await UserDialogs.Instance.ConfirmAsync("Deseja realmente limpar todas as fotos?", "Cancelar", "Sim", "Não");
                    if (confirmLimpaFoto) await LimparFotos();
                    break;
                case "Limpar Dados":
                    var confirmLimpaDados = await UserDialogs.Instance.ConfirmAsync($"Deseja realmente limpar todos os Dados?", "Cancelar", "Sim", "Não");
                    if (confirmLimpaDados) await LimparDados();
                    break;
                //case "Sincronizar":
                //    await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage", System.UriKind.Relative), null, true);
                //    break;
                //case "Ambiente Atual: Produção":
                //    Utils.Settings.Ambiente = "T";
                //    Utils.Settings.DownloadUrl = ParametrosSistema.DOWNLOADURL + "_T";
                //    Utils.Settings.UploadUrl = ParametrosSistema.UPLOADURL + "_T";
                //    await Load();
                //    break;
                //case "Ambiente Atual: Teste":
                //    Utils.Settings.Ambiente = "P";
                //    Utils.Settings.DownloadUrl = ParametrosSistema.DOWNLOADURL + "_P";
                //    Utils.Settings.UploadUrl = ParametrosSistema.UPLOADURL + "_P";
                //    await Load();
                //    break;
                case "Gerar backup":
                    var ConfirmaBackup = await UserDialogs.Instance.ConfirmAsync($"Deseja fazer o backup da base de dados?", "Backup", "Sim", "Não");
                    if (ConfirmaBackup) await GerarBackup();
                    break;
                default:
                    break;
            }
        }
        private async Task LimparFotos()
        {
            try
            {
                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageFotosSendoApagadas");
                UserDialogs.Instance.ShowLoading(MsgResources);
                await _initializer.LimparFotos();
                UserDialogs.Instance.HideLoading();
                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageFotosApagadas");
                await UserDialogs.Instance.AlertAsync(MsgResources, AppName);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageErroLimparFotos");
                await UserDialogs.Instance.AlertAsync(MsgResources + ex.Message, AppName);
            }
        }
        private async Task LimparDados()
        {
            try
            {
                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageDadosSendoApagados");
                UserDialogs.Instance.ShowLoading(MsgResources);
                await _initializer.LimparDados();
                UserDialogs.Instance.HideLoading();
                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageDadosApagados");
                await UserDialogs.Instance.AlertAsync(MsgResources, AppName);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                MsgResources = _translateExtension.GetMessage("LoginPageVMMessageErroLimparDados");
                await UserDialogs.Instance.AlertAsync(MsgResources + ex.Message, AppName);
            }
        }
        private async void TrocarAmbiente()
        {
            //string[] options = new string[] { "Produção", "Teste" };
            //var result = await UserDialogs.Instance.ActionSheetAsync("Selecione o ambiente.", "Cancelar", null, null, options);
            //switch (result)
            //{
            //    case "Teste":
            //        Utils.Settings.Ambiente = "T";
            //        Utils.Settings.DownloadUrl = ParametrosSistema.DOWNLOADURL + "_T";
            //        Utils.Settings.UploadUrl = ParametrosSistema.UPLOADURL + "_T";
            //        await Load();
            //        break;
            //    case "Produção":
            //        Utils.Settings.Ambiente = "P";
            //        Utils.Settings.DownloadUrl = ParametrosSistema.DOWNLOADURL + "_P";
            //        Utils.Settings.UploadUrl = ParametrosSistema.UPLOADURL + "_P";
            //        await Load();
            //        break;
            //    default:
            //        break;
            //}
        }

        //private async Task GerarBackup()
        //{
        //    try
        //    {
        //        UserDialogs.Instance.ShowLoading("Gerando backup do banco.");
        //        await Task.Delay(2000);
        //        var fileAccessHelper = DependencyService.Get<ISqliteConnection>();
        //        var path = await fileAccessHelper.GerarBackup();
        //        UserDialogs.Instance.HideLoading();

        //        if (Session.USUARIO_LOGADO.CodUsuario == 0)
        //        {
        //            var user = await _usuarioRepository.BuscarUsuario(new AutenticarUsuarioCommand(Login, "ARAI96AFV"));
        //            Session.USUARIO_LOGADO = user;
        //        }

        //        var conected = CrossConnectivity.Current.IsConnected;

        //        if (conected)
        //        {
        //            bool sucess = false;
        //            if (Device.RuntimePlatform == Device.UWP)
        //            {
        //                sucess = await UploadDropbox(path);
        //            }
        //            else
        //            {
        //                UserDialogs.Instance.ShowLoading("Enviando backup para o servidor.");
        //                sucess = await UploadDropbox(path);
        //                UserDialogs.Instance.HideLoading();
        //            }

        //            if (sucess == true)
        //                await UserDialogs.Instance.AlertAsync("Backup gerado com sucesso!", AppName, "OK");
        //            else
        //                await UserDialogs.Instance.AlertAsync("Erro ao enviar o backup, tente novamente.", AppName, "OK");
        //        }
        //        else
        //            await UserDialogs.Instance.AlertAsync("Dispositivo sem acesso a internet, por favor verifique sua conexão.");
        //    }
        //    catch (Exception e)
        //    {
        //        UserDialogs.Instance.HideLoading();
        //        await UserDialogs.Instance.AlertAsync(e.Message, AppName);
        //    }
        //}

        private async Task GerarBackup() //LINK
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Gerando backup do banco.");
                await Task.Delay(2000);
                //var fileAccessHelper = DependencyService.Get<ISqliteConnection>();
                //var path = await fileAccessHelper.GerarBackup();

                byte[] path = null;

                if (Device.RuntimePlatform == Device.iOS)
                {
                    var fileAccessHelper = DependencyService.Get<ISqliteConnection>();
                    path = await fileAccessHelper.GerarBackup();
                }
                else
                {
                    path = await _iSqliteConnection.GerarBackup();
                }

                UserDialogs.Instance.HideLoading();

                if (Session.USUARIO_LOGADO.CodUsuario == 0)
                {
                    var user = await _usuarioRepository.BuscarUsuario(new AutenticarUsuarioCommand(Login, "ARAI96AFV"));
                    Session.USUARIO_LOGADO = user;
                }

                var conected = CrossConnectivity.Current.IsConnected;

                if (conected)
                {
                    bool sucess = false;
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        sucess = await UploadDropbox(path);
                    }
                    else
                    {
                        UserDialogs.Instance.ShowLoading("Enviando backup para o servidor.");
                        sucess = await UploadDropbox(path);
                        UserDialogs.Instance.HideLoading();
                    }

                    if (sucess == true)
                        await UserDialogs.Instance.AlertAsync("Backup gerado com sucesso!", AppName, "OK");
                    else
                        await UserDialogs.Instance.AlertAsync("Erro ao enviar o backup, tente novamente.", AppName, "OK");

                }
                else
                    await UserDialogs.Instance.AlertAsync("Dispositivo sem acesso a internet, por favor verifique sua conexão.");
            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(e.Message, AppName);
            }
        }

        static async Task<bool> UploadDropbox(byte[] val)
        {
            bool retorno = true;
            try
            {
                string date = DateTime.Now.ToString("dd/MM H:mm");
                date = date.Replace("/", "");
                date = date.Replace(" ", "");
                date = date.Replace(":", "");

                var config = new DropboxClientConfig();
                HttpClient client = new HttpClient();
                client.Timeout = new TimeSpan(0, 10, 0);
                config.HttpClient = client;

                using (UserDialogs.Instance.Loading("Fazendo uploado do banco para servidor.", maskType: MaskType.Gradient))
                {
                    var userBackup = Session.USUARIO_LOGADO == null ? 0 : Session.USUARIO_LOGADO.CodUsuario;
                    var file = "/bkp" + date + "_" + userBackup + "Clio.sqlite.zip";
                    var commit = new CommitInfo(file, WriteMode.Overwrite.Instance, true, DateTime.Now);

                    using (var dbx = new DropboxClient(AccessTpken, config))
                    {
                        var checkRetorno = dbx.Files.UploadAsync(commit, new MemoryStream(val)).Result;

                        if (checkRetorno.Size == 0)
                        {
                            retorno = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                var erro = e.Message;
                throw e;
            }
            return retorno;
        }

        #endregion
    }
}