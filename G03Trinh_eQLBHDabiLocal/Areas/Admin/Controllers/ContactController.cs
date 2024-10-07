using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private ContactService contactService = new ContactService();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            ViewBag.title = "Liên hệ";
            ViewBag.active = "contact";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            return View(contactService.list());
        }
    }
}