using System;
using System.Web.Mvc;

namespace G03Trinh_eQLBHDabiLocal.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();
            context.MapRoute(
                "Parameter_Route",
                "admin/{controller}/edit/{id}",
                new { controller = "customer", Action = "edit", id = UrlParameter.Optional }
                );
            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}",
                new { controller = "Home", action = "Index" },
                namespaces: new String[] { "G03Trinh_eQLBHDabiLocal.Areas.Admin.Controllers" }
            );
        }
    }
}