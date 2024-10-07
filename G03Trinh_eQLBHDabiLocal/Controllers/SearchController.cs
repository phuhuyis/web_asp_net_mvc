using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    [RoutePrefix("search")]
    public class SearchController : Controller
    {
        private CategoryService categoryService = new CategoryService(new Entity.EFDbContext());
        private ProductService productService = new ProductService(new Entity.EFDbContext());
        // GET: Search
        [Route("")]
        public ActionResult Index(String key, int? page = 1)
        {
            ViewBag.title = "Tìm kiếm " + "\"" + key + "\"";
            ViewBag.key = key;
            ViewBag.page = page;
            ViewBag.fullPage = productService.getFullPageSearch(key);
            return View(productService.search(key, page));
        }
    }
}