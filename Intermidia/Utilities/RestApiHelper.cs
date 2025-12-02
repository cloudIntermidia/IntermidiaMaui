using Intermidia.Intermidia.Infra.Domain.Commands.Results;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Intermidia.Utilities
{
    public static class RestApiHelper
    {
        public static async Task<MemoryStream> DownloadFileAsync(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new Exception("Identificamos você está sem conexão a internet,\nfavor conectar a internet para buscar dados no servidor.");
            }

            try
            {
                var stream = new MemoryStream();
                using (var httpClient = new HttpClient())
                {
                    var downloadStream = await httpClient.GetStreamAsync(new Uri(url));
                    if (downloadStream != null)
                    {
                        await downloadStream.CopyToAsync(stream);
                    }
                }

                return stream;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return null;
            }
        }

        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 5, 0);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }


        public static async Task<List<Dictionary<string, object>>> PostStringAsync(string url, string json, bool comprimirJson, bool retornoEstaComprimido)
        {
            using (var client = GetHttpClient())
            {
                string jsonCompactado = comprimirJson ? StringHelper.CompressString(json) : json;
                HttpContent content = new StreamContent(StringHelper.GenerateStreamFromString(jsonCompactado));

#if DEBUG
                Debug.WriteLine($"JSON ABERTO: {json}");
                Debug.WriteLine($"JSON COMPACTADO: {jsonCompactado}");
                Debug.WriteLine($"URL : {url}");
#endif

                var result = await client.PostAsync(url, content).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (resultContent.Contains("<html>"))
                    resultContent = resultContent.Split(new string[] { "<html>" }, StringSplitOptions.RemoveEmptyEntries)[0];

                return retornoEstaComprimido ? JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(StringHelper.DecompressString(resultContent))
                                     : JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(resultContent);
            }
        }

        public static async Task<List<ItemCommandResult>> PostStringAsyncPedidoMae(string url, string json, bool comprimirJson, bool retornoEstaComprimido)
        {
            using (var client = GetHttpClient())
            {
                string jsonCompactado = comprimirJson ? StringHelper.CompressString(json) : json;
                HttpContent content = new StreamContent(StringHelper.GenerateStreamFromString(jsonCompactado));

#if DEBUG
                Debug.WriteLine($"JSON ABERTO: {json}");
                Debug.WriteLine($"JSON COMPACTADO: {jsonCompactado}");
                Debug.WriteLine($"URL : {url}");
#endif

                var result = await client.PostAsync(url, content).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (resultContent.Contains("<html>"))
                    resultContent = resultContent.Split(new string[] { "<html>" }, StringSplitOptions.RemoveEmptyEntries)[0];

                return JsonConvert.DeserializeObject<List<ItemCommandResult>>(resultContent);
            }
        }

        public static async Task<byte[]> GetStringAsync(string url, bool retornoEstaComprimido)
        {
            using (var client = GetHttpClient())
            {
                var result = await client.GetAsync(url).ConfigureAwait(false);
                var resultContent = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<byte[]>(resultContent);
            }
        }
    }

    //public static class API_Generica
    //{
    //    //public API_Generica()
    //    //{
    //    //    //tipoCampanha = tpCampanha;
    //    //    //ehAdminTemp = ehAdmin;
    //    //    //DefineCampanha();
    //    //}


    //    private static String processaHttpClient_GET(HttpClient oHttpClient, String token, String UrlApi)
    //    {
    //        ServicePointManager.Expect100Continue = true;
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

    //        oHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    //        var retornoJson = oHttpClient.GetAsync($"{UrlApi}").Result.Content.ReadAsStringAsync().Result;
    //        return retornoJson;
    //    }

    //    private static String processaHttpClient_POST(HttpClient oHttpClient, String metodoAPI, String requestBody, Boolean consideraConsultaToken = false)
    //    {
    //        ServicePointManager.Expect100Continue = true;
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

    //        //if (!consideraConsultaToken)
    //        //{
    //        //    String token = String.Empty;
    //        //    if (!ehAdminTemp)
    //        //        token = HttpContext.Current.Session["TokenAcessoAPI"].ToString();
    //        //    else
    //        //        token = AdminTokenTemp;

    //        //    oHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    //        //}

    //        //var retornoJson = oHttpClient.GetAsync($"{UrlApi}/{metodoAPI}").Result.Content.ReadAsStringAsync().Result;

    //        var content = new StringContent(requestBody, Encoding.UTF8, "application/json");


    //        var retornoJson = oHttpClient.PostAsync(metodoAPI, content).Result.Content.ReadAsStringAsync().Result;


    //        return retornoJson;
    //    }

    //    public static async Task<String> PostText(String urlAPI, String textoMensagem)
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            //var requestBody = Newtonsoft.Json.JsonConvert.SerializeObject(new
    //            //{
    //            //    //token = token,
    //            //    pontos = Convert.ToInt32(Math.Ceiling(pontos)).ToString(new System.Globalization.CultureInfo("en-US"))
    //            //});

    //            //var retornoJson = processaHttpClient_POST(client, "reservar-pontos", requestBody);
    //            var retornoJson = processaHttpClient_POST(client, urlAPI, textoMensagem);

    //            retornoJson = TrataJSON(retornoJson);

    //            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<String>(retornoJson);

    //            //if (!resultado.Sucesso)
    //            //    throw new Exception(resultado.Mensagem);

    //            //if (resultado == null || resultado.dados == null || string.IsNullOrEmpty(resultado.dados.extratoId))
    //            //    throw new Exception("Falha ao reservar pontos");

    //            return resultado;
    //        }
    //    }


    //    private static String TrataJSON(String valor)
    //    {
    //        String retorno = valor;

    //        String textoTratar = "\"dados\":[],";

    //        if (retorno.Contains(textoTratar))
    //        {
    //            retorno = retorno.Replace(textoTratar, "");
    //        }

    //        return retorno;
    //    }



    //    //public class RetornoAPI
    //    //{
    //    //    //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    //    //    public UsuarioWsServico dados { get; set; }
    //    //    //public List<UsuarioWsServico> dados { get; set; }
    //    //    public Boolean Sucesso { get; set; }
    //    //    public string Mensagem { get; set; }
    //    //    public int statusCode { get; set; }
    //    //    public string message { get; set; }

    //    //    public String messageTratada
    //    //    {
    //    //        get
    //    //        {
    //    //            String retorno = String.Empty;

    //    //            //if (!String.IsNullOrEmpty(message))
    //    //            //{
    //    //            //    retorno = message;
    //    //            //    switch (statusCode)
    //    //            //    {
    //    //            //        case 401: //Unauthorized
    //    //            //            retorno = "Token Inválido!";
    //    //            //            break;
    //    //            //    }
    //    //            //}

    //    //            if (!String.IsNullOrEmpty(message))
    //    //                retorno = message;
    //    //            else if (!String.IsNullOrEmpty(Mensagem))
    //    //                retorno = Mensagem;
    //    //            else if (!String.IsNullOrEmpty(error))
    //    //                retorno = error;

    //    //            if (!String.IsNullOrEmpty(retorno))
    //    //            {
    //    //                switch (statusCode)
    //    //                {
    //    //                    case 401: //Unauthorized
    //    //                        retorno = "Token Inválido!";
    //    //                        break;
    //    //                }
    //    //            }

    //    //            return retorno;
    //    //        }
    //    //    }

    //    //    public string access_token { get; set; }
    //    //    public string error { get; set; }

    //    //}

    //    //public class RetornoUsuarioWsServico_List
    //    //{
    //    //    public List<UsuarioWsServico> dados { get; set; }
    //    //    public Boolean Sucesso { get; set; }
    //    //    public string Mensagem { get; set; }
    //    //}

    //}
}