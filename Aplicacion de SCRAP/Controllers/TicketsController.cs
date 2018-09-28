using Aplicacion_de_SCRAP.Models;
using Aplicacion_de_SCRAP.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using PagedList;

namespace Aplicacion_de_SCRAP.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private SCRAPContext db = new SCRAPContext();
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: Tickets
        [Authorize(Roles = "Admin, SuperUser, Ger_Jef, Ingeniero, Supervisor, Tecnico")]
        public ActionResult Index(int? page, string sortOrder)
        {
            var Session = User.Identity.GetUserId();
            int user = Convert.ToInt32(Session);
            List<Tickets> tickets = new List<Tickets>();
            if (User.IsInRole("Procesos") || User.IsInRole("Ingeniero") || User.IsInRole("Ger_Jef") || User.IsInRole("Admin"))
            {
                //var tickets = db.Tickets.Include(t => t.AreaScrap).Include(t => t.DefectCodes).Include(t => t.Lines).Include(t => t.Naeds).Include(t => t.NoParts).Include(t => t.RootCause).Include(t => t.Sub_Ensambles);
                var Tickets = from u in db.Tickets
                              join a in db.Autorizantes on u.TicketScrapID equals a.TicketID
                              where a.UserId == user
                              where a.FechaDeAutorizacion == null
                              where a.Checked == false
                              where u.TicketStatus == TicketStatus.Pendiente
                              where a.dpto == "Procesos"
                              select u;

                ViewBag.AreaSortParm = String.IsNullOrEmpty(sortOrder) ? "AreaSortParm" : "";
                ViewBag.Codigos = String.IsNullOrEmpty(sortOrder) ? "Codigos" : "";
                ViewBag.Linea = String.IsNullOrEmpty(sortOrder) ? "Linea" : "";
                ViewBag.Naed = String.IsNullOrEmpty(sortOrder) ? "Naed" : "";
                ViewBag.SubEnsamble = String.IsNullOrEmpty(sortOrder) ? "SubEnsamble" : "";
                ViewBag.NoDeParte = String.IsNullOrEmpty(sortOrder) ? "NoDeParte" : "";
                ViewBag.CausaRaiz = String.IsNullOrEmpty(sortOrder) ? "CausaRaiz" : "";
                ViewBag.Creador = String.IsNullOrEmpty(sortOrder) ? "Creador" : "";
                ViewBag.Cantidad = String.IsNullOrEmpty(sortOrder) ? "Cantidad" : "";
                ViewBag.Costo = String.IsNullOrEmpty(sortOrder) ? "Costo" : "";
                ViewBag.Designacion = String.IsNullOrEmpty(sortOrder) ? "Designacion" : "";
                ViewBag.Reparacion = String.IsNullOrEmpty(sortOrder) ? "Reparacion" : "";
                switch (sortOrder)
                {
                    case "AreaSortParm":
                        Tickets = Tickets.OrderByDescending(s => s.AreaScrap.NAreaScrap);
                        break;
                    case "Codigos":
                        Tickets = Tickets.OrderByDescending(s => s.DefectCodes.Description);
                        break;
                    case "Linea":
                        Tickets = Tickets.OrderByDescending(s => s.Lines.LineID);
                        break;
                    case "Naed":
                        Tickets = Tickets.OrderByDescending(s => s.Naeds.NNAED);
                        break;
                    case "SubEnsamble":
                        Tickets = Tickets.OrderByDescending(s => s.Sub_Ensambles.Sub_Ensamble_Description);
                        break;
                    case "NoDeParte":
                        Tickets = Tickets.OrderByDescending(s => s.NoParts.NPart);
                        break;
                    case "CausaRaiz":
                        Tickets = Tickets.OrderByDescending(s => s.RootCause.Description);
                        break;
                    case "Creador":
                        Tickets = Tickets.OrderByDescending(s => s.Creator);
                        break;
                    case "Cantidad":
                        Tickets = Tickets.OrderByDescending(s => s.Quantity);
                        break;
                    case "Costo":
                        Tickets = Tickets.OrderByDescending(s => s.Cost);
                        break;
                    case "Designacion":
                        Tickets = Tickets.OrderByDescending(s => s.Designation);
                        break;
                    case "Reparacion":
                        Tickets = Tickets.OrderByDescending(s => s.Repair);
                        break;
                    default:
                        Tickets = Tickets.OrderBy(s => s.TicketScrapID);
                        break;
                }

                int pageSize = 15;
                int pageNumber = (page ?? 1);
                return View(Tickets.ToPagedList(pageNumber, pageSize));
            }
            int pagesize = 15;
            int pagenumber = (page ?? 1);
            //return View(Tickets.ToPagedList(pageNumber, pageSize));
            return View(tickets.ToPagedList(pagenumber, pagesize));
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin, SuperUser, Ger_Jef, Ingeniero, Supervisor, Tecnico")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            var listAreas = db.AreaScraps.ToList();
            listAreas.Add(new AreaScrap { AreaSCRAPID = 0, NAreaScrap = "[Seleccione Área...]" });
            listAreas = listAreas.OrderBy(t => t.NAreaScrap).ToList();
            ViewBag.AreaSCRAPID = new SelectList(listAreas, "AreaSCRAPID", "NAreaScrap");

            var listDC = db.DefectCodes.ToList();
            listDC.Add(new DefectCodes { DefectCodeID = 0, Description = "[Seleccione código...]" });
            listDC = listDC.OrderBy(l => l.Description).ToList();
            ViewBag.DefectCodeID = new SelectList(listDC, "DefectCodeID", "Description");

            var listaLineas = from u in db.Lines where u.Tipo == 0 select u;
            List<Line> listLines = new List<Line>();
            listLines = listaLineas.ToList();
            listLines.Add(new Line { LineID = 0, LineName = "[Seleccione Linea...]" });
            listLines = listLines.OrderBy(l => l.LineName).ToList();
            ViewBag.LineID = new SelectList(listLines, "IIdLinea", "LineName");

            var listNaeds = db.Naeds.ToList();
            listNaeds.Add(new Naed { NaedID = 0, NaedDescription = "[Seleccione Naed...]", NNAED = 0 });
            listNaeds = listNaeds.OrderBy(l => l.NNAED).ToList();
            ViewBag.NaedID = new SelectList(listNaeds, "NaedID", "NNaed");

            var listSub_Ensambles = db.Sub_Ensamble.ToList();
            listSub_Ensambles.Add(new Sub_Ensamble { Sub_EnsambleID = 0, Sub_Ensamble_Description = "[Seleccione Sub-Ensamble...]" });
            listSub_Ensambles = listSub_Ensambles.OrderBy(l => l.Sub_Ensamble_Description).ToList();
            ViewBag.Sub_EnsambleID = new SelectList(listSub_Ensambles, "Sub_EnsambleID", "Sub_Ensamble_Description");

            var listPartNo = db.PartNoes.ToList();
            listPartNo.Add(new PartNo { PartNoID = 0, NPart = "[Seleccione No. de parte...]" });
            listPartNo = listPartNo.OrderBy(l => l.NPart).ToList();
            ViewBag.PartNoID = new SelectList(listPartNo, "PartNoID", "NPart");

            var listRoot = db.RootCauses.ToList();
            listRoot.Add(new RootCause { RootCauseID = 0, Description = "[Seleccione causa...]" });
            listRoot = listRoot.OrderBy(l => l.NRootCause).ToList();
            ViewBag.RootCauseID = new SelectList(listRoot, "RootCauseID", "Description");

            var listsup = GetUsersInRole("Supervisor");
            listsup.Add(new ApplicationUser { Id = "0", Name = "[Seleccione supervisor...]" });
            listsup = listsup.OrderBy(l => l.Id).ToList();
            ViewBag.SupervisorID = new SelectList(listsup, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketScrapID,Quantity,LineID,SupervisorID,Location,AreaSCRAPID,NaedID,Sub_EnsambleID,PartNoID,DefectCodeID,RootCauseID,Repair,Location1,Location2,Location3")] Tickets tickets)
        {
            var Sub_EnsambleID = Request["Sub_EnsambleID"];
            var AreaSCRAPID = Request["AreaSCRAPID"];
            var DefectCodeID = Request["DefectCodeID"];
            var LineID = Request["LineID"];
            var NaedID = Request["NaedID"];
            var PartNoID = Request["PartNoID"];
            var RootCauseID = Request["RootCauseID"];
            var SupervisorID = Request["SupervisorID"];
            int id = 0;

            if (AreaSCRAPID == "0" || DefectCodeID == "0" || LineID == "0" || NaedID == "0" || PartNoID == "0" || RootCauseID == "0" || SupervisorID == "0" || Sub_EnsambleID == "0")
            {
                TempData["Alerta"] = "<script>swal('Debe llenar/actualizar todos los campos!');</script>";
                var listAreas = db.AreaScraps.ToList();
                listAreas.Add(new AreaScrap { AreaSCRAPID = 0, NAreaScrap = "[Seleccione Área...]" });
                listAreas = listAreas.OrderBy(t => t.NAreaScrap).ToList();
                ViewBag.AreaSCRAPID = new SelectList(listAreas, "AreaSCRAPID", "NAreaScrap");

                var listDC = db.DefectCodes.ToList();
                listDC.Add(new DefectCodes { DefectCodeID = 0, Description = "[Seleccione código...]" });
                listDC = listDC.OrderBy(l => l.Description).ToList();
                ViewBag.DefectCodeID = new SelectList(listDC, "DefectCodeID", "Description");

                var listaLineas = from u in db.Lines where u.Tipo == 0 select u;
                List<Line> listLines = new List<Line>();
                listLines = listaLineas.ToList();
                listLines.Add(new Line { LineID = 0, LineName = "[Seleccione Linea...]" });
                listLines = listLines.OrderBy(l => l.LineName).ToList();
                ViewBag.LineID = new SelectList(listLines, "IIdLinea", "LineName");

                var listNaeds = db.Naeds.ToList();
                listNaeds.Add(new Naed { NaedID = 0, NaedDescription = "[Seleccione Naed...]", NNAED = 0 });
                listNaeds = listNaeds.OrderBy(l => l.NNAED).ToList();
                ViewBag.NaedID = new SelectList(listNaeds, "NaedID", "NNaed");

                var listSub_Ensambles = db.Sub_Ensamble.ToList();
                listSub_Ensambles.Add(new Sub_Ensamble { Sub_EnsambleID = 0, Sub_Ensamble_Description = "[Seleccione Sub-Ensamble...]" });
                listSub_Ensambles = listSub_Ensambles.OrderBy(l => l.Sub_Ensamble_Description).ToList();
                ViewBag.Sub_EnsambleID = new SelectList(listSub_Ensambles, "Sub_EnsambleID", "Sub_Ensamble_Description");

                var listPartNo = db.PartNoes.ToList();
                listPartNo.Add(new PartNo { PartNoID = 0, NPart = "[Seleccione No. de parte...]" });
                listPartNo = listPartNo.OrderBy(l => l.NPart).ToList();
                ViewBag.PartNoID = new SelectList(listPartNo, "PartNoID", "NPart");

                var listRoot = db.RootCauses.ToList();
                listRoot.Add(new RootCause { RootCauseID = 0, Description = "[Seleccione causa...]" });
                listRoot = listRoot.OrderBy(l => l.NRootCause).ToList();
                ViewBag.RootCauseID = new SelectList(listRoot, "RootCauseID", "Description");

                var listsup = GetUsersInRole("Supervisor");
                listsup.Add(new ApplicationUser { Id = "0", Name = "[Seleccione supervisor...]" });
                listsup = listsup.OrderBy(l => l.Id).ToList();
                ViewBag.SupervisorID = new SelectList(listsup, "Id", "Name");
                return View(tickets);
            }

            var NpartPrice = db.PartNoes.Find(tickets.PartNoID);
            tickets.Cost = NpartPrice.UnitPrice * tickets.Quantity;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == User.Identity.GetUserId());

            tickets.Creator = Convert.ToInt32(user.Id);
            tickets.CreateDate = DateTime.Now;

            string date = DateTime.Now.ToShortTimeString();
            DateTime Date = Convert.ToDateTime(date);
            if (Date >= Convert.ToDateTime("06:00 AM") && Date <= Convert.ToDateTime("14:30 PM"))
            {
                tickets.Turn = 1;
            }
            else if (Date > Convert.ToDateTime("14:30 PM") && Date <= Convert.ToDateTime("22:30 PM"))
            {
                tickets.Turn = 2;
            }
            else if (Date > Convert.ToDateTime("22:30 PM") || Date < Convert.ToDateTime("06:00 AM"))
            {
                if (Date > Convert.ToDateTime("22:30 PM") && Date < Convert.ToDateTime("23:59 AM"))
                    tickets.CreateDate.AddDays(1);
                tickets.Turn = 3;
            }

            tickets.Designation = tickets.Location + " - " + tickets.Location1 + " - " + tickets.Location2 + " - " + tickets.Location3;
            if (ModelState.IsValid)
            {
                db.Tickets.Add(tickets);
                try
                {
                    db.SaveChanges();

                    id = tickets.TicketScrapID;
                }
                catch (DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }

            if (tickets.Cost < 34)
            {
                var super = GetUsersInRole("Supervisor");
                var tecnicos = GetUsersInRole("Tecnico");
                if (super.Count() != 0 || tecnicos.Count() != 0)
                {
                    SaveAutorizantes(super, id, tickets, "Supervisor");
                    SaveAutorizantes(tecnicos, id, tickets, "Tecnico");
                }
                else
                {
                    TempData["Message"] = "<script>swal(" + "'Llame al administrador'" + "," + "'No se puede crear un ticket si no hay alguien asignado para que autorice..'" + "," + "'warning'" + ");</script>";
                    return RedirectToAction("Create");
                }
                TempData["Message"] = "<script>swal('Folio: " + tickets.TicketScrapID.ToString() + " ', 'El ticket se creó correctamente!');</script>";
                return RedirectToAction("Create");
            }
            else if (tickets.Cost >= 34 && tickets.Cost < 70)
            {
                var Ing = GetUsersInRole("Ingeniero");
                if (Ing.Count() != 0)
                {
                    SaveAutorizantes(Ing, id, tickets, "Ingeniero");
                }
                else
                {
                    TempData["Message"] = "<script>swal(" + "'Llame al administrador'" + "," + "'No se puede crear un ticket si no hay alguien asignado para que autorice..'" + "," + "'warning'" + ");</script>";
                    return RedirectToAction("Create");
                }
                TempData["Message"] = "<script>swal('Folio: " + tickets.TicketScrapID.ToString() + " ', 'El ticket se creó correctamente!');</script>";
                return RedirectToAction("Create");
            }
            else if (tickets.Cost >= 70)
            {
                var Ger = GetUsersInRoleGerJef("Ger_Jef");
                if (Ger.Count() != 0)
                {
                    SaveAutorizantes(Ger, id, tickets, "Ger_Jef");
                }
                else
                {
                    TempData["Message"] = "<script>swal(" + "'Llame al administrador'" + "," + "'No se puede crear un ticket si no hay alguien asignado para que autorice..'" + "," + "'warning'" + ");</script>";
                    return RedirectToAction("Create");
                }
                TempData["Message"] = "<script>swal('Folio: " + tickets.TicketScrapID.ToString() + " ', 'El ticket se creó correctamente!');</script>";
                return RedirectToAction("Create");
            }

            ViewBag.AreaSCRAPID = new SelectList(db.AreaScraps.OrderBy(a => a.NAreaScrap), "AreaSCRAPID", "NAreaScrap");
            ViewBag.DefectCodeID = new SelectList(db.DefectCodes.OrderBy(dc => dc.DefectCode), "DefectCodeID", "DefectCode");
            var ListaLineas = (from u in db.Lines where u.Tipo == 0 select u).ToList();
            ViewBag.LineID = new SelectList(ListaLineas.OrderBy(l => l.LineName), "IIdLinea", "LineName");
            ViewBag.NaedID = new SelectList(db.Naeds.OrderBy(nn => nn.NNAED), "NaedID", "NNaed");
            ViewBag.SubEnsambleID = new SelectList(db.Sub_Ensamble.OrderBy(se => se.Sub_Ensamble_Description), "Sub_EnsambleID", "Sub_Ensamble_Description");
            ViewBag.PartNoID = new SelectList(db.PartNoes.OrderBy(np => np.NPart), "PartNoID", "Part");
            ViewBag.RootCauseID = new SelectList(db.RootCauses.OrderBy(rc => rc.NRootCause), "RootCauseID", "Description");
            ViewBag.SupervisorID = new SelectList(GetUsersInRole("Supervisor"), "Id", "Name");
            return View(tickets);
        }

        public void SaveAutorizantes(List<ApplicationUser> users, int id, Tickets tickets, string rol)
        {
            var NameUser = "";
            int UserId;

            var roleUserIdsQuery = from role in ctx.Roles
                                   where role.Name == "Procesos"
                                   from u in role.Users
                                   select u.UserId;

            List<ApplicationUser> usuarios = new List<ApplicationUser>();
            usuarios = ctx.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();

            foreach (var item in users)
            {
                foreach (var user in usuarios)
                {
                    if (item.Id == user.Id)
                    {
                        NameUser = item.Name;
                        UserId = Convert.ToInt32(item.Id);
                        Autorizantes auth = new Autorizantes
                        {
                            UserId = UserId,
                            UserName = NameUser,
                            Checked = false,
                            FechaDeAutorizacion = null,
                            TicketID = id,
                            Position = rol,
                            dpto = "Procesos"
                        };
                        db.Autorizantes.Add(auth);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            // Retrieve the error messages as a list of strings.
                            var errorMessages = ex.EntityValidationErrors
                                    .SelectMany(x => x.ValidationErrors)
                                    .Select(x => x.ErrorMessage);

                            // Join the list to a single string.
                            var fullErrorMessage = string.Join("; ", errorMessages);

                            // Combine the original exception message with the new one.
                            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                            // Throw a new DbEntityValidationException with the improved exception message.
                            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        }
                    }
                }
            }
        }

        public List<ApplicationUser> GetUsersInRole(string RoleName)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var roleUserIdsQuery = from role in ctx.Roles
                                       where role.Name == RoleName
                                       from u in role.Users
                                       select u.UserId;

                var usuarios = ctx.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
                return usuarios;
            }
        }

        public List<ApplicationUser> GetUsersInRoleGerJef(string RoleName)
        {
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                var roleUserIdsQuery = from role in ctx.Roles
                                       where role.Name == RoleName
                                       from u in role.Users
                                       select u.UserId;
                var usuarios = ctx.Users.Where(u => roleUserIdsQuery.Contains(u.Id)).ToList();
                return usuarios;
            }
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, SuperUser")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaSCRAPID = new SelectList(db.AreaScraps.OrderBy(a => a.NAreaScrap), "AreaSCRAPID", "NAreaScrap");
            ViewBag.DefectCodeID = new SelectList(db.DefectCodes.OrderBy(dc => dc.DefectCode), "DefectCodeID", "DefectCode");
            var listaLineas = (from u in db.Lines where u.Tipo == 0 select u).ToList();
            ViewBag.LineID = new SelectList(listaLineas.OrderBy(l => l.LineName), "IIdLinea", "LineName");
            ViewBag.NaedID = new SelectList(db.Naeds.OrderBy(nn => nn.NNAED), "NaedID", "NNaed");
            ViewBag.SubEnsambleID = new SelectList(db.Sub_Ensamble.OrderBy(se => se.Sub_Ensamble_Description), "Sub_EnsambleID", "Sub_Ensamble_Description");
            ViewBag.PartNoID = new SelectList(db.PartNoes.OrderBy(np => np.NPart), "PartNoID", "NPart");
            ViewBag.RootCauseID = new SelectList(db.RootCauses.OrderBy(rc => rc.NRootCause), "RootCauseID", "Description");
            ViewBag.SupervisorID = new SelectList(GetUsersInRole("Supervisor"), "Id", "Name");
            return View(tickets);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperUser")]
        public ActionResult Edit([Bind(Include = "TicketScrapID,TicketStatus,NameoftheCreator,Quantity,LineID,SupervisorID,Location,AreaSCRAPID,Sub_EnsambleID,NaedID,PartNoID,DefectCodeID,RootCauseID,Repair")] Tickets tickets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tickets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaSCRAPID = new SelectList(db.AreaScraps.OrderBy(a => a.NAreaScrap), "AreaSCRAPID", "NAreaScrap");
            ViewBag.DefectCodeID = new SelectList(db.DefectCodes.OrderBy(dc => dc.DefectCode), "DefectCodeID", "DefectCode");
            var listaLineas = (from u in db.Lines where u.Tipo == 0 select u).ToList();
            ViewBag.LineID = new SelectList(listaLineas.OrderBy(l => l.LineName), "IIdLinea", "LineName");
            ViewBag.NaedID = new SelectList(db.Naeds.OrderBy(nn => nn.NNAED), "NaedID", "NNaed");
            ViewBag.SubEnsambleID = new SelectList(db.Sub_Ensamble.OrderBy(se => se.Sub_Ensamble_Description), "Sub_EnsambleID", "Sub_Ensamble_Description");
            ViewBag.PartNoID = new SelectList(db.PartNoes.OrderBy(np => np.NPart), "PartNoID", "Part");
            ViewBag.RootCauseID = new SelectList(db.RootCauses.OrderBy(rc => rc.NRootCause), "RootCauseID", "Description");
            ViewBag.SupervisorID = new SelectList(GetUsersInRole("Supervisor"), "Id", "Name");
            return View(tickets);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin, SuperUser")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            return View(tickets);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, SuperUser")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tickets tickets = db.Tickets.Find(id);
            db.Tickets.Remove(tickets);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Export()
        {
            var data = new JsonResult() { Data = GetTickets(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            try
            {
                var ultimoExportado = (from u in db.Ultimo where u.bf == 1 select u.ultimo).Max();
                ViewBag.ultimoFolio = ultimoExportado.ToString();
            }
            catch (Exception ex)
            {
                ViewBag.ultimoFolio = "No se ah exportado ningun ticket";
            }

            return View();
        }

        [AllowAnonymous]
        public JsonResult ExportData()
        {
            //var tickets = GetTickets();
            var data = GetTickets();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //Export to excel action

        [Authorize(Roles = "Admin, SuperUser, Ger_Jef, Ingeniero, Supervisor, Tecnico, Capturista")]
        public ActionResult ExportToExcel()
        {
            //var tickets = GetTickets();
            var folio = (from t in db.Tickets select t.TicketScrapID).Max();
            Export ultimo = new Export();
            ultimo.ultimo = folio;
            ultimo.bf = 1;
            var ultimoExportado = (from u in db.Ultimo where u.bf == 1 select u.ultimo).Max();
            try
            {
                if (ModelState.IsValid)
                {
                    if (ultimo.ultimo != ultimoExportado)
                    {
                        db.Ultimo.Add(ultimo);
                        db.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }


            ViewBag.ultimoFolio = ultimoExportado.ToString();

            var data = db.Tickets
            .Include(t => t.AreaScrap)
            .Include(t => t.DefectCodes)
            .Include(t => t.Lines)
            .Include(t => t.Naeds)
            .Include(t => t.NoParts)
            .Include(t => t.RootCause)
            .Include(t => t.Sub_Ensambles)
            .Include(t => t.SupervisorID)
            .Select(t => new
            {
                Folio = t.TicketScrapID,
                Area = t.AreaScrap.NAreaScrap,
                Costo_USD = t.Cost,
                Estado = t.TicketStatus.ToString(),
                Autorizo = t.Authorizing,
                FechaDeCreacion = t.CreateDate.ToString(),
                Usuario = t.Creator,
                Defecto = t.DefectCodes.Description,
                Linea = t.Lines.LineName,
                Designacion = t.Designation,
                Naed = t.Naeds.NNAED,
                NoDeParte = t.NoParts.NPart,
                Cantidad = t.Quantity,
                TipoDeReparacion = t.Repair,
                Supervisor = t.SupervisorID,
                CausaRaiz = t.RootCause.Description,
                SubEnsamble = t.Sub_Ensambles.Sub_Ensamble_Description
            }).Where(t => t.Estado == "Aceptado" && t.Folio > ultimoExportado).ToList().OrderBy(t => t.Folio);

            if (data.Count() != 0)
            {
                var gv = new GridView();
                gv.DataSource = data;
                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=ReporteScrap " + DateTime.Now.ToString("dd/MM/yy H:mm tt") + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());
                Response.Flush();
                Response.End();
                return View();
            }
            else
            {
                TempData["Message"] = "<script>swal('No hay tickets para exportar!');</script>";
                return View("Export");
            }
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, SuperUser, Ger_Jef, Ingeniero, Supervisor, Tecnico")]
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            int ID = (int)id;
            if (User.IsInRole("Supervisor") == true)
            {
                if (ValidateAutorization("Tecnico", ID))
                {
                    tickets.TicketStatus = TicketStatus.Aceptado;
                    SaveAutorization(ID, "Supervisor");
                }
                else
                {
                    tickets.TicketStatus = TicketStatus.Pendiente;
                    SaveAutorization(ID, "Supervisor");
                }
            }
            else if (User.IsInRole("Tecnico") == true)
            {
                if (ValidateAutorization("Supervisor", ID))
                {
                    tickets.TicketStatus = TicketStatus.Aceptado;
                    SaveAutorization(ID, "Tecnico");
                }
                else
                {
                    tickets.TicketStatus = TicketStatus.Pendiente;
                    SaveAutorization(ID, "Tecnico");
                }
            }
            else if (User.IsInRole("Ingeniero"))
            {
                tickets.TicketStatus = TicketStatus.Aceptado;
                SaveAutorization(ID, "Ingeniero");
            }
            else if (User.IsInRole("Ger_Jef"))
            {
                tickets.TicketStatus = TicketStatus.Aceptado;
                SaveAutorization(ID, "Ger_Jef");
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == User.Identity.GetUserId());

            tickets.Location = "nul";
            tickets.Authorizing = Convert.ToInt32(user.Id);
            db.Entry(tickets).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            return RedirectToAction("Index");
        }

        public int SaveAutorization(int id, string rol)
        {
            var Session = User.Identity.GetUserId();
            int user = Convert.ToInt32(Session);
            using (SCRAPContext db = new SCRAPContext())
            {
                List<Autorizantes> Auth = new List<Autorizantes>();
                Auth = (from a in db.Autorizantes where a.Position == rol && a.TicketID == id where a.dpto == "Procesos" select a).ToList();
                foreach (var Autorizante in Auth)
                {
                    if (Autorizante.UserId == user && Autorizante.TicketID == id)
                    {
                        Autorizante.Checked = true;
                        Autorizante.FechaDeAutorizacion = DateTime.Now;
                        db.Entry(Autorizante).State = EntityState.Modified;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            // Retrieve the error messages as a list of strings.
                            var errorMessages = ex.EntityValidationErrors
                                    .SelectMany(x => x.ValidationErrors)
                                    .Select(x => x.ErrorMessage);

                            // Join the list to a single string.
                            var fullErrorMessage = string.Join("; ", errorMessages);

                            // Combine the original exception message with the new one.
                            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                            // Throw a new DbEntityValidationException with the improved exception message.
                            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        }
                    }
                    Autorizante.Checked = true;
                    db.Entry(Autorizante).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        // Retrieve the error messages as a list of strings.
                        var errorMessages = ex.EntityValidationErrors
                                .SelectMany(x => x.ValidationErrors)
                                .Select(x => x.ErrorMessage);

                        // Join the list to a single string.
                        var fullErrorMessage = string.Join("; ", errorMessages);

                        // Combine the original exception message with the new one.
                        var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                        // Throw a new DbEntityValidationException with the improved exception message.
                        throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                    }
                }
            }
            return 0;
        }

        public static bool ValidateAutorization(string rol, int id)
        {
            bool validado = false;
            using (SCRAPContext db = new SCRAPContext())
            {
                List<Autorizantes> autorizantes = new List<Autorizantes>();
                autorizantes = (from a in db.Autorizantes where a.Position == rol && a.TicketID == id select a).ToList();

                foreach (var autorizante in autorizantes)
                {
                    if (autorizante.Checked == true)
                    {
                        validado = true;
                        return validado;
                    }
                }
            }
            return validado;
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, SuperUser, Ger_Jef, Ingeniero, Supervisor, Tecnico")]
        public ActionResult Refuse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tickets tickets = db.Tickets.Find(id);
            if (tickets == null)
            {
                return HttpNotFound();
            }
            tickets.Location = "nul";
            tickets.TicketStatus = TicketStatus.Rechazado;
            db.Entry(tickets).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            return RedirectToAction("Index");
        }

        private dynamic GetTickets()
        {

            var tickets = db.Tickets
            .Include(t => t.AreaScrap)
            .Include(t => t.DefectCodes)
            .Include(t => t.Lines)
            .Include(t => t.Naeds)
            .Include(t => t.NoParts)
            .Include(t => t.RootCause)
            .Include(t => t.Sub_Ensambles)
            .Include(t => t.SupervisorID)
            .Select(t => new
            {
                Folio = t.TicketScrapID,
                Area = t.AreaScrap.NAreaScrap,
                Costo_USD = t.Cost,
                Estado = t.TicketStatus.ToString(),
                Autorizo = t.Authorizing,
                FechaDeCreacion = t.CreateDate.ToString(),
                Usuario = t.Creator,
                Defecto = t.DefectCodes.Description,
                Linea = t.Lines.LineName,
                Designacion = t.Designation,
                Naed = t.Naeds.NNAED,
                NoDeParte = t.NoParts.NPart,
                Cantidad = t.Quantity,
                TipoDeReparacion = t.Repair,
                Supervisor = t.SupervisorID,
                CausaRaiz = t.RootCause.Description,
                SubEnsamble = t.Sub_Ensambles.Sub_Ensamble_Description
            }).Where(t => t.Estado == "Aceptado").ToList().OrderBy(t => t.Folio);

            return tickets;
        }

        public JsonResult GetNotifications()
        {
            var Session = User.Identity.GetUserId();
            int user = Convert.ToInt32(Session);
            var Tickets = from u in db.Tickets
                          join a in db.Autorizantes on u.TicketScrapID equals a.TicketID
                          where a.UserId == user
                          where a.FechaDeAutorizacion == null
                          where a.Checked == false
                          where u.TicketStatus == TicketStatus.Pendiente
                          where a.dpto == "Procesos"
                          select u;

            return Json(Tickets.Count(), JsonRequestBehavior.AllowGet);
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