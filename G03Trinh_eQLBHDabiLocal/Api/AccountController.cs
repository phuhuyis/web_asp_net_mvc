using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers.Api
{
    [System.Web.Http.RoutePrefix("api/auth")]
    public class AccountController : ApiController
    {
        private readonly AccountService accountService = new AccountService(new Entity.EFDbContext());

        [System.Web.Http.Route("logout")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult logout()
        {
            if (Common.Session.getSession(Common.Constant.userSession) != null)
                Common.Session.delSession(Common.Constant.userSession);
            else
                return NotFound();
            return Ok("Success");
        }
    }
}