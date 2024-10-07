using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private ProductService productService = new ProductService(new Entity.EFDbContext());
        private CustomerService customerService = new CustomerService(new Entity.EFDbContext());

        // GET: Product
        [Route("{id?}")]
        public ActionResult Index(int? id)
        {
            if(id == null)
                return RedirectToAction("Index", "Home");
            Product model = productService.get(id);
            ViewBag.title = model == null ? "Sản phẩm không tồn tại" : model.name;
            ViewBag.similar = productService.similar(id);
            if(Common.Session.getSession(Common.Constant.userSession) != null)
                ViewBag.customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username);
            return View(model);
        }
    }
}