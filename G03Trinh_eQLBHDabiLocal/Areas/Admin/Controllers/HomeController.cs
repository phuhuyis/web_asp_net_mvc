using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Controllers;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private DashboardService dashboardService = new DashboardService();
        private BillService billService = new BillService();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.title = G03Trinh_eQLBHDabiLocal.Common.Constant.websiteName;
            ViewBag.active = "home";
            ViewBag.user = (Common.Session.getSession(Constant.userSession) as Account) != null ? (Common.Session.getSession(Constant.userSession) as Account).username : "";
            getDataDashboard();
            return View(billService.list());
        }

        public void getDataDashboard()
        {
            decimal priceMonthNew = dashboardService.revenueMonthNew();
            decimal priceMonthOld = dashboardService.revenueMonthOld();
            ViewBag.revenueMonthNew = priceMonthNew;
            if(priceMonthNew == 0 && priceMonthOld > 0)
            {
                ViewBag.percentPriceMonth = -100;
            }
            else
            {
                if(priceMonthOld == 0 && priceMonthNew > 0)
                {
                    ViewBag.percentPriceMonth = 100;
                }
                else
                {
                    if(priceMonthOld == 0 && priceMonthNew == 0)
                    {
                        ViewBag.percentPriceMonth = 0;
                    }
                    else
                    {
                        ViewBag.percentPriceMonth = Math.Round((priceMonthNew / priceMonthOld) * 100, 0) - 100;
                    }
                }
            }
            ViewBag.countCustomer = dashboardService.countCustomer();
            decimal priceYearNew = dashboardService.revenueYearNew();
            decimal priceYearOld = dashboardService.revenueYearOld();
            ViewBag.revenueYearNew = priceYearNew;
            if (priceYearNew == 0 && priceYearOld > 0)
            {
                ViewBag.percentPriceYear = -100;
            }
            else
            {
                if (priceYearOld == 0 && priceYearNew > 0)
                {
                    ViewBag.percentPriceYear = 100;
                }
                else
                {
                    if (priceYearOld == 0 && priceYearNew == 0)
                    {
                        ViewBag.percentPriceYear = 0;
                    }
                    else
                    {
                        ViewBag.percentPriceYear = Math.Round((priceYearNew / priceYearOld) * 100, 0) - 100;
                    }
                }
            }
        }

        [ChildActionOnly]
        public ActionResult listBill()
        {
            return PartialView("../Shared/Pages/Bill/List", billService.list());
        }
    }
}