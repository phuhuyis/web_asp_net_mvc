using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Controllers;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult List(String key)
        {
            ViewBag.active = "category";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if(key == null)
            {
                ViewBag.title = "Quản lý hãng";
                return View(categoryService.list());
            }
            ViewBag.title = $"Tìm kiếm hãng {key}";
            return View(categoryService.list(key));
        }

        public ActionResult Add()
        {
            ViewBag.title = "Thêm hãng";
            ViewBag.active = "category";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Category category)
        {
            ViewBag.title = "Thêm hãng";
            ViewBag.active = "category";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            try
            {
                if (ModelState.IsValid)
                {
                    if (!categoryService.add(category))
                    {
                        ViewBag.error = "Đường dẫn đã tồn tại, thêm hãng không thành công";
                        return View(category);
                    }
                    ViewBag.success = "Thêm hãng thành công";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    return View(category);
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Cập nhập hãng";
            ViewBag.active = "category";
            Category category = categoryService.get(id);
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            ViewBag.title = "Cập nhập hãng";
            ViewBag.active = "category";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (ModelState.IsValid)
            {
                if (categoryService.update(id, category))
                {
                    ViewBag.success = "Cập nhập hãng thành công";
                }
                else
                {
                    ViewBag.error = "Đường dẫn đã tồn tại, Cập nhập hãng không thành công";
                }
            }
            return View(categoryService.get(id));
        }
    }
}