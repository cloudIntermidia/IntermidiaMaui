namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class WcfModelResult
    {
        public string CodCarrinho { get; set; }
        public object SUCCESS { get; set; }
        public object CODMENSAGEM { get; set; }
        public object CODIGO { get; set; }
        public object EXCEPTION { get; set; }
        public object SITUACAO { get; set; }
        public object VALOR_PEDIDO { get; set; }

        public WcfModelResult(List<Dictionary<string, object>> result)
        {
            foreach (var item in result)
            {
                if (item.ContainsKey("SUCCESS"))
                    SUCCESS = item["SUCCESS"];
                if (item.ContainsKey("CODMENSAGEM"))
                    CODMENSAGEM = item["CODMENSAGEM"];
                if (item.ContainsKey("CODIGO"))
                    CODIGO = item["CODIGO"];
                if (item.ContainsKey("EXCEPTION"))
                    EXCEPTION = item["EXCEPTION"];
                if (item.ContainsKey("SITUACAO"))
                    SITUACAO = item["SITUACAO"];
                if (item.ContainsKey("VALOR_PEDIDO"))
                    VALOR_PEDIDO = item["VALOR_PEDIDO"];
            }

        }
    }

}
