using MvaCToolkitCs.v1_2.Cryptography;
using MvaCToolkitCs.v1_2.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExpress.v1_1Core
{
    public class CxUtil
    {
        //我擁用 Server端 Public Key, 用以編碼訊息給 Server
        public const string RsaServerMsgPubKey = @"<RSAKeyValue><Modulus>3O3k2dDUGDCW6WxK3ryfjMQQmFHQhRDa0SoCUvEe8ewIpvMiLp6st04DtLLEWot4zvWuvZ+dkhsUCbi6L+5h/D6rUq0KHY3QwWbUGD4f9Re9gHpIzEVaFCE/QvxBoWV2FK1C+Zy3wTezzuLAs/BVgLvjyTxl9ErcZiQzzmZcZJ0=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        //我擁用 Client端 Full Key, 用以解碼訊息來自 Server 的訊息
        public const string RsaClienMsgKey = @"<RSAKeyValue><Modulus>xlWQahaXHb8qkjz6TqNjEBPWtFkaGK3qMR3TU4aFIOrkbUCc2RCD9ZmablbbeeWHZ1MQS03Wuf7pugk5a+gNebyxLcanqEg3sCdXijQOBBRDrrTmEeXGKnM9/utHFn3zdQmZT9Uyn0UKhLoaYJl+PFlg+kxLjWNLPYIMq3ognTE=</Modulus><Exponent>AQAB</Exponent><P>0ws+JUHRijobYiCIoseZrZn8B2I4NJQh4WZkg5CcMJMFaIwP6SDX1zgW+FYSCQuPGQf9wHe8jIz6QBNmXrIc7w==</P><Q>8JU69sdR1j52nMDpMu7bpCaWuxmH2X7mIlCWzvmptRQD+coYaBN5Y9fCp1aleYQJqXAV2/fn474UKyqigH8n3w==</Q><DP>r5A/v5C0dvAFzajQ4dF4B5H8tkvABAVi9fZPEZ+gP0xfCGzT45U92T1A5o/7aujhvuoF7zHseWPh3qIE3CwBow==</DP><DQ>UerOL/cAU43jP06kWNZh7UvCaSSxUApYe/iq0QDLmz+cdvIsS0vOOoPd4LKtF2oxDAOBPev0hmRTY7tZp50kSw==</DQ><InverseQ>TOmMYW1UC3bI7SiIKMsP3P8JkaPsAQtUqg+E5q0+t5TyYL7oTX1tIPYf2fSKcebhu/J6RA+wfozm9pJ7dcfGfA==</InverseQ><D>cSHHkaw3IOG1n9smkIrEbBxU4WTd7OvvZXr023UlJUS3bdZoYaz5CLRIkd37rYmO9hI8fif1bv0IR3URiwq//ApXTCby6y7iJoq/6mCOI0Gy1dMucGbQqLjjU9rhAT+Of+3V8YlqGwu4L7eQi94n26quSUBQ+Yz7MuEYl0o3cr0=</D></RSAKeyValue>";
        //我擁用 Certify Private Key, 用以解碼來自 Server 的驗證資料
        public const string RsaCertifyKeyPri = @"<P>49E8nW1o5R0rlp3uOPKKXFTOx5s8lnDPcfR0LXYcBE9jbC/FAlTJkGzheYde9D+d4m+gHAPNX54jf4TwilVBhw==</P><Q>32vbOuWkYusmUVJousewzC5BgXGCiefnd3EGCJPh9z3mDBY9132VWtxoe6B9+MaOTnk7Kl3J73TQDsSU9c1aUw==</Q><DP>tbL2M2FF3iaL008W5xiavdVuLsljY2GKMosT9gZThiSDWZAoAxn4wVX152XOl+P9WtJI0s89h9uq2FQBPXOOFQ==</DP><DQ>emO+M7Rq6dlBI1lOj8smeaN5NOy2gs8sCE087eP87BDXfZWNiQd9ksmc3uRbPbWfbTOSCrJYFbna48wigchdiw==</DQ><InverseQ>brBDmpUA3m3vnGo3DEaGVcrD/S0oP8ZPdO6epOVAgG6uTrg6wfF4htovpCAcFFcqshF58ksO0pidWVDwUYKO9Q==</InverseQ><D>fPSex/m4ZuUmKQ47OTAz83KEga9WvBQNHYsUOdqmV1axw2vH3IFwd2dVccdrbUOSiWw6g9AAlOf3JifU5/cFPXDK0WBFvmWX0vCdhnDZBjjc60yBlVfjGBWI/zZciG+6zXgB8w5uRt2+KLvnABpLFbFpKqoY+lIJUHgnOelQsjE=</D></RSAKeyValue>";

        static bool m_hadLicense = false;
        public static void SetLicense() { m_hadLicense = true; }
        public static bool HadLicense() { return m_hadLicense; }


        public static void CheckLicenseEncrypt()
        {
            if (CxUtil.HadLicense()) return;

            var fiLicense = new FileInfo("cx.lf");//license file
            var fiRequest = new FileInfo("cx.lreq");//license request
            var fiUser = new FileInfo("cx.luser");//license user

            if (fiLicense.Exists)
            {
                CxCertify cxCertify = null;
                using (var rsa = CtkCryptographyRsa.FromXml(CxUtil.RsaClienMsgKey))
                {
                    var content = File.ReadAllText(fiLicense.Name);
                    cxCertify = rsa.DecryptJsonObject<CxCertify>(content);
                }


                if (cxCertify.User == "CwTech")
                {
                    CxUtil.SetLicense();
                    //AppDomain currentDomain = AppDomain.CurrentDomain;
                    //foreach (var assembly in currentDomain.GetAssemblies())
                    //{
                    //    if (assembly.FullName.Contains("SensingNet"))
                    //    {
                    //        CxUtil.SetLicense();
                    //        return;
                    //    }
                    //}
                }



                using (var rsa = CtkCryptographyRsa.FromXml(cxCertify.CertifyAlogPub + CxUtil.RsaCertifyKeyPri))
                {
                    var certifies = new List<string>();
                    foreach (var mac in cxCertify.CertifyMacs)
                        certifies.Add(rsa.DecryptString(mac));

                    foreach (var mac in cxCertify.Macs)
                    {
                        var cnt = certifies.Where(x => x == string.Format("{0}/{1}", cxCertify.User, mac)).Count();
                        if (cnt > 0)
                        {
                            CxUtil.SetLicense();
                            return;
                        }
                    }


                    System.Diagnostics.Debug.Assert(false, "License is wrong", "");
                }

            }
            else
            {
                if (!fiRequest.Exists)
                {
                    if (fiUser.Exists)
                    {
                        var cxCertify = new CxCertify();
                        cxCertify.Macs = CtkNetUtil.GetMacAddressEnthernet();

                        var lines = File.ReadAllLines(fiUser.Name);
                        foreach (var line in lines)
                        {
                            var data = line.Split('=');
                            switch (data[0])
                            {
                                case "User": cxCertify.User = data[1]; break;
                                case "CertifyCode": cxCertify.CertifyCode = data[1]; break;
                            }
                        }
                        using (var rsa = CtkCryptographyRsa.FromXml(CxUtil.RsaServerMsgPubKey))
                            File.WriteAllText(fiRequest.Name, rsa.EncryptJsonObject(cxCertify));
                    }
                    else
                    {
                        File.WriteAllText(fiUser.Name, "User=\n" + "CertifyCode=\n");
                        System.Diagnostics.Debug.Assert(false, "Please fill infomation in CodeExpress.license.user", "");
                    }
                }

                System.Diagnostics.Debug.Assert(false, "Please send CodeExpress.license.request to us for get license",
                    "if you want to re-enter your user information, please delete CodeExpress.license.user and CodeExpress.license.request files");

            }



        }


        public static void CheckLicenseNonEncrypt()
        {
            if (CxUtil.HadLicense()) return;

            var fiLicense = new FileInfo("cx.lf");//license file
            var fiRequest = new FileInfo("cx.lreq");//license request

            if (fiLicense.Exists)
            {
                CxCertify cxCertify = null;
                using (var rsa = CtkCryptographyRsa.FromXml(CxUtil.RsaClienMsgKey))
                {
                    var content = File.ReadAllText(fiLicense.Name);
                    cxCertify = rsa.DecryptJsonObject<CxCertify>(content);
                }


                if (String.Compare(cxCertify.User, "CwTech", true) == 0)
                {
                    CxUtil.SetLicense();
                    return;
                }

                if (String.Compare(cxCertify.User, "Test", true) == 0)
                {
                    if (DateTime.Now < cxCertify.CreateTime.AddYears(3))
                    {
                        CxUtil.SetLicense();
                        return;
                    }
                }




                using (var rsa = CtkCryptographyRsa.FromXml(cxCertify.CertifyAlogPub + CxUtil.RsaCertifyKeyPri))
                {
                    var certifies = new List<string>();
                    foreach (var mac in cxCertify.CertifyMacs)
                        certifies.Add(rsa.DecryptString(mac));

                    foreach (var mac in cxCertify.Macs)
                    {
                        var cnt = certifies.Where(x => x == string.Format("{0}/{1}", cxCertify.User, mac)).Count();
                        if (cnt > 0)
                        {
                            CxUtil.SetLicense();
                            return;
                        }
                    }


                    System.Diagnostics.Debug.Assert(false, "License is wrong", "");
                }

            }
            else
            {
                if (!fiRequest.Exists)
                {
                    var cxCertify = new CxCertify();
                    cxCertify.Macs = CtkNetUtil.GetMacAddressEnthernet();

                    using (var rsa = CtkCryptographyRsa.FromXml(CxUtil.RsaServerMsgPubKey))
                        File.WriteAllText(fiRequest.FullName, JsonConvert.SerializeObject(cxCertify));
                }


                System.Diagnostics.Debug.Assert(false, "Please fill user name and certify code in cx.lreq, " +
                    "and then send cx.lreq to us for get license file. ",
                    "if you want to re-generate computer information , please delete cx.lreq...");

            }



        }


    }
}

