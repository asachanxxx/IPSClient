
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using IPS_Web.ViewModels;
using System.Web.Mvc;
using System.Web;

namespace IPS_Web
{
    public class ApiHelper
    {
        private string MerchantId = "PRIME4TEST";
        private string Password = "123456";
        private string Key = "MTIzNDU2UFJJTUU0VEVTV";
        private string Version = "1.00";
        private string API_URL = "https://testipg.nationsdev.com/ipg/servlet_exppear";
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        static byte[] dataToPost;
        static string encodedData;
        static string responseString;

        TempDataDictionary TempData;
        HttpSessionStateBase Session;


        public ApiHelper(HttpSessionStateBase session)
        {

            Session = session;
            var configuration = System.Configuration.ConfigurationManager.AppSettings;
            MerchantId = (configuration["MerchantId"] != null) ? configuration["MerchantId"] : "PRIME4TEST";
            Password = (configuration["Password"] != null) ? configuration["Password"] : "123456";
            Key = (configuration["Key"] != null) ? configuration["Key"] : "MTIzNDU2UFJJTUU0VEVTV";
            API_URL = (configuration["API_URL_exppear"] != null) ? configuration["API_URL_exppear"] : "https://testipg.nationsdev.com/ipg/servlet_exppear";

        }


        public Dictionary<string, string> saleTxn()
        {
            Dictionary<string, string> SalesData = new Dictionary<string, string>();
            string Action = "";
            string CurrencyCode = "";
            string MerRefID = "";
            string TxnAmount = "";
            string LanguageCode = "";
            string ReturnURL = "";
            string MerVar1 = "";
            string MerVar2 = "";
            string MerVar3 = "";
            string MerVar4 = "";

            Action = (Session["ipay_in__action"] != null) ? Session["ipay_in__action"].ToString() : "";
            CurrencyCode = (Session["ipay_in__cur"] != null) ? Session["ipay_in__cur"].ToString() : "";
            MerRefID = (Session["ipay_in__mer_ref_id"] != null) ? Session["ipay_in__mer_ref_id"].ToString() : "";
            TxnAmount = (Session["ipay_in__txn_amt"] != null) ? Session["ipay_in__txn_amt"].ToString() : "";
            LanguageCode = (Session["ipay_in__lang"] != null) ? Session["ipay_in__lang"].ToString() : "";
            ReturnURL = (Session["ipay_in__ret_url"] != null) ? Session["ipay_in__ret_url"].ToString() : "";
            MerVar1 = (Session["ipay_in__mer_var1"] != null) ? Session["ipay_in__mer_var1"].ToString() : "";
            MerVar2 = (Session["ipay_in__mer_var2"] != null) ? Session["ipay_in__mer_var2"].ToString() : "";
            MerVar3 = (Session["ipay_in__mer_var3"] != null) ? Session["ipay_in__mer_var3"].ToString() : "";
            MerVar4 = (Session["ipay_in__mer_var4"] != null) ? Session["ipay_in__mer_var4"].ToString() : "";


            string PTInvoice = "<req>" +
              "<mer_id>" + MerchantId + "</mer_id>" +
              "<mer_txn_id>" + MerRefID + "</mer_txn_id>" +
              "<action>" + Action + "</action>" +
              "<txn_amt>" + TxnAmount + "</txn_amt>" +
              "<cur>" + CurrencyCode + "</cur>" +
              "<lang>" + LanguageCode + "</lang>";

            if ((ReturnURL != null) && (ReturnURL.Length > 0))
            {
                PTInvoice = PTInvoice + "<ret_url>" + ReturnURL + "</ret_url>";
            }

            if ((MerVar1 != null) && (MerVar1.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var1>" + MerVar1 + "</mer_var1>";
            }

            if ((MerVar2 != null) && (MerVar2.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var2>" + MerVar2 + "</mer_var2>";
            }

            if ((MerVar3 != null) && (MerVar3.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var3>" + MerVar3 + "</mer_var3>";
            }

            if ((MerVar4 != null) && (MerVar4.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var4>" + MerVar4 + "</mer_var4>";
            }

            PTInvoice = PTInvoice + "</req>";

            var byteArray = Encoding.UTF8.GetBytes(PTInvoice);

            var finalHex = Cryptographer.ByteArrayToString(byteArray);

            return PearToPear(finalHex, false, "SaleTxn");

        }

        public Dictionary<string, string> saleTxnVerify()
        {
            string Action = "";
            string MerRefID = "";
            string TxnUUID = "";

            Action = (Session["ipay_in__action"] != null) ? Session["ipay_in__action"].ToString() : "saleTxnVerify";
            MerRefID = (Session["ipay_in__mer_ref_id"] != null) ? Session["ipay_in__mer_ref_id"].ToString() : "";
            TxnUUID = (Session["ipay_out__txn_uuid"] != null) ? Session["ipay_out__txn_uuid"].ToString() : "";

            string PTInvoice = "<req>" +
               "<mer_id>" + MerchantId + "</mer_id>" +
               "<mer_txn_id>" + MerRefID + "</mer_txn_id>" +
               "<txn_uuid>" + TxnUUID + "</txn_uuid>" +
               "<action>" + Action + "</action>";

            PTInvoice = PTInvoice + "</req>";

            var byteArray = Encoding.UTF8.GetBytes(PTInvoice);

            var finalHex = Cryptographer.ByteArrayToString(byteArray);

            return PearToPear(finalHex, false, "saleTxnVerify");
        }

        private Dictionary<string, string> PearToPear(string sessionXML, bool bEncrypt, string Action)
        {

            Dictionary<string, string> PeerReturns = new Dictionary<string, string>();
            string UserAgent = "Mozilla/4.0";

            var hashedData = Cryptographer.ConvertToSHA256(sessionXML);
            encodedData = "VERSION=" + Version + "&PWD=" + WebUtility.UrlDecode(Password) + "&MERCHANTID=" + MerchantId + "&KEY=" + Key + "&HASH=" + hashedData;

            if (bEncrypt)
            {
                encodedData = encodedData + "&ENINVOICE=" + Cryptographer.EncryptInvoice(sessionXML);
            }
            else
            {

                encodedData = encodedData + "&PTINVOICE=" + sessionXML;
            }

            dataToPost = Encoding.UTF8.GetBytes(encodedData);

            // Create a new HttpWebRequest object.
            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(API_URL);
            request2.UserAgent = UserAgent;
            request2.ContentType = "application/x-www-form-urlencoded";
            request2.ContentLength = dataToPost.Length;

            // Set the Method property to 'POST' to post data to the URI.
            request2.Method = "POST";

            // start the asynchronous operation
            request2.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), request2);

            // Keep the main thread from continuing while the asynchronous
            // operation completes. A real world application
            // could do something useful such as updating its user interface.
            allDone.WaitOne();

            //Break the return values by char '&' 
            var dicVariables = GenaralHelpers.BreakResponseString(responseString);

            var errorCode = dicVariables.ToList().Find(a => a.Key == "ERROR_CODE").Value;
            if (errorCode == "000")
            {
                //Extract UUID from the Data budnle. these lines can be writen in same line. but for demostration purpose we keep it separated
                PeerReturns = GenaralHelpers.ReadFromXmlString(dicVariables.ToList().Find(a => a.Key == "PTRECEIPT").Value, Action);
            }
            else {
                PeerReturns.Add("ERROR_CODE", errorCode);
            }
            //PeerReturns.Add("ipay_out__txn_uuid", UUID);
            //PeerReturns.Add("ipay_out__ipg_txn_id", UUID);

            return PeerReturns;
        }

        private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            Stream postStream = request.EndGetRequestStream(asynchronousResult);

            // Write to the request stream.
            postStream.Write(dataToPost, 0, encodedData.Length);
            postStream.Close();

            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        private static void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            responseString = streamRead.ReadToEnd();
            // Close the stream object
            streamResponse.Close();
            streamRead.Close();

            // Release the HttpWebResponse
            response.Close();
            allDone.Set();
        }



    }

}

