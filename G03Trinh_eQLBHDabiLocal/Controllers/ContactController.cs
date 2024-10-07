using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class ContactController : Controller
    {
        private ContactService contactService = new ContactService();
        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.title = "Liên hệ";
            if(Common.Session.getSession(Common.Constant.userSession) != null 
                && (Common.Session.getSession(Common.Constant.userSession) as Account).permission == 0)
                return View();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Index(Contact contact)
        {
            ViewBag.title = "Liên hệ";
            if (Common.Session.getSession(Common.Constant.userSession) != null
                && (Common.Session.getSession(Common.Constant.userSession) as Account).permission == 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (!contactService.add(contact, (Common.Session.getSession(Common.Constant.userSession) as Account).username))
                        {
                            ViewBag.error = "Bạn chưa đăng nhập";
                            return View();
                        }
                        ViewBag.success = "Form của bạn đã được gửi đi thành công";
                        ModelState.Clear();
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
            return RedirectToAction("Index", "Login");
        }
    }
}