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
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;
using G03Trinh_eQLBHDabiLocal.Common;
using G03Trinh_eQLBHDabiLocal.Models;
using G03Trinh_eQLBHDabiLocal.Services;

namespace G03Trinh_eQLBHDabiLocal.Controllers
{
    public class FilterAdmin : ActionFilterAttribute
    {
        private AccountService accountService = new AccountService(new Entity.EFDbContext());
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            String url = filterContext.HttpContext.Request.Url.ToString();
            if (url.ToLower().Contains("/admin") && !url.ToLower().Contains("/login"))
            {
                //Debug.WriteLine(Session.getSession(Constant.userSession));
                Account user = Session.getSession(Constant.userSession) as Account;
                if (user == null || user.permission != 1)
                {
                    filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", Action = "Index", Area = "Admin" }));
                }
                base.OnActionExecuted(filterContext);
            }
            base.OnActionExecuted(filterContext);
        }

        /*if (HttpContext.Current.Request.Headers.GetValues("Authorization") != null)
                {
                    foreach (string vl in HttpContext.Current.Request.Headers.GetValues("Authorization"))
                    {
                        Debug.WriteLine(vl);
                    }
}
String token = Session.getSession(Constant.userSession) != null ? Session.getSession(Constant.userSession).ToString() : null;
if (token == null || accountService.getPermission(AuthenticationUtil.ValidateToken(token)) != 1)
{
    filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Login", Action = "Index", Area = "Admin" }));
}*/
    }
}