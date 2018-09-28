using Aplicacion_de_SCRAP.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplicacion_de_SCRAP.Controllers
{
    public class HomeController : Controller
    {
        private SCRAPContext db = new SCRAPContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetNotifications()
        {
            var Session = User.Identity.GetUserId();
            int user = Convert.ToInt32(Session);
            int count = 0;
            if (User.IsInRole("SMT") || User.IsInRole("Ingeniero") || User.IsInRole("Ger_Jef") || User.IsInRole("Admin"))
            {
                var Tickets = from u in db.TicketsSMTs
                              join a in db.Autorizantes on u.TicketSMTID equals a.TicketID
                              where a.UserId == user
                              where a.FechaDeAutorizacion == null
                              where a.Checked == false
                              where u.TicketStatus == TicketStatus.Pendiente
                              where a.dpto == "SMT"
                              select u;

                count = Tickets.Count();
            }
            if(count == 0)
            {
                if (User.IsInRole("Procesos") || User.IsInRole("Ingeniero") || User.IsInRole("Ger_Jef") || User.IsInRole("Admin"))
                {
                    var tickets = from u in db.Tickets
                                  join a in db.Autorizantes on u.TicketScrapID equals a.TicketID
                                  where a.UserId == user
                                  where a.FechaDeAutorizacion == null
                                  where a.Checked == false
                                  where u.TicketStatus == TicketStatus.Pendiente
                                  where a.dpto == "Procesos"
                                  select u;
                    count = tickets.Count();
                }
            }
            return Json(count, JsonRequestBehavior.AllowGet);
        }
    }
}