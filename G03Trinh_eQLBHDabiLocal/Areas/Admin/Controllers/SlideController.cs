using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        private SlideService slideService = new SlideService();
        public ActionResult List()
        {
            ViewBag.title = "Danh sách hình ảnh slide";
            ViewBag.active = "slide";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (Common.Session.getSession(Common.Constant.userSession) == null)
                return View();
            return View(slideService.list());
        }
    }
}