using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHD_TAS_LIB.db;
using PHD_TAS_LIB.util;
using PHD_TAS_LIB.entity.user;
using PHD_TAS_LIB.entity.transaction;
using DbExtensions;
using System.Threading;


public enum MailTypes
{
    Validade,
    Preventiva   
}



namespace SISCOMAN_MAIL_SERVICE
{       

    public class ServiceSend
    {
        Mail mail = new Mail();

        private static bool isMangueiraLate(Mangueira mangueira)
        {
            //enviar mail toda semana
            if (DateTime.Now >= mangueira.dateLastRevision.AddMonths(10) && mangueira.status == "APROVADO")
                return true;
            else return false;
        }



        private static bool isMangueiraPraVencer(Mangueira mangueira)
        {


            //enviar mail toda semana
            if (mangueira.dataValidade <= DateTime.Now.AddYears(9).AddMonths(6) && mangueira.status == "APROVADO")
                return true;
            else return false;
        }

        public void verifyMangueira(MailTypes modal)
        {
            
            var db = DBFactory.GetEnsureOpen();

            User[] Users = db.Table<User>().ToArray();
            Mangueira[] Mangueiras = db.Table<Mangueira>().ToArray();
            Cliente[] clientes = db.Table<Cliente>().ToArray();

            List<string> mailsInsp = new List<string>();
            List<string> mailsVencer = new List<string>();


            Dictionary<int, Mangueira[]> MangueirasDoCliente = new Dictionary<int, Mangueira[]>();
            
            foreach(var i in clientes)
            {
                MangueirasDoCliente.Add(i.id,Mangueiras.Where(m => m.idcliente == i.id).ToArray());
            }


         //   int ii = 0;
            foreach (var m in MangueirasDoCliente)
            {

                Dictionary<int, DateTime> codeLasRevision = new Dictionary<int, DateTime>();
                Dictionary<int, DateTime> codeAVencer = new Dictionary<int, DateTime>();

                foreach (Mangueira i in m.Value)
                {

                    //   Console.WriteLine("id "+m.Key + " " + i.code + " mangueira criente " + ii);
                    //  ii++;

                    if (MailTypes.Validade == modal)
                    {
                        if (isMangueiraPraVencer(i))
                        {
                            codeAVencer.Add(i.code, i.dataValidade);
                            mailsVencer = Users.Where(u => u.idCliente == i.idcliente && u.active).Select(u => u.email).ToList();
                        }
                    }

                    if (MailTypes.Preventiva == modal)
                    {
                        if (isMangueiraLate(i))
                        {
                            codeLasRevision.Add(i.code, i.dateLastRevision);
                            mailsInsp = Users.Where(u => u.idCliente == i.idcliente && u.active).Select(u => u.email).ToList();
                        }
                    }
                }

                if (MailTypes.Validade == modal)
                {
                    if (codeAVencer.Count != 0)
                    {
                        mail.sendMailVenc(codeAVencer, mailsVencer);
                        Thread.Sleep(3000);
                    }
                }

                if (MailTypes.Preventiva == modal)
                {
                    if (codeLasRevision.Count != 0)
                    {
                        mail.sendMailInsp(codeLasRevision, mailsInsp);
                        Thread.Sleep(3000);
                    }
                }
            }

        }

    }
}
