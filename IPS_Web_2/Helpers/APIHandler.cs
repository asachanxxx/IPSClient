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

namespace IPS_Web_2.Helpers
{
    public class APIHandler
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

        public Dictionary<string, string> PearToPear(string sessionXML, bool bEncrypt, string Action)
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
            else
            {
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