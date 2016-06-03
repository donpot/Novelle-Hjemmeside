using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Novelle_Hjemmeside.Models
{
    public class NovelleModel
    {
        public int Novelle_ID { get; set; }
        public int N_User_ID { get; set; }

        public DateTime N_Date { get; set; }

        public string N_Username { get; set; }
        public string NovelleNavn { get; set; }
        public string NovelleBeskrivelse { get; set; }
    }
}