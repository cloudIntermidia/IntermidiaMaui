namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class SelecaoMarcaCommandResult : BaseViewModel
    {
        private string codMarca;
        public string CodMarca { get => codMarca; set => SetProperty(ref codMarca, value); }

        private string descricao;
        public string Descricao { get => descricao; set => SetProperty(ref descricao, value); }

        private string marca;
        public string Marca { get => marca; set => SetProperty(ref marca, value); }

        private string imagem;
        public string Imagem { get => imagem; set => SetProperty(ref imagem, value); }

        private string fundoCatalogo;
        public string FundoCatalogo { get => fundoCatalogo; set => SetProperty(ref fundoCatalogo, value); }

        private string icone;
        public string Icone { get => icone; set => SetProperty(ref icone, value); }

        private string corPrimaria;
        public string CorPrimaria { get => corPrimaria; set => SetProperty(ref corPrimaria, value); }

        private string corDestaque;
        public string CorDestaque { get => corDestaque; set => SetProperty(ref corDestaque, value); }

        private string corButtonNiveis;
        public string CorButtonNiveis { get => corButtonNiveis; set => SetProperty(ref corButtonNiveis, value); }

        private string corFontButtonNiveis;
        public string CorFontButtonNiveis { get => corFontButtonNiveis; set => SetProperty(ref corFontButtonNiveis, value); }

        private int consultaSoComAtendimento;
        public int ConsultaSoComAtendimento { get => consultaSoComAtendimento; set => SetProperty(ref consultaSoComAtendimento, value); }

        private string codTabelaPreco;
        public string CodTabelaPreco { get => codTabelaPreco; set => SetProperty(ref codTabelaPreco, value); }

        private decimal? percentualDescontoPermitido;
        public decimal? PercentualDescontoPermitido { get => percentualDescontoPermitido; set => SetProperty(ref percentualDescontoPermitido, value); }
    }

}
