using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Api
{
    public class BillController : ApiController
    {
        private BillService billService = new BillService();
        private CustomerService customerService = new CustomerService(new EFDbContext());
        private BankService bankService = new BankService(new EFDbContext());
        private CartService cartService = new CartService();
        [Route("api/admin/bill/{id}")]
        [HttpDelete]
        public IHttpActionResult progress(int id)
        {
            if (billService.get(id) == null)
                return NotFound();
            billService.progress(id);
            return Ok("success");
        }

        [Route("api/admin/bill/{id}")]
        [HttpPut]
        public IHttpActionResult handle(int id)
        {
            if (billService.get(id) == null)
                return NotFound();
            billService.handle(id);
            return Ok("success");
        }

        [Route("api/bill")]
        [HttpPost]
        public IHttpActionResult add(Bill bill)
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            bill.customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username).id;
            bill.booking_date = DateTime.Now;
            bill.status = 0;
            if (!billService.add(bill))
                return BadRequest();
            if (bill.payment == 0)
            {
                cartService.removeAll((Common.Session.getSession(Common.Constant.userSession) as Account).username);
                return Ok("success");
            }
            return Ok(bankService.createQR((Common.Session.getSession(Common.Constant.userSession) as Account).username).data.qrDataURL);
        }
        [Route("api/bill/create")]
        [HttpPost]
        public IHttpActionResult create(Bill bill)
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            bill.customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username).id;
            bill.booking_date = DateTime.Now;
            bill.status = 0;
            if (!billService.create(bill))
                return BadRequest();
            if (bill.payment == 0)
            {
                return Ok("success");
            }
            return Ok(bankService.createQR(bill).data.qrDataURL);
        }
    }
}
