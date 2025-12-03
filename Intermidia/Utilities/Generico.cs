using Intermidia.Intermidia.Infra.Domain;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Repositories.Interface;
using Prism.Navigation;

namespace Intermidia.Utilities
{
    public class FuncaoGenerica_Imagem
    {
        public enum TipoImagem
        {
            Nulo,
            LogoCliente,
            FundoIntermidia,
            LogoCliente_Cabecalho,
            //FundoCatalogo
        }

        public async Task<ImageSource> RetornaImagem(IFotoRepository _fotoRepository, TipoImagem tipoImagem)
        {
            ImageSource retorno = null;

            if (tipoImagem != TipoImagem.Nulo)
            {

                String nomeFoto = String.Empty;
                String arquivoFoto = String.Empty;

                switch (tipoImagem)
                {
                    case TipoImagem.LogoCliente:

                        nomeFoto = "Logo";
                        arquivoFoto = "logo_cliente.png";

                        break;

                    case TipoImagem.FundoIntermidia:

                        nomeFoto = "Fundo_Login";
                        arquivoFoto = "fundo_Intermidia_semCliente.png";

                        break;

                    case TipoImagem.LogoCliente_Cabecalho:

                        nomeFoto = "LogoMarcaClara";
                        arquivoFoto = "logo_white.png";

                        break;


                        //case TipoImagem.FundoCatalogo:

                        //    nomeFoto = "Fundo_Catalogo";
                        //    arquivoFoto = "fundo_catalogo.png";

                        break;

                        //case TipoImagem.LogoFundoLogin_SemCliente:

                        //    nomeFoto = "Fundo_Login";
                        //    arquivoFoto = "fundo_catalogo_semCliente.png";

                        //    break;


                }

                var logoCliente = await _fotoRepository.BuscarFotoMarca(nomeFoto, Session.USUARIO_LOGADO.CodMarca);
                retorno = logoCliente == null ? ImageSource.FromFile(arquivoFoto) : ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(Convert.ToString(logoCliente))));
            }

