using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Security
{
    public class CtkMd5
    {


        public static string ComputeHash(string val)
        {
            System.Security.Cryptography.MD5 myHash = new System.Security.Cryptography.MD5CryptoServiceProvider();
            myHash.ComputeHash(System.Text.Encoding.Default.GetBytes(val));
            return Convert.ToBase64String(myHash.Hash);
        }

    }
}
