using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CToolkitCs.v1_2.Cryptography
{
    public class CtkCryptographyRsa : IDisposable
    {
        RSACryptoServiceProvider m_rsa;
        public RSACryptoServiceProvider Rsa { get { return this.m_rsa; } }
        public string XmlPublicKey { get { return this.m_rsa.ToXmlString(false); } }
        public string XmlPrivateKey { get { return this.m_rsa.ToXmlString(true); } }



        public string EncryptString(string content, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            var buffer = encoding.GetBytes(content);


            var encryptes = new List<string>();

            for (var idx = 0; idx * 64 < buffer.Length; idx++)
            {
                var length = 64;
                if (idx * 64 + 64 > buffer.Length) length = buffer.Length - idx * 64;
                var mybuffer = new byte[length];

                Array.Copy(buffer, idx * 64, mybuffer, 0, mybuffer.Length);
                var encrypyBuffer = this.m_rsa.Encrypt(mybuffer, false);
                encryptes.Add(Convert.ToBase64String(encrypyBuffer));
            }

            return string.Join("\n", encryptes);
        }
        public string EncryptJsonObject(object obj, Encoding encoding = null)
        {
            var json = JsonConvert.SerializeObject(obj);
            return this.EncryptString(json);
        }



        public string DecryptString(string encryptedContent, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            var encryptes = encryptedContent.Split('\n');
            var rtn = new StringBuilder();
            foreach (var encrypt in encryptes)
            {
                var encryptBuffer = Convert.FromBase64String(encrypt);
                var buffer = this.m_rsa.Decrypt(encryptBuffer, false);
                rtn.Append(Encoding.UTF8.GetString(buffer));
            }

            return rtn.ToString();
        }
        public T DecryptJsonObject<T>(string encryptedContent, Encoding encoding = null)
        {
            var json = this.DecryptString(encryptedContent, encoding);
            return JsonConvert.DeserializeObject<T>(json);
        }




        #region IDisposable

        // Flag: Has Dispose already been called?
        bool disposed = false;

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



        void DisposeSelf()
        {
            if (this.m_rsa != null)
                using (this.m_rsa) { }
        }


        #endregion


        //=== Static ================================================================


        public static CtkCryptographyRsa FromXml(string xml)
        {
            var rtn = new CtkCryptographyRsa();
            var rsa = new RSACryptoServiceProvider();
            rtn.m_rsa = rsa;

            rsa.FromXmlString(xml);


            return rtn;
        }

        public static CtkCryptographyRsa Create()
        {
            var rtn = new CtkCryptographyRsa();
            var rsa = new RSACryptoServiceProvider();
            rtn.m_rsa = rsa;




            return rtn;
        }

    }
}
