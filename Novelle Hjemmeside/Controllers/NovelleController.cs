using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Novelle_Hjemmeside.Classes;
using Novelle_Hjemmeside.Models;
using System.IO;

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
        [ValidateInput(false)]
        public ActionResult OpretNovelle_Post(NovelleModel addNovel)
        {
            Database addNovelDb = new Database();
            UserModel addNovelUser = new UserModel();

            string NovIndhold = Request.Unvalidated.Form["mytextarea"];

            addNovelUser = (UserModel)Session["login"];
            if (ModelState.IsValid)
            {

                string path = @"C:\Users\Christian\Source\Repos\Novelle-Hjemmeside\Novelle Hjemmeside\Novels\" + addNovel.NovelleNavn + ".txt";
                FileInfo info = new FileInfo(path);
                if (!info.Exists)
                {
                    // Create a file to write to.
                    using (StreamWriter sw = info.CreateText())
                    {
                        sw.WriteLine(NovIndhold);
                    }
                }

                addNovelDb.TilføjNovelle(
                    addNovelUser.UserName,
                    addNovelUser.User_ID,
                    addNovel.NovelleNavn,
                    addNovel.NovelleBeskrivelse);

                return RedirectToAction("Info", "Bruger");
            }

            return View();

        }
    }
}