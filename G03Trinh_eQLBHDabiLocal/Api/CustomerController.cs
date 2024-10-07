using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    [RoutePrefix("api/admin/customer")]
    public class CustomerController : ApiController
    {
        private CustomerService customerService = new CustomerService(new EFDbContext());

        [HttpDelete]
        [Route("{id}")]
        [ResponseType(typeof(customer))]
        public IHttpActionResult Deletecustomer(int id)
        {
            Customer customer = customerService.get(id);
            if (customer == null || !customerService.delete(customer))
            {
                return NotFound();
            }
            customer.username = null;
            customer.password = null;
            return Ok(customer);
        }
    }
}