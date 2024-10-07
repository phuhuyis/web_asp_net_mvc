using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using G03Trinh_eQLBHDabiLocal.Controllers;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers.Api
{
    [RoutePrefix("api/admin/category")]
    public class CategoryController : ApiController
    {
        private CategoryService categoryService = new CategoryService(new EFDbContext());

        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(category))]
        public IHttpActionResult Deletecustomer(int id)
        {
            Category category = categoryService.get(id);
            if (category == null || !categoryService.delete(category))
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
