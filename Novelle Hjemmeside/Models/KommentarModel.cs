using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Novelle_Hjemmeside.Models
{
    public class KommentarModel
    {
        public int Ko_Kapitle_ID;
        public int Ko_Novelle_ID;
        public int Ko_Bruger_ID;

        public string Kommentar;
        public string Bruger;

        public DateTime Dato;
    }
}