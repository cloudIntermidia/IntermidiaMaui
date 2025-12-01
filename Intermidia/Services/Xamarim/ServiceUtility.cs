using Intermidia.Intermidia.Infra.Domain;
using Intermidia.Intermidia.Infra.Domain.Commands.Inputs;
using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Intermidia.Intermidia.Infra.Domain.Repositories;
using Newtonsoft.Json;

namespace Intermidia.Services.Xamarim
{
    public static class ServiceUtility
    {
        public static async Task<List<ItemCommandResult>> BuscaStatusPedido(IParametroSincronizacaoRepository _parametroSincronizacaRepository, string codPedido)
        {
            using (var client = new HttpClient())
            {
                var obj = new { PEDIDO = $"{codPedido}" };
                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);
                var url = $"{baseUrl}/StatusPedidoMae";
                // var url = "http://mv.Clio.com.br:8082/Clio_uploadteste/Service.svc/StatusPedidoMae";
                string body = JsonConvert.SerializeObject(obj);
                var retorno = await RestApiHelper.PostStringAsyncPedidoMae(url, body, false, ApiConfig.ReceberJsonCompactadoPedido);

                //var model = new AtualizaStatusPedidoResult(retorno);
                return retorno;
            }
        }

        public static async Task<WcfModelResult> TransmitirFeedback(CriarFeedbackClienteCommand feedback, IParametroSincronizacaoRepository _parametroSincronizacaRepository)
        {
            using (var client = new HttpClient())
            {
                // WcfPedidoModelInput modelRequest = await _carrinhoRepository.BuscarCarrinhoParaTransmissao(codCarrinho);
                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);
                var url = $"{baseUrl}/PutFeedback";
                string body = JsonConvert.SerializeObject(feedback);
                //var retorno = await RestApiHelper.PostStringAsync(url, body, ApiConfig.EnviarJsonCompactadoPedido, ApiConfig.ReceberJsonCompactadoPedido);

                var retorno = await RestApiHelper.PostStringAsync(url, body, ApiConfig.EnviarJsonCompactadoPedido, ApiConfig.ReceberJsonCompactadoPedido);

                var model = new WcfModelResult(retorno);
                return model;
            }
        }


        public static async Task<WcfModelResult> TransmitirPedidoCancelado(ICarrinhoRepository _carrinhoRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository, string codCarrinho)
        {
            using (var client = new HttpClient())
            {
                WcfPedidoModelInput modelRequest = await _carrinhoRepository.BuscarCarrinhoParaTransmissao(codCarrinho, null);
                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);
                var url = $"{baseUrl}/PutCancelarPedido";
                string body = JsonConvert.SerializeObject(modelRequest);
                var retorno = await RestApiHelper.PostStringAsync(url, body, ApiConfig.EnviarJsonCompactadoPedido, ApiConfig.ReceberJsonCompactadoPedido);

                var model = new WcfModelResult(retorno);
                return model;
            }
        }

        public static async Task<WcfModelResult> EnviarMarketingPorEmail(IParametroSincronizacaoRepository _parametroSincronizacaRepository, MarketingCommandResult marketing, string emails)
        {
            using (var client = new HttpClient())
            {
                string body = "{"
                                    + "\"PEDIDOVENDA\": "
                                        + "{\"FileName\": \"" + marketing.NomeArquivo
                                        + "\",\"Titulo\": \"" + marketing.Titulo
                                        + "\",\"Descricao\": \"" + marketing.Descricao
                                        + "\",\"Emails\": \"" + emails
                                        + "\"}"
                               + "}";

                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);
                //baseUrl = @"http://localhost:24022/Service.svc";
                var url = $"{baseUrl}/SendFileEmail";
                var retorno = await RestApiHelper.PostStringAsync(url, body, true, false);

                var model = new WcfModelResult(retorno);
                if (model.EXCEPTION != null && model.EXCEPTION.ToString().ToUpper().Contains("SIZE LIMITS"))
                {
                    model.EXCEPTION = "Tamanho do anexo excede o limite permitido por e-mail.";
                }
                return model;
            }
        }
        public static async Task<WcfModelResult> EnviarPedidoExcelPorEmail(IParametroSincronizacaoRepository _parametroSincronizacaRepository, string codCarrinho, string emails)
        {
            using (var client = new HttpClient())
            {
                string body = "{\"PEDIDOVENDA\": {\"CodCarrinho\": \"" + codCarrinho + "\",\"Emails\": \"" + emails + "\"}}";
                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);
                var url = $"{baseUrl}/PutExportExcel";
                var retorno = await RestApiHelper.PostStringAsync(url, body, true, false);

                var model = new WcfModelResult(retorno);
                return model;
            }
        }
        public static async Task<int> AutenticarNoServidor(IParametroSincronizacaoRepository _parametroSincronizacaRepository, string login, string senha)
        {
            int id = 0;
            using (var client = new HttpClient())
            {
                /*Autentica no servidor*/
                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.DOWNLOADURL);
                var url = $"{baseUrl}/AuthenticateUser?deviceType=tablet&user={login}&pass={senha}";
                var result = await client.GetAsync(url).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultContent);
                var dictionary = list[1];

                if (dictionary.ContainsKey("id"))
                {
                    var obj = dictionary["id"];
                    id = Convert.ToInt32(obj);
                }
                else if (dictionary.ContainsKey("msg"))
                {
                    string msg = dictionary["msg"] as string;
                    msg = msg.ToUpper().Contains("SQL SERVER") || msg.ToUpper().Contains("ORACLE") ? "Não foi possível conectar ao banco de dados do cliente, por favor tente novamente.(Se persistir entre em contato com o administrador do sistema)" : msg;
                    throw new Exception(msg);
                }
            }

            return id;
        }
        public static async Task<bool> UsuarioPrecisaSincronizar(IParametroRepository _parametroRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository)
        {
            string dias = await _parametroRepository.BuscarValorParametro(ParametrosSistema.QTDDIASSINCRONIZACAO);

            // Não tem configuração para validar DIAS SEM SINCRONIZAR
            if (string.IsNullOrEmpty(dias))
                return false;

            var usuarioSinc = await _parametroSincronizacaRepository.BuscarUltimaDataSincUsuario(Session.USUARIO_LOGADO.CodUsuario.ToString());
            if (usuarioSinc == null)
                return true;

            //string ultimaSincronizacao = usuarioSinc.DataUltimaSincronizacao.ToString("yyyy-MM-ddTHH:mm:ss");
            //await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.DATAULTIMAATUALIZACAO);
            DateTime dataAtual = DateTime.Now.Date;
            DateTime dataUltimaAtualizacao = usuarioSinc.DataUltimaSincronizacao.Date; //Convert.ToDateTime(ultimaSincronizacao).Date;
            TimeSpan timeSpan = dataAtual.Subtract(dataUltimaAtualizacao);
            return timeSpan.Days > int.Parse(dias) ? true : false;
        }

        public static async Task<bool> SalvarIdioma(IParametroRepository _parametroRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository, string culture)
        {

            // SalvarParametro

            await _parametroSincronizacaRepository.SalvarParametro("Idioma", culture);

            return true;
        }


        public static async Task<string> getIdioma(IParametroRepository _parametroRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository)
        {

            // SalvarParametro

            var idioma = await _parametroSincronizacaRepository.BuscarValorParametro("Idioma");

            if (string.IsNullOrEmpty(idioma))
            {
                idioma = "pt";
            }

            return idioma;
        }


        public static async Task<bool> TemVersaoNova(IParametroSincronizacaoRepository _parametroSincronizacaRepository)
        {
            string versaoServer, versaoLocal;
            versaoServer = versaoLocal = string.Empty;
            using (var client = new HttpClient())
            {
                string baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.DOWNLOADURL);
                versaoLocal = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.VERSAOSISTEMA_XAM);
                string url = $"{baseUrl}/GetSyncParameter?parametercode=VERSAOSISTEMA_XAM";
                var result = await client.GetAsync(url).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultContent);
                var dictionary = list[1];

                if (dictionary.ContainsKey("value"))
                {
                    versaoServer = dictionary["value"].ToString();
                }
                else if (dictionary.ContainsKey("msg"))
                {
                    string msg = dictionary["msg"] as string;
                    msg = msg.ToUpper().Contains("SQL SERVER") || msg.ToUpper().Contains("ORACLE") ? "Não foi possível conectar ao banco de dados do cliente, por favor tente novamente.(Se persistir entre em contato com o administrador do sistema)" : msg;
                    throw new Exception(msg);
                }
            }
            return versaoServer != versaoLocal;
        }
        public static async Task<int> GetCodInstalacao(IParametroSincronizacaoRepository _parametroSincronizacaRepository, int codUsuario, string ipaddress, string deviceName, string deviceType)
        {
            if (deviceName.Length > 29)
                deviceName = deviceName.Substring(0, 29);

            int codInstalacao = 0;
            using (var client = new HttpClient())
            {
                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.DOWNLOADURL);
                var url = $"{baseUrl}/GetInstallationID?deviceType={deviceType}&machineName={deviceName}&ip={ipaddress}&userCode={codUsuario}";
                var result = await client.GetAsync(url).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                var list = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultContent);
                var dictionary = list[1];

                if (dictionary.ContainsKey("id"))
                {
                    var obj = dictionary["id"];
                    codInstalacao = Convert.ToInt32(obj);
                }
                else
                {
                    throw new Exception($"Ocorreu um erro ao buscar o código de instalação. {dictionary["msg"].ToString()}");
                }
            }

            return codInstalacao;
        }
        public static async Task<string> BuscarVersaoLocal(IParametroSincronizacaoRepository _parametroSincronizacaRepository)
        {
            var versao = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.VERSAOSISTEMA_XAM);
            return "Versão: " + versao;
        }

        public static async Task<WcfModelResult> ValidarCupom(WcfValidarCupomInput command)
        {
            using (var client = new HttpClient())
            {
                IParametroSincronizacaoRepository _parametroSincronizacaRepository = ServiceLocator.Current.GetInstance<IParametroSincronizacaoRepository>();
                var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);
                var url = $"{baseUrl}/ValidarCupom";
                string body = JsonConvert.SerializeObject(command);
                var retorno = await RestApiHelper.PostStringAsync(url, body, true, false);

                var model = new WcfModelResult(retorno);
                return model;
            }
        }

        public static async Task<WcfModelResult> TransmitirPedido(ICarrinhoRepository _carrinhoRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository,
                                                                  string codCarrinho, string codSufarmaCliente, Boolean materialPDV)
        {
            return await Enviar_Generico(Tipo.Enviar_Pedido, _carrinhoRepository, _parametroSincronizacaRepository, codCarrinho, codSufarmaCliente, "", "", "", "", materialPDV);
        }

        //        public static async Task<WcfModelResult> TransmitirCliente(IClienteRepository _clienteRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository, string codPessoaCliente)
        public static async Task<WcfModelResult> TransmitirCliente(IClienteRepository _clienteRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository, string codPessoaCliente,
            String codPessoaRepresentante)
        {
            return await Enviar_Generico(Tipo.Enviar_Cliente, _clienteRepository, _parametroSincronizacaRepository, "", "", codPessoaCliente, codPessoaRepresentante);
        }

        //public static async Task<WcfModelResult> Consultar_XML_NF_ERP(IClienteRepository _clienteRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository, 
        //    string codNuNota)
        //{
        //    return await Enviar_Generico(Tipo.Consultar_XML_NF_ERP, _clienteRepository, _parametroSincronizacaRepository, "", "", "", "", codNuNota);
        //}

        //public static async Task<WcfModelResult> Consultar_XML_NF_API(IClienteRepository _clienteRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository, 
        //                                                              String conteudoXML)
        //{
        //    return await Enviar_Generico(Tipo.Consultar_XML_NF_ERP, _clienteRepository, _parametroSincronizacaRepository, "", "", "", "", "", conteudoXML);
        //}

        public static async Task<byte[]> Consultar_XML_NF(IClienteRepository _clienteRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository,
            string codNuNota)
        {
            return await Enviar_Generico_Byte(Tipo.Consultar_XML_NF, _parametroSincronizacaRepository, codNuNota);
        }

        #region Generico

        private enum Tipo
        {
            Enviar_Pedido,
            Enviar_Cliente,
            Consultar_XML_NF
        }

        private static String retornaURL_LocalHost(String valor)
        {
            String retorno = valor;

            //retorno = "http://localhost:24022/Service.svc";
            //baseUrl = "http://177.136.200.209:8380/Service.svc";

            return retorno;
        }

        private static async Task<WcfModelResult> Enviar_Generico(Tipo tipoServico, Object _carrinhoRepository, IParametroSincronizacaoRepository _parametroSincronizacaRepository,
                                                                  string codCarrinho = "", string codSufarmaCliente = "", string codPessoaCliente = "", String codPessoaRepresentante = "",
                                                                  String codNuNota = "", String conteudoXML = "", Boolean materialPDV = false)
        {
            var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);

            String nomeMetodo = String.Empty;
            String bodyJson = String.Empty;
            Boolean comprimirJson = false;
            Boolean retornoEstaComprimido = false;

            baseUrl = retornaURL_LocalHost(baseUrl);

            switch (tipoServico)
            {
                case Tipo.Enviar_Pedido:

                    #region Enviar_Pedido

                    nomeMetodo = "PutPedido";
                    ICarrinhoRepository objRepository = (ICarrinhoRepository)_carrinhoRepository;
                    WcfPedidoModelInput modelRequest = await objRepository.BuscarCarrinhoParaTransmissao(codCarrinho, codSufarmaCliente, materialPDV);
                    modelRequest.FLG_AMBIENTE = Utils.Settings.Ambiente;
                    bodyJson = JsonConvert.SerializeObject(modelRequest);
                    comprimirJson = ApiConfig.EnviarJsonCompactadoPedido;
                    retornoEstaComprimido = ApiConfig.ReceberJsonCompactadoPedido;

                    #endregion Enviar_Pedido

                    break;

                case Tipo.Enviar_Cliente:

                    #region Enviar_Cliente

                    nomeMetodo = "PutCliente";
                    IClienteRepository objRepository_Cliente = (IClienteRepository)_carrinhoRepository;
                    WcfClienteModelInput modelRequest_Cliente = await objRepository_Cliente.BuscarClienteParaTransmissao(codPessoaCliente);
                    modelRequest_Cliente.FLG_AMBIENTE = Utils.Settings.Ambiente;
                    modelRequest_Cliente.CodPessoaRepresentante = codPessoaRepresentante;
                    bodyJson = JsonConvert.SerializeObject(modelRequest_Cliente);
                    comprimirJson = ApiConfig.EnviarJsonCompactadoPedido;
                    retornoEstaComprimido = ApiConfig.ReceberJsonCompactadoPedido;

                    #endregion Enviar_Cliente

                    break;
            }

            //if (tipoServico == Tipo.Enviar_Cliente)
            //{
            using (var client = new HttpClient())
            {
                var url = $"{baseUrl}/{nomeMetodo}";
                //var retorno = await RestApiHelper.PostStringAsync(url, bodyJson, comprimirJson, retornoEstaComprimido);

                //var retorno;
                WcfModelResult model = null;
                switch (tipoServico)
                {
                    case Tipo.Enviar_Pedido:
                    case Tipo.Enviar_Cliente:
                        var retorno = await RestApiHelper.PostStringAsync(url, bodyJson, comprimirJson, retornoEstaComprimido);
                        model = new WcfModelResult(retorno);
                        break;
                }

                //var model = new WcfModelResult(retorno);
                return model;
            }
            //}
            //else
            //{
            //    return null;
            //}

        }

        private static async Task<byte[]> Enviar_Generico_Byte(Tipo tipoServico, IParametroSincronizacaoRepository _parametroSincronizacaRepository, String codNuNota = "")
        {
            byte[] retorno = null;

            var baseUrl = await _parametroSincronizacaRepository.BuscarValorParametro(ParametrosSistema.UPLOADURL);

            String nomeMetodo = String.Empty;
            Boolean retornoEstaComprimido = false;

            baseUrl = retornaURL_LocalHost(baseUrl);

            switch (tipoServico)
            {
                case Tipo.Consultar_XML_NF:

                    #region Consultar_XML_NF

                    nomeMetodo = String.Format("GetXMLNF?CodNuNota={0}", codNuNota);
                    retornoEstaComprimido = ApiConfig.ReceberJsonCompactadoPedido;

                    #endregion Consultar_XML_NF

                    break;
            }

            using (var client = new HttpClient())
            {
                var url = $"{baseUrl}/{nomeMetodo}";
                switch (tipoServico)
                {
                    case Tipo.Consultar_XML_NF:
                        retorno = await RestApiHelper.GetStringAsync(url, retornoEstaComprimido);
                        break;
                }

                return retorno;
            }
        }

        #endregion Generico

    }

}