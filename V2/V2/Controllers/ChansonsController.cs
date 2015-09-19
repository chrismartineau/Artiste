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
    public class ChansonsController : Controller
    {
        private V2_bdEntities db = new V2_bdEntities();

        // GET: Chansons
        public ActionResult Index()
        {
            if (db.Chanson.ToList() == null)
            {
              
                return RedirectToAction("Create");
            }
            var chanson = db.Chanson.Include(c => c.Genre);
            return View(chanson.ToList());
        }

        // GET: Chansons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i2 = (int)id;
            return RedirectToAction("ListVersion", "Versions", new { id = i2 });
        }

        // GET: Chansons/Create
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genre, "GenreID", "Nom");
            return View();
        }

        // POST: Chansons/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChansonID,Titre,GenreID")] Chanson chanson)
        {
            if (ModelState.IsValid)
            {
                chanson.DateCreation = DateTime.Now;
                db.Chanson.Add(chanson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreID = new SelectList(db.Genre, "GenreID", "Nom", chanson.GenreID);
            return View(chanson);
        }

        // GET: Chansons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chanson chanson = db.Chanson.Find(id);
            if (chanson == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genre, "GenreID", "Nom", chanson.GenreID);
            return View(chanson);
        }

        // POST: Chansons/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChansonID,Titre,GenreID")] Chanson chanson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chanson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreID = new SelectList(db.Genre, "GenreID", "Nom", chanson.GenreID);
            return View(chanson);
        }

        // GET: Chansons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chanson chanson = db.Chanson.Find(id);
            if (chanson == null)
            {
                return HttpNotFound();
            }
            return View(chanson);
        }

        // POST: Chansons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chanson chanson = db.Chanson.Find(id);
            db.Chanson.Remove(chanson);
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