using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Areas.Admin;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class HomeController : Controller
    {
        private CategoryService categoryService = new CategoryService(new Entity.EFDbContext());
        private SlideService slideService = new SlideService();
        // GET: Home
        public ActionResult Index(String id)
        {
            ViewBag.title = Common.Constant.websiteName;
            return View(categoryService.listEntity());
        }

        [ChildActionOnly]
        public ActionResult MenuNav()
        {
            List<Category> categories = categoryService.list();
            return PartialView("../Shared/Pages/Navbar", categories);
        }
        [ChildActionOnly]
        public ActionResult MenuFooter()
        {
            List<Category> categories = categoryService.list();
            return PartialView("../Shared/Pages/Footer", categories);
        }
        [ChildActionOnly]
        public ActionResult Slide()
        {
            List<Slide> slides = slideService.list();
            return PartialView("../Shared/Pages/Slide", slides);
        }
    }
}