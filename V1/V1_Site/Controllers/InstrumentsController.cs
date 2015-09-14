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
    public class InstrumentsController : Controller
    {
        private V1Entities4 db = new V1Entities4();

        // GET: Instruments
        public ActionResult Index()
        {
            return View(db.Instrument.ToList());
        }

        // GET: Instruments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = db.Instrument.Find(id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // GET: Instruments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instruments/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstrumentID,Nom,Type")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                db.Instrument.Add(instrument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instrument);
        }

        // GET: Instruments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = db.Instrument.Find(id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // POST: Instruments/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstrumentID,Nom,Type")] Instrument instrument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instrument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instrument instrument = db.Instrument.Find(id);
            if (instrument == null)
            {
                return HttpNotFound();
            }
            return View(instrument);
        }

        // POST: Instruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instrument instrument = db.Instrument.Find(id);
            db.Instrument.Remove(instrument);
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
