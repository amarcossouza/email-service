using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SISCOMAN_MAIL_SERVICE
{
    public class MailConfig
    {

        StreamReader file = new StreamReader(Environment.CurrentDirectory + @"\config\mailConfig.txt");
        
        public Config getConfigData()
        {   
           Config configs = JsonConvert.DeserializeObject<Config>(file.ReadLine());
            return configs;
        }

    }
}
