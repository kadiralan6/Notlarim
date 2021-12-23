using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Common.Helper
{ //22,12- 15
    public class MailHelper
    {
       // 22,12- 17   18 notlarımusermanagerde
       public static bool SendMail(string body,string to,string subject,bool isHtml = true)
        {
            //to kime gönderecez. 1 den olur cokda 1 kişi olursa bu coklu olursa asğıgdaki dizayn yapılacak
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }
        public static bool SendMail(string body,List<string> to,string subject,bool isHtml=true)
        {
            bool result = false;
            try
            {
                var message = new MailMessage(); //config helper daki metotdan geliyor
                message.From = new MailAddress(ConfigHelper.Get<string>("MailUser"));
                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;
                //MailHost web configdeyi yazımla aynı olmali

                using (var smtp = new SmtpClient(ConfigHelper.Get<string>("MailHost"), ConfigHelper.Get<int>("MailPort")))
                {
                    smtp.EnableSsl = true;//network using System.Net; bağlantı protokolu
                    smtp.Credentials = new NetworkCredential(ConfigHelper.Get<string>("MailUser"), ConfigHelper.Get<string>("MailPass"));
                    smtp.Send(message);
                    result = true;
                }
            }
            catch (Exception)
            {

                
            }
            return result;
        }
    }
}
