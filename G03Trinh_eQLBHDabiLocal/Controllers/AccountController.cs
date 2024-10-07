using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        private CustomerService customerService = new CustomerService(new Entity.EFDbContext());
        [Route("edit")]
        public ActionResult Edit()
        {
            ViewBag.title = "Cập nhập thông tin";
            if(Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission == 1)
                return RedirectToAction("Index", "Login");
            return View(customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username));
        }

        [Route("edit")]
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            ViewBag.title = "Cập nhập thông tin";
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission == 1)
                return RedirectToAction("Index", "Login");
            try
            {
                if (ModelState.IsValidField("name") &&
                    ModelState.IsValidField("address") &&
                    ModelState.IsValidField("phone") &&
                    ModelState.IsValidField("enterPassword") &&
                    ModelState.IsValidField("password") &&
                    ModelState.IsValidField("email"))
                {
                    if (customer.enterPassword.Equals(customer.password) 
                        && customerService.update(customer, (Common.Session.getSession(Common.Constant.userSession) as Account).username))
                    {
                        ViewBag.success = "Cập nhập thành công";
                        ModelState.Clear();
                        return View(customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username));
                    }
                    ViewBag.error = "Mật khẩu và nhập lại mật khẩu không giống nhau";
                    return View(customer);
                }
                else
                {
                    return View(customer);
                }
            }
            catch
            {
                return View();
            }
        }
    }
}