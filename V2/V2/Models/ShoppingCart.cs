using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V2.Models
{
    public class ShoppingCart
    {

        chansons storeDB = new chansons();
        private static ShoppingCart cart = null;
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        /// <summary>
        /// Constructeur protégé pour mon singleton
        /// </summary>
        protected ShoppingCart()
        {
        }
        /// <summary>
        /// Retourne l'instance du panier
        /// </summary>
        /// <param name="context">HttpContextBase pour assigner une valeur de session</param>
        /// <returns></returns>
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            if (cart != null)
                return cart;
            else
            {
                cart = new ShoppingCart();
                cart.ShoppingCartId = cart.GetCartId(context);
                return cart;
            }
        }
        /// <summary>
        /// S'il n'existe pas de variable de session trouver le nom d'utilisateur ou un Guid généré.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetCartId(HttpContextBase context)
        {

            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                else
                {
                    HttpCookie myCookie = new HttpCookie("MyTestCookie");
                    Guid tempCartId = Guid.NewGuid();
                    myCookie.Value = tempCartId.ToString();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }
        public void AddAlbumToCart(Album album)
        {
            var cartItem = storeDB.Achat.SingleOrDefault(c => c.CartID == ShoppingCartId
                                                         && c.AlbumID == album.AlbumID);

            if (cartItem == null)
            {
                cartItem = new Achat
                {
                    AlbumID = album.AlbumID,
                    CartID = ShoppingCartId,
                    Album = storeDB.Album.Find(album.AlbumID)
                     
                };

                storeDB.Achat.Add(cartItem);
            }
            storeDB.SaveChanges();
        }

        public void AddVersionToCart(Version version)
        {
            var cartItem = storeDB.Achat.SingleOrDefault(c => c.CartID == ShoppingCartId
                                                         && c.VersionID == version.VersionID);

            if (cartItem == null)
            {
                cartItem = new Achat
                {
                     VersionID = version.VersionID,
                    CartID = ShoppingCartId,
                    Version = storeDB.Version.Find(version.VersionID)

                };

                storeDB.Achat.Add(cartItem);
            }
            storeDB.SaveChanges();
        }

        public void RemoveAlbumFromCart(int id)
        {
            var cartItem = storeDB.Achat.Single(cart => cart.CartID == ShoppingCartId
                                                && cart.AlbumID == id);

            if (cartItem != null)
            {             
                storeDB.Achat.Remove(cartItem);
                storeDB.SaveChanges();
            }
        }

        public void RemoveVersionFromCart(int id)
        {
            var cartItem = storeDB.Achat.Single(cart => cart.CartID == ShoppingCartId
                                                && cart.VersionID == id);

            if (cartItem != null)
            {
                storeDB.Achat.Remove(cartItem);
                storeDB.SaveChanges();
            }
        }
        /// <summary>
        /// Efface tous les enregistrements pour l'utilisateur courant
        /// </summary>
        public void EmptyCart()
        {
            var cartItems = storeDB.Achat.Where(cart => cart.CartID == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Achat.Remove(cartItem);
            }

            // Save changes
            storeDB.SaveChanges();
        }
        /// <summary>
        /// Retourne la liste des enregistrements pour l'affichage
        /// </summary>
        /// <returns></returns>
        public List<Achat> GetCartItems()
        {
            return storeDB.Achat.Where(cart => cart.CartID == ShoppingCartId).ToList();
        }
        /// <summary>
        /// Retourne le nombre d'éléments dans le panier
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            int? count;
            try
            {


                if (storeDB.Achat.Count() > 0)
                {
                    count = (from cartItems in storeDB.Achat
                             where cartItems.CartID == ShoppingCartId
                             select cartItems).Count();
                }
                else
                {
                    count = 0;
                }
            }
            catch (Exception e)
            {
                return 0;
            }

            return count ?? 0;
        }
        /// <summary>
        /// Retourne le grand total en argent.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotal()
        {
            decimal? totalAlbum = (from cartItems in storeDB.Achat
                              where cartItems.CartID == ShoppingCartId
                              select cartItems.Album.Prix).Sum();
            decimal? totalVersion = (from cartItems in storeDB.Achat
                                   where cartItems.CartID == ShoppingCartId
                                   select cartItems.Version.Prix).Sum();
            if (totalAlbum == null)
                totalAlbum = 0;
            if (totalVersion == null)
                totalVersion = 0;
            decimal? total = totalAlbum + totalVersion;
            return total ?? decimal.Zero;
        }
        /// <summary>
        /// Fait la migration des éléments du panier vers un nouveau userName
        /// </summary>
        /// <param name="userName"></param>
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Achat.Where(c => c.CartID == ShoppingCartId);

            foreach (Achat item in shoppingCart)
                item.CartID = userName;
            storeDB.SaveChanges();
        }
    }
}