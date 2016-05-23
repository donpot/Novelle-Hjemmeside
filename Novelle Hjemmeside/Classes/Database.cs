using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Novelle_Hjemmeside.Models;

namespace Novelle_Hjemmeside.Classes
{
    class Database
    {
        public static string connectionstring = "";

        SqlConnection Connection = new SqlConnection(connectionstring);

        public List<UserModel> GetUser()
        {
            List<UserModel> BrugerListe = new List<UserModel>();

            Connection.Open();
            using (SqlCommand BrugerCommand = new SqlCommand("SELECT * FROM Bruger", Connection))
            {
                SqlDataReader reader = BrugerCommand.ExecuteReader();
                while (reader.Read())
                {
                    UserModel u = new UserModel();
                    u.Admin = Convert.ToBoolean(reader["Administrator"]);
                    u.Date = Convert.ToDateTime(reader["Dato"]);
                    u.Email = reader["Emal"].ToString();
                    u.PassWord = reader["Passw"].ToString();
                    u.UserName = reader["Username"].ToString();
                    u.User_ID = Convert.ToInt32(reader["B_ID"]);

                    BrugerListe.Add(u);                  
                }
            }

                return BrugerListe;
        }

        public List<NovelleModel> GetNovelle()
        {
            List<NovelleModel> NovelleListe = new List<NovelleModel>();

            return NovelleListe;
        }

        public List<KommentarModel> GetKommentar()
        {
            List<KommentarModel> KommentarList = new List<KommentarModel>();

            return KommentarList;
        }

        public List<AnmeldelserModel> GetAnmeldelse()
        {
            List<AnmeldelserModel> AnmeldelseListe = new List<AnmeldelserModel>();

            return AnmeldelseListe;
        }

        public List<KapittleModel> GetKapitle()
        {
            List<KapittleModel> KapitleListe = new List<KapittleModel>();

            return KapitleListe;
        }
    }
}
