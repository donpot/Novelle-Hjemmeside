using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Novelle_Hjemmeside.Models
{
    public class AnmeldelserModel
    {
        public string Anmeldelser;

        public DateTime Date;

        public int A_Bruger_ID;
        public int A_Novelle_ID;
        public int Bedømelse;
    }
}