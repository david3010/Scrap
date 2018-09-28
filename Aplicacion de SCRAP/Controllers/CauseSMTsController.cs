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
    public class CauseSMTsController : Controller
    {
        private SCRAPContext db = new SCRAPContext();

        // GET: CauseSMTs
        public ActionResult Index()
        {
            return View(db.CauseSMTs.ToList());
        }

        // GET: CauseSMTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauseSMT causeSMT = db.CauseSMTs.Find(id);
            if (causeSMT == null)
            {
                return HttpNotFound();
            }
            return View(causeSMT);
        }

        // GET: CauseSMTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CauseSMTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CauseSMTID,Code,Description")] CauseSMT causeSMT)
        {
            if (ModelState.IsValid)
            {
                db.CauseSMTs.Add(causeSMT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(causeSMT);
        }

        // GET: CauseSMTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauseSMT causeSMT = db.CauseSMTs.Find(id);
            if (causeSMT == null)
            {
                return HttpNotFound();
            }
            return View(causeSMT);
        }

        // POST: CauseSMTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CauseSMTID,Code,Description")] CauseSMT causeSMT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(causeSMT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(causeSMT);
        }

        // GET: CauseSMTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CauseSMT causeSMT = db.CauseSMTs.Find(id);
            if (causeSMT == null)
            {
                return HttpNotFound();
            }
            return View(causeSMT);
        }

        // POST: CauseSMTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CauseSMT causeSMT = db.CauseSMTs.Find(id);
            db.CauseSMTs.Remove(causeSMT);
            db.SaveChanges();
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
