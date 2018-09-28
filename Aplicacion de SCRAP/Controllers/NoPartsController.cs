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
    public class NoPartsController : Controller
    {
        private SCRAPContext db = new SCRAPContext();

        // GET: NoParts
        public ActionResult Index(int ? page)
        {
            var NumerosdeParte = db.PartNoes.OrderBy(np => np.NPart);
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(NumerosdeParte.ToPagedList(pageNumber, pageSize));
        }

        // GET: NoParts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartNo partNo = db.PartNoes.Find(id);
            if (partNo == null)
            {
                return HttpNotFound();
            }
            return View(partNo);
        }

        // GET: NoParts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NoParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartNoID,NPart,PartDescription,UnitPrice")] PartNo partNo)
        {
            if (ModelState.IsValid)
            {
                db.PartNoes.Add(partNo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partNo);
        }

        // GET: NoParts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartNo partNo = db.PartNoes.Find(id);
            if (partNo == null)
            {
                return HttpNotFound();
            }
            return View(partNo);
        }

        // POST: NoParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartNoID,NPart,PartDescription,UnitPrice")] PartNo partNo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partNo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partNo);
        }

        // GET: NoParts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartNo partNo = db.PartNoes.Find(id);
            if (partNo == null)
            {
                return HttpNotFound();
            }
            return View(partNo);
        }

        // POST: NoParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartNo partNo = db.PartNoes.Find(id);
            db.PartNoes.Remove(partNo);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "El componente no se puede eliminar poque existen registros de tickets asignados a el.";
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
