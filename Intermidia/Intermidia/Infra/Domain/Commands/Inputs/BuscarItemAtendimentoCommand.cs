namespace Intermidia.Intermidia.Infra.Domain.Commands.Inputs
{
    public class BuscarItemAtendimentoCommand
    {
        public BuscarItemAtendimentoCommand(string codProduto, string codDeposito, string codGrade, string codAtendimento)
        {
            CodProduto = codProduto;
            CodDeposito = codDeposito;
            CodGrade = codGrade;
            CodAtendimento = codAtendimento;
        }

        public string CodProduto { get; set; }
        public string CodDeposito { get; set; }
        public string CodGrade { get; set; }
        public string CodAtendimento { get; set; }
    }


}
