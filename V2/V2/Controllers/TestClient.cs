using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using V2.Models;
using V2.PurolatorReference;

namespace V2.Controllers.Purolator
{
    class TestClient
    {
        List<DictionaryCollection> lstShipping = new List<DictionaryCollection>();
        /// <summary>
        /// Créer un service
        /// </summary>
        /// <returns></returns>
        private EstimatingService CreateEstimatingService()
        {
            EstimatingService service = new EstimatingService();

            // Setup the credentials for basic authentication
            //TODO : Define the Production (or development) Key and Password
            service.Credentials = new NetworkCredential("2ded8b175ea140cca57e594dcea32016", "GV1M{)%;");

            // Set the request's context values
            service.RequestContextValue = new RequestContext();
            service.RequestContextValue.Version = "1.4";
            service.RequestContextValue.Language = Language.en;
            service.RequestContextValue.GroupID = "";
            service.RequestContextValue.RequestReference = "RequestReference";
            return service;
        }


        /// <summary>
        /// Rempli la liste des options de shipping
        /// </summary>
        /// <param name="ville"></param>
        /// <param name="country"></param>
        /// <param name="province"></param>
        /// <param name="postalcode"></param>
        public void CallGetQuickEstimate(string ville,string country,string province, string postalcode)
        {
            EstimatingService service = CreateEstimatingService();
            GetQuickEstimateRequestContainer request = new GetQuickEstimateRequestContainer();
            GetQuickEstimateResponseContainer response = new GetQuickEstimateResponseContainer();

            // Setup the request to perform a create shipment
            //TODO : Define the Billing account and the account that is registered with PWS
            request.BillingAccountNumber = "9999999999";
            //TODO : Populate the Origin Information
            request.SenderPostalCode = "J2S6A6";
            //TODO : Populate the Desination Information
            request.ReceiverAddress = new ShortAddress();
            request.ReceiverAddress.City = ville;
            request.ReceiverAddress.Country = country;
            request.ReceiverAddress.Province = province;
            request.ReceiverAddress.PostalCode = postalcode;
            request.PackageType = "ExpressEnvelope";
            request.TotalWeight = new TotalWeight();
            request.TotalWeight.Value = 1;
            request.TotalWeight.WeightUnit = WeightUnit.lb;

            try
            {
                // Call the service
                response = service.GetQuickEstimate(request);
                int i = 0;
                foreach (ShipmentEstimate sh in response.ShipmentEstimates)
                {
                    DictionaryCollection dc = new DictionaryCollection();
                    dc.DateArrivee = response.ShipmentEstimates[i].ExpectedDeliveryDate;
                    dc.DateDepart = response.ShipmentEstimates[i].ShipmentDate;
                    dc.Prix = response.ShipmentEstimates[i].TotalPrice;
                    dc.ServiceNom = response.ShipmentEstimates[i].ServiceID;
                    // Display the response
                    lstShipping.Add(dc);
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Michael Veilleux
        /// Retourne la liste
        /// 11/18/2015
        /// </summary>
        /// <returns></returns>
        public List<DictionaryCollection> GetList(string ville,string pays,string province,string postalcode)
        {
            CallGetQuickEstimate(ville,pays,province,postalcode);
            return lstShipping;
        }
    }
}

       

      