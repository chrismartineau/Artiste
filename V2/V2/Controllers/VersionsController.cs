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
    public class VersionsController : Controller
    {
        private V2_bdEntities db = new V2_bdEntities();

        // GET: Versions
        public ActionResult Index()
        {
            var version = db.Chanson.Include(v => v.Version).OrderBy(c => c.Titre);
            // var version = db.Version.Include(v => v.Album).Include(v => v.Chanson).OrderBy(v => v.Chanson);
            return View(version.ToList());
        }

        // GET: Versions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V2.Models.Version version = db.Version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            return View(version);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Versions/Create
        public ActionResult Create()
        {
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Nom");
            ViewBag.ChansonID = new SelectList(db.Chanson, "ChansonID", "Titre");
            return View();
        }

        // POST: Versions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VersionID,ChansonID,AlbumID,Commentaire,DateCreation,Demo,Duree,NbEcoutes,Prix,Visible")] V2.Models.Version version)
        {
            if (ModelState.IsValid)
            {
                version.DateCreation = DateTime.Now;
                int index = version.Demo.IndexOf("height=");
                if (index != 0)
                {
                    version.Demo = version.Demo.Remove(index, 14);
                    version.Demo = version.Demo.Insert(index, "height=110");
                }
                index = version.Demo.IndexOf("show_comments=");
                if (index != 0)
                {
                    version.Demo = version.Demo.Remove(index, 18);
                    version.Demo = version.Demo.Insert(index, "show_comments=false");
                }
                index = version.Demo.IndexOf("visual=");
                if (index != 0)
                {
                    version.Demo = version.Demo.Remove(index, 11);
                    version.Demo = version.Demo.Insert(index, "visual=false");
                }
                db.Version.Add(version);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Nom", version.AlbumID);
            ViewBag.ChansonID = new SelectList(db.Chanson, "ChansonID", "Titre", version.ChansonID);
            return View(version);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Versions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V2.Models.Version version = db.Version.Find(id);
            if (version == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "Nom", version.AlbumID);
            ViewBag.ChansonID = new SelectList(db.Chanson, "ChansonID", "Titre", version.ChansonID);
            return View(version);
        }

        // POST: Versions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VersionID,ChansonID,AlbumID,Commentaire,DateCreation,Demo,Duree,NbEcoutes,Prix,Visible")] V2.Models.Version version)
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

        [Authorize(Roles = "Administrateur")]
        // GET: Versions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            V2.Models.Version version = db.Version.Find(id);
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
            V2.Models.Version version = db.Version.Find(id);
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

        public ActionResult ListVersionAlbum(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Album.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            var versions = (from c in db.Version
                            where c.AlbumID == id
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

        [HttpGet]
        public ActionResult AjouterFichier(int? id)
        {
            var version = db.Version.Find(id);
            return View(version);
        }


        [HttpPost]
        public ActionResult AjouterFichier(int? idVersion, HttpPostedFileBase file)
        {
            string[] split = file.ContentType.Split('/');
            if (db.Album.Find(idVersion) == null)
            {

            }
            string filename;
            try
            {
                var f = db.Version.Where(a => a.VersionID == idVersion).FirstOrDefault();
                filename = f.Chanson.Titre + "_" + f.VersionID + "." + split[1];
                filename = filename.Replace(' ', '-');
                filename = filename.Replace('\'', '_');
            }
            catch (Exception e)
            {
                filename = "test." + split[1];
            }
            if (file == null || split[0] != "audio")
            {
                return View();
            }
            System.IO.File.Copy(file.FileName, Server.MapPath("~/Chansons/" + filename), true);
            db.Version.Find(idVersion).Path = "../../Chansons/" + filename;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = idVersion });
        }
    }
}
