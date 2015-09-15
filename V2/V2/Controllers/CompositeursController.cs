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
    public class CompositeursController : Controller
    {
        private V2_bdEntities db = new V2_bdEntities();

        // GET: Compositeurs
        public ActionResult Index()
        {
            return View(db.Compositeur.ToList());
        }

        // GET: Compositeurs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compositeur compositeur = db.Compositeur.Find(id);
            if (compositeur == null)
            {
                return HttpNotFound();
            }
            return View(compositeur);
        }

        // GET: Compositeurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compositeurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompositeurID,Nom")] Compositeur compositeur)
        {
            if (ModelState.IsValid)
            {
                db.Compositeur.Add(compositeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compositeur);
        }

        // GET: Compositeurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compositeur compositeur = db.Compositeur.Find(id);
            if (compositeur == null)
            {
                return HttpNotFound();
            }
            return View(compositeur);
        }

        // POST: Compositeurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompositeurID,Nom")] Compositeur compositeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compositeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compositeur);
        }

        // GET: Compositeurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compositeur compositeur = db.Compositeur.Find(id);
            if (compositeur == null)
            {
                return HttpNotFound();
            }
            return View(compositeur);
        }

        // POST: Compositeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compositeur compositeur = db.Compositeur.Find(id);
            db.Compositeur.Remove(compositeur);
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
