using IPS_Web_Final.Helpers;
using IPS_Web_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPS_Web_Final.Controllers
{
    public class HomeController : Controller
    {
        string SalesTransactionType = ""; //saleTxnGroup  or SaleTxn
        string Return_Url = "";
        string API_URL_exppear = "https://testipg.nationsdev.com/ipg/servlet_exppear";
        string API_URL_Pay = "https://testipg.nationsdev.com/ipg/servlet_exppay";
        GenaralHelpers gHelper = null;
        public HomeController()
        {
            var _Configuration = System.Configuration.ConfigurationManager.AppSettings;
            SalesTransactionType = (_Configuration["SalesFunction"] != null) ? _Configuration["SalesFunction"] : "SaleTxn";
            Return_Url = (_Configuration["ipay_in__ret_url"] != null) ? _Configuration["ipay_in__ret_url"] : "";
            API_URL_Pay = (_Configuration["API_URL_Pay"] != null) ? _Configuration["API_URL_Pay"] : "https://testipg.nationsdev.com/ipg/servlet_exppay";
            API_URL_exppear = (_Configuration["API_URL_exppear"] != null) ? _Configuration["API_URL_exppear"] : "https://testipg.nationsdev.com/ipg/servlet_exppear";
            

        }
        public ActionResult Index()
        {
            Cart obj = new ObjectHelper().GetFilledCard();
            return View(obj);
        }

        [HttpPost]
        public ActionResult IndexPost()
        {
            gHelper = new GenaralHelpers(Session);
            try
            {
                ViewBag.Message = "Your application description page.";
                PostingVM vm = new PostingVM();
                string sAmount = "10.00";
                string Mer_ref = GenaralHelpers.GetRandomNumber().ToString();
                Session["ipay_in__mer_ref_id"] = Mer_ref;
                //Set session Data
                vm.TxnAmount = sAmount;
                vm.Action = SalesTransactionType;
                vm.CurrencyCode = "LKR";
                vm.LanguageCode = "eng";
                vm.MerRefID = Mer_ref;
                vm.ItemList = "";
                vm.ReturnURL = Return_Url;
                vm.MerVar1 = "";
                vm.MerVar2 = "";
                vm.MerVar3 = "";
                vm.MerVar4 = "";

                string autoHtml = "";

                switch (SalesTransactionType)
                {
                    case "SaleTxn":
                        autoHtml = gHelper.DoApiSaleTransaction(SalesTransactionType, vm, API_URL_Pay);
                        break;
                    case "saleTxnGroup":
                        autoHtml = gHelper.DoApiSaleTransaction(SalesTransactionType, vm, API_URL_Pay);
                        break;
                }

                return Content(autoHtml);
            }
            catch (Exception ex) {
                //Need to include your error handling code here. just for demostration purpose we direct the full error message to view
                var errorMessage = ex.Message;
                ViewBag.Error = errorMessage;
                return View();
            }
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

      

        public ActionResult PostBackFromPayment()
        {
            ViewBag.Message = "Your application description page.";
            TransVerifyVM vm = new TransVerifyVM();
            gHelper = new GenaralHelpers(Session);
            
            vm.TxnUUID = (Session["ipay_out__txn_uuid"] != null)? Session["ipay_out__txn_uuid"].ToString(): ""; //Get this from session
            vm.Action = "saleTxnVerify";
            vm.MerRefID =(Session["ipay_in__mer_ref_id"] != null)? Session["ipay_in__mer_ref_id"].ToString():"";
            var backVM = gHelper.DoApiSaleTransactionVerify(vm);
            return View(backVM);
        }



    }
}