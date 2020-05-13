using MaskAutoCleaner.Dal.Entities;
using MvLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MaskAutoCleaner.Dal
{


    public class MsSqlDbMgr : MacDbMgr
    {

        protected string host;

        public MsSqlDbMgr(string host)
        {
            this.host = host;
        }


        public override DbConnection DbBasic_Get()
        {

            var dbName = "MaskAutoCleanerDb";
            var connStr = "";
            if (!this.isOpennedDb.ContainsKey(dbName))
            {
                lock (this)
                {
                    this.isOpennedDb[dbName] = true;
                    connStr = string.Format(@"data source={0};integrated security=True;MultipleActiveResultSets=True;", this.host);
                    using (var dbConn = new SqlConnection(connStr))
                    {
                        dbConn.Open();
                        CreateIfDatabaseIsNotExist(dbConn, dbName);
                        dbConn.ChangeDatabase(dbName);
                        CreateIfTableIsNotExist<mac_state_machine_history>(dbConn);
                        CreateIfTableIsNotExist<mac_alarm_history>(dbConn);
                        CreateIfTableIsNotExist<mac_recipe_param>(dbConn);
                        CreateIfTableIsNotExist<mac_log>(dbConn);
                        dbConn.Close();
                    }
                    GC.Collect();
                }
            }

            connStr = string.Format(@"data source={0};initial catalog={1};integrated security=True;MultipleActiveResultSets=True;", this.host, dbName);
            return new SqlConnection(connStr);
        }


        public override MacDbContext GetDbContext(DbConnection dbConn) { return new MsSqlDbEntities(dbConn as SqlConnection); }


        public void CreateIfDatabaseIsNotExist(SqlConnection dbConn, string dbname)
        {

            var sqlsb = new StringBuilder();
            sqlsb.AppendFormat(@"
                IF DB_ID (N'{0}') IS NULL Begin
	                CREATE DATABASE {0};  
                End

                -- Verify the database files and sizes  
                --SELECT name, size, size*1.0/128 AS [Size in MBs]   
                --FROM sys.master_files  
                --WHERE name = N'{0}';
            ", dbname);

            using (var command = new SqlCommand(sqlsb.ToString(), dbConn))
                command.ExecuteNonQuery();


        }

        public void CreateIfTableIsNotExist<T>(SqlConnection dbConn)
        {
            var table = typeof(T).Name;

            var cols = new List<string>();
            var idxs = new List<string>();
            foreach (var pi in typeof(T).GetProperties())
            {
                var type = pi.PropertyType;
                var sqlType = "nvarchar";
                var sqlNullable = "";
                if (type == typeof(string))
                {
                    var attr = pi.GetCustomAttribute<StringLengthAttribute>();
                    if (attr != null)
                        sqlType = string.Format("nvarchar({0})", attr.MaximumLength);
                    else
                        sqlType = "nvarchar(MAX)";
                    //ntext: microsoft 不建議使用, 在未來版木會移除, 請使用nvarchar(max)取代

                    var colAttr = pi.GetCustomAttribute<ColumnAttribute>();
                    if (colAttr != null)
                    {
                        if (string.Compare(colAttr.TypeName, "xml", true) == 0) sqlType = "xml";
                    }

                }

                if (type == typeof(long) || type == typeof(long?) || type == typeof(int) || type == typeof(int?))
                    sqlType = "int";
                if (type == typeof(double) || type == typeof(double?))
                    sqlType = "real";
                if (type == typeof(float) || type == typeof(float?))
                    sqlType = "float";
                if (type == typeof(Guid))
                    sqlType = "uniqueidentifier";
                if (type == typeof(DateTime) || type == typeof(DateTime?))
                    sqlType = "datetime";
                if (type == typeof(DateTime))
                    sqlNullable = "NOT NULL";



                var pkAttr = pi.GetCustomAttribute<KeyAttribute>();
                var idxAttr = pi.GetCustomAttribute<IndexAttribute>();


                var sqlExtra = @"";
                if (pkAttr != null && sqlType == "int")
                    sqlExtra = @"IDENTITY(1,1) NOT NULL";
                else if (pkAttr != null && sqlType == "uniqueidentifier")
                    sqlExtra = @"ROWGUIDCOL NOT NULL";


                var colSql = string.Format(@"[{0}] {1} {2} {3}", pi.Name, sqlType, sqlNullable, sqlExtra);
                cols.Add(colSql);







                if (pkAttr != null && sqlType == "uniqueidentifier")
                {
                    var idxDesc = string.Format(@"ALTER TABLE [{0}] ADD  CONSTRAINT [{1}]  DEFAULT (newid()) FOR [{2}]", table, "DF_" + table + "_" + pi.Name, pi.Name);
                    idxs.Add(idxDesc);//PK + RowGuid 不可沒有此約束條件
                }

                if (idxAttr != null)
                {
                    var idxDesc = string.Format(@"CREATE INDEX {0} ON {1} ({2})", idxAttr.Name, table, pi.Name);
                    idxs.Add(idxDesc);
                }


            }

            /*
                CREATE TABLE [dbo].[Table_1](
	                [pkid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	                [test1] [datetime] NULL
                ) ON [PRIMARY]
                GO

                ALTER TABLE [dbo].[Table_1] ADD  CONSTRAINT [DF_Table_1_pkid]  DEFAULT (newid()) FOR [pkid]
                GO
            */
            var sqlsb = new StringBuilder();
            sqlsb.AppendFormat("IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='{0}' AND xtype='U') Begin\r\n", table);
            sqlsb.AppendFormat("Create Table {0}(\r\n", table);
            sqlsb.AppendFormat("\t{0}\r\n", string.Join(",\r\n\t", cols));
            sqlsb.AppendFormat(");\r\n");

            sqlsb.AppendFormat("\r\n{0};", string.Join(";\r\n", idxs));

            sqlsb.AppendFormat("\r\nEnd");

            var sql = sqlsb.ToString();

            using (var command = new SqlCommand(sqlsb.ToString(), dbConn))
                command.ExecuteNonQuery();

        }





        #region IDisposable

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                this.DisposeManaged();
            }

            // Free any unmanaged objects here.
            //
            this.DisposeUnmanaged();

            this.DisposeSelf();

            disposed = true;
        }



        void DisposeManaged()
        {
        }

        void DisposeUnmanaged()
        {
        }

        void DisposeSelf()
        {
            try
            {

            }
            catch (Exception ex) { MvLog.Write(ex); }
        }

        #endregion







    }
}
