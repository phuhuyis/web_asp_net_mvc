using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private AccountService accountService = new AccountService(new Entity.EFDbContext());
        public ActionResult ChangePassword()
        {
            ViewBag.title = "Đổi mật khẩu";
            ViewBag.active = "changePass";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (Common.Session.getSession(Common.Constant.userSession) == null)
                return View();
            return View(accountService.get((Common.Session.getSession(Common.Constant.userSession) as Account).username));
        }

        [HttpPost]
        public ActionResult ChangePassword(Account account)
        {
            ViewBag.title = "Đổi mật khẩu";
            ViewBag.active = "changePass";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            account.username = (Common.Session.getSession(Constant.userSession) as Account).username;
            try
            {
                if (ModelState.IsValidField("password") && ModelState.IsValidField("enterPass"))
                {
                    if (account.enterPass.Equals(account.password))
                    {
                        if (!accountService.changePass((Common.Session.getSession(Constant.userSession) as Account).username, account.password))
                        {
                            ViewBag.error = "Đổi mật khẩu không thành công";
                            return View(account);
                        }
                        ViewBag.success = "Đổi mật khẩu thành công";
                        ModelState.Clear();
                        return View();
                    }
                    else
                    {
                        ViewBag.error = "Mật khẩu và nhập lại mật khẩu không giống nhau";
                        return View(account);
                    }
                    
                }
                else
                {
                    return View(account);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Add()
        {
            ViewBag.title = "Thêm tài khoản quản trị";
            ViewBag.active = "home";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Account account)
        {
            ViewBag.title = "Thêm tài khoản quản trị";
            ViewBag.active = "home";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            try
            {
                if (ModelState.IsValidField("password") && ModelState.IsValidField("username"))
                {
                    if (!accountService.add(account.username, account.password))
                    {
                        ViewBag.error = "Tài khoản đã tồn tại";
                        return View(account);
                    }
                    ViewBag.success = "Thêm tài khoản thành công";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    return View(account);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            ViewBag.title = "Đổi mật khẩu";
            ViewBag.active = "home";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            Common.Session.delSession(Common.Constant.userSession);
            return RedirectToAction("Index", "Login");
        }
    }
}