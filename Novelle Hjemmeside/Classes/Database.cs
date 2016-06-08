using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Novelle_Hjemmeside.Models;

namespace Novelle_Hjemmeside.Classes
{
    public class Database
    {
        public static string connectionstring = "Data Source=.;Initial Catalog=Noveller;Integrated Security=True";
        //public static string connectionstring = "Data Source=CHRISTIAN-PC\\SQLEXPRESS;Initial Catalog=Noveller;Integrated Security=True";

        SqlConnection Connection = new SqlConnection(connectionstring);

        #region Resterende

        public UserModel BrugerLogin(string Un, string Pw)
        {
            Connection.Open();
            UserModel LogInBruger = new UserModel();
            using (SqlCommand LoginCommand = new SqlCommand("SELECT * FROM Bruger WHERE Username = '" + Un + "' AND Passw = '" + Pw + "'", Connection))
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
                Connection.Close();
                return LogInBruger;
            }
        }

        public decimal GetBedømmelse(int id)
        {
            Decimal SamletBedømmelse = 0;
            int rows = 0;

            Connection.Open();
            using (SqlCommand BedømCommand = new SqlCommand("SELECT Bedømelse FROM Anmeldelse WHERE N_ID = '" + id + "'", Connection))
            {
                SqlDataReader BedømReader = BedømCommand.ExecuteReader();
                while (BedømReader.Read())
                {
                    SamletBedømmelse = SamletBedømmelse + Convert.ToDecimal(BedømReader["Bedømelse"]);
                    rows++;
                }
                SamletBedømmelse = SamletBedømmelse / rows;

            }
            Connection.Close();
            return SamletBedømmelse;
        }

