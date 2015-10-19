﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V2.Models;

namespace V2.Controllers
{
    public class ShoppingCartController : Controller
    {

        V2_bdEntities storeDB = new V2_bdEntities();

        public ActionResult Index()
        {
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

            ShoppingCartViewModel viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
            };
            ViewBag.Total = cart.GetTotal();


            return View(viewModel);
        }
        /// <summary>
        ///  Ajoute un album au panier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddAlbumToCart(int id)
        {

            var addedAlbum = storeDB.Album
            .Single(album => album.AlbumID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddAlbumToCart(addedAlbum);
            return RedirectToAction("Index");
        }

        /// <summary>
        ///  Ajoute une version au panier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddVersionToCart(int id)
        {

            var addedVersion = storeDB.Version
            .Single(version => version.VersionID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddVersionToCart(addedVersion);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Enleve un album au panier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveAlbumFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string albumName = storeDB.Achat
             .Single(item => item.AchatID == id).Album.Nom;
            cart.RemoveAlbumFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                " a été retiré de votre panier.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                DeleteId = id
            };
            return Json(results);
        }

        /// <summary>
        /// Enleve une version au panier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveVersionFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string VersionName = storeDB.Achat
             .Single(item => item.AchatID == id).Version.Chanson.Titre;
            cart.RemoveAlbumFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(VersionName) +
                " a été retiré de votre panier.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                DeleteId = id
            };
            return Json(results);
        }

        /// <summary>
        /// Permet de retourner à une vue partielle 
        /// le nombre d'éléments dans le panier.
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }

        public ActionResult AchatComplet()
        {           

            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

            ShoppingCartViewModel viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
            };
            ViewBag.Total = cart.GetTotal();
            //foreach (var c in cart.GetCartItems())
            //{
            //    storeDB.Achat.Add(c);
            //}
            storeDB.SaveChanges();
            return View(viewModel);
        }

        public ActionResult EnvoyerProduitEmail(string email)
        {
             ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

            ShoppingCartViewModel viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
            };
            if (email != null)
            {
                ReleveTransaction releve = new ReleveTransaction();
                releve.Acheteur = email.ToString();
                releve.CoutTotal = cart.GetTotal();
                releve.Date = DateTime.Now;
                foreach (var c in cart.GetCartItems())
                {
                    releve.Achat.Add(c);
                }                
            }
            return View();
        }
    }
}