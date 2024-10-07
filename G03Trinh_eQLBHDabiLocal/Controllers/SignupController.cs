using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class SignupController : Controller
    {
        private CustomerService customerService = new CustomerService(new Entity.EFDbContext());
        private AccountService accountService = new AccountService(new Entity.EFDbContext());
        // GET: Signup
        public ActionResult Index()
        {
            ViewBag.title = "Đăng ký";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Customer customer)
        {
            ViewBag.title = "Đăng ký";
            try
            {
                if (ModelState.IsValidField("name") &&
                    ModelState.IsValidField("address") &&
                    ModelState.IsValidField("phone") &&
                    ModelState.IsValidField("username") &&
                    ModelState.IsValidField("password") &&
                    ModelState.IsValidField("enterPassword") &&
                    ModelState.IsValidField("email"))
                {
                    if (customer.password.Equals(customer.enterPassword))
                    {
                        //add customer
                        int id = customerService.add(customer);
                        if (id == -1)
                        {
                            ViewBag.error = "Tài khoản đã tồn tại, đăng ký không thành công";
                            return View(customer);
                        }
                        Account account = accountService.get(customer.username);
                        account.password = null;
                        account.permission = 0;
                        Common.Session.setSession(Common.Constant.userSession, account);
                        ViewBag.success = "Đăng ký thành công";
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Mật khẩu và nhập lại mật khẩu không giống nhau";
                        return View(customer);
                    }
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