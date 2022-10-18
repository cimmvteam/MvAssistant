using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;

namespace MvaCToolkitCs.v1_2.Net.FtpTx
{
    public class CtkNetFtpTransaction : IDisposable
    {
        public NetworkCredential Credentials;
        public FluentFTP.FtpClient FluentFtp;
        public String Host;
        public bool isUseSslTls = false;

        public bool IsDisposed { get { return this.disposed; } }

        public void Sign()
        {



        }

        public void Close()
        {
            if (this.FluentFtp != null)
            {
                using (var obj = this.FluentFtp)
                    obj.Disconnect();
            }
        }

        public FluentFTP.FtpClient UseFluent()
        {
            lock (this)
            {
                if (this.FluentFtp != null) return this.FluentFtp;

                var rtn = this.FluentFtp = new FluentFTP.FtpClient();
                rtn.Host = this.Host;
                rtn.Credentials = this.Credentials;
                if (this.isUseSslTls)
                {
                    rtn.EncryptionMode = FluentFTP.FtpEncryptionMode.Explicit;
                    rtn.ValidateCertificate += (ss, ee) => { ee.Accept = true; };
                }
                rtn.Connect();
                return rtn;
            }
        }



        public List<String> ListFiles(Uri uri)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.EnableSsl = this.isUseSslTls;

                request.Credentials = this.Credentials;
                using (var response = (FtpWebResponse)request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream);
                    string names = reader.ReadToEnd();

                    responseStream.Close();
                    response.Close();
                    return names.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            try { this.Close(); }
            catch (Exception ex) { CtkLog.Write(ex); }
        }
        #endregion





        #region Static


        public static void TestFluent()
        {
            // connect to the FTP server
            var client = new FluentFTP.FtpClient();
            client.Host = "123.123.123.123";
            client.Credentials = new NetworkCredential("david", "pass123");
            client.Connect();

            // upload a file
            client.UploadFile(@"C:\MyVideo.mp4", "/htdocs/big.txt");

            // rename the uploaded file
            client.Rename("/htdocs/big.txt", "/htdocs/big2.txt");

            // download the file again
            client.DownloadFile(@"C:\MyVideo_2.mp4", "/htdocs/big2.txt");
        }

        public static void TestFluentUpload(String host, String acc, String pwd, String src, String dest)
        {
            // connect to the FTP server
            using (var client = new FluentFTP.FtpClient())
            {
                client.Host = host;
                client.Credentials = new NetworkCredential(acc, pwd);
                client.Connect();
                // upload a file
                client.UploadFile(src, dest);
            }
        }


        #endregion



    }
}
