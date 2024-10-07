using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Controllers;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerService customerService;

        public CustomerController(CustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: Admin/Customer
        public ActionResult List(String key)
        {
            ViewBag.active = "customer";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if(key == null)
            {
                ViewBag.title = "Quản lý khách hàng";
                return View(customerService.list());
            }
            ViewBag.title = $"Tìm kiếm khách hàng {key}";
            return View(customerService.list(key));
        }

        public ActionResult Add()
        {
            ViewBag.title = "Thêm khách hàng";
            ViewBag.active = "customer";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            ViewBag.title = "Thêm khách hàng";
            ViewBag.active = "customer";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            try
            {
                if(ModelState.IsValidField("name") &&
                    ModelState.IsValidField("address") &&
                    ModelState.IsValidField("phone") &&
                    ModelState.IsValidField("username") &&
                    ModelState.IsValidField("password") &&
                    ModelState.IsValidField("email"))
                {
                    //add customer
                    int id = customerService.add(customer);
                    if (id == -1)
                    {
                        ViewBag.error = "Tài khoản đã tồn tại, thêm khách hàng không thành công";
                        return View(customer);
                    }
                    ViewBag.success = "Thêm khách hàng thành công";
                    ModelState.Clear();
                    return View();
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
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Cập nhập khách hàng";
            ViewBag.active = "customer";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            Customer customer = customerService.get(id);
            if(customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            ViewBag.title = "Cập nhập khách hàng";
            ViewBag.active = "customer";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (ModelState.IsValidField("name") && ModelState.IsValidField("address") && ModelState.IsValidField("phone") && ModelState.IsValidField("email"))
            {
                if (customerService.update(id, customer))
                {
                    ViewBag.success = "Cập nhập khách hàng thành công";
                }
                else
                {
                    ViewBag.error = "Cập nhập khách hàng không thành công, vui lòng kiểm tra lại thông tin";
                }
            }
            return View(customerService.get(id));
        }
    }
}