using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Controllers;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    public class ProductController : Controller
    {
        private CategoryService categoryService;
        private ProductService productService;

        public ProductController(CategoryService categoryService, ProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        [Route("product/list/{page?}")]
        // GET: Admin/Product
        public ActionResult List(String key, int? page = 1)
        {
            ViewBag.active = "product";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            ViewBag.categories = ListDropdownCategory();
            ViewBag.page = page;
            if (key == null)
            {
                ViewBag.title = "Quản lý túi sách";
                ViewBag.fullPage = productService.getFullPage();
                return View(productService.list(page));
            }
            ViewBag.title = $"Tìm kiếm túi sách {key}";
            ViewBag.fullPage = productService.getFullPage(key);
            if(page != 1 && productService.getFullPage(key) < page)
            {
                return RedirectToAction("List", "Product", new
                {
                    key = key,
                    page = 1
                });
            }
            return View(productService.list(key, page));
        }

        public ActionResult Add()
        {
            ViewBag.title = "Thêm túi sách";
            ViewBag.active = "product";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            ViewBag.categories = ListDropdownCategory();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Product product)
        {
            ViewBag.title = "Thêm túi sách";
            ViewBag.active = "product";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            ViewBag.categories = ListDropdownCategory();
            if (product.image != null && !productService.checkFile(product.image))
                ViewBag.error_img = "Hình ảnh không đúng định dạng chỉ nhận file png, jpg hoặc jpeg";
            if (product.price != null && product.price <= 0)
            {
                ModelState.AddModelError("price", "Giá phải lớn hơn 0");
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (productService.checkFile(product.image))
                    {
                        if (!productService.add(product))
                        {
                            ViewBag.error = "Thêm túi sách không thành công";
                            return View(product);
                        }
                        ViewBag.success = "Thêm túi sách thành công";
                        ModelState.Clear();
                    }
                    return View();
                }
                else
                {
                    return View(product);
                }
            }
            catch
            {
                return View();
            }
        }
        private List<SelectListItem> ListDropdownCategory()
        {
            List<Category> categories = categoryService.list();
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (Category category in categories)
            {
                result.Add(new SelectListItem() { Value = category.id.ToString(), Text = category.name });
            }
            return result;
        }
        [ValidateInput(false)]
        public ActionResult Edit(int id)
        {
            ViewBag.title = "Cập nhập túi sách";
            ViewBag.active = "product";
            ViewBag.categories = ListDropdownCategory();
            Product product = productService.get(id);
            product.change_img = false;
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Product data)
        {
            ViewBag.title = "Cập nhập túi sách";
            ViewBag.user = Common.Session.getSession(Constant.userSession) == null ? "" : (Common.Session.getSession(Constant.userSession) as Account).username;
            ViewBag.active = "product";
            ViewBag.categories = ListDropdownCategory();
            if (data.price != null && data.price <= 0)
            {
                ModelState.AddModelError("price", "Giá phải lớn hơn 0");
            }
            if (data.change_img)
            {
                if (data.image != null && !productService.checkFile(data.image))
                    ViewBag.error_img = "Hình ảnh không đúng định dạng chỉ nhận file png, jpg hoặc jpeg";
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (productService.checkFile(data.image))
                        {
                            if (!productService.update(id, data))
                            {
                                ViewBag.error = "Cập nhập túi sách không thành công";
                                return View(data);
                            }
                            ViewBag.success = "Cập nhập túi sách thành công";
                            ModelState.Clear();
                        }
                        return View();
                    }
                    else
                    {
                        return View(data);
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                try
                {
                    if (ModelState.IsValidField("name") &&
                        ModelState.IsValidField("description") &&
                        ModelState.IsValidField("unit") &&
                        ModelState.IsValidField("price") &&
                        ModelState.IsValidField("category"))
                    {
                        if (!productService.update(id, data))
                        {
                            ViewBag.error = "Cập nhập túi sách không thành công";
                            return View(data);
                        }
                        ViewBag.success = "Cập nhập túi sách thành công";
                        ModelState.Clear();
                        return View();
                    }
                    else
                    {
                        return View(data);
                    }
                }
                catch
                {
                    return View();
                }
            }
        }
    }
}