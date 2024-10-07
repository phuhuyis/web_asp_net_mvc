using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly AccountService accountService;

        public LoginController(AccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Account user)
        {
            try
            {
                if(ModelState.IsValidField("username") && ModelState.IsValidField("password"))
                {
                    if (!accountService.login(user, 1))
                    {
                        ViewBag.error = "Tên đăng nhập hoặc mật khẩu không chính xác!";
                        return View(user);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(user);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}