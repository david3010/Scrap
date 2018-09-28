using Aplicacion_de_SCRAP.Models;
using Aplicacion_de_SCRAP.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
namespace Aplicacion_de_SCRAP.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index(int ? page)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();
            foreach (var user in users)
            {
                var userView = new UserView
                {
                    UserID = user.Id,
                    EMail = user.Email,
                    Name = user.Name
                };
                usersView.Add(userView);
            }
            usersView.OrderBy(u => u.UserID);

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(usersView.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Roles(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RolView>();

            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RolView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }
            if (rolesView.Count == 0)
            {
                TempData["Empty"] = "<p>El usuario no tiene permisos asignados<p>";
            }


            var userView = new UserView
            {
                UserID = user.Id,
                EMail = user.Email,
                Name = user.Name,
                Roles = rolesView
            };

            return View(userView);
        }

        [HttpGet]
        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                UserID = user.Id,
                EMail = user.Email,
                Name = user.Name
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = "[Seleccione permiso...]" });
            list = list.OrderBy(p => p.Name).ToList();
            ViewBag.RoleID = new SelectList(list, "Id", "Name");
            return View(userView);
        }

        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            var roleID = Request["RoleID"];

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                UserID = user.Id,
                EMail = user.Email,
                Name = user.Name
            };

            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.Empty = "Debes seleccionar un permiso";

                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Seleccione permiso...]" });
                list = list.OrderBy(p => p.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name");
                return View(userView);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == roleID);
            if (!userManager.IsInRole(user.Id, role.Name))
            {
                userManager.AddToRole(user.Id, role.Name);
            }

            var rolesView = new List<RolView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RolView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }
            if (rolesView.Count == 0)
            {
                TempData["Empty"] = "<p>El usuario no tiene permisos asignados<p>";
            }


            userView = new UserView
            {
                UserID = user.Id,
                EMail = user.Email,
                Name = user.Name,
                Roles = rolesView
            };

            return View("Roles", userView);
        }

        public ActionResult DeleteRole(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.Users.ToList().Find(u => u.Id == userID);
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleID);

            //Delete Rol
            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }
            //Return userView
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RolView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RolView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }
            if (rolesView.Count == 0)
            {
                TempData["Empty"] = "<p>El usuario no tiene permisos asignados<p>";
            }


            var userView = new UserView
            {
                UserID = user.Id,
                EMail = user.Email,
                Name = user.Name,
                Roles = rolesView
            };

            return View("Roles" , userView);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}