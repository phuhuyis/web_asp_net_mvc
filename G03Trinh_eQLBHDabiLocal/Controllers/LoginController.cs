using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccountService accountService = new AccountService(new Entity.EFDbContext());
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.title = "Đăng nhập";
            return View();
        }

        [HttpPost]
        public ActionResult Index(Account account)
        {
            ViewBag.title = "Đăng nhập";
            try
            {
                if (ModelState.IsValidField("username") && ModelState.IsValidField("password"))
                {
                    if (!accountService.login(account, 0))
                    {
                        ViewBag.error = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                        return View(account);
                    }
                    ViewBag.success = "Đăng nhập thành công";
                    HttpContext.Request.Headers.Add("Authorization", "Bearer a");
                    return View();
                }
                else
                {
                    return View(account);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}