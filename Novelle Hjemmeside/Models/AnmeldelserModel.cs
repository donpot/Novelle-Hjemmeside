using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Novelle_Hjemmeside.Models
{
    public class AnmeldelserModel
    {
        public string Anmeldelser { get; set; }
        public string Bruger { get; set; }

        public DateTime Date { get; set; }

        public int A_Bruger_ID { get; set; }
        public int A_Novelle_ID { get; set; }
        public int Bedømelse { get; set; }
    }
}