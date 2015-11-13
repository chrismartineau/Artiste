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
    public class AlbumsController : Controller
    {
        private chansons db = new chansons();

        // GET: Albums
        public ActionResult Index()
        {
            if (db.Album.ToList() == null)
            {
                return RedirectToAction("Create");
            }
            return View(db.Album.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int i2 = (int)id;
            return RedirectToAction("ListVersionAlbum", "Versions", new { id = i2 });
        }

        [Authorize(Roles="Administrateur")]
        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,DateCreation,Description,Prix,Nom")] Album album)
        {
            if (ModelState.IsValid)
            {
                album.DateCreation = DateTime.Now;
                db.Album.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
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
            return View(album);
        }

        // POST: Albums/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumID,DateCreation,Description,Prix,Nom")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        [Authorize(Roles = "Administrateur")]
        public ActionResult RemovePrice(int? id)
        {
            Album album = db.Album.Find(id);
            album.Prix = null;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
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
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Album.Find(id);
            db.Album.Remove(album);
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

        public ActionResult Search(string contenu)
        {
            if (db.Album.ToList() == null)
            {
                return RedirectToAction("Create");
            }
            var album = db.Album.Where(a => a.Nom.Contains(contenu)).ToList();
            return View(album);
        }
        
        [HttpGet]
        public ActionResult AjouterImage(int idAlbum)
        {
            var album = db.Album.Find(idAlbum);
            return View(album);
        }

        [HttpPost]
        public ActionResult AjouterImage(int? idAlbum, HttpPostedFileBase file)
        {
            string[] split = file.ContentType.Split('/');
            if (db.Album.Find(idAlbum) == null)
            {

            }
            string filename;
            try
            {
                filename = db.Album.Where(a => a.AlbumID == idAlbum).FirstOrDefault().Nom + "." + split[1];
                filename = filename.Replace(' ', '-');
                filename = filename.Replace('\'', '-');
            }
            catch (Exception)
            {
                filename = "test." + split[1];
            }
            if (file == null || split[0] != "image")
            {
                return View();
            }
            string fullPath = Request.MapPath("~/Images/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.Album.Find(idAlbum).Image = "";
            db.SaveChanges();
            System.IO.File.Copy(file.FileName, Server.MapPath("~/Images/" + filename), true);
            db.Album.Find(idAlbum).Image = "../../Images/" + filename;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = idAlbum });
        }

    }
}