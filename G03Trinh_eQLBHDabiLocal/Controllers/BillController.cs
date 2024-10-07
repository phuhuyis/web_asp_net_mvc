using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    [RoutePrefix("bill")]
    public class BillController : Controller
    {
        private BillService billService = new BillService();

        [Route]
        public ActionResult Index()
        {
            ViewBag.title = "Theo dõi đơn hàng";
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission == 1)
                return RedirectToAction("Index", "Login");
            return View(billService.list((Common.Session.getSession(Common.Constant.userSession) as Account).username));
        }

        [Route("{id}")]
        public ActionResult DeltailBill(int id)
        {
            ViewBag.title = "Thông tin chi tiết đơn hàng";
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission == 1)
                return RedirectToAction("Index", "Login");
            return View(billService.listAllDetails(id));
        }
    }
}