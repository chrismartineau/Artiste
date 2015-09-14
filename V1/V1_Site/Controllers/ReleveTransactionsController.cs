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
    public class ReleveTransactionsController : Controller
    {
        private V1Entities4 db = new V1Entities4();

        // GET: ReleveTransactions
        public ActionResult Index()
        {
            return View(db.ReleveTransaction.ToList());
        }

        // GET: ReleveTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleveTransaction releveTransaction = db.ReleveTransaction.Find(id);
            if (releveTransaction == null)
            {
                return HttpNotFound();
            }
            return View(releveTransaction);
        }

        // GET: ReleveTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReleveTransactions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReleveTransactionID,Acheteur,CoutTotal,Date")] ReleveTransaction releveTransaction)
        {
            if (ModelState.IsValid)
            {
                db.ReleveTransaction.Add(releveTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(releveTransaction);
        }

        // GET: ReleveTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleveTransaction releveTransaction = db.ReleveTransaction.Find(id);
            if (releveTransaction == null)
            {
                return HttpNotFound();
            }
            return View(releveTransaction);
        }

        // POST: ReleveTransactions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReleveTransactionID,Acheteur,CoutTotal,Date")] ReleveTransaction releveTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(releveTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(releveTransaction);
        }

        // GET: ReleveTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleveTransaction releveTransaction = db.ReleveTransaction.Find(id);
            if (releveTransaction == null)
            {
                return HttpNotFound();
            }
            return View(releveTransaction);
        }

        // POST: ReleveTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReleveTransaction releveTransaction = db.ReleveTransaction.Find(id);
            db.ReleveTransaction.Remove(releveTransaction);
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
