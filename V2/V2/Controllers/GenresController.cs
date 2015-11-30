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
    public class GenresController : Controller
    {
        private chansons db = new chansons();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.Genre.ToList());
        }

        //// GET: Genres/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Genre genre = db.Genre.Find(id);
        //    if (genre == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(genre);
        //}

        [Authorize(Roles = "Administrateur")]
        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreID,Nom")] Genre genre)
        {
            if (ModelState.IsValid && genre.Nom != null)
            {
                db.Genre.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Create", "Chansons");
            }

            return View(genre);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Genres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genre.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreID,Nom")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }

        [Authorize(Roles = "Administrateur")]
        // GET: Genres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genre.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Genre genre = db.Genre.Find(id);
            db.Genre.Remove(genre);
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
            var album = db.Chanson.Where(a => a.Genre.Nom.Contains(contenu)).ToList();
            if (album.Count() == 1)
                return RedirectToAction("Details", "Chansons", new { id = album.FirstOrDefault().ChansonID });
            if (album.Count() == 0)
                return RedirectToAction("Index", "Version");
            return View(album);
        }
    }
}
