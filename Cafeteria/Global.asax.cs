using Cafeteria.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Cafeteria
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateRoles();
        }
        private void CreateRoles()
        {
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Administrador"))
            {
                var role = new IdentityRole("Administrador");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Cajero"))
            {
                var role = new IdentityRole("Cajero");
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Cocinero"))
            {
                var role = new IdentityRole("Cocinero");
                roleManager.Create(role);
            }

            var admin = userManager.FindByEmail("administrador@gmail.com");
            if (admin != null && !userManager.IsInRole(admin.Id, "Administrador"))
            {
                userManager.AddToRole(admin.Id, "Administrador");
            }
            var cajero = userManager.FindByEmail("cajero@gmail.com");
            if (cajero != null && !userManager.IsInRole(cajero.Id, "Cajero"))
            {
                userManager.AddToRole(cajero.Id, "Cajero");
            }
            var cocinero = userManager.FindByEmail("cocinero@gmail.com");
            if (cocinero != null && !userManager.IsInRole(cocinero.Id, "Cocinero"))
            {
                userManager.AddToRole(cocinero.Id, "Cocinero");
            }


        }
    }
}

