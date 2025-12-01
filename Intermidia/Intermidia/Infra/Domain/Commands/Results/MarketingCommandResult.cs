namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class MarketingCommandResult : BaseViewModel
    {
        private string tipoMarketing;
        private string titulo;
        private string descricao;
        private string tipoArquivo;
        private string url;
        private string nomeArquivo;
        private decimal? indAtivo;

        public string TipoMarketing { get => tipoMarketing; set => SetProperty(ref tipoMarketing, value); }
        public string Titulo { get => titulo; set => SetProperty(ref titulo, value); }
        public string Descricao { get => descricao; set => SetProperty(ref descricao, value); }
        public string TipoArquivo { get => tipoArquivo; set => SetProperty(ref tipoArquivo, value); }
        public string Url { get => url; set => SetProperty(ref url, value); }
        public string NomeArquivo { get => nomeArquivo; set => SetProperty(ref nomeArquivo, value); }
        public decimal? IndAtivo { get => indAtivo; set => SetProperty(ref indAtivo, value); }
    }
}
