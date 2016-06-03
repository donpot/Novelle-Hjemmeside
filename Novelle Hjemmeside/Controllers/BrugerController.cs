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
                ViewBag.Message = "Forket brugernavn eller password";
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
        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Registre()
        {
            return View();
        }
    }
}