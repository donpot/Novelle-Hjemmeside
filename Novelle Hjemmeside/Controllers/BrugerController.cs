using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Novelle_Hjemmeside.Models;
using Novelle_Hjemmeside.Classes;

namespace Novelle_Hjemmeside.Controllers
{
    public class BrugerController : Controller
    {
        [HttpGet]
        [ActionName("Login")]
        public ActionResult LoginGet()
        {
            if (Session["login"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPost(UserModel Bruger)
        {
            string UserName = Bruger.UserName;
            string PassWord = Bruger.PassWord;

            UserModel b = new UserModel();
            Database db = new Database();

            b = db.BrugerLogin(UserName, PassWord);

            if(UserName == b.UserName && PassWord == b.PassWord)
            {
                Session["login"] = b;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["login"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("Info")]
        public ActionResult InfoGet()
        {

            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                UserModel b = new UserModel();

                b = (UserModel)Session["login"];

                ViewBag.Brugernavn = b.UserName.ToString();
                ViewBag.Email = b.Email.ToString();
                ViewBag.Dato = b.Date.Date.ToShortDateString();

                return View();
            }
        }

        [HttpPost]
        [ActionName("Info")]
        public ActionResult InfoPost()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Registre")]
        public ActionResult RegistreGet()
        {
            if(Session["login"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ActionName("Registre")]
        public ActionResult RegistrePost(UserModel user)
        {
            Database db = new Database();
            UserModel u = new UserModel();

            string username = user.UserName;
            string password = user.PassWord;
            string email = user.Email;
            bool admin = false;

            db.TilføjBruger(username, password, email, admin);

            return RedirectToAction("Login", "Bruger");
        }
    }
}