        #endregion

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
            Connection.Close();
            return BrugerListe;
        }

        public List<NovelleModel> GetNovelle()
        {
            List<NovelleModel> NovelleListe = new List<NovelleModel>();

            Connection.Open();
            using (SqlCommand NovelleCommand = new SqlCommand("SELECT * FROM Novel ORDER BY Dato DESC", Connection))
            {
                SqlDataReader Nreader = NovelleCommand.ExecuteReader();
                while (Nreader.Read())
                {
                    NovelleModel n = new NovelleModel();
                    n.NovelleBeskrivelse = Nreader["Beskrivelse"].ToString();
                    n.NovelleNavn = Nreader["NovelleNavn"].ToString();
                    n.Novelle_ID = Convert.ToInt32(Nreader["N_ID"]);
                    n.N_Date = Convert.ToDateTime(Nreader["Dato"]);
                    n.N_Username = Nreader["Bruger"].ToString();
                    n.N_User_ID = Convert.ToInt32(Nreader["B_ID"]);

                    NovelleListe.Add(n);
                }
            }
            Connection.Close();
            return NovelleListe;
        }

        public List<KommentarModel> GetKommentar()
        {
            List<KommentarModel> KommentarList = new List<KommentarModel>();

            Connection.Open();
            using (SqlCommand Kocommand = new SqlCommand("SELECT * FROM Kommenrar", Connection))
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
            Connection.Close();
            return KommentarList;
        }

        public List<AnmeldelserModel> GetAnmeldelse()
        {
            List<AnmeldelserModel> AnmeldelseListe = new List<AnmeldelserModel>();

            Connection.Open();
            using (SqlCommand Acommand = new SqlCommand("SELECT * FROM Anmeldelser", Connection))
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
            Connection.Close();
            return AnmeldelseListe;
        }

        #endregion //Lists

        #region Forside-Layout

        public NovelleModel GetRandomNovelle()
        {
            Connection.Open();
            NovelleModel RanNov = new NovelleModel();
            using (SqlCommand RandomCommand = new SqlCommand("SELECT TOP 1 * from Novel ORDER BY NEWID()", Connection))
                {
                SqlDataReader RanRead = RandomCommand.ExecuteReader();
                if (RanRead.HasRows)
                {
                    RanRead.Read();
                    RanNov.NovelleBeskrivelse = RanRead["Beskrivelse"].ToString();
                    RanNov.NovelleNavn = RanRead["NovelleNavn"].ToString();
                    RanNov.Novelle_ID = Convert.ToInt32(RanRead["N_ID"]);
                    RanNov.N_Date = Convert.ToDateTime(RanRead["Dato"]);
                    RanNov.N_Username = RanRead["Bruger"].ToString();
                    RanNov.N_User_ID = Convert.ToInt32(RanRead["B_ID"]);
                }
                else
                {

                }
                Connection.Close();
                return RanNov;
                }
        }

        public List<NovelleModel> Get5RandomNovelle()
        {
            Connection.Open();

            List<NovelleModel> Listr = new List<NovelleModel>();
            
            using (SqlCommand RandomCommand = new SqlCommand("SELECT TOP 5 * from Novel ORDER BY NEWID()", Connection))
            {
                SqlDataReader RandRead = RandomCommand.ExecuteReader();
                while (RandRead.Read())
                {
                    NovelleModel r = new NovelleModel();
                    r.NovelleBeskrivelse = RandRead["Beskrivelse"].ToString();
                    r.NovelleNavn = RandRead["NovelleNavn"].ToString();
                    r.Novelle_ID = Convert.ToInt32(RandRead["N_ID"]);
                    r.N_Date = Convert.ToDateTime(RandRead["Dato"]);
                    r.N_Username = RandRead["Bruger"].ToString();
                    r.N_User_ID = Convert.ToInt32(RandRead["B_ID"]);

                    Listr.Add(r);
                }
                Connection.Close();
                return Listr;
            }
        }

        public List<NovelleModel> Get5NyestNoveller()
        {

            Connection.Open();

            List<NovelleModel> Listny = new List<NovelleModel>();

            using (SqlCommand NewCommand = new SqlCommand("SELECT TOP 5 * from Novel ORDER BY Dato DESC", Connection))
            {
                SqlDataReader NewRead = NewCommand.ExecuteReader();
                while (NewRead.Read())
                {
                    NovelleModel n = new NovelleModel();
                    n.NovelleBeskrivelse = NewRead["Beskrivelse"].ToString();
                    n.NovelleNavn = NewRead["NovelleNavn"].ToString();
                    n.Novelle_ID = Convert.ToInt32(NewRead["N_ID"]);
                    n.N_Date = Convert.ToDateTime(NewRead["Dato"]);
                    n.N_Username = NewRead["Bruger"].ToString();
                    n.N_User_ID = Convert.ToInt32(NewRead["B_ID"]);

                    Listny.Add(n);
                }
                Connection.Close();
                return Listny;
            }
        }

        #endregion

        #region Tilføj

        public void TilføjBruger(string Un, string Pw, string Em, bool A )
        {
            Connection.Open();

            using (SqlCommand TBCommand = new SqlCommand("INSERT INTO Bruger VALUES ('" + Un + "', '" + Pw + "', '" + Em + "', '" + DateTime.Today + "', '" + A + "');", Connection))
            {
                TBCommand.ExecuteNonQuery();
                
            }
            Connection.Close();
        }

        public void TilføjNovelle(string Bn, int bid, string Nn, string Nb)
        {
            Connection.Open();

            using (SqlCommand TNCommand = new SqlCommand("INSERT INTO Novel VALUES ('" + DateTime.Today + "', '" + Bn + "', '" + bid + "', '" + Nn + "', '" + Nb + "');", Connection))
            {
                TNCommand.ExecuteNonQuery();
            }
            Connection.Close();
        }

        public void TilføjKommentar(string Kt, string Bn, int nid, int bid)
        {
            Connection.Open();

            using (SqlCommand TKCommand = new SqlCommand("INSERT INTO Kommentar VALUES ('" + Kt + "', '" + Bn + "', '" + DateTime.Today + "', '" + nid + "', '" + bid + "');", Connection))
            {
                TKCommand.ExecuteNonQuery();
            }
            Connection.Close();
        }

        public void TilføjAnmeldelse(string At, string Bn, int nid, int bid, int bedøm)
        {
            Connection.Open();

            using (SqlCommand TACommand = new SqlCommand("INSERT INTO anmeldelser VALUES ('" + At + "', '" + Bn + "', '" + DateTime.Today + "', '" + nid + "', '" + bid + "', '" + bedøm + "');", Connection))
            {
                TACommand.ExecuteNonQuery();
            }
            Connection.Close();
        }

        #endregion
    }
}
