using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SISCOMAN_MAIL_SERVICE
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceSend serviceSend = new ServiceSend();
            bool triggerPrevent = true;
            bool triggerValidade = true;

            while (true)
            {
                if (DateTime.Now.DayOfWeek.ToString() == "Monday" && triggerPrevent)
                {
                    Console.WriteLine("EMAIL PREVENT");
                    serviceSend.verifyMangueira(MailTypes.Preventiva);
                    triggerPrevent = false;
                }

                if (DateTime.Now.DayOfWeek.ToString() == "Tuesday")
                {
                    triggerPrevent = true;
                }


                if (DateTime.Now.Day.ToString() == "1" && triggerValidade)
                {
                    Console.WriteLine("EMAIL VALID");
                    serviceSend.verifyMangueira(MailTypes.Validade);
                    triggerValidade = false;
                }

                if (DateTime.Now.Day.ToString() == "2")
                {
                    triggerValidade = true;
                }

                Thread.Sleep(3600000);


            }
        }
    }
}
