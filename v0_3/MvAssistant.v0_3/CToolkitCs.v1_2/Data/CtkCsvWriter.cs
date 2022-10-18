using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Data
{
    public class CtkCsvWriter : StreamWriter
    {
        public string FieldTerminator = ",";
        public char FieldQuotationCharacter = '"';
        public string LineTerminator = "\r\n";
        public string NullSymbol = "null";
        public bool IsForceQuotation = false;


        public CtkCsvWriter(Stream stream) : base(stream) { }
        public CtkCsvWriter(string path) : base(path) { }
        public CtkCsvWriter(string path, bool append) : base(path, append) { }


        public void WriteRow<T>(IEnumerable<T> row)
        {
            var list = new List<string>();
            foreach (var cell in row)
                list.Add(GetCellString(cell));

            var rowStr = string.Join(",", list);
            this.Write(rowStr);
            this.Write(this.LineTerminator);
        }

        public void WriteEntity<T>(T row)
        {
            if (row == null) return;
            var props = row.GetType().GetProperties();
            var list = new List<string>();
            foreach (var pi in props)
            {
                var cell = pi.GetValue(row, null);
                list.Add(GetCellString(cell));
            }

            var rowStr = string.Join(",", list);
            this.Write(rowStr);
            this.Write(this.LineTerminator);
        }


        public string GetCellString(object data)
        {
            if (data == null) return this.NullSymbol;
            var type = data.GetType();
            if (type == typeof(string))
                return string.Format("\"{0}\"", data.ToString().Replace("\"", "\"\""));

            if (this.IsForceQuotation) return string.Format("\"{0}\"", data.ToString().Replace("\"", "\"\""));

            return data.ToString();
        }

    }
}
