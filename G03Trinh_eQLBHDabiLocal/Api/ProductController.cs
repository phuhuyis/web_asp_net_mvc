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
    [RoutePrefix("api/admin/product")]
    public class ProductController : ApiController
    {
        private ProductService productService = new ProductService(new EFDbContext());
        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(customer))]
        public IHttpActionResult Deletecustomer(int id)
        {
            Product product = productService.get(id);
            if (product == null || !productService.delete(product))
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
