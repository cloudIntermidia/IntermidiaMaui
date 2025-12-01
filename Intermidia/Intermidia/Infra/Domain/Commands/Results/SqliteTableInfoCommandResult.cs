namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class SqliteTableInfoCommandResult
    {
        public string cid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string notnull { get; set; }
        public string dflt_value { get; set; }
        public int pk { get; set; }
    }
}
