using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aplicacion_de_SCRAP.Models;

namespace Aplicacion_de_SCRAP.Controllers
{
    [Authorize(Roles = "Admin, SuperUser")]
    public class AreasController : Controller
    {
        private SCRAPContext db = new SCRAPContext();

        // GET: Areas
        public ActionResult Index()
        {
            return View(db.AreaScraps.ToList());
        }

        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaScrap areaScrap = db.AreaScraps.Find(id);
            if (areaScrap == null)
            {
                return HttpNotFound();
            }
            return View(areaScrap);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AreaSCRAPID,NAreaScrap")] AreaScrap areaScrap)
        {
            if (ModelState.IsValid)
            {
                db.AreaScraps.Add(areaScrap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaScrap);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaScrap areaScrap = db.AreaScraps.Find(id);
            if (areaScrap == null)
            {
                return HttpNotFound();
            }
            return View(areaScrap);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AreaSCRAPID,NAreaScrap")] AreaScrap areaScrap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaScrap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaScrap);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AreaScrap areaScrap = db.AreaScraps.Find(id);
            if (areaScrap == null)
            {
                return HttpNotFound();
            }
            return View(areaScrap);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaScrap areaScrap = db.AreaScraps.Find(id);
            db.AreaScraps.Remove(areaScrap);
            try
            {
                db.SaveChanges();
            }catch(Exception ex)
            {
                ViewBag.msg = "El área no se puede eliminar poque existen registros de tickets asignados";
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
