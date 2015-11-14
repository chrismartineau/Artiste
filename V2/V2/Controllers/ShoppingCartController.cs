using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V2.Models;

namespace V2.Controllers
{
    public class ShoppingCartController : Controller
    {

        chansons storeDB = new chansons();

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
        public ActionResult RemoveAlbumFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            string albumName = storeDB.Achat
             .Single(item => item.AlbumID == id).Album.Nom;
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
        public ActionResult RemoveVersionFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var VersionName = storeDB.Achat.Where(a => a.VersionID == id).FirstOrDefault();
            cart.RemoveVersionFromCart(id);
            var results = new ShoppingCartRemoveViewModel
            {
                //Message = Server.HtmlEncode(VersionName) +
                //" a été retiré de votre panier.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                DeleteId = id
            };
            return RedirectToAction("Index");
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

        public ActionResult EmptyCart(int id)
        {
            using (chansons StoreDB = new chansons())
            {
                ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);

                foreach (var c in cart.GetCartItems())
                {
                    StoreDB.Achat.Find(c.AchatID).ReleveTransactionID = id;
                }
                StoreDB.SaveChanges();
            }
            return RedirectToAction("Commandes", "Account");
        }
    }
}