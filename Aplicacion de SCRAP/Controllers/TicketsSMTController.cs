using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aplicacion_de_SCRAP.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace Aplicacion_de_SCRAP.Controllers
{
    [Authorize]
    public class TicketsSMTController : Controller
    {
        private SCRAPContext db = new SCRAPContext();
        private ApplicationDbContext ctx = new ApplicationDbContext();

        // GET: TicketsSMT
        public ActionResult Index(int? page)
        {
            var Session = User.Identity.GetUserId();
            int user = Convert.ToInt32(Session);
            List<TicketsSMT> tickets = new List<TicketsSMT>();
            //var ticketsSMTs = db.TicketsSMTs.Include(t => t.CauseSMT).Include(t => t.CodesSMT).Include(t => t.Lines).Include(t => t.NoParts).Include(t => t.Sub_Ensambles);
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
                Tickets = Tickets.OrderBy(t => t.TicketSMTID);

                int pageSize = 15;
                int pageNumber = (page ?? 1);
                return View(Tickets.ToPagedList(pageNumber, pageSize));
            }
            int pagesize = 15;
            int pagenumber = (page ?? 1);
            return View(tickets.ToPagedList(pagenumber, pagesize));
        }

        // GET: TicketsSMT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsSMT ticketsSMT = db.TicketsSMTs.Find(id);
            if (ticketsSMT == null)
            {
                return HttpNotFound();
            }
            return View(ticketsSMT);
        }

        public List<SelectListItem> LoadScrap()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "[Seleccione tipo de SCRAP...]", Value = "--" });
            li.Add(new SelectListItem { Text = "PCB", Value = "0" });
            li.Add(new SelectListItem { Text = "Componente", Value = "1" });
            return li;
        }

        public JsonResult GetOrigen(string id)
        {
            List<SelectListItem> Origins = new List<SelectListItem>();
            switch (id)
            {
                case "0":
                    Origins.Add(new SelectListItem { Text = "Atoramiento", Value = "A"});
                    Origins.Add(new SelectListItem { Text = "Ajustes de procesos", Value = "B" });
                    Origins.Add(new SelectListItem { Text = "Mal manejo", Value = "C" });
                    Origins.Add(new SelectListItem { Text = "PCB pandeado", Value = "D" });
                    Origins.Add(new SelectListItem { Text = "Mala operación", Value = "E" });
                    Origins.Add(new SelectListItem { Text = "Corte de energía", Value = "F" });
                    Origins.Add(new SelectListItem { Text = "ReparacionSMT", Value = "G" });
                    break;
                case "1":
                    Origins.Clear();
                    Origins.Add(new SelectListItem { Text = "SCRAP-Componente", Value = "H" });
                    break;
            }
            return Json(new SelectList(Origins, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        //Cargar causas en DropDownList de Create
        public JsonResult GetCausa(string id)
        {
            var Causes = db.CauseSMTs.Select(u => new { Value = u.Code, Text = u.Description, id = u.CauseSMTID }).Where(u => u.Value == "1").ToList();
            switch (id)
            {
                case "A":
                    Causes = db.CauseSMTs.Select(u => new { Value=u.Code, Text=u.Description, id=u.CauseSMTID }).Where(u => u.Value == "A").ToList();
                    break;
                case "B":
                    Causes = db.CauseSMTs.Select(u => new { Value=u.Code, Text=u.Description, id=u.CauseSMTID }).Where(u => u.Value == "B").ToList();
                    break;
                case "C":
                    Causes = db.CauseSMTs.Select(u => new { Value = u.Code, Text = u.Description, id = u.CauseSMTID }).Where(u => u.Value == "C").ToList();
                    break;
                case "D":
                    Causes = db.CauseSMTs.Select(u => new { Value = u.Code, Text = u.Description, id = u.CauseSMTID }).Where(u => u.Value == "D").ToList();
                    break;
                case "E":
                    Causes = db.CauseSMTs.Select(u => new { Value = u.Code, Text = u.Description, id = u.CauseSMTID }).Where(u => u.Value == "E").ToList();
                    break;
                case "F":
                    Causes = db.CauseSMTs.Select(u => new { Value = u.Code, Text = u.Description, id = u.CauseSMTID }).Where(u => u.Value == "F").ToList();
                    break;
                case "G":
                    Causes = db.CauseSMTs.Select(u => new { Value = u.Code, Text = u.Description, id = u.CauseSMTID }).Where(u => u.Value == "G").ToList();
                    break;
                case "H":
                    Causes = db.CauseSMTs.Select(u => new { Value = u.Code, Text = u.Description, id = u.CauseSMTID }).Where(u => u.Value == "H").ToList();
                    break;
            }
            return Json(Causes , JsonRequestBehavior.AllowGet);
        }


        // GET: TicketsSMT/Create
        public ActionResult Create()
        {
            ViewBag.Scrap = LoadScrap();

            var listaLineas = from u in db.Lines where u.Tipo == 1 select u;
            List<Line> listLines = new List<Line>();
            listLines = listaLineas.ToList();
            listLines.Add(new Line { LineID = 0, LineName = "[Seleccione Linea...]" });
            listLines = listLines.OrderBy(l => l.LineName).ToList();
            ViewBag.LineID = new SelectList(listLines, "IIdLinea", "LineName");

            var listSub_Ensambles = db.Sub_Ensamble.ToList();
            listSub_Ensambles.Add(new Sub_Ensamble { Sub_EnsambleID = 0, Sub_Ensamble_Description = "[Seleccione Sub-Ensamble...]" });
            listSub_Ensambles = listSub_Ensambles.OrderBy(l => l.Sub_Ensamble_Description).ToList();
            ViewBag.Sub_EnsambleID = new SelectList(listSub_Ensambles, "Sub_EnsambleID", "Sub_Ensamble_Description");

            var listCauses = db.CauseSMTs.ToList();
            listCauses.Add(new CauseSMT { CauseSMTID = 0, Description = "[Seleccione Causa...]" });
            listCauses = listCauses.OrderBy(l => l.Description).ToList();
            //ViewBag.CauseSMTID = new SelectList(listCauses, "CauseSMTID", "Description");
            return View();
        }

        // POST: TicketsSMT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketSMTID,TicketStatus,Sub_EnsambleID,LineID,CreateDate,Quantity,PartNo,CauseSMTID,Creator,Cost,Authorizing")] TicketsSMT ticketsSMT)
        {
            int id = 0;

            var Sub_EnsamblesID = Request["Sub_EnsambleID"];
            var LineID = Request["LineID"];
            var CauseSMTID = Request["CauseSMTID"];

            if (LineID == "0" || Sub_EnsamblesID == "0" || CauseSMTID == "--" || CauseSMTID == null)
            {
                TempData["Alerta"] = "<script>swal('Debe llenar/actualizar todos los campos!');</script>";

                var listaLineas = from u in db.Lines where u.Tipo == 1 select u;
                List<Line> listLines = new List<Line>();
                listLines = listaLineas.ToList();
                listLines.Add(new Line { LineID = 0, LineName = "[Seleccione Linea...]" });
                listLines = listLines.OrderBy(l => l.LineName).ToList();
                ViewBag.LineID = new SelectList(listLines, "IIdLinea", "LineName");

                var listSub_Ensambles = db.Sub_Ensamble.ToList();
                listSub_Ensambles.Add(new Sub_Ensamble { Sub_EnsambleID = 0, Sub_Ensamble_Description = "[Seleccione Sub-Ensamble...]" });
                listSub_Ensambles = listSub_Ensambles.OrderBy(l => l.Sub_Ensamble_Description).ToList();
                ViewBag.Sub_EnsambleID = new SelectList(listSub_Ensambles, "Sub_EnsambleID", "Sub_Ensamble_Description");

                var listCauses = db.CauseSMTs.ToList();
                listCauses.Add(new CauseSMT { CauseSMTID = 0, Description = "[Seleccione Causa...]" });
                listCauses = listCauses.OrderBy(l => l.Description).ToList();
                //ViewBag.CauseSMTID = new SelectList(listCauses, "CauseSMTID", "Description");
                return View(ticketsSMT);
            }

            PartNo NumPart = new PartNo();

            try
            {
                NumPart = (from n in db.PartNoes where n.NPart == ticketsSMT.PartNo select n).FirstOrDefault();
                ticketsSMT.Cost = NumPart.UnitPrice * ticketsSMT.Quantity;
                ticketsSMT.PartNo = NumPart.NPart;
            }
            catch (Exception ex)
            {
                TempData["Message"] = "<script>swal(" + "'Error'" + "," + "'Debe de ingresar correctamente el número de parte..'" + "," + "'error'" + ");</script>";

                var listaLineas = from u in db.Lines where u.Tipo == 1 select u;
                List<Line> listLines = new List<Line>();
                listLines = listaLineas.ToList();
                listLines.Add(new Line { LineID = 0, LineName = "[Seleccione Linea...]" });
                listLines = listLines.OrderBy(l => l.LineName).ToList();
                ViewBag.LineID = new SelectList(listLines, "IIdLinea", "LineName");

                var listSub_Ensambles = db.Sub_Ensamble.ToList();
                listSub_Ensambles.Add(new Sub_Ensamble { Sub_EnsambleID = 0, Sub_Ensamble_Description = "[Seleccione Sub-Ensamble...]" });
                listSub_Ensambles = listSub_Ensambles.OrderBy(l => l.Sub_Ensamble_Description).ToList();
                ViewBag.Sub_EnsambleID = new SelectList(listSub_Ensambles, "Sub_EnsambleID", "Sub_Ensamble_Description");

                var listCauses = db.CauseSMTs.ToList();
                listCauses.Add(new CauseSMT { CauseSMTID = 0, Description = "[Seleccione Causa...]" });
                listCauses = listCauses.OrderBy(l => l.Description).ToList();
                //ViewBag.CauseSMTID = new SelectList(listCauses, "CauseSMTID", "Description");
                return View(ticketsSMT);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == User.Identity.GetUserId());
            ticketsSMT.Creator = Convert.ToInt32(user.Id);

            ticketsSMT.CreateDate = DateTime.Now;
            ticketsSMT.Authorizing = 0;

            string date = DateTime.Now.ToShortTimeString();
            DateTime Date = Convert.ToDateTime(date);
            if (Date >= Convert.ToDateTime("06:00 AM") && Date <= Convert.ToDateTime("14:30 PM"))
            {
                ticketsSMT.turn = 1;
            }
            else if (Date > Convert.ToDateTime("14:30 PM") && Date <= Convert.ToDateTime("22:30 PM"))
            {
                ticketsSMT.turn = 2;
            }
            else if (Date > Convert.ToDateTime("22:30 PM") || Date < Convert.ToDateTime("06:00 AM"))
            {
                if (Date > Convert.ToDateTime("22:30 PM") && Date < Convert.ToDateTime("23:59 AM"))
                    ticketsSMT.CreateDate.AddDays(1);
                ticketsSMT.turn = 3;
            }

            if (ModelState.IsValid)
            {
                db.TicketsSMTs.Add(ticketsSMT);
                try
                {
                    db.SaveChanges();

                    id = ticketsSMT.TicketSMTID;

                    if (ticketsSMT.Cost < 34)
                    {
                        var super = GetUsersInRole("Supervisor");
                        SaveAutorizantes(super, id, ticketsSMT, "Supervisor");
                        var tecnicos = GetUsersInRole("Tecnico");
                        SaveAutorizantes(tecnicos, id, ticketsSMT, "Tecnico");
                        TempData["Message"] = "<script>swal('Folio: " + ticketsSMT.TicketSMTID.ToString() + " ', 'El ticket se creó correctamente!');</script>";
                        return RedirectToAction("Create");
                    }
                    else if (ticketsSMT.Cost >= 34 && ticketsSMT.Cost < 70)
                    {
                        var Ing = GetUsersInRole("Ingeniero");
                        SaveAutorizantes(Ing, id, ticketsSMT, "Ingeniero");
                        TempData["Message"] = "<script>swal('Folio: " + ticketsSMT.TicketSMTID.ToString() + " ', 'El ticket se creó correctamente!');</script>";
                        return RedirectToAction("Create");
                    }
                    else if (ticketsSMT.Cost >= 70)
                    {
                        var Ger = GetUsersInRole("Ger_Jef");
                        SaveAutorizantes(Ger, id, ticketsSMT, "Ger_Jef");
                        TempData["Message"] = "<script>swal('Folio: " + ticketsSMT.TicketSMTID.ToString() + " ', 'El ticket se creó correctamente!');</script>";
                        return RedirectToAction("Create");
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
            }

            //ViewBag.CauseSMTID = new SelectList(db.CauseSMTs, "CauseSMTID", "Code", ticketsSMT.CauseSMTID);
            var ListaLineas = (from u in db.Lines where u.Tipo == 0 select u).ToList();
            ViewBag.LineID = new SelectList(ListaLineas.OrderBy(l => l.LineName), "IIdLinea", "LineName");
            ViewBag.Sub_EnsambleID = new SelectList(db.Sub_Ensamble, "Sub_EnsambleID", "Sub_Ensamble_Description", ticketsSMT.Sub_EnsambleID);
            ViewBag.Scrap = LoadScrap();
            TempData["Message"] = "<script>swal('Error. Verifique los datos ingresados.');</script>";
            return View(ticketsSMT);
        }

        // GET: TicketsSMT/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsSMT ticketsSMT = db.TicketsSMTs.Find(id);
            if (ticketsSMT == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CauseSMTID = new SelectList(db.CauseSMTs, "CauseSMTID", "Code", ticketsSMT.CauseSMTID);
            var ListaLineas = (from u in db.Lines where u.Tipo == 0 select u).ToList();
            ViewBag.LineID = new SelectList(ListaLineas.OrderBy(l => l.LineName), "IIdLinea", "LineName");
            ViewBag.Sub_EnsambleID = new SelectList(db.Sub_Ensamble, "Sub_EnsambleID", "Sub_Ensamble_Description", ticketsSMT.Sub_EnsambleID);
            ViewBag.Scrap = LoadScrap();
            return View(ticketsSMT);
        }

        // POST: TicketsSMT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketSMTID,TicketStatus,Sub_EnsambleID,LineID,CreateDate,Quantity,PartNo,CauseSMTID,Creator,Cost,Authorizing")] TicketsSMT ticketsSMT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketsSMT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CauseSMTID = new SelectList(db.CauseSMTs, "CauseSMTID", "Code", ticketsSMT.CauseSMTID);
            var ListaLineas = (from u in db.Lines where u.Tipo == 0 select u).ToList();
            ViewBag.LineID = new SelectList(ListaLineas.OrderBy(l => l.LineName), "IIdLinea", "LineName");
            ViewBag.Sub_EnsambleID = new SelectList(db.Sub_Ensamble, "Sub_EnsambleID", "Sub_Ensamble_Description", ticketsSMT.Sub_EnsambleID);
            ViewBag.Scrap = LoadScrap();
            return View(ticketsSMT);
        }

        // GET: TicketsSMT/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsSMT ticketsSMT = db.TicketsSMTs.Find(id);
            if (ticketsSMT == null)
            {
                return HttpNotFound();
            }
            return View(ticketsSMT);
        }

        // POST: TicketsSMT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketsSMT ticketsSMT = db.TicketsSMTs.Find(id);
            db.TicketsSMTs.Remove(ticketsSMT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Export()
        {
            return View();
        }

        public JsonResult ExportData()
        {
            var tickets = new JsonResult { Data = GetTickets(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return tickets;
        }

        public static void SaveAutorizantes(List<ApplicationUser> users, int id, TicketsSMT ticketsSMT, string rol)
        {
            using (SCRAPContext db = new SCRAPContext())
            {
                var NameUser = "";
                int UserId;
                foreach (var item in users)
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
                        dpto = "SMT"
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

        public static List<ApplicationUser> GetUsersInRole(string RoleName)
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

        // POST: TicketsSMT/Accept/5
        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsSMT ticketsSMT = db.TicketsSMTs.Find(id);
            if (ticketsSMT == null)
            {
                return HttpNotFound();
            }
            int ID = (int)id;
            if (User.IsInRole("Supervisor") == true)
            {
                if (ValidateAutorization("Tecnico", ID))
                {
                    ticketsSMT.TicketStatus = TicketStatus.Aceptado;
                    SaveAutorization(ID, "Supervisor");
                }
                else
                {
                    ticketsSMT.TicketStatus = TicketStatus.Pendiente;
                    SaveAutorization(ID, "Supervisor");
                }
            }
            else if (User.IsInRole("Tecnico") == true)
            {
                if (ValidateAutorization("Supervisor", ID))
                {
                    ticketsSMT.TicketStatus = TicketStatus.Aceptado;
                    SaveAutorization(ID, "Tecnico");
                }
                else
                {
                    ticketsSMT.TicketStatus = TicketStatus.Pendiente;
                    SaveAutorization(ID, "Tecnico");
                }
            }
            else if (User.IsInRole("Ingeniero"))
            {
                ticketsSMT.TicketStatus = TicketStatus.Aceptado;
                SaveAutorization(ID, "Ingeniero");
            }
            else if (User.IsInRole("Ger_Jef"))
            {
                ticketsSMT.TicketStatus = TicketStatus.Aceptado;
                SaveAutorization(ID, "Ger_Jef");
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == User.Identity.GetUserId());

            ticketsSMT.Authorizing = Convert.ToInt32(user.Id);

            if (ModelState.IsValid)
            {
                db.Entry(ticketsSMT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        public int SaveAutorization(int id, string rol)
        {
            var Session = User.Identity.GetUserId();
            int user = Convert.ToInt32(Session);
            using (SCRAPContext db = new SCRAPContext())
            {
                List<Autorizantes> Auth = new List<Autorizantes>();
                Auth = (from a in db.Autorizantes where a.Position == rol && a.TicketID == id where a.dpto == "SMT" select a).ToList();
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
                autorizantes = (from a in db.Autorizantes where a.Position == rol && a.TicketID == id where a.dpto == "SMT" select a).ToList();

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


        // POST: TicketsSMT/Refuse/5
        public ActionResult Refuse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsSMT ticketsSMT = db.TicketsSMTs.Find(id);
            if (ticketsSMT == null)
            {
                return HttpNotFound();
            }

            ticketsSMT.TicketStatus = TicketStatus.Rechazado;

            if (ModelState.IsValid)
            {
                db.Entry(ticketsSMT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        private dynamic GetTickets()
        {

            var ticketsSMTs = db.TicketsSMTs.Include(t => t.CauseSMT).Include(t => t.Lines).Include(t => t.NoParts).Include(t => t.Sub_Ensambles).Select(t => new
            {
                Folio = t.TicketSMTID,
                Estado = t.TicketStatus.ToString(),
                SubEnsamble = t.Sub_Ensambles.Sub_Ensamble_Description,
                Linea = t.Lines.LineName,
                FechaDeCreacion = t.CreateDate.ToString(),
                Cantidad = t.Quantity,
                NoDeParte = t.PartNo,
                Causa = t.CauseSMT.Description,
                Creador = t.Creator,
                Autorizo = t.Authorizing
            }).Where(t => t.Estado == "Aceptado").ToList().OrderBy(t => t.Folio);

            return ticketsSMTs;
        }

        public JsonResult GetNotifications()
        {
            var Session = User.Identity.GetUserId();
            int user = Convert.ToInt32(Session);
            var Tickets = from u in db.TicketsSMTs
                          join a in db.Autorizantes on u.TicketSMTID equals a.TicketID
                          where a.UserId == user
                          where a.FechaDeAutorizacion == null
                          where a.Checked == false
                          where u.TicketStatus == TicketStatus.Pendiente
                          where a.dpto == "SMT"
                          select u;

            int count = Tickets.Count();
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
