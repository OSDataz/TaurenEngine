namespace ExcelDataReader
{
    public class DataSet
    {
        public DataSet()
        {
            Tables = new DataTableCollection();
        }

        public DataTableCollection Tables { get; set; }

        public void AcceptChanges()
        {
        }

        public override string ToString()
        {
            if (Tables == null || Tables.Count == 0)
            {
                return "无数据";
            }

            string str = "";

            foreach (var table in Tables)
            {
                int rowCount = table.Rows.Count;
                int colCount = table.Columns.Count;

                if (!string.IsNullOrEmpty(str))
                    str += "\n";

                str += $"表名：{table.TableName} 行数：{rowCount} 列数：{colCount} --------------------------";

                for (var i = 0; i < rowCount; ++i)
                {
                    var row = table.Rows[i];
                    str += $"\nline {i}：";

                    for (var j = 0; j < colCount; ++j)
                    {
                        str += row[j] + " ";
                    }
                }
            }

            return str;
        }
    }
}
