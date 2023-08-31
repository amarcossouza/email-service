using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;



namespace SISCOMAN_MAIL_SERVICE
{
    class Mail
    {


        public void sendMailInsp(Dictionary<int, DateTime> codes, List<string> mails)
        {
            MailConfig mailConfig = new MailConfig();
            Config config = mailConfig.getConfigData();
            if (config.Enable)
            {
                SmtpClient client = new SmtpClient(config.Host, config.Port);

                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(config.Sender, config.Pass);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(config.Sender, config.Dsp);


                Console.WriteLine("mailssss: " + mails.Count);
                mail.To.Add("contato@tspro.com.br");
                foreach (string i in mails)
                {
                    mail.To.Add(new MailAddress(i));

                    Console.WriteLine(i);

                }


                  mail.Subject =  $"dados do subject";
                string mailBody = $"<b style='color:red;'>TSPRO SISCOMAN INFORMA!</b>";


                foreach (var i in codes)
                {
                    mailBody += $"<p> A MANGUEIRA Nº <b style='color:red;'>{i.Key}</b> ESTÁ NO PERÍODO DE PREVENTIVA, SENDO SUA ÚLTIMA REVISÃO REALIZADA EM <b>{i.Value}</b>.</p>";
                    Console.WriteLine(i.Key);

                }
                mail.Body = mailBody;

                mail.IsBodyHtml = true;
                
                try
                {
                    client.SendMailAsync(mail);
                    Console.WriteLine("send");

                }
                catch (Exception erro)
                {
                    Console.WriteLine(erro.Message);
                }
                finally
                {
                    mail = null;
                }
            }
        }


        public void sendMailVenc(Dictionary<int, DateTime> codes, List<string> mails)
        {
            MailConfig mailConfig = new MailConfig();
            Config config = mailConfig.getConfigData();
            if (config.Enable)
            {
                SmtpClient client = new SmtpClient(config.Host, config.Port);

                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(config.Sender, config.Pass);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(config.Sender, config.Dsp);


                Console.WriteLine("mailssss: " + mails.Count);
                mail.To.Add("contato@tspro.com.br");
                foreach (string i in mails)
                {
                    mail.To.Add(new MailAddress(i));

                    Console.WriteLine(i);

                }


                mail.Subject = $"TSPRO SISCOMAN INFORMA! MANGUEIRAS COM PRAZO DE VALIDADE PRÓXIMO DO FIM!";
                string mailBody = $"<b style='color:red;'>TSPRO SISCOMAN INFORMA!</b>";


                foreach (var i in codes)
                {
                    mailBody += $"<p> A MANGUEIRA Nº <b style='color:red;'>{i.Key}</b> ESTÁ PRÓXIMA DE VENCER, SENDO SUA DATA DE VALIDADE <b>{i.Value}</b>.</p>";
                    Console.WriteLine(i.Key);

                }
                mail.Body = mailBody;

                mail.IsBodyHtml = true;

                try
                {
                    client.SendMailAsync(mail);
                    Console.WriteLine("send");

                }
                catch (Exception erro)
                {
                    Console.WriteLine(erro.Message);
                }
                finally
                {
                    mail = null;
                }
            }
        }
    }
}
