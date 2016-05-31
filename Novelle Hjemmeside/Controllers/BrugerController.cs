using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Novelle_Hjemmeside.Controllers
{
    public class BrugerController : Controller
    {
        // GET: Bruger
        public ActionResult Index()
        {
            return View();
        }
    }
}