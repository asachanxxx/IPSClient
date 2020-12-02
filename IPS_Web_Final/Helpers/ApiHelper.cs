
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using System.Web;
using IPS_Web_Final.Models;

namespace IPS_Web_Final
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
       


        public ApiHelper()
        {

            var configuration = System.Configuration.ConfigurationManager.AppSettings;
            MerchantId = (configuration["MerchantId"] != null) ? configuration["MerchantId"] : "PRIME4TEST";
            Password = (configuration["Password"] != null) ? configuration["Password"] : "123456";
            Key = (configuration["Key"] != null) ? configuration["Key"] : "MTIzNDU2UFJJTUU0VEVTV";
            API_URL = (configuration["API_URL_exppear"] != null) ? configuration["API_URL_exppear"] : "https://testipg.nationsdev.com/ipg/servlet_exppear";

        }


        public Dictionary<string, string> saleTxn(PostingVM vm)
        {
            Dictionary<string, string> SalesData = new Dictionary<string, string>();

            string PTInvoice = "<req>" +
              "<mer_id>" + MerchantId + "</mer_id>" +
              "<mer_txn_id>" + vm.MerRefID + "</mer_txn_id>" +
              "<action>" + vm.Action + "</action>" +
              "<txn_amt>" + vm.TxnAmount + "</txn_amt>" +
              "<cur>" + vm.CurrencyCode + "</cur>" +
              "<lang>" + vm.LanguageCode + "</lang>";

            if ((vm.ReturnURL != null) && (vm.ReturnURL.Length > 0))
            {
                PTInvoice = PTInvoice + "<ret_url>" + vm.ReturnURL + "</ret_url>";
            }

            if ((vm.MerVar1 != null) && (vm.MerVar1.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var1>" + vm.MerVar1 + "</mer_var1>";
            }

            if ((vm.MerVar2 != null) && (vm.MerVar2.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var2>" + vm.MerVar2 + "</mer_var2>";
            }

            if ((vm.MerVar3 != null) && (vm.MerVar3.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var3>" + vm.MerVar3 + "</mer_var3>";
            }

            if ((vm.MerVar4 != null) && (vm.MerVar4.Length > 0))
            {
                PTInvoice = PTInvoice + "<mer_var4>" + vm.MerVar4 + "</mer_var4>";
            }
            PTInvoice = PTInvoice + "</req>";

            var finalHex = Cryptographer.ByteArrayToString(Encoding.UTF8.GetBytes(PTInvoice));

            return PearToPear(finalHex, false, vm.Action);
        }

        public Dictionary<string, string> saleTxnVerify(TransVerifyVM vm)
        {
            string PTInvoice = "<req>" +
               "<mer_id>" + MerchantId + "</mer_id>" +
               "<mer_txn_id>" + vm.MerRefID + "</mer_txn_id>" +
               "<txn_uuid>" + vm.TxnUUID + "</txn_uuid>" +
               "<action>" + vm.Action + "</action>";

            PTInvoice = PTInvoice + "</req>";

            var finalHex = Cryptographer.ByteArrayToString(Encoding.UTF8.GetBytes(PTInvoice));

            return PearToPear(finalHex, false, vm.Action);
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
            var dicVariables = BreakResponseString(responseString);

        
            //PeerReturns.Add("ipay_out__txn_uuid", UUID);
            //PeerReturns.Add("ipay_out__ipg_txn_id", UUID);

            return dicVariables;
        }



        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
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

        private void GetResponseCallback(IAsyncResult asynchronousResult)
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

        public Dictionary<string, string> BreakResponseString(string input)
        {
            Dictionary<string, string> theList = new Dictionary<string, string>();
            var breakdown = input.Split('&').ToList();
            if (breakdown.Count > 0)
            {
                foreach (var item in breakdown)
                {
                    var secondbreak = item.Split('=');
                    if (secondbreak.Length == 2)
                    {
                        var converted = "";
                        switch (secondbreak[0])
                        {
                            case "PTRECEIPT":
                                converted = Encoding.ASCII.GetString(FromHex(secondbreak[1]));
                                theList.Add(secondbreak[0], converted);
                                break;
                            default:
                                theList.Add(secondbreak[0], secondbreak[1]);
                                break;
                        }

                    }
                }
                var toLst = theList.ToList();
            }
            else
            {
                theList.Add("ERROR_CODE", "NullException");
            }
            return theList;
        }

        public  byte[] FromHex(string hex)
        {
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }



    }

}

