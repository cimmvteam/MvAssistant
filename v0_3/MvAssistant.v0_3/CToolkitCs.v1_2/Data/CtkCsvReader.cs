using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Data
{
    public class CtkCsvReader : StreamReader
    {

        public CtkCsvEnumReadType LastReadType { get; private set; }


        public CtkCsvReader(Stream stream) : base(stream) { }
        public CtkCsvReader(string path) : base(path) { }



        public CtkCsvEnumReadType ReadValue(dynamic val, string field)
        {
            var rs = "";
            var dynval = val as IDictionary<String, object>;

            var rt = ReadValue(out rs);
            dynval[field] = rs;
            return rt;
        }
        public CtkCsvEnumReadType ReadValue(out dynamic val)
        {
            var rs = "";
            var rt = ReadValue(out rs);
            val = rs;
            return rt;
        }

        public String ReadValue(out CtkCsvEnumReadType rt)
        {
            var rs = "";
            rt = ReadValue(out rs);
            return rs;
        }

        public CtkCsvEnumReadType ReadValue(out string value)
        {

            var c = 0;
            var sb = new StringBuilder();
            while ((c = this.Read()) >= 0)
            {

                if (c == '"')
                    while ((c = this.Read()) >= 0)
                        if (c == '"')
                        {
                            if ((c = this.Read()) == '"')
                                sb.Append((char)c);
                            else break;
                        }
                        else
                            sb.Append((char)c);

                if (c == ',')
                {
                    value = sb.ToString();
                    return this.LastReadType = CtkCsvEnumReadType.Cell;
                }
                else if (c == '\r') { }
                else if (c == '\n')
                {
                    value = sb.ToString();
                    return this.LastReadType = CtkCsvEnumReadType.RowEnd;
                }
                else
                    sb.Append((char)c);
            }

            value = null;

            if (sb.Length == 0)
                return this.LastReadType = CtkCsvEnumReadType.NoData;

            value = sb.ToString();
            return this.LastReadType = CtkCsvEnumReadType.RowEnd;
        }

        public CtkCsvEnumReadType MoveToRowEnd()
        {
            var val = "";
            CtkCsvEnumReadType rt = CtkCsvEnumReadType.Cell;
            while ((rt = ReadValue(out val)) == CtkCsvEnumReadType.Cell) ;
            return rt;
        }


        public static DataTable ReadCsvFile(String file, bool hasHeader = false)
        {
            var rs = new DataTable();
            using (var csv = new CtkCsvReader(file))
            {
                string val = null;
                CtkCsvEnumReadType readtype = CtkCsvEnumReadType.Cell;
                DataRow row = null;
                int idx = 0;

                if (hasHeader)
                {
                    while (readtype == CtkCsvEnumReadType.Cell)
                    {
                        readtype = csv.ReadValue(out val);
                        rs.Columns.Add(val, typeof(string));
                    }
                }
                else
                {
                    while (readtype != CtkCsvEnumReadType.RowEnd)
                    {
                        if (readtype == CtkCsvEnumReadType.RowEnd || row == null)
                        {
                            row = rs.NewRow();
                            idx = 0;
                        }

                        readtype = csv.ReadValue(out val);

                        //一次性的加入Header
                        //不在後面用if是減少效能消耗
                        rs.Columns.Add("Col_" + idx, typeof(string));

                        row[idx] = val;
                        idx++;
                    }
                }

                while (readtype != CtkCsvEnumReadType.NoData)
                {
                    if (readtype == CtkCsvEnumReadType.RowEnd || row == null)
                    {
                        if (row != null) rs.Rows.Add(row);
                        row = rs.NewRow();
                        idx = 0;
                    }

                    readtype = csv.ReadValue(out val);

                    row[idx] = val;
                    idx++;
                }

            }


            return rs;
        }





    }
}
