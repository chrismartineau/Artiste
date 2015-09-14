using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using V1_Site.Models;

namespace V1_Site.Controllers
{
    public class VersionsController : Controller
    {
        private V1Entities4 db = new V1Entities4();

        // GET: Versions
        public ActionResult Index()
        {
            var version = db.Version.Include(v => v.Album).Include(v => v.Chanson);
            return View(version.ToList());
        }

        // GET: Versions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V1_Site.Models.Version version = db.Version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(version);
        }

        // GET: Versions/Create
        public ActionResult Create()
        {
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description");
            ViewBag.ChansonID = new SelectList(db.Chanson, "ChansonID", "Titre");
            return View();
        }

        // POST: Versions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VersionID,ChansonID,AlbumID,Commentaire,DateCreation,Demo,Duree,NbEcoutes,Prix,Visible")] V1_Site.Models.Version version)
        {
            if (ModelState.IsValid)
            {
                version.DateCreation = DateTime.Now;
                
                db.Version.Add(version);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description", version.AlbumID);
            ViewBag.ChansonID = new SelectList(db.Chanson, "ChansonID", "Titre", version.ChansonID);
            return View(version);
        }

        // GET: Versions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V1_Site.Models.Version version = db.Version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description", version.AlbumID);
            ViewBag.ChansonID = new SelectList(db.Chanson, "ChansonID", "Titre", version.ChansonID);
            return View(version);
        }

        // POST: Versions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VersionID,ChansonID,AlbumID,Commentaire,DateCreation,Demo,Duree,NbEcoutes,Prix,Visible")] V1_Site.Models.Version version)
        {
            if (ModelState.IsValid)
            {
                db.Entry(version).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Description", version.AlbumID);
            ViewBag.ChansonID = new SelectList(db.Chanson, "ChansonID", "Titre", version.ChansonID);
            return View(version);
        }

        // GET: Versions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V1_Site.Models.Version version = db.Version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(version);
        }

        // POST: Versions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            V1_Site.Models.Version version = db.Version.Find(id);
            db.Version.Remove(version);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ListVersion(int? id)
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
            var versions = (from c in db.Version
                            where c.ChansonID == id
                            select c);
            return View(versions.ToList());
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
