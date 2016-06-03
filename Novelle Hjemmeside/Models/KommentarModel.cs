using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Novelle_Hjemmeside.Models
{
    public class KommentarModel
    {
        public int Ko_Kapitle_ID { get; set; }
        public int Ko_Novelle_ID { get; set; }
        public int Ko_Bruger_ID { get; set; }

        public string Kommentar { get; set; }
        public string Bruger { get; set; }

        public DateTime Dato { get; set; }
    }
}