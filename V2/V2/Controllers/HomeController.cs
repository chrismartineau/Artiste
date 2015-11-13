using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V2.Models;

namespace V2.Controllers
{
    public class HomeController : Controller
    {
        chansons db = new chansons();
        public ActionResult Index()
        {
            return View(db.Stanley.FirstOrDefault());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search(string recherche, string sujet)
        {
            if (recherche == null && sujet == null)
            {
                return View("Search");
            }
            return RedirectToAction("Search", sujet, new { contenu = recherche });
        }

        [HttpGet]
        public ActionResult EditBio()
        {
            if (db.Stanley.FirstOrDefault() != null)
                return View(db.Stanley.FirstOrDefault());
            else
                return RedirectToAction("CreateBio");
        }

        [HttpPost]
        public ActionResult EditBio(string bio)
        {
            db.Stanley.FirstOrDefault().biographie = bio;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateBio()
        {
            if (db.Stanley.FirstOrDefault() != null)
                return RedirectToAction("EditBio");
            else
                return View();
        }

        [HttpGet]
        public ActionResult CreateBio(string bio)
        {
            Stanley stan = new Stanley();
            stan.biographie = bio;
            db.Stanley.Add(stan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditImage(HttpPostedFileBase file)
        {
            Stanley stan;
            if (db.Stanley.FirstOrDefault() == null)
            {
                stan = new Stanley();
            }
            else
                stan = db.Stanley.FirstOrDefault();

            string[] split = file.ContentType.Split('/');
            string filename = "Stanley." + split[1];
            if (file == null || split[0] != "image")
            {
                return View();
            }
            string fullPath = Request.MapPath("~/Images/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            if (db.Stanley.Count() == 0)
                db.Stanley.Add(stan);
            db.SaveChanges();
            System.IO.File.Copy(file.FileName, Server.MapPath("~/Images/" + filename), true);
            stan.image = "../../Images/" + filename;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


        
    }
}