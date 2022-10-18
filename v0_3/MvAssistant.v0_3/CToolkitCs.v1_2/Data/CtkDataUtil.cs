using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace CToolkitCs.v1_2.Data
{
    public class CtkDataUtil
    {


        public static List<Dictionary<string, Object>> ToListDictionary(DataTable dataTable)
        {
            var dataRows = dataTable.Select();
            return (from row in dataRows
                    select row.ItemArray.Select((a, i) => new { Name = dataTable.Columns[i].ColumnName, Value = a })
                       .ToDictionary(x => x.Name, x => x.Value)
                       ).ToList();
        }
    }
}
