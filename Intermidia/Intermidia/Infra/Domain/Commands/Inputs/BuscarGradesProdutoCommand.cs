namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarGradesProdutoCommand
    {

        // Campos novos em um construtor que já é utilizado precisa ser opcional e na ultima posição disponível
        public BuscarGradesProdutoCommand(string codAtendimento,
                                          string codProduto,
                                          string codEstoque,
                                          string codModelo = null,
                                          DateTime? dataEntrega = null,
                                          string codGrade = null,
                                          string codItemProntaEntrega = null,
                                          string codTabelaPreco = null
                                          )
        {
            CodAtendimento = codAtendimento;
            CodProduto = codProduto;
            CodEstoque = codEstoque;
            CodModelo = codModelo;
            DataEntrega = dataEntrega;
            CodGrade = codGrade;
            CodItemProntaEntrega = codItemProntaEntrega;
            CodTabelaPreco = codTabelaPreco;
        }

        public string CodAtendimento { get; set; }
        public string CodItemProntaEntrega { get; set; }
        public string CodEstoque { get; set; }
        public string CodProduto { get; set; }
        public string CodModelo { get; set; }
        public string CodGrade { get; set; }
        public string ValidaEstoque { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string CodTabelaPreco { get; set; }
    }



}
