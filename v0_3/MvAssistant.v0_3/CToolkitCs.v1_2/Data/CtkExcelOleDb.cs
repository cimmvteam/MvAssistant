using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace CToolkitCs.v1_2.Data
{
    public class CtkExcelOleDb : IDisposable
    {
        public OleDbConnection DbConn { get; private set; }    //連線物件
        List<OleDbCommand> dbCommands = new List<OleDbCommand>();//命令集合
        List<OleDbDataReader> dbDataReaders = new List<OleDbDataReader>();//讀取器集合
        List<OleDbDataAdapter> dbDataAdapters = new List<OleDbDataAdapter>();//讀取器集合




        public const string excelConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;"
            + "Extended Properties=Excel 8.0;"
            + "Data Source=";

        public const string refExcelConnString_00001 = "Provider=Microsoft.Jet.OLEDB.4.0; "
            + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'"
            + "Data Source={0};";//For all data type to string
        public const string refCsvConnString_00001 = @"Provider=Microsoft.Jet.OleDb.4.0; "
               + @"Extended Properties='Text;HDR=YES;FMT=Delimited'; "
               + @"Data Source=A Directory";//CSV, set directory & select file name


        public const string defConnStr = excelConnStr;



        /*=====資料庫連線相關=============================================================*/
        public CtkExcelOleDb()
        {
        }
        public CtkExcelOleDb(string dbName)
        {
            OpenConnection(defConnStr + dbName);
        }
        //解構子-關閉連線
        ~CtkExcelOleDb()
        {
            Close();
        }


        //開啟連線
        public void OpenConnection(string connStr)
        {
            try
            {
                /*OleDb只能在 x86上運行, 請設定應用程式 或 其它 Db 存取方式*/


                Close();
                DbConn = new OleDbConnection(connStr);
                DbConn.Open();
            }
            catch (CtkException ex) { throw new CtkException("連線建立失敗" + ex.Message); }
        }

        //傳回連線是否存在
        public bool IsConnOpen()
        {
            if (DbConn == null) { return false; }
            if (DbConn.State == ConnectionState.Open) { return true; }
            return false;
        }


        /*=========關閉=============================================================*/


        //關閉連線
        public void Close()
        {
            CloseDataAdapter();
            CloseDataReader();
            CloseCommand();
            CloseConnection();
        }
        public void CloseConnection()
        {
            try { if (DbConn != null) { DbConn.Close(); DbConn.Dispose(); } }
            catch (CtkException) { }
        }
        public void CloseCommand()
        {
            lock (dbCommands)
            {
                foreach (OleDbCommand loop in dbCommands)
                {
                    try { CloseCommand(loop); }
                    catch (CtkException) { }
                }
            }
        }
        public void CloseCommand(OleDbCommand argcmd)
        {
            if (argcmd == null) { return; }
            argcmd.Connection = null;
            argcmd.Dispose();
        }
        public void CloseDataReader()
        {
            lock (dbDataReaders)
            {
                foreach (OleDbDataReader loop in dbDataReaders)
                {
                    try { CloseDataReader(loop); }
                    catch (CtkException) { }
                }
            }
        }
        public void CloseDataReader(OleDbDataReader argdr)
        {
            if (argdr == null) { return; }
            argdr.Close();
        }
        public void CloseDataAdapter()
        {
            lock (dbDataAdapters)
            {
                foreach (OleDbDataAdapter loop in dbDataAdapters)
                {
                    try { CloseDataAdapter(loop); }
                    catch (CtkException) { }
                }
            }
        }
        public void CloseDataAdapter(OleDbDataAdapter argda)
        {
            if (argda == null) { return; }
            argda.Dispose();
        }





        /*=========GetDataAdapter=============================================================*/
        public OleDbDataAdapter GetDataAdapter(string sSql)
        {
            OleDbDataAdapter reda = null;
            try
            {
                reda = new OleDbDataAdapter(sSql, DbConn);
                dbDataAdapters.Add(reda);
                return reda;
            }
            finally { CloseDataAdapter(reda); }
        }








        #region Query


        public DataTable Query(string sSql, List<CtkExcelOleDbParam> ctkParams = null)
        {

            var dbParams = new List<OleDbParameter>();
            if (ctkParams != null && ctkParams.Count > 0)
                dbParams = (from row in ctkParams
                            select new OleDbParameter(row.Name, row.Value)
                            {
                                OleDbType = DbTypeOf(row.Value)
                            }).ToList();
            return this.Query(sSql, dbParams);
        }


        public DataTable Query(string sSql, List<OleDbParameter> oledbParams)
        {
            OleDbDataAdapter dataAdapter = null;
            OleDbCommand command = null;
            DataTable rtn = null;
            try
            {
                using (command = new OleDbCommand(sSql, DbConn))
                {
                    if (oledbParams != null && oledbParams.Count > 0)
                        command.Parameters.AddRange(oledbParams.ToArray());

                    dbCommands.Add(command);

                    using (dataAdapter = new OleDbDataAdapter(command))
                    {
                        dbDataAdapters.Add(dataAdapter);
                        rtn = new DataTable();
                        dataAdapter.Fill(rtn);
                    }
                }
            }
            finally { CloseDataAdapter(dataAdapter); }
            return rtn;
        }


        //取得所有資料表(Sheet)
        public DataTable QueryAllTable()
        {
            return DbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //dataTable.Rows[0]["TABLE_NAME"] is table name...
        }



        #endregion




        /*=========Execute=============================================================*/
        public int Execute(string sSql) { return Execute(sSql, null); }
        public int Execute(string sSql, List<OleDbParameter> paraList)
        {
            OleDbCommand mycmd = null;
            try
            {
                mycmd = new OleDbCommand();
                mycmd.Connection = this.DbConn;
                mycmd.CommandText = sSql;
                if (paraList != null) { mycmd.Parameters.AddRange(paraList.ToArray()); }
                return mycmd.ExecuteNonQuery();
            }
            finally { CloseCommand(mycmd); }
        }





        /*=========Excel特殊轉換=============================================================*/
        public string dbStr_yearDate(object argObj)
        {
            return argObj.ToString();
        }
        public string dbStr_year(object argObj)
        {
            return "Year(" + argObj.ToString() + ")";
        }
        public string dbStr_month(object argObj)
        {
            return "Month(" + argObj.ToString() + ")";
        }
        public string dbStr_day(object argObj)
        {
            return "Day(" + argObj.ToString() + ")";
        }


        public static OleDbType DbTypeOf(Object value)
        {
            var valueType = value.GetType();
            if (valueType == typeof(String))
                return OleDbType.VarChar;
            if (valueType == typeof(Int32))
                return OleDbType.Integer;
            if (valueType == typeof(Int16))
                if (valueType == typeof(Byte))
                    return OleDbType.TinyInt;


            throw new ArgumentException("Non-define DbType");
        }




        #region IDisposable

        // Flag: Has Dispose already been called?
        protected bool disposed = false;
        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }
        protected virtual void DisposeSelf()
        {
            try
            {
                this.CloseDataAdapter();
                this.CloseCommand();
                this.CloseDataReader();
                this.CloseConnection();
            }
            catch (Exception ex) { CtkLog.Write(ex); }
        }


        #endregion


    }
}
