//using Flunt.Notifications;

using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Models.Xamarin;
using Intermidia.Services.Contracts;

namespace Intermidia.Intermidia.Infra.Domain
{
    public static class ParametrosSistema
    {
        /*AFV_PARAMETRO_SINCRONIZACAO*/
        public static readonly string CODINSTALACAO = "CODINSTALACAO";
        public static readonly string CODUSUARIO = "CODUSUARIO";
        public static readonly string DATAULTIMAATUALIZACAO = "DATAULTIMAATUALIZACAO";
        public static readonly string DATAATUALIZACAOCOMPL = "DATAATUALIZACAOCOMPL";
        public static readonly string UPLOADURL = "UPLOADURL";
        public static readonly string DOWNLOADURL = "DOWNLOADURL";
        public static readonly string VERSAOSISTEMA_XAM = "VERSAOSISTEMA_XAM";
        public static readonly string URL_DOWNLOAD_APOIO = "URL_DOWNLOAD_APOIO";

        /*TBT_PARAMETRO*/
        // SISTEMA
        public static readonly string LIBERA = "LIBERA";
        public static readonly string USUARIOPARAMETRO = "USUARIOPARAMETRO";
        public static readonly string NOMEPROJETO = "NOMEPROJETO";
        public static readonly string AMBIENTE = "AMBIENTE";
        public static readonly string MINIMOPEDIDO = "MINIMOPEDIDO";
        public static readonly string VALIDAVALORMINIMO = "VALIDAVALORMINIMO";
        public static readonly string MINIMODUPLICATA = "MINIMODUPLICATA";
        public static readonly string LIMITEDESCCOMERCIAL = "LIMITEDESCCOMERCIAL";
        public static readonly string LIBERACLIENTEFEIRA = "LIBERACLIENTEFEIRA";
        public static readonly string MULTIMARCA = "MULTIMARCA";
        public static readonly string TABELAPRECOPADRAO = "TABELAPRECOPADRAO";
        public static readonly string DESCMAXREP = "DESCMAXREP";
        public static readonly string DESCMAXGER = "DESCMAXGER";

        public static readonly string MOSTRAPRIVATELBL = "MOSTRAPRIVATELBL";

        public static readonly string QTDDIASSINCRONIZACAO = "QTDDIASSINCRONIZACAO";
        public static readonly string VALIDAESTOQUE = "VALIDAESTOQUE";
        public static readonly string IMPRESCLIENTEPDF = "IMPRESCLIENTEPDF";

        public static readonly string MARKUPPADRAO = "MARKUPPADRAO";
        public static readonly string QTDMAXITENSPEDIDO = "QTDMAXITENSPEDIDO";
        public static readonly string FOTOPORMODELO = "FOTOPORMODELO";

        public static readonly string VALIDARMULTIPLOS = "VALIDARMULTIPLOS";
        public static readonly string DATAMINIMAESPECIFICA = "DATAMINIMAESPECIFICA";

        //valida se o usuário é obrigado a abrir atendimento para poder visualizar preços
        public static readonly string PRECOATENDIMENTO = "PRECOATENDIMENTO";

        /// <summary>
        /// Em alguns casos o preço pode sofrer modificação antes de ser exibido por diversos fatores.
        /// Exemplo : Conforme prazo(LinkComercial), regiao(Timberland) e etc...
        /// </summary>
        public static readonly string CALCULAPRECO = "CALCULAPRECO";
        public static readonly string APLICADESCCATAL = "APLICADESCCATAL";

        //Parametro utilizado para indicar se o cliente mantem cadastro da carteira de cliente ou não
        // Padrão é manter.
        public static readonly string MANTEMCADASTRO = "MANTEMCADASTRO";
        public static readonly string VALIDAMINIMOFRETE = "VALIDAMINIMOFRETE";

        public static readonly string TROCATEMPLATE = "TROCATEMPLATE";
        public static readonly string TITULOSELECAOCLIENTE = "TITULOSELECAOCLIENTE";
        public static readonly string OCULTABTNDESMBTO = "OCULTABTNDESMBTO";

        public static readonly string NUMCOLUMNCATAL = "NUMCOLUMNCATAL";

        //GERENCIAMENTO
        public static readonly string FEEDBACK = "FEEDBACK";
        // ATENDIMENTO
        public static readonly string CLIENTEEMFEIRA = "CLIENTEEMFEIRA";
        public static readonly string PRAZOADICIONALFEIRA = "PRAZOADICIONALFEIRA";
        public static readonly string PRAZOMEDIOPADRAO = "PRAZOMEDIOPADRAO";
        public static readonly string CUPOM = "CUPOM";

        // DETALHE
        public static readonly string TELADETALHE = "TELADETALHE";

        //COMUNICAÇÃO
        public static readonly string URLGETMARKETING = "URLGETMARKETING";
        public static readonly string URLFACEBOOK = "URLFACEBOOK";
        public static readonly string URLINSTAGRAM = "URLINSTAGRAM";
        public static readonly string URLYOUTUBE = "URLYOUTUBE";
        public static readonly string URLSITE = "URLSITE";

        public static readonly string GOOGLEMAPS_API_KEY = "AIzaSyBF7v49sFiHdiht_yCyhwuUsUIAh2f7L8U";

        public static readonly string VALIDA_MARCA_USUARIO = "VALIDA_MARCA_USUARIO";

        public static readonly string DIMENSOESIMGPDFH = "DIMENSOESIMGPDFH";
        public static readonly string DIMENSOESIMGPDFW = "DIMENSOESIMGPDFW";
        public static readonly string OCULTAMARCAPDF = "OCULTAMARCAPDF";

        public static readonly string EDICAOQTDNAGRADE = "EDICAOQTDNAGRADE";
        public static readonly string PERMITEEDITARPRECO = "PERMITEEDITARPRECO";


        public static readonly string NIVELAGRUPAMENTO = "NIVELAGRUPAMENTO";
        public static readonly string MOSTRABTNSEGMENTCLIE = "MOSTRABTNSEGMENTCLIE";


    }
}
