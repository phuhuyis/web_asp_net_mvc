using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using G03Trinh_eQLBHDabiLocal.Entity;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Api
{
    public class CartController : ApiController
    {
        private CartService cartService = new CartService();
        private BillService billService = new BillService();
        private CustomerService customerService = new CustomerService(new EFDbContext());
        private BankService bankService = new BankService(new EFDbContext());

        [Route("api/cart")]
        [HttpPost]
        [ResponseType(typeof(Cart))]
        public IHttpActionResult add(Cart cart)
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            cart.customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username).id;
            Cart cartRs = cartService.add(cart);
            if (cartRs == null)
                return BadRequest();
            return Ok(cartRs);
        }

        [Route("api/cart")]
        [HttpGet]
        [ResponseType(typeof(List<cart>))]
        public IHttpActionResult list()
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            List<cart> carts = cartService.list((Common.Session.getSession(Common.Constant.userSession) as Account).username);
            if(carts == null)
                return BadRequest();
            return Ok(carts);
        }

        [Route("api/cart/count")]
        [HttpGet]
        public IHttpActionResult getCount()
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            List<cart> carts = cartService.list((Common.Session.getSession(Common.Constant.userSession) as Account).username);
            if (carts == null)
                return Ok(new { count = 0 });
            return Ok(new { count = carts.Count });
        }

        [Route("api/cart/{product}")]
        [HttpGet]
        [ResponseType(typeof(cart))]
        public IHttpActionResult get(int product)
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            Cart cartModel = new Cart() { 
                product = product,
                customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username).id
            };
            cart cart = cartService.get(cartModel);
            if (cart == null)
                return BadRequest();
            return Ok(cart);
        }

        [Route("api/cart/all")]
        [HttpPut]
        public IHttpActionResult updateAll(List<Cart> carts)
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            foreach(Cart cart in carts)
            {
                cart.customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username).id;
            }
            cartService.updateAll(carts);
            return Ok("success");
        }

        [Route("api/cart")]
        [HttpPut]
        public IHttpActionResult update(Cart cart)
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            cart.customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username).id;
            cartService.update(cart);
            return Ok("success");
        }

        [Route("api/cart")]
        [HttpDelete]
        public IHttpActionResult delete(Cart cart)
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            cart.customer = customerService.findByUsername((Common.Session.getSession(Common.Constant.userSession) as Account).username).id;
            cartService.delete(cart);
            return Ok("success");
        }

        [Route("api/cart/all")]
        [HttpDelete]
        public IHttpActionResult removeAll()
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            cartService.removeAll((Common.Session.getSession(Common.Constant.userSession) as Account).username);
            return Ok("success");
        }

        [Route("api/pay")]
        [HttpDelete]
        public IHttpActionResult pay()
        {
            if (Common.Session.getSession(Common.Constant.userSession) == null
                || (Common.Session.getSession(Common.Constant.userSession) as Account).permission != 0)
                return Unauthorized();
            if (!billService.pay((Common.Session.getSession(Common.Constant.userSession) as Account).username))
                return BadRequest();
            return Ok(bankService.createQR((Common.Session.getSession(Common.Constant.userSession) as Account).username).data.qrDataURL);
        }
    }
}
