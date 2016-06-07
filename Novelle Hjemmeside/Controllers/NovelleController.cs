using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Novelle_Hjemmeside.Classes;
using Novelle_Hjemmeside.Models;

namespace Novelle_Hjemmeside.Controllers
{
    public class NovelleController : Controller
    {
        // GET: Novelle
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [ActionName("OpretNovelle")]
        public ActionResult OpretNovelle_Get()
        {
            return View();
        }
        [HttpPost]
        [ActionName("OpretNovelle")]
        public ActionResult OpretNovelle_Post(NovelleModel addNovel)
        {
            Database addNovelDb = new Database();
            UserModel addNovelUser = new UserModel();

            addNovelUser = (UserModel)Session["login"];
            if (ModelState.IsValid)
            {
                addNovelDb.TilføjNovelle(
                    addNovelUser.UserName,
                    addNovelUser.User_ID,
                    addNovel.NovelleNavn,
                    addNovel.NovelleBeskrivelse);
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}