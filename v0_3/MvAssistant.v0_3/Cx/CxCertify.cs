using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MvaCodeExpress.v1_1
{
    public class CxCertify
    {

        public string User;
        public string CertifyCode;
        public List<string> Macs = new List<string>();
        public List<string> CertifyMacs = new List<string>();
        public string CertifyAlogPub;
        public DateTime CreateTime = DateTime.Now;

    }
}
