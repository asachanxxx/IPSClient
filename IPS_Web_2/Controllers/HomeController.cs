using IPS_Web_2.Helpers;
using IPS_Web_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPS_Web_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        [HttpPost]
        public ActionResult IndexPost()
        {
            string autoHtml = "";
            string UUID = "";
            var API_URL_Pay = "https://testipg.nationsdev.com/ipg/servlet_exppay";
            string SessionXML = "3c7265713e3c6d65725f69643e544553544c4b52313c2f6d65725f69643e3c6d65725f74786e5f69643e31333434373c2f6d65725f74786e5f69643e3c616374696f6e3e53616c6554786e3c2f616374696f6e3e3c74786e5f616d743e3830302e30303c2f74786e5f616d743e3c6375723e4c4b523c2f6375723e3c6c616e673e656e673c2f6c616e673e3c7265745f75726c3e68747470733a2f2f3230322e3132342e3137322e3134313a3434332f5052494d4534544553542f526573706f6e73652e6a73703c2f7265745f75726c3e3c2f7265713e";
            APIHandler handler = new APIHandler();
            var returnData = handler.PearToPear(SessionXML, false, "SaleTxn");
            UUID = returnData["txn_uuid"];

            //Add the UUID to the session so it can be used when trns verifying
            Session["ipay_out__txn_uuid"] = UUID;

            //Commence AutoRedirect 
            autoHtml = GenaralHelpers.GenarateBrowserRedirectionHTML(API_URL_Pay, UUID);

            return Content(autoHtml);
        }

        public ActionResult PostBackFromPayment()
        {
            APIHandler apiCaller = new APIHandler();
            string UUID = Session["ipay_out__txn_uuid"].ToString();
            var API_URL_Pay = "https://testipg.nationsdev.com/ipg/servlet_exppay";
            string SessionXML = "3c7265713e3c6d65725f69643e544553544c4b52313c2f6d65725f69643e3c6d65725f74786e5f69643e39313935333c2f6d65725f74786e5f69643e3c74786e5f757569643e63303031633264332d376464622d346564622d386439372d6562393363666636313935613c2f74786e5f757569643e3c616374696f6e3e73616c6554786e5665726966793c2f616374696f6e3e3c2f7265713e";
            APIHandler handler = new APIHandler();
            var returnData = handler.PearToPear(SessionXML, false, "saleTxnVerify");

            

            if (returnData.ContainsKey("ERROR_CODE"))
            {
                //**************************THIS MEAN AN ERROR ON RESPONSE*********************
                //NEED TO HANDLE THIS *********************************************************
                return View();
            }
            else
            {

                PostBackVM postbackVM = new PostBackVM()
                {
                    AuthCode = (returnData["auth_code"] != null) ? returnData["auth_code"] : "",
                    BankRefID = (returnData["bank_ref_id"] != null) ? returnData["bank_ref_id"] : "",
                    CurrencyCode = (returnData["cur"] != null) ? returnData["cur"] : "",
                    CustomerName = (returnData["name"] != null) ? returnData["name"] : "",
                    FailReason = (returnData["reason"] != null) ? returnData["reason"] : "",
                    IPGTransactionID = (returnData["ipg_txn_id"] != null) ? returnData["ipg_txn_id"] : "",
                    MaskedAccNo = (returnData["acc_no"] != null) ? returnData["acc_no"] : "",
                    ServerTime = (returnData["server_time"] != null) ? returnData["server_time"] : "",
                    LanguageCode = (returnData["lang"] != null) ? returnData["lang"] : "",
                    MerRefID = (returnData["mer_txn_id"] != null) ? returnData["mer_txn_id"] : "",
                    TxnAmount = (returnData["txn_amt"] != null) ? returnData["txn_amt"] : "",
                    TxnStatus = (returnData["txn_status"] != null) ? returnData["txn_status"] : "",
                    MerVar1 = (returnData["mer_var1"] != null) ? returnData["mer_var1"] : "",
                    MerVar2 = (returnData["mer_var2"] != null) ? returnData["mer_var2"] : "",
                    MerVar3 = (returnData["mer_var2"] != null) ? returnData["mer_var2"] : "",
                    MerVar4 = (returnData["mer_var2"] != null) ? returnData["mer_var2"] : "",
                };
                return View(postbackVM);
            }

        }
    }
}