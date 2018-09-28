using Aplicacion_de_SCRAP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Aplicacion_de_SCRAP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer
                (
                    new MigrateDatabaseToLatestVersion<Models.SCRAPContext, Migrations.Configuration>()
                );
            ApplicationDbContext db = new ApplicationDbContext();
            CreateSuperUser(db);
            CreateRoles(db);
            AddPremissionsToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        private void AddPremissionsToSuperUser(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindById("132996");

            if (!userManager.IsInRole(user.Id, "SuperUser"))
            {
                userManager.AddToRole(user.Id, "SuperUser");
            }
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("132996");

            if (user == null)
            {
                using (UsersDataDataContext ctx = new UsersDataDataContext())
                {
                    string newUserID = "132996";
                    var usuarioexistente = (from u in ctx.tblUsuarios
                                            where (newUserID == u.iIdNumUsuario.ToString())
                                            select u).FirstOrDefault();
                    if (usuarioexistente != null)
                    {
                        try
                        {
                            user = new ApplicationUser
                            {
                                UserName = usuarioexistente.iIdNumUsuario.ToString(),
                                Id = usuarioexistente.iIdNumUsuario.ToString(),
                                Email = usuarioexistente.strEmail,
                                BossId = usuarioexistente.iIdJefe,
                                Name = usuarioexistente.strNombre,
                            };
                            userManager.Create(user, usuarioexistente.strPassword);
                        }
                        catch { }
                    }
                }
            }
        }

        protected void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("SuperUser"))
            {
                roleManager.Create(new IdentityRole("SuperUser"));
            }

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            if (!roleManager.RoleExists("Ger_Jef"))
            {
                roleManager.Create(new IdentityRole("Ger_Jef"));
            }

            if (!roleManager.RoleExists("Ingeniero"))
            {
                roleManager.Create(new IdentityRole("Ingeniero"));
            }

            if (!roleManager.RoleExists("Tecnico"))
            {
                roleManager.Create(new IdentityRole("Tecnico"));
            }

            if (!roleManager.RoleExists("Supervisor"))
            {
                roleManager.Create(new IdentityRole("Supervisor"));
            }

            if (!roleManager.RoleExists("Capturista"))
            {
                roleManager.Create(new IdentityRole("Capturista"));
            }

            if (!roleManager.RoleExists("Procesos"))
            {
                roleManager.Create(new IdentityRole("Procesos"));
            }

            if (!roleManager.RoleExists("SMT"))
            {
                roleManager.Create(new IdentityRole("SMT"));
            }
        }
    }
}
