using System.Globalization;

namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class FuncaoGenerica
    {
        public string FormataCNPJ(string valor)
        {
            string retorno = valor;

            if (!string.IsNullOrEmpty(retorno))
            {
                if (retorno.Length == 14)
                    retorno = string.Format("{0}.{1}.{2}/{3}-{4}", retorno.Substring(0, 2), retorno.Substring(2, 3), retorno.Substring(5, 3), retorno.Substring(8, 4), retorno.Substring(12, 2));
            }

            return retorno;
        }

        public string FormataPercentual(decimal valor)
        {
            string retorno = string.Empty;

            if (valor != decimal.MinValue && valor > 0)
            {
                retorno = string.Format("({0}%)", valor);
            }

            return retorno;
        }

        public string FormataCEP(string valor)
        {
            string retorno = valor;

            if (!string.IsNullOrEmpty(retorno))
            {
                if (retorno.Length == 8)
                    retorno = string.Format("{0}.{1}-{2}", retorno.Substring(0, 2), retorno.Substring(2, 3), retorno.Substring(5, 3));
            }

            return retorno;
        }

        public string FormataCPF(string valor)
        {
            string retorno = valor;

            if (!string.IsNullOrEmpty(retorno))
            {
                if (retorno.Length == 11)
                    retorno = string.Format("{0}.{1}.{2}-{3}", retorno.Substring(0, 3), retorno.Substring(3, 3), retorno.Substring(6, 3), retorno.Substring(9, 2));
            }

            return retorno;
        }

        public string Remove_Formatacao_CNPJ_CPF(string valor)
        {
            string retorno = valor;

            if (!string.IsNullOrEmpty(retorno))
            {
                retorno = retorno.Replace("/", "").Replace("-", "").Replace(".", "").Replace("\\", "");
            }

            return retorno;
        }

        public string FormataMoeda(decimal value)
        {
            decimal? moeda = (decimal?)value;
            decimal valorTruncado = 0;
            if (moeda != null)
            {
                //string padrao = "#,##0.00";
                valorTruncado = Math.Truncate(100 * moeda.Value) / 100;
                //return "R$ " + valorTruncado.ToString(padrao);
                return string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorTruncado);
            }
            // var retorno = moeda == null ? string.Empty : $"{string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", moeda)}";
            //var teste = moeda.ToString();
            return string.Empty;
            //return retorno;
        }
    }
//}
}
