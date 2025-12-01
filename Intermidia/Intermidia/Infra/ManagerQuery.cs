using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Intermidia.Intermidia.Infra
{
    public static class ManagerQuery
    {
        public static string FormatScriptFromStream(string fileName, string folderName, Stream stream, object parameters)
        {
            if (stream == null)
                throw new System.Exception($"Arquivo não encontrada.\n {fileName}");

            string text = string.Empty;
            using (var reader = new System.IO.StreamReader(stream))
                text = reader.ReadToEnd();

            return FormatScriptFromString(text, folderName, parameters, fileName);
        }
        public static string FormatScriptFromString(string text, string folderName, object parameters, string fileName = "")
        {
            if (parameters != null)
                foreach (var prop in parameters.GetType().GetRuntimeProperties())
                {
                    if (prop.GetValue(parameters) != null)
                    {
                        if (text.Contains($"FUNCTION_SPLIT('@{prop.Name}')"))
                        {
                            string value = prop.GetValue(parameters).ToString();
                            string[] splitValues = value.Split(',');
                            value = $"'{string.Join("','", splitValues)}'";
                            text = text.Replace($"FUNCTION_SPLIT('@{prop.Name}')", value);
                        }

                        if (prop.GetValue(parameters) is string)
                        {
                            /*Primeiro trata os likes...*/
                            if (text.Contains($"'%@{prop.Name}%'") || text.Contains($"'%@{prop.Name}'") || text.Contains($"'@{prop.Name}%'"))
                            {
                                text = text.Replace($"'%@{prop.Name}%'", $"'%{prop.GetValue(parameters).ToString()}%'");
                                text = text.Replace($"'%@{prop.Name}'", $"'%{prop.GetValue(parameters).ToString()}'");
                                text = text.Replace($"'@{prop.Name}%'", $"'{prop.GetValue(parameters).ToString()}%'");
                            }
                            text = text.Replace($"'@{prop.Name}'", $"'{prop.GetValue(parameters).ToString()}'");
                            text = text.Replace($"@{prop.Name} ", $"{prop.GetValue(parameters).ToString()}");
                        }
                        else if (prop.GetValue(parameters) is decimal || prop.GetValue(parameters) is int || prop.GetValue(parameters) is double || prop.GetValue(parameters) is float)
                            text = text.Replace($"'@{prop.Name}'", $"{prop.GetValue(parameters).ToString().Replace(",", ".")}");
                        else if (prop.GetValue(parameters) is DateTime)
                        {
                            DateTime data = (DateTime)prop.GetValue(parameters);
                            if (data.Date != DateTime.MinValue.Date)
                            {
                                text = text.Replace($"'@{prop.Name}'", $"'{data.ToString("yyyy-MM-ddTHH:mm:ss")}'");
                            }
                            else
                            {
                                if (folderName != "CRUD")
                                {
                                    text = text.Replace($"@{prop.Name}", "-1");
                                }
                                else
                                {
                                    text = text.Replace($"'@{prop.Name}'", "NULL");
                                }
                            }
                        }
                    }
                    else
                    {
                        if (folderName == "CRUD")
                            text = text.Replace($"'@{prop.Name}'", "null");
                        else
                        {

                            if (text.Contains($"FUNCTION_SPLIT('@{prop.Name}')"))
                                text = text.Replace($"FUNCTION_SPLIT('@{prop.Name}')", "('-1')");

                            text = text.Replace($"@{prop.Name}", "-1");
                        }
                    }
                }

            //Debug.WriteLine($"\n========{fileName}=========\n");
            //Debug.WriteLine($"{text}");

            return text.Trim();
        }

        public static string MakeUpdate(List<string> campos, string tabela, List<string> camposWhere, object parameters)
        {
            var properties = parameters.GetType().GetRuntimeProperties().Select(x => x.Name);
            campos = campos.Where(x => properties.Contains(x)).ToList();
            string columns = string.Empty;
            string where = string.Empty;
            foreach (var campo in campos)
                columns += $"{campo} = '@{campo}',";

            foreach (var campo in camposWhere)
                where += $"{campo} = '@{campo}' AND ";

            columns = columns.Substring(0, columns.Length - 1);
            where = where.Substring(0, where.Length - 4);
            string sql = $"UPDATE {tabela} SET {columns}  WHERE {where}";
            return  FormatScriptFromString(sql, "CRUD", parameters, tabela);
        }

        public static string MakeInsertOrReplace(List<string> campos, string tabela, object parameters)
        {
            string columns = string.Empty;
            string values = string.Empty;
            var properties = parameters.GetType().GetRuntimeProperties().Select(x => x.Name);
            campos = campos.Where(x => properties.Contains(x)).ToList();
            foreach (var campo in campos)
            {
                columns += $"{campo},";
                values += $"'@{campo}',";
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            string sql = $"INSERT OR REPLACE INTO {tabela} ({columns}) VALUES ({values})";
            return FormatScriptFromString(sql, "CRUD", parameters, tabela);
        }

        public static string MakeSql(string fileName, string folderName, object parameters)
        {
            string fileFullName = $"Seculus.Core.DataBase.{folderName}.{fileName}";
            var assembly = typeof(ManagerQuery).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(fileFullName);
            return FormatScriptFromStream(fileName, folderName, stream, parameters);
        }
    }
}
