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
    [RouteArea("admin")]
    [RoutePrefix("bill")]
    public class BillController : Controller
    {
        private BillService billService = new BillService();

        [Route]
        public ActionResult Index()
        {
            ViewBag.title = "Hóa đơn";
            ViewBag.active = "bill";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            return View(billService.list());
        }

        [Route("{id}")]
        public ActionResult DeltailBill(int id)
        {
            ViewBag.title = "Chi tiết hóa đơn";
            ViewBag.active = "bill";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            billService.showDetail(id);
            return View(billService.listAllDetails(id));
        }
    }
}