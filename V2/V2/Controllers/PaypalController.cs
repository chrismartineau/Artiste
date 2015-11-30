using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using V2.Models;
using Microsoft.AspNet.Identity;

namespace V2.Controllers
{
    [Authorize]
    public class PaypalController : Controller
    {
        chansons StoreDB = new chansons();
        /// <summary>
        /// Permet d'exécuter un Express Checkout avec les items dans le panier
        /// </summary>
        /// <returns></returns>
        public ActionResult Checkout(decimal? livraison)
        {
            NVPAPICaller test = new NVPAPICaller();
            string retMsg = "";
            string token = "";
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
            if (livraison == null)
                livraison = 0;
            decimal damt = cart.GetTotal();
            if (livraison != null)
                damt += (decimal)livraison;

            if (damt > 0)
            {
                string amt = damt.ToString("N2", CultureInfo.InvariantCulture);
                string baseURL = HttpContext.Request.Url.Authority;
                string retunURL = "http://" + baseURL + Url.Action("RevisePaiement");
                string cancelURL = "http://" + baseURL + Url.Action("Cancel");


                bool ret = test.ShortcutExpressCheckout(amt, ref token, ref retMsg, cart.GetCartItems(), retunURL, cancelURL, livraison);
                if (ret)
                {
                    HttpContext.Session["token"] = token;
                    return Redirect(retMsg);
                }
                else
                {
                    return Redirect("APIError.cshtml?" + retMsg);
                }
            }
            else
                return Redirect("APIError.cshtml?ErrorCode=AmtMissing");

        }

        public ActionResult APIError(string errorCode, string Desc, string Desc2)
        {
            ViewBag.errorCode = errorCode;
            ViewBag.Desc = Desc;
            ViewBag.Desc2 = Desc2;

            return View();

        }


        /// <summary>
        /// Permet de réviser notre achat
        /// </summary>
        /// <param name="token">le token de paypal</param>
        /// <param name="payerId">l'id de l'acheteur</param>
        /// <returns></returns>
        public ActionResult RevisePaiement(string token, string payerId)
        {

            NVPAPICaller test = new NVPAPICaller();
            string shippingAddress = "";

            string retMsg = "";


            token = Session["token"].ToString();

            bool ret = test.GetShippingDetails(token, ref payerId, ref shippingAddress, ref retMsg);
            if (ret)
            {
                Session["payerId"] = payerId;
                Session["token"] = token;
                ViewBag.shippingAddress = shippingAddress;
            }
            else
            {
                Response.Redirect("APIError?" + retMsg);
            }

            return View();
        }
        public ActionResult Cancel()
        {

            return RedirectToAction("Index", "ShoppingCart");
        }

        public ActionResult ConfirmePaiement()
        {
            NVPAPICaller test = new NVPAPICaller();

            string retMsg = "";
            string token = "";
            string finalPaymentAmount = "";
            string payerId = "";
            string shippingAddress = "";
            NVPCodec decoder = null;

            token = Session["token"].ToString();
            payerId = Session["payerId"].ToString();
            ShoppingCart cart = ShoppingCart.GetCart(this.HttpContext);
            finalPaymentAmount = cart.GetTotal().ToString("N2", CultureInfo.InvariantCulture);
            //finalPaymentAmount = Session["payment_amt"].ToString();

            bool ret = test.ConfirmPayment(finalPaymentAmount, token, payerId, ref decoder, ref retMsg);
            if (ret)
            {
                // Unique transaction ID of the payment. Note:  If the PaymentAction of the request was Authorization or Order, this value is your AuthorizationID for use with the Authorization & Capture APIs. 
                string transactionId = decoder["PAYMENTINFO_0_TRANSACTIONID"];

                // The type of transaction Possible values: l  cart l  express-checkout 
                string transactionType = decoder["PAYMENTINFO_0_TRANSACTIONTYPE"];

                // Indicates whether the payment is instant or delayed. Possible values: l  none l  echeck l  instant 
                string paymentType = decoder["PAYMENTINFO_0_PAYMENTTYPE"];

                // Time/date stamp of payment
                string orderTime = decoder["PAYMENTINFO_0_ORDERTIME"];

                // The final amount charged, including any shipping and taxes from your Merchant Profile.
                string amt = decoder["PAYMENTINFO_0_AMT"];

                // A three-character currency code for one of the currencies listed in PayPay-Supported Transactional Currencies. Default: USD.    
                string currencyCode = decoder["PAYMENTINFO_0_CURRENCYCODE"];

                // PayPal fee amount charged for the transaction    
                string feeAmt = decoder["PAYMENTINFO_0_FEEAMT"];

                // Amount deposited in your PayPal account after a currency conversion.    
                string settleAmt = decoder["PAYMENTINFO_0_SETTLEAMT"];

                // Tax charged on the transaction.    
                string taxAmt = decoder["PAYMENTINFO_0_TAXAMT"];

                //' Exchange rate if a currency conversion occurred. Relevant only if your are billing in their non-primary currency. If 
                string exchangeRate = decoder["PAYMENTINFO_0_EXCHANGERATE"];


                ret = test.GetShippingDetails(token, ref payerId, ref shippingAddress, ref retMsg);

                ReleveTransaction releve = new ReleveTransaction();
                string[] split = shippingAddress.Split(';');
                releve.Adresse = split[0];
                releve.Province = split[2];
                releve.Ville = split[1];
                releve.Zip = split[3];
                releve.Acheteur = User.Identity.GetUserName();
                releve.CoutTotal = cart.GetTotal();
                releve.Date = DateTime.Now;
                StoreDB.ReleveTransaction.Add(releve);
                foreach (var c in cart.GetCartItems())
                {
                    if (c.AlbumID == null)
                    {
                        releve.Envoyer = true;
                    }
                    else
                        releve.Envoyer = false;
                }
                StoreDB.SaveChanges();

                return RedirectToAction("EmptyCart", "ShoppingCart", new { id = releve.ReleveTransactionID});
            }
            else
            {
                Response.Redirect("APIError?" + retMsg);
            }
            return View();
        }
    }
}