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

        public UserModel BrugerLogin(string Un, string Pw)
        {
            Connection.Open();
            UserModel LogInBruger = new UserModel();
            using (SqlCommand LoginCommand = new SqlCommand("SELECT * FROM Bruger WHERE Username = '" + Un + " AND Passw = '" + Pw + "'"))
            {
                SqlDataReader readerlogin = LoginCommand.ExecuteReader();
                if (readerlogin.HasRows)
                {
                    readerlogin.Read();
                    LogInBruger.UserName = readerlogin["Username"].ToString();
                    LogInBruger.PassWord = readerlogin["Passw"].ToString();
                    LogInBruger.Email = readerlogin["Email"].ToString();
                    LogInBruger.Admin = Convert.ToBoolean(readerlogin["Administrator"]);
                    LogInBruger.Date = Convert.ToDateTime(readerlogin["Dato"]);
                    LogInBruger.User_ID = Convert.ToInt32(readerlogin["B_ID"]);
                }
                else
                {
                    LogInBruger.Admin = false;
                }
                return LogInBruger;
            }
        }

        public decimal GetBedømmelse(int id)
        {
            Decimal SamletBedømmelse = 0;
            int rows = 0;

            Connection.Open();
            using (SqlCommand BedømCommand = new SqlCommand("SELECT Bedømelse FROM Anmeldelse WHERE N_ID = '" + id + "'"))
            {
                SqlDataReader BedømReader = BedømCommand.ExecuteReader();
                while (BedømReader.Read())
                {
                    SamletBedømmelse = SamletBedømmelse + Convert.ToDecimal(BedømReader["Bedømelse"]);
                    rows++;
                }
                SamletBedømmelse = SamletBedømmelse / rows;

            }
            return SamletBedømmelse;
        }

        #region Lists-Region

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

            Connection.Open();
            using (SqlCommand NovelleCommand = new SqlCommand("SELECT * FROM Novel"))
            {
                SqlDataReader Nreader = NovelleCommand.ExecuteReader();
                while (Nreader.Read())
                {
                    NovelleModel n = new NovelleModel();
                    n.NovelleBeskrivelse = Nreader["Beskrivelse"].ToString();
                    n.NovelleNavn = Nreader["NovelleNavn"].ToString();
                    n.Novelle_ID = Convert.ToInt32(Nreader["N_ID"]);
                    n.N_Date = Convert.ToDateTime(Nreader["Dato"]);
                    n.N_Username = Nreader["BBruger"].ToString();
                    n.N_User_ID = Convert.ToInt32(Nreader["B_ID"]);

                    NovelleListe.Add(n);
                }
            }
                return NovelleListe;
        }

        public List<KommentarModel> GetKommentar()
        {
            List<KommentarModel> KommentarList = new List<KommentarModel>();

            Connection.Open();
            using (SqlCommand Kocommand = new SqlCommand("SELECT * FROM Kommenrar"))
            {
                SqlDataReader Koreader = Kocommand.ExecuteReader();
                while (Koreader.Read())
                {
                    KommentarModel ko = new KommentarModel();
                    ko.Kommentar = Koreader["Kommentar"].ToString();
                    ko.Bruger = Koreader["Bruger"].ToString();
                    ko.Ko_Novelle_ID = Convert.ToInt32(Koreader["N_ID"]);
                    ko.Ko_Bruger_ID = Convert.ToInt32(Koreader["B_ID"]);
                    ko.Dato = Convert.ToDateTime(Koreader["Dato"]);

                    KommentarList.Add(ko);
                }
            }
                return KommentarList;
        }

        public List<AnmeldelserModel> GetAnmeldelse()
        {
            List<AnmeldelserModel> AnmeldelseListe = new List<AnmeldelserModel>();

            Connection.Open();
            using (SqlCommand Acommand = new SqlCommand("SELECT * FROM Anmeldelser"))
            {
                SqlDataReader Areader = Acommand.ExecuteReader();
                while (Areader.Read())
                {
                    AnmeldelserModel a = new AnmeldelserModel();
                    a.Anmeldelser = Areader["anmeldelser"].ToString();
                    a.Bruger = Areader["Bruger"].ToString();
                    a.A_Bruger_ID = Convert.ToInt32(Areader["B_ID"]);
                    a.A_Novelle_ID = Convert.ToInt32(Areader["N_ID"]);
                    a.Bedømelse = Convert.ToInt32(Areader["Bedømelse"]);
                    a.Date = Convert.ToDateTime(Areader["Dato"]);

                    AnmeldelseListe.Add(a);
                }
            }
                return AnmeldelseListe;
        }

        #endregion //Lists

        #region Forside-Layout

        public NovelleModel GetRandomNovelle()
        {
            //SELECT TOP 1 from Bruger ORDER BY NEWID()  1 Random
        }

        public List<NovelleModel> Get5RandomNovelle()
        {
            //SELECT TOP 5 from Bruger ORDER BY NEWID()  5 Random
        }

        public List<NovelleModel> Get5NyestNoveller()
        {
            //SELECT TOP 5 FROM Bruger ORDER BY Date DESC
        }

        #endregion
    }
}
