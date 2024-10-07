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
    public class BankController : Controller
    {
        private AccountService accountService = new AccountService(new Entity.EFDbContext());
        private BankService bankService = new BankService(new Entity.EFDbContext());
        // GET: Admin/Bank
        public ActionResult Index()
        {
            ViewBag.title = "Cập nhập tài khoản ngân hàng";
            ViewBag.active = "bank";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (Common.Session.getSession(Common.Constant.userSession) == null)
                return View();
            ViewBag.listBank = bankService.listBank();
            return View(bankService.getBank());
        }
        [HttpPost]
        public ActionResult Index(Bank model)
        {
            ViewBag.title = "Cập nhập tài khoản ngân hàng";
            ViewBag.active = "bank";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (Common.Session.getSession(Common.Constant.userSession) == null)
                return View();
            ViewBag.listBank = bankService.listBank();
            try
            {
                if (ModelState.IsValid)
                {
                    bankService.setBank(model);
                    ViewBag.success = "Cập nhập tài khoản ngân hàng thành công";
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}