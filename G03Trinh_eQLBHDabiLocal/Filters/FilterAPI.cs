using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using System.Web.Routing;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class FilterAPI : ActionFilterAttribute
    {
        private AccountService accountService = new AccountService(new Entity.EFDbContext());
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            String url = actionContext.Request.RequestUri.ToString();
            if (url.ToLower().Contains("/admin") && !url.ToLower().Contains("/auth"))
            {
                Account user = Session.getSession(Constant.userSession) as Account;
                if (user == null || user.permission != 1)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    var data = JsonConvert.SerializeObject(new { message = "Chưa xác thực" });
                    actionContext.Response.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }
                /*if (!HttpContext.Current.Request.IsAuthenticated ||
                    accountService.getPermission(AuthenticationUtil.ValidateToken(HttpContext.Current.Request.Headers.GetValues("Authorization"))) != 1)
                {

                }
                String token = Session.getSession(Constant.userSession) != null ? Session.getSession(Constant.userSession).ToString() : null;
                if (token == null || accountService.getPermission(AuthenticationUtil.ValidateToken(token)) != 1)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    var data = JsonConvert.SerializeObject(new { message = "Chưa xác thực" });
                    actionContext.Response.Content = new StringContent(data, Encoding.UTF8, "application/json");
                }*/
            }
            base.OnActionExecuting(actionContext);
        }

    }
}