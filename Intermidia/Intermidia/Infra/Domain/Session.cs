//using Flunt.Notifications;

using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Models.Xamarin;
using Intermidia.Services.Contracts;

namespace Intermidia.Intermidia.Infra.Domain
{
    public static class Session
    {
        public static SelecaoMarcaCommandResult MARCA { get; set; }
        public static UsuarioCommandResult USUARIO_LOGADO { get; set; }
        public static AtendimentoCommandResult ATENDIMENTO_ATUAL { get; set; }
        public static decimal MarkupPadrao { get; set; }
        public static string VERSAO_SISTEMA { get; set; }
        public static string URI_BUNDLE { get; set; }
        public static string TELA_INICIAL { get; set; }
        //public static ICatalogoViewModel CATALOGO_VIEW_MODEL { get; set; }
        public static ICatalogoSortidoViewModel CATALOGOSORTIDO_VIEW_MODEL { get; set; }
        public static List<DataTemplateConfiguracao> DATATEMPLATES_CONFIG { get; set; }
        public static List<DerivacaoGradeResult> ULTIMA_GRADE_DIGITADA { get; set; }
        public static bool MultiMarca { get; set; }
        public static bool GroupByNoProduto { get; set; }
        public static bool UsaApenasCorFundoLogin { get; set; }
        public static bool UsaMetas { get; set; }
        public static bool EdicaoAutomaticaValidaEstoque { get; set; }
        public static bool UsaGeracaoCatalogo { get; set; }
        public static bool FotoPrincipalEstaNasExtras { get; set; }
        public static bool UsaCatalogoSortidos { get; set; }
        public static string Idioma { get; set; }
        public static string NOME_PROJETO { get; set; }
        public static bool MostrarEstoque { get; set; }

        //public static int NumColumnCatalogue { get; set; }

        public static void Clear()
        {
            //CATALOGO_VIEW_MODEL = null;
            ATENDIMENTO_ATUAL = null;
            CATALOGOSORTIDO_VIEW_MODEL = null;
        }

        //public static int NumPagina_PesquisaCatologo { get; set; }
        //public static BuscarModeloCommand FiltroUltimaConsultaCatalogo;
        //public static Boolean PesquisaVoltaCatalogo { get; set; }
        //public static double NumLinha_PesquisaCatologo { get; set; }
        ////public static ExtendedScrollView ScrollView_PesquisaCatologo { get; set; }

    }


    public static class Session_Catalogo
    {
        public static int NumPagina_PesquisaCatologo { get; set; }
        public static BuscarModeloCommand FiltroUltimaConsultaCatalogo;
        public static Boolean PesquisaVoltaCatalogo { get; set; }
        public static double NumLinha_PesquisaCatologo { get; set; }
        //public static ExtendedScrollView ScrollView_PesquisaCatologo { get; set; }
        public static int NumSegundo_PesquisaCatologo { get; set; }
        public static List<ModeloCommandResult> ListaProduto_UltimaConsulta { get; set; }

        public static Boolean PesquisaProcessada { get; set; }


        //public static String Filtro_Nivel1 { get; set; }
        public static List<NivelAtributoResult> FiltroSelecionado;
        public static String FiltroSelecionado_Texto { get; set; }

    }
}
