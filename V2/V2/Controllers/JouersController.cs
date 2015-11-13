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
    public class JouersController : Controller
    {
        private chansons db = new chansons();
        
        // GET: Jouers
        public ActionResult Index()
        {
            var jouer = db.Jouer.Include(j => j.Artiste).Include(j => j.Instrument).Include(j => j.Version);
            return View(jouer.ToList());
        }

        // GET: Jouers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jouer jouer = db.Jouer.Find(id);
            if (jouer == null)
            {
                return HttpNotFound();
            }
            return View(jouer);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Jouers/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom");
            ViewBag.InstrumentID = new SelectList(db.Instrument, "InstrumentID", "Nom");
            ViewBag.VersionID = new SelectList(db.Version,"VersionID", "Commentaire", db.Version.Find(id).VersionID);
            return View();
        }

        // POST: Jouers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JouerID,ArtisteID,InstrumentID,VersionID")] Jouer jouer)
        {
            if (ModelState.IsValid)
            {

                db.Jouer.Add(jouer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            

            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom", jouer.ArtisteID);
            ViewBag.InstrumentID = new SelectList(db.Instrument, "InstrumentID", "Nom", jouer.InstrumentID);
            ViewBag.VersionID = new SelectList(db.Version, "VersionID", "Chanson.Titre" , jouer.VersionID);
            return View(jouer);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Jouers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jouer jouer = db.Jouer.Find(id);
            if (jouer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom", jouer.ArtisteID);
            ViewBag.InstrumentID = new SelectList(db.Instrument, "InstrumentID", "Nom", jouer.InstrumentID);
            ViewBag.VersionID = new SelectList(db.Version, "VersionID", "Commentaire", jouer.VersionID);
            return View(jouer);
        }

        // POST: Jouers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JouerID,ArtisteID,InstrumentID,VersionID")] Jouer jouer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jouer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtisteID = new SelectList(db.Artiste, "ArtisteID", "Nom", jouer.ArtisteID);
            ViewBag.InstrumentID = new SelectList(db.Instrument, "InstrumentID", "Nom", jouer.InstrumentID);
            ViewBag.VersionID = new SelectList(db.Version, "VersionID", "Commentaire", jouer.VersionID);
            return View(jouer);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Jouers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jouer jouer = db.Jouer.Find(id);
            if (jouer == null)
            {
                return HttpNotFound();
            }
            return View(jouer);
        }

        // POST: Jouers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jouer jouer = db.Jouer.Find(id);
            db.Jouer.Remove(jouer);
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
