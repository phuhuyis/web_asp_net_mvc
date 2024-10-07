using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Models;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.title = "Báo cáo, thống kê";
            ViewBag.active = "report";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            return View();
        }
    }
}