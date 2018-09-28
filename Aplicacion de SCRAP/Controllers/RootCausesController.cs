using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aplicacion_de_SCRAP.Models;
using PagedList;

namespace Aplicacion_de_SCRAP.Controllers
{
    [Authorize(Roles = "Admin, SuperUser")]
    public class RootCausesController : Controller
    {
        private SCRAPContext db = new SCRAPContext();

        // GET: RootCauses
        public ActionResult Index(int? page)
        {
            var RootCauses = db.RootCauses.OrderBy(r => r.RootCauseID).ToList();
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(RootCauses.ToPagedList(pageNumber, pageSize));
        }

        // GET: RootCauses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RootCause rootCause = db.RootCauses.Find(id);
            if (rootCause == null)
            {
                return HttpNotFound();
            }
            return View(rootCause);
        }

        // GET: RootCauses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RootCauses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RootCauseID,NRootCause, Description")] RootCause rootCause)
        {
            if (ModelState.IsValid)
            {
                db.RootCauses.Add(rootCause);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rootCause);
        }

        // GET: RootCauses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RootCause rootCause = db.RootCauses.Find(id);
            if (rootCause == null)
            {
                return HttpNotFound();
            }
            return View(rootCause);
        }

        // POST: RootCauses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RootCauseID, NRootCause, Description")] RootCause rootCause)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rootCause).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rootCause);
        }

        // GET: RootCauses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RootCause rootCause = db.RootCauses.Find(id);
            if (rootCause == null)
            {
                return HttpNotFound();
            }
            return View(rootCause);
        }

        // POST: RootCauses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RootCause rootCause = db.RootCauses.Find(id);
            db.RootCauses.Remove(rootCause);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.msg = "La causa raíz no se puede eliminar poque tiene registros de tickets asignados.";
                return View();
            }
            return RedirectToAction("Index");
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
