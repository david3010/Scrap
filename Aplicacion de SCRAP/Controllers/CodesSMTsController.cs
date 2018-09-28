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
    public class CodesSMTsController : Controller
    {
        private SCRAPContext db = new SCRAPContext();

        // GET: CodesSMTs
        public ActionResult Index()
        {
            return View(db.CodesSMTs.ToList());
        }

        // GET: CodesSMTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodesSMT codesSMT = db.CodesSMTs.Find(id);
            if (codesSMT == null)
            {
                return HttpNotFound();
            }
            return View(codesSMT);
        }

        // GET: CodesSMTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodesSMTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodesSMTId,code,description")] CodesSMT codesSMT)
        {
            if (ModelState.IsValid)
            {
                db.CodesSMTs.Add(codesSMT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codesSMT);
        }

        // GET: CodesSMTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodesSMT codesSMT = db.CodesSMTs.Find(id);
            if (codesSMT == null)
            {
                return HttpNotFound();
            }
            return View(codesSMT);
        }

        // POST: CodesSMTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodesSMTId,code,description")] CodesSMT codesSMT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codesSMT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codesSMT);
        }

        // GET: CodesSMTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodesSMT codesSMT = db.CodesSMTs.Find(id);
            if (codesSMT == null)
            {
                return HttpNotFound();
            }
            return View(codesSMT);
        }

        // POST: CodesSMTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodesSMT codesSMT = db.CodesSMTs.Find(id);
            db.CodesSMTs.Remove(codesSMT);
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
