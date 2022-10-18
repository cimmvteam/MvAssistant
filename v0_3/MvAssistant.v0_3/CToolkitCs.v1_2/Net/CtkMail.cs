using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace CToolkitCs.v1_2.Net
{
    public class CtkMail
    {

        public const string hinet = "msa.hinet.net";
        public const string sonet = "so-net.net.tw";
        public const string taiwanAdsl = "smtp.anet.net.tw";
        public const string googleSmtp = "smtp.gmail.com";
        public const int googlePort = 587;



        public static void SendMailLogin(string smtp, int port, string userid, string password, bool ssl, string send_person, string receiver, string mailTitle, string content)
        {
            System.Net.Mail.SmtpClient MySmtp = new System.Net.Mail.SmtpClient(smtp, port);

            //設定你的帳號密碼

            MySmtp.Credentials = new System.Net.NetworkCredential(userid, password);

            //Gmial 的 smtp 使用 SSL

            MySmtp.EnableSsl = ssl;

            MailMessage msgMail = new MailMessage();
            msgMail.Sender = new MailAddress(send_person);
            msgMail.From = new MailAddress(send_person);

            String[] receiverAry = receiver.Split(',');
            foreach (String receiverPerson in receiverAry)
            { msgMail.To.Add(new MailAddress(receiverPerson)); }

            msgMail.IsBodyHtml = true;
            msgMail.Subject = mailTitle;
            msgMail.Body = content;


            //發送Email
            //MySmtp.Send(send_person, receiver, mailTitle, content);
            MySmtp.Send(msgMail);


        }

        public static void SendMailNoLogin(string argSmtp, string argFrom, string argTo, string argSub, string argContent)
        {
            //SmtpClient mysc = new SmtpClient("msa.hinet.net");//Net
            //SmtpClient mysc = new SmtpClient("220.132.97.187");//Net
            //SmtpClient mysc = new SmtpClient("localhost");//Net
            SmtpClient mysc = new SmtpClient(argSmtp);//Net

            MailMessage msgMail = new MailMessage();//Net
            //System.Web.Mail.MailMessage msgMail = new System.Web.Mail.MailMessage();//Web


            ((MailAddressCollection)msgMail.To).Add(argTo);//Net
            //msgMail.To = "yuchiihsuan@yahoo.com.tw";//web

            //msgMail.Cc = "webmaster@263.Com";//Web

            msgMail.From = new MailAddress(argFrom);//Net
            //msgMail.From = "yuchiihsuan@yahoo.com.tw";//Web

            msgMail.Subject = argSub;//Net,Web

            //msgMail.BodyFormat = MailFormat.Html;//Web

            msgMail.Body = argContent;//Net, Web

            mysc.Send(msgMail);//Net
            //System.Web.Mail.SmtpMail.Send(msgMail);//Web


            //Hinet     msa.hinet.net
            //So-net    so-net.net.tw
            //台灣固網  smtp.anet.net.tw
        }



        public static bool IsValideMail(string argmail)
        {
            if (argmail == null) { return false; }
            if (System.Text.RegularExpressions.Regex.IsMatch(argmail, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$")) { return true; }
            return false;
        }
    }
}
