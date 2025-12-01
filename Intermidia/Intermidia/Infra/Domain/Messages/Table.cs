namespace Intermidia.Intermidia.Infra.Domain.Messages
{
    public class Table
    {
        public int Id { get; set; }
        public string NomeTabela { get; set; }
        public int Ordem { get; set; }
        public int IndGenerica { get; set; }
        public int MaxRegister { get; set; }
        public string TipoSincronizacao { get; set; }
        public string TipoAcaoTabela { get; set; }
        public string NomeTabelaCliente { get; set; }
        public int IndDelete { get; set; }
    }
}
