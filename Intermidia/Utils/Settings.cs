using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Intermidia.Utils
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string Login
        {
            get => AppSettings.GetValueOrDefault(nameof(Login), "");
            set => AppSettings.AddOrUpdateValue(nameof(Login), value);
        }

        public static string Senha
        {
            get => AppSettings.GetValueOrDefault(nameof(Senha), "");
            set => AppSettings.AddOrUpdateValue(nameof(Senha), value);
        }

        public static string Culture
        {
            get => AppSettings.GetValueOrDefault(nameof(Culture), "pt");
            set => AppSettings.AddOrUpdateValue(nameof(Culture), value);
        }


        public static string NomeProjeto
        {
            get => AppSettings.GetValueOrDefault(nameof(NomeProjeto), "Mobili Vendas");
            set => AppSettings.AddOrUpdateValue(nameof(NomeProjeto), value);
        }

        public static string Ambiente
        {
            get => AppSettings.GetValueOrDefault(nameof(Ambiente), "T"); // [T = TESTE | P = Produção]
            set => AppSettings.AddOrUpdateValue(nameof(Ambiente), value);
        }

        public static string UploadUrl
        {
            get => AppSettings.GetValueOrDefault(nameof(UploadUrl), "");
            set => AppSettings.AddOrUpdateValue(nameof(UploadUrl), value);
        }

        public static string DownloadUrl
        {
            get => AppSettings.GetValueOrDefault(nameof(DownloadUrl), "");
            set => AppSettings.AddOrUpdateValue(nameof(DownloadUrl), value);
        }
    }
}
