using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class CartController : Controller
    {
        private CartService cartService = new CartService();
        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.title = "Giỏ hàng";
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission == 1)
                return RedirectToAction("Index", "Login");
            return View(cartService.list((Common.Session.getSession(Common.Constant.userSession) as Account).username));
        }
    }
}