            return retorno;
        }

    }

    public class FuncaoGenerica_Arquivo
    {
        public enum Contexto
        {
            Nulo,
            Catalogo
        }

        public enum Plataforma
        {
            Windows,
            Android,
            Android_2022,
            iOS
        }

        public async Task<String> RetornaCaminho(Plataforma tipoPlataforma, Contexto tipoContexto, String caminhoDiretorio = "")
        {
            String retorno = String.Empty;

            String nomeArquivo = "";

            switch (tipoContexto)
            {
                case Contexto.Catalogo:

                    nomeArquivo = "CATALOGO";

                    break;
            }

            if (!String.IsNullOrEmpty(nomeArquivo))
                nomeArquivo = String.Concat(nomeArquivo, "_");

            //retorno = Path.Combine((string)Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), string.Format("{0}{1}.pdf", nomeArquivo, DateTime.Now.ToString("dd_MM_yyyy")));

            //            String dtAtual = DateTime.Now.ToString("dd_MM_yyyy");
            String dtAtual = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

            nomeArquivo = String.Format("{0}{1}.pdf", nomeArquivo, dtAtual);

            switch (tipoPlataforma)
            {
                case Plataforma.Windows:

                    retorno = Path.Combine((string)Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), nomeArquivo);

                    break;

                case Plataforma.Android:

                    retorno = Path.Combine((string)Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), nomeArquivo);

                    break;

                case Plataforma.Android_2022:

                    retorno = System.IO.Path.Combine(caminhoDiretorio, nomeArquivo);

                    break;
            }

            return retorno;
        }

        public String RetornaCaminho_PDF_Pedido(Plataforma tipoPlataforma, List<CarrinhoCommandResult> listaPedido, String caminhoDiretorio = "")
        {
            String retorno = String.Empty;

            string nameFile = listaPedido.Count > 1 ? "pdf_pedidos" : listaPedido[0].CodCarrinho;
            //String nomeArrquivo  = String.Format("{0}_{1}_{2}.pdf", nameFile, DateTime.Now.ToString("dd_MM_yyyy"), DateTime.Now.Minute);
            String nomeArquivo = String.Format("{0}_{1}.pdf", nameFile, DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"));
            String arquivoConsiderarExcluir = String.Concat("_", DateTime.Now.ToString("dd_MM_yyyy_"));

            //string[] arquivos = Directory.GetFiles((string)Environment.GetFolderPath(Environment.SpecialFolder.InternetCache), "*.pdf", SearchOption.AllDirectories);
            //foreach (var arq in arquivos)
            //{
            //    File.Delete(arq);
            //}

            //Environment.SpecialFolder tipoDir = Environment.SpecialFolder.InternetCache;
            String pastaPlataforma = String.Empty;

            //string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            switch (tipoPlataforma)
            {
                case Plataforma.Windows:
                    //case Plataforma.iOS:
                    //tipoDir = Environment.SpecialFolder.InternetCache;
                    pastaPlataforma = (string)Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                    break;
                case Plataforma.Android:
                    //tipoDir = Environment_Android.DirectoryDownloads;
                    //pastaPlataforma = Environment_Android.GetExternalStoragePublicDirectory(Environment_Android.DirectoryDownloads).ToString();
                    //pastaPlataforma = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).ToString();

                    //            string filePdf = System.IO.Path.Combine(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).ToString(), string.Format("{0}_{1}.pdf", filename, System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")));

                    //string filename = listOrders.Count > 0 ? "pdf_pedidos" : listOrders[0].CodCarrinho;
                    //string[] arquivos = Directory.GetFiles(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDownloads).ToString(), "*.pdf", SearchOption.AllDirectories);
                    //foreach (var arq in arquivos)
                    //{
                    //    File.Delete(arq);
                    //}
                    //string filePdf2 = System.IO.Path.Combine(Environment_Android.GetExternalStoragePublicDirectory(Environment_Android.DirectoryDownloads).ToString(), string.Format("{0}_{1}.pdf", filename, System.DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")));
                    pastaPlataforma = caminhoDiretorio;

                    break;
                case Plataforma.iOS:
                    //tipoDir = Environment.SpecialFolder.Personal;
                    pastaPlataforma = (string)Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    break;
            }

            #region Exclusao

            string[] arquivos = Directory.GetFiles(pastaPlataforma, "*.pdf", SearchOption.AllDirectories);
            foreach (var arq in arquivos)
            {
                //File.Delete(arq);

                //Excluir somente arquivo de outro data
                if (!arq.Contains(arquivoConsiderarExcluir))
                {
                    File.Delete(arq);
                }

                //String arquivoExcluir = String.Concat("_", DateTime.Now.ToString("dd_MM_yyyy_"));
            }

            #endregion Exclusao

            retorno = Path.Combine(pastaPlataforma, nomeArquivo);






            return retorno;
        }

    }

    public class FuncaoGenerica_Imagem_Produto
    {
        public object RetornaImagem(String image)
        {
            object retorno = null;

            if (String.IsNullOrEmpty(image))
            {
                retorno = ImageSource.FromFile("NaoDisp.png");
            }
            else
            {
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(image));
                retorno = ImageSource.FromStream(() => memoryStream);
                memoryStream.Close();
            }

            return retorno;
        }

        public object RetornaImagem2(String image)
        {
            object retorno = null;

            if (string.IsNullOrEmpty(image))
            {
                retorno = ImageSource.FromFile("NaoDisp.png");
            }
            else
            {
                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(image));
                retorno = ImageSource.FromStream(() => memoryStream);
            }

            return retorno;
        }

    }

    public class FuncaoGenerica_Validacao
    {
        public bool ValidarCPF(string cpf)
        {
            if (cpf == null) return false;

            cpf = cpf.Replace("/", "").Replace("-", "").Replace(".", "").Replace("\\", "");
            if (cpf.Length != 11 || decimal.Parse(cpf) == 0) return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public bool ValidarCNPJ(string cnpj)
        {
            if (cnpj == null) return false;

            cnpj = cnpj.Replace("/", "").Replace("-", "").Replace(".", "").Replace("\\", "");
            if (cnpj.Length != 14 || decimal.Parse(cnpj) == 0) return false;

            int calcularUm = 0;
            int calcularDois = 0;
            for (int i = 0, x = 5; i <= 11; i++, x--)
            {
                x = (x < 2) ? 9 : x;
                int number = int.Parse(cnpj.Substring(i, 1));
                calcularUm += number * x;
            }

            for (int i = 0, x = 6; i <= 12; i++, x--)
            {
                x = (x < 2) ? 9 : x;
                int numberDois = int.Parse(cnpj.Substring(i, 1));
                calcularDois += numberDois * x;
            }

            int digitoUm = ((calcularUm % 11) < 2) ? 0 : 11 - (calcularUm % 11);
            int digitoDois = ((calcularDois % 11) < 2) ? 0 : 11 - (calcularDois % 11);

            if (digitoUm != int.Parse(cnpj.Substring(12, 1)) || digitoDois != int.Parse(cnpj.Substring(13, 1)))
            {
                return false;
            }

            return true;
        }
    }


    public class FuncaoGenerica_Dispositivo
    {
        public Boolean SmartPhone()
        {
            //var idiom = DeviceInfo.Idiom;

            Boolean retorno = false;

            switch (Device.Idiom)
            {
                //case TargetIdiom.Desktop:
                //    // UWP desktop
                //    break;
                case TargetIdiom.Phone:
                    //                case TargetIdiom.Unsupported:
                    // Fones
                    retorno = true;
                    break;
                    //case TargetIdiom.Tablet:
                    //    // Tablets
                    //    break;
                    //case TargetIdiom.Unsupported:
                    //    // dispositivos não suportados
                    //    break;
            }

            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    //iOS stuff
            //}
            //else if (Device.RuntimePlatform == Device.Android)
            //{
            //    //Android stuff
            //}

            return retorno;
        }
    }

    public class FuncaoGenerica_Direcionamento
    {
        public enum Link
        {
            Catalogo,
            Sincronizacao,
            DetalheProduto,
            DetalheProduto_CodigoBarra
        }

        public async Task Acessar(Link link, INavigationService NavigationService, NavigationParameters parametros = null)
        {
            Boolean ehSmartPhone = false;

            if (new FuncaoGenerica_Dispositivo().SmartPhone())
                ehSmartPhone = true;

            String telainicial = String.Empty;

            switch (link)
            {
                case Link.Catalogo:
                    if (!ehSmartPhone)
                    {
                        telainicial = string.IsNullOrEmpty(Session.TELA_INICIAL) ? "CatalogoPage" : Session.TELA_INICIAL;
                        await NavigationService.NavigateAsync(new System.Uri($"{Session.URI_BUNDLE}/{telainicial}", System.UriKind.Absolute), parametros);
                    }
                    else
                    {
                        telainicial = "Tela_Principal";
                        await NavigationService.NavigateAsync(new System.Uri($"{Session.URI_BUNDLE}/{telainicial}", System.UriKind.Absolute), parametros);
                    }
                    break;

                case Link.Sincronizacao:
                    if (!ehSmartPhone)
                    {
                        await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage", System.UriKind.Relative), null, true);
                    }
                    else
                    {
                        await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage_SmartPhone", System.UriKind.Relative), null, true);
                    }
                    break;

                case Link.DetalheProduto:
                case Link.DetalheProduto_CodigoBarra:
                    if (!ehSmartPhone)
                    {
                        //await NavigationService.NavigateAsync(new System.Uri("SincronizacaoPage", System.UriKind.Relative), null, true);
                        await NavigationService.NavigateAsync(new System.Uri($"{Session.URI_BUNDLE}/CatalogoDetalheTipo1Page", System.UriKind.Absolute), parametros, true);
                    }
                    else
                    {
                        if (link == Link.DetalheProduto)
                        {
                            await NavigationService.NavigateAsync(new System.Uri($"{Session.URI_BUNDLE}/CatalogoDetalheTipo1Page_SmartPhone", System.UriKind.Absolute), parametros, true);
                        }
                        else
                        {
                            //Tela_Principal zxingView = new Tela_Principal();

                            //App.Current.MainPage.Navigation.PushModalAsync(new Tela_Principal());
                            //Application.Current.MainPage as Tela_Principal();
                            //await App.Current.MainPage.Navigation.PopAsync();
                            await App.Current.MainPage.Navigation.PopModalAsync();
                            await NavigationService.NavigateAsync(new System.Uri($"{Session.URI_BUNDLE}/CatalogoDetalheTipo1Page_SmartPhone", System.UriKind.Absolute), parametros, true);
                        }
                    }
                    break;

            }



        }
    }
}