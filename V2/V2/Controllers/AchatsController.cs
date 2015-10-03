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
    public class AchatsController : Controller
    {
        private V2_bdEntities db = new V2_bdEntities();

        // GET: Achats
        public ActionResult Index()
        {
            var achat = db.Achat.Include(a => a.Album).Include(a => a.Version);
            return View(achat.ToList());
        }

        // GET: Achats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = db.Achat.Find(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            return View(achat);
        }

        // GET: Achats/Create
        public ActionResult Create()
        {
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description");
            ViewBag.VersionID = new SelectList(db.Version, "VersionID", "Commentaire");
            return View();
        }

        // POST: Achats/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AchatID,Cout,AlbumID,VersionID")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                db.Achat.Add(achat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description", achat.AlbumID);
            ViewBag.VersionID = new SelectList(db.Version, "VersionID", "Commentaire", achat.VersionID);
            return View(achat);
        }

        // GET: Achats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = db.Achat.Find(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description", achat.AlbumID);
            ViewBag.VersionID = new SelectList(db.Version, "VersionID", "Commentaire", achat.VersionID);
            return View(achat);
        }

        // POST: Achats/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AchatID,Cout,AlbumID,VersionID")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(achat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description", achat.AlbumID);
            ViewBag.VersionID = new SelectList(db.Version, "VersionID", "Commentaire", achat.VersionID);
            return View(achat);
        }

        // GET: Achats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achat achat = db.Achat.Find(id);
            if (achat == null)
            {
                return HttpNotFound();
            }
            return View(achat);
        }

        // POST: Achats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achat achat = db.Achat.Find(id);
            db.Achat.Remove(achat);
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
