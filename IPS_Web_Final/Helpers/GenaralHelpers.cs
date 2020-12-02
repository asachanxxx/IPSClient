using IPS_Web_Final.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace IPS_Web_Final
{ 
    public class GenaralHelpers
    {
        HttpSessionStateBase _Session;

        public GenaralHelpers(HttpSessionStateBase session)
        {
            _Session = session;
        }

        public static int GetRandomNumber() {
            Random rand = new Random();
            return rand.Next(1, 100000) + 1;
        }

        public int GetRoundWithDecimalPrecision(string Number)
        {
            decimal number = decimal.Zero;
            decimal.TryParse(Number, out number);
            return int.Parse(number.ToString());
        }

        public string GenarateBrowserRedirectionHTML(string Api_Url,string UUid)
        {
            var finalHtml = "";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine(" <meta charset=\"UTF - 8\">");
            sb.AppendLine("<meta name=\"viewport\" content=\"width = device - width, initial - scale = 1.0\">");
            sb.AppendLine("<title>HDPS-IPS.net Browser Redirection</title>");

            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine("function closethisasap() {");
            sb.AppendLine("document.forms['redirectpost'].submit();");
            sb.AppendLine("}");
            sb.AppendLine("</script>");

            sb.AppendLine("</head>");
            sb.AppendLine("<body onload='closethisasap();'>");
            sb.AppendLine("<form name='redirectpost' method='post' action='" + Api_Url + "'>");
            sb.AppendLine("<input type='hidden' name='TXN_UUID' value='" + UUid + "'>");
            sb.AppendLine("</form>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            finalHtml = sb.ToString();
            return finalHtml;
        }


        public  Dictionary<string, string> ReadFromXmlString(string UUIDXmlString, string saleTransType)
        {
            if (saleTransType == "saleTxnVerify") {
                //UUIDXmlString = "<res><txn_amt>400.00</txn_amt><cur>LKR</cur><server_time>2020-11-19 10:42:25</server_time><reason>ERR_04_28 - Internal error occurred. Contact bank administrator with error code.|ERR_04_31 - Credit sale failed.</reason><ipg_txn_id>VIDUNATEXT160576274503141</ipg_txn_id><bank_ref_id>39485</bank_ref_id><acc_no>376657XXXXX4809</acc_no><name>test</name><action>SaleTxn</action><lang>eng</lang><txn_status>REJECTED</txn_status><mer_txn_id>81945</mer_txn_id></res>";
            }

            Dictionary<string, string> ParamOut = new Dictionary<string, string>();
            using (XmlReader reader = XmlReader.Create(new StringReader(UUIDXmlString)))
            {
                while (reader.Read())
                {
                    switch (saleTransType)
                    {
                        case "SaleTxn":
                            switch (reader.Name.ToString().Trim())
                            {
                                case "txn_uuid":
                                    ParamOut.Add("txn_uuid", reader.ReadString());
                                    break;
                                case "ipg_txn_id":
                                    ParamOut.Add("ipg_txn_id", reader.ReadString());
                                    break;
                                case "mer_txn_id":
                                    ParamOut.Add("mer_txn_id", reader.ReadString());
                                    break;
                            }
                            break;
                        case "saleTxnGroup":
                            switch (reader.Name.ToString().Trim())
                            {
                                case "txn_uuid":
                                    ParamOut.Add("txn_uuid", reader.ReadString());
                                    break;
                                case "ipg_txn_id":
                                    ParamOut.Add("ipg_txn_id", reader.ReadString());
                                    break;
                                case "mer_txn_id":
                                    ParamOut.Add("mer_txn_id", reader.ReadString());
                                    break;
                            }
                            break;
                        case "saleTxnVerify":
                            switch (reader.Name.ToString().Trim())
                            {
                                case "bank_ref_id":
                                    ParamOut.Add("bank_ref_id", reader.ReadString());
                                    break;
                                case "cur":
                                    ParamOut.Add("cur", reader.ReadString());
                                    break;
                                case "ipg_txn_id":
                                    ParamOut.Add("ipg_txn_id", reader.ReadString());
                                    break;
                                case "mer_txn_id":
                                    ParamOut.Add("mer_txn_id", reader.ReadString());
                                    break;
                                case "txn_amt":
                                    ParamOut.Add("txn_amt", reader.ReadString());
                                    break;
                                case "txn_status":
                                    ParamOut.Add("txn_status", reader.ReadString());
                                    break;
                                case "server_time":
                                    ParamOut.Add("server_time", reader.ReadString());
                                    break;
                                case "acc_no":
                                    ParamOut.Add("acc_no", reader.ReadString());
                                    break;
                                case "lang":
                                    ParamOut.Add("lang", reader.ReadString());
                                    break;
                                case "mer_var1":
                                    ParamOut.Add("mer_var1", reader.ReadString());
                                    break;
                                case "mer_var2":
                                    ParamOut.Add("mer_var2", reader.ReadString());
                                    break;
                                case "mer_var3":
                                    ParamOut.Add("mer_var3", reader.ReadString());
                                    break;
                                case "mer_var4":
                                    ParamOut.Add("mer_var4", reader.ReadString());
                                    break;
                                case "name":
                                    ParamOut.Add("name", reader.ReadString());
                                    break;
                                case "reason":
                                    ParamOut.Add("reason", reader.ReadString());
                                    break;
                                case "auth_code":
                                    ParamOut.Add("auth_code", reader.ReadString());
                                    break;

                            }
                            break;
                        case "AddWallet":
                            switch (reader.Name.ToString().ToUpper())
                            {
                                case "first_name":
                                    ParamOut.Add("first_name", reader.ReadString());
                                    break;
                                case "last_name":
                                    ParamOut.Add("last_name", reader.ReadString());
                                    break;
                                case "email_address":
                                    ParamOut.Add("email_address", reader.ReadString());
                                    break;
                                case "wallet_id":
                                    ParamOut.Add("wallet_id", reader.ReadString());
                                    break;
                                case "card_out_list":
                                    ParamOut.Add("card_out_list", reader.ReadString());
                                    break;
                                case "reason":
                                    ParamOut.Add("reason", reader.ReadString());
                                    break;
                            }
                            break;
                    }
                }
            }
            return ParamOut;
        }

        internal PostBackVM DoApiSaleTransactionVerify(TransVerifyVM vm)
        {
            ApiHelper apiCaller = new ApiHelper();
            
            var dataDic = apiCaller.saleTxnVerify(vm);
            var callback  = ReadFromXmlString(dataDic.ToList().Find(a => a.Key == "PTRECEIPT").Value, "saleTxnVerify");

            PostBackVM pvm = new PostBackVM();

            if (callback.ContainsKey("txn_status"))
            {

                pvm.AuthCode = (callback.ContainsKey("auth_code")) ? callback["auth_code"] : "";
                pvm.BankRefID = (callback.ContainsKey("bank_ref_id")) ? callback["bank_ref_id"] : "";
                pvm.CurrencyCode = (callback.ContainsKey("cur")) ? callback["cur"] : "";
                pvm.CustomerName = (callback.ContainsKey("name")) ? callback["name"] : "";
                pvm.FailReason = (callback.ContainsKey("reason")) ? callback["reason"] : "";
                pvm.IPGTransactionID = (callback.ContainsKey("ipg_txn_id")) ? callback["ipg_txn_id"] : "";
                pvm.LanguageCode = (callback.ContainsKey("lang")) ? callback["lang"] : "";
                pvm.MaskedAccNo = (callback.ContainsKey("acc_no")) ? callback["acc_no"] : "";
                pvm.MerRefID = (callback.ContainsKey("mer_var1")) ? callback["mer_var1"] : "";
                pvm.MerVar1 = (callback.ContainsKey("mer_var1")) ? callback["mer_var1"] : "";
                pvm.MerVar2 = (callback.ContainsKey("mer_var2")) ? callback["mer_var2"] : "";
                pvm.MerVar3 = (callback.ContainsKey("mer_var3")) ? callback["mer_var3"] : "";
                pvm.MerVar4 = (callback.ContainsKey("mer_var4")) ? callback["mer_var4"] : "";
                pvm.ServerTime = (callback.ContainsKey("server_time")) ? callback["server_time"] : "";
                pvm.TxnAmount = (callback.ContainsKey("txn_amt")) ? callback["txn_amt"] : "";
                pvm.TxnStatus = (callback.ContainsKey("txn_status")) ? callback["txn_status"] : "";
            }
            else {
                pvm.TxnStatus = "UnAuthorized";
            }

            return pvm;
        }

        internal  string DoApiSaleTransaction(string sAction, PostingVM vm, string API_URL_Pay)
        {
            string autoHtml = "";
            //Create Api Helper Instance. Don't use static methods inside  ApiHelper or use them anywhere 
            ApiHelper apiCaller = new ApiHelper();

            //Calling the api caller Sale Transaction initiate with data 				               
            var returnData = apiCaller.saleTxn(vm);
            var errorCode = returnData.ToList().Find(a => a.Key == "ERROR_CODE").Value;
            var errorMessage = returnData.ToList().Find(a => a.Key == "ERROR_MSG").Value;
            var tranUuid = "";
            if (errorCode == "000")
            {
                //Extract UUID from the Data budnle. these lines can be writen in same line. but for demostration purpose we keep it separated
                var dataDic = ReadFromXmlString(returnData.ToList().Find(a => a.Key == "PTRECEIPT").Value, sAction);
                tranUuid = dataDic["txn_uuid"];

                //Add the UUID to the session so it can be used when trns verifying
                _Session["ipay_out__txn_uuid"] = tranUuid;

                autoHtml = GenarateBrowserRedirectionHTML(API_URL_Pay, tranUuid);
            }
            else
            {
                throw new Exception(errorMessage);
            }
            return autoHtml;
        }

     

    }
}
