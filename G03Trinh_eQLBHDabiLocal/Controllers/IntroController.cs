using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class IntroController : Controller
    {
        // GET: Intro
        public ActionResult Index()
        {
            ViewBag.title = "Giới thiệu";
            return View();
        }
    }
}