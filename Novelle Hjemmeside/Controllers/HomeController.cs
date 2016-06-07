using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Novelle_Hjemmeside.Classes;
using Novelle_Hjemmeside.Models;

namespace Novelle_Hjemmeside.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Database NovelDb = new Database();
            NovelleModel RandNovel = new NovelleModel();
            RandNovel = NovelDb.GetRandomNovelle();

            return View(RandNovel);
        }
    }
}