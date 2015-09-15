using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using V2.Models;

namespace V2.Controllers
{
    public class BiographiesController : Controller
    {
        private V2_bdEntities db = new V2_bdEntities();

        // GET: Biographies
        public ActionResult Index()
        {
            var biographie = db.Biographie.Include(b => b.Artiste);
            return View(biographie.ToList());
        }

        // GET: Biographies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biographie biographie = db.Biographie.Find(id);
            if (biographie == null)
            {
                return HttpNotFound();
            }
            return View(biographie);
        }

        // GET: Biographies/Create
        public ActionResult Create()
        {
            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom");
            return View();
        }

        // POST: Biographies/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BiographieID,Biographie1,DateDernierChangement,ArtisteID")] Biographie biographie)
        {
            if (ModelState.IsValid)
            {
                db.Biographie.Add(biographie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom", biographie.ArtisteID);
            return View(biographie);
        }

        // GET: Biographies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biographie biographie = db.Biographie.Find(id);
            if (biographie == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom", biographie.ArtisteID);
            return View(biographie);
        }

        // POST: Biographies/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BiographieID,Biographie1,DateDernierChangement,ArtisteID")] Biographie biographie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biographie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom", biographie.ArtisteID);
            return View(biographie);
        }

        // GET: Biographies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biographie biographie = db.Biographie.Find(id);
            if (biographie == null)
            {
                return HttpNotFound();
            }
            return View(biographie);
        }

        // POST: Biographies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Biographie biographie = db.Biographie.Find(id);
            db.Biographie.Remove(biographie);
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
