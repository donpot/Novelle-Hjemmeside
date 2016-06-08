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
        [HttpGet]
        public ActionResult NovelleList()
        {
            Database db = new Database();
            List<NovelleModel> nvlist = new List<NovelleModel>();

            nvlist = db.GetNovelle();

            return View(nvlist);
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

                //string path = @"C:\Users\Christian\Source\Repos\Novelle-Hjemmeside\Novelle Hjemmeside\Novels\" + addNovel.NovelleNavn + ".txt";
                string path = @"C:\Users\HVF-E308\Source\Repos\Novelle-Hjemmeside\Novelle Hjemmeside\Novels\" + addNovel.NovelleNavn + ".txt";
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
        public ActionResult Novel(NovelleModel novel)
        {
            //string path = @"C:\Users\Christian\Source\Repos\Novelle-Hjemmeside\Novelle Hjemmeside\Novels\" + novel.NovelleNavn + ".txt";
            string path = @"C:\Users\HVF-E308\Source\Repos\Novelle-Hjemmeside\Novelle Hjemmeside\Novels\" + novel.NovelleNavn + ".txt";

            ViewBag.forfatter = novel.N_Username;
            ViewBag.dato = novel.N_Date.Date.ToShortDateString();
            ViewBag.novelle = novel.NovelleNavn;


            if (Session["login"] == null)
            {
                ViewBag.myData = "<br/>" + novel.NovelleBeskrivelse;
            }
            else
            {

                FileInfo info = new FileInfo(path);

                using (StreamReader sr = info.OpenText())
                {
                    string s = "";

                    while ((s = sr.ReadLine()) != null)
                    {
                        ViewBag.myData = s;
                    }
                }
            }
            return View();

        }
    }
}