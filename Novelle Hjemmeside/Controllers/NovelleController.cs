using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Novelle_Hjemmeside.Controllers
{
    public class NovelleController : Controller
    {
        // GET: Novelle
        public ActionResult Index()
        {
            return View();
        }
    }
}