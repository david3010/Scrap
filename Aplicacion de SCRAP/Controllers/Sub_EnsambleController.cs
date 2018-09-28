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
    public class Sub_EnsambleController : Controller
    {
        private SCRAPContext db = new SCRAPContext();

        // GET: Sub_Ensamble
        public ActionResult Index(int ? page)
        {
            var SubEnsambles = db.Sub_Ensamble.OrderBy(s => s.Sub_Ensamble_Description);
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(SubEnsambles.ToPagedList(pageNumber, pageSize));
        }

        // GET: Sub_Ensamble/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Ensamble sub_Ensamble = db.Sub_Ensamble.Find(id);
            if (sub_Ensamble == null)
            {
                return HttpNotFound();
            }
            return View(sub_Ensamble);
        }

        // GET: Sub_Ensamble/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sub_Ensamble/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sub_EnsambleID,Sub_Ensamble_Description")] Sub_Ensamble sub_Ensamble)
        {
            if (ModelState.IsValid)
            {
                db.Sub_Ensamble.Add(sub_Ensamble);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sub_Ensamble);
        }

        // GET: Sub_Ensamble/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Ensamble sub_Ensamble = db.Sub_Ensamble.Find(id);
            if (sub_Ensamble == null)
            {
                return HttpNotFound();
            }
            return View(sub_Ensamble);
        }

        // POST: Sub_Ensamble/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sub_EnsambleID,Sub_Ensamble_Description")] Sub_Ensamble sub_Ensamble)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_Ensamble).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sub_Ensamble);
        }

        // GET: Sub_Ensamble/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Ensamble sub_Ensamble = db.Sub_Ensamble.Find(id);
            if (sub_Ensamble == null)
            {
                return HttpNotFound();
            }
            return View(sub_Ensamble);
        }

        // POST: Sub_Ensamble/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_Ensamble sub_Ensamble = db.Sub_Ensamble.Find(id);
            db.Sub_Ensamble.Remove(sub_Ensamble);
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
