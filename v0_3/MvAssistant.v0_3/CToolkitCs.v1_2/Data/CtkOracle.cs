using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Data
{
    public class CtkOracle
    {

        public const string oracleConnStr = ""//"Provider=MSDAORA.1"
            //+ ";Server=192.168.0.222"
           + ";User ID=" + "system"
           + ";Password=" + "ITRI1106"
           + ";Data Source=";//+dbName (ex:orcl)
        public const string oracleConnStr_remote = "SERVER = ("
            + "DESCRIPTION = (ADDRESS = "
            + "(PROTOCOL = TCP)(HOST = 140.96.179.55)(PORT = 1521))"
            + "(CONNECT_DATA = "
            + "(SERVICE_NAME = orcl))"
            + ");"
            + "uid = system; pwd = ITRI1106;";//用於System.Data.Oracle

        public const string oracleConnStr_remote2 = "Data Source = ("
            + "DESCRIPTION = (ADDRESS = "
            + "(PROTOCOL = TCP)(HOST = 140.96.179.55)(PORT = 1521))"
            + "(CONNECT_DATA = "
            + "(SERVER=DEDICATED)"
            + "(SERVICE_NAME = orcl))"
            + ");"
            + ";User ID=" + "system"
            + ";Password=" + "ITRI1106";//用於OleDb或Oracle.DataAccess
    }
}
