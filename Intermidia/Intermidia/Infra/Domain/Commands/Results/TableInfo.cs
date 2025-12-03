namespace Intermidia.Intermidia.Infra.Domain.Commands.Results
{
    public class TableInfo
    {
        public TableInfo()
        {

        }

        public TableInfo(string columnName, string columnValue, string dataType = "System.String")
        {
            ColumnName = columnName;
            DataType = dataType;
            ColumnValue = columnValue;
        }

        public string ColumnName { get; set; }
        public string DataType { get; set; }

        public string ColumnValue { get; set; }
    }
}
