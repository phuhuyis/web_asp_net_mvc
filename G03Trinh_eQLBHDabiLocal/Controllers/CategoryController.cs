using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        private CategoryService categoryService = new CategoryService(new Entity.EFDbContext());
        private ProductService productService = new ProductService(new Entity.EFDbContext());
        // GET: Category
        [Route("{metaTitle}/{page?}")]
        public ActionResult Index(String metaTitle, int? page = 1)
        {
            if(metaTitle == null)
            {
                return RedirectToAction("Index", "Home");
            }    
            if(categoryService.getByMetaTitle(metaTitle) == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.title = "Hãng " + categoryService.getByMetaTitle(metaTitle).name;
            ViewBag.metaTitle = metaTitle;
            ViewBag.page = page;
            ViewBag.fullPage = productService.getFullPageByMetaTitle(metaTitle);
            return View(productService.listByMetaTitle(metaTitle, page));
        }
    }
}