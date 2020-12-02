using IPS_Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IPS_Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
			//var session = HttpContext.Current.Session["__MySession__"];
		}

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                ToSu1 = "0"
            };

            return View(viewModel);
        }

		[HttpPost]
		public ActionResult IndexPost(IndexViewModel Model)
		{
			string UUID = "";
			string autoHtml = "";
			var _Configuration = System.Configuration.ConfigurationManager.AppSettings;
			var SalesTransactionType = (_Configuration["SalesFunction"] != null) ? _Configuration["SalesFunction"] : "SaleTxn";
			string Return_Url = (_Configuration["ipay_in__ret_url"] != null) ? _Configuration["ipay_in__ret_url"] : "";
			string API_URL_Pay = (_Configuration["API_URL_Pay"] != null) ? _Configuration["API_URL_Pay"] : "";

			//Set session Data
			Session["ipay_in__txn_amt"] = GenaralHelpers.GetRoundWithDecimalPrecision(Model.ToSu1.ToString()).ToString() + ".00";
			Session["ipay_in__action"] = SalesTransactionType;
			Session["ipay_in__cur"] = "LKR";
			Session["ipay_in__lang"] = "eng";
			Session["ipay_in__mer_ref_id"] = GenaralHelpers.GetRandomNumber().ToString();
			Session["ipay_in__item_list"] = "";
			Session["ipay_in__ret_url"] = Return_Url;
			Session["ipay_in__mer_var1"] = "";
			Session["ipay_in__mer_var2"] = "";
			Session["ipay_in__mer_var3"] = "";
			Session["ipay_in__mer_var4"] = "";


			//Create Api Helper Instance. Don't use static methods inside  ApiHelper or use them anywhere 
			ApiHelper apiCaller = new ApiHelper(Session);

			switch (SalesTransactionType)
			{
				case "SaleTxn":
					//Calling the api caller Sale Transaction initiate with data 				               
					var returnData = apiCaller.saleTxn();
					if (returnData.ContainsKey("ERROR_CODE"))
					{
						//**************************THIS MEAN AN ERROR ON RESPONSE*********************
						//NEED TO HANDLE THIS *********************************************************
					}
					else
					{
						UUID = returnData["txn_uuid"];

						//Add the UUID to the session so it can be used when trns verifying
						Session["ipay_out__txn_uuid"] = UUID;

						//Commence AutoRedirect 
						autoHtml = GenaralHelpers.GenarateBrowserRedirectionHTML(API_URL_Pay, UUID);
					}

					break;
				case "saleTxnGroup":
					//Calling the api caller Sale Transaction initiate with data 				               
					var returnDataVerify = apiCaller.saleTxn();
					if (returnDataVerify.ContainsKey("ERROR_CODE"))
					{
						//**************************THIS MEAN AN ERROR ON RESPONSE*********************
						//NEED TO HANDLE THIS *********************************************************
					}
					else
					{
						//Get UUID from the Return data
						UUID = returnDataVerify["ipay_out__txn_uuid"];


						//Add the UUID to the session so it can be used when trns verifying
						Session["ipay_out__txn_uuid"] = UUID;

						//Commence AutoRedirect 
						autoHtml = GenaralHelpers.GenarateBrowserRedirectionHTML(API_URL_Pay, UUID);

					}
					break;
			}
			return Content(autoHtml);
		}


        public ActionResult PostBackFromPayment() 
		{
			Session["ipay_in__action"] = "saleTxnVerify";

			ApiHelper apiCaller = new ApiHelper(Session);
			var returnData = apiCaller.saleTxnVerify();

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
    }
}