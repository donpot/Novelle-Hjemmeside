using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Novelle_Hjemmeside.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }

        public DateTime Date { get; set; }

        public int User_ID { get; set; }

        public bool Admin { get; set; }
    }
}