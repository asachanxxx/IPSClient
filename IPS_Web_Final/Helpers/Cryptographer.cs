using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IPS_Web_Final
{
    public class Cryptographer
    {
        public static string ConvertToSHA256(String TextToConvert)
        {
            StringBuilder Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(TextToConvert));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static string EncryptInvoice(string pass) {
            //To be implement the Encryption Machanism
            return "";
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public void decodeResponse(String sAction, String sResponse, bool bEncrypt)
        {
            //string success = "ERROR_CODE=000&HASH=c68ce32c03cae11ed1e9e2ecf48f938595437533cbf12d16392d7a105dd55cc8&PTRECEIPT=3C7265733E3C74786E5F757569643E61356638393533662D376665342D343139612D616138392D3333666562653735393635393C2F74786E5F757569643E3C2F7265733E";

            //Dictionary<string, string> hmResponse = new Dictionary<string, string>();
            //StringTokenizer stTok = new StringTokenizer(sResponse, "&");
            //while (stTok.hasMoreTokens())
            //{
            //    StringTokenizer stInternalTokenizer = new StringTokenizer(stTok.nextToken(), "=");
            //    if (stInternalTokenizer.countTokens() == 2)
            //    {
            //        String key = URLDecoder.decode(stInternalTokenizer.nextToken());
            //        String value = URLDecoder.decode(stInternalTokenizer.nextToken());
            //        hmResponse.put(key.toUpperCase(), value);
            //    }
            //}

            //string PTReceipt = { get; set; }
            //string MaskedAccNo = { get; set; }
            //string BankRefID = { get; set; }
            //string CurrencyCode = { get; set; }
            //string IPGTransactionID = { get; set; }
            //string LanguageCode = { get; set; }
            //string MerRefID = { get; set; }
            //string MerVar1 = { get; set; }
            //string MerVar2 = { get; set; }
            //string MerVar3 = { get; set; }
            //string MerVar4 = { get; set; }
            //string CustomerName = { get; set; }
            //string FailReason = { get; set; }
            //string TxnAmount = { get; set; }
            //string TxnStatus = { get; set; }
            //string ServerTime = { get; set; }
            //string AuthCode = { get; set; }
            //string TxnUuid = { get; set; }
            //string FirstName = { get; set; }
            //string LastName = { get; set; }
            //string eMailAddress = { get; set; }
            //string WalletID = { get; set; }
            //string CardOutList = { get; set; }

            //string sErrorCode = (string)hmResponse.get("ERROR_CODE");

            //System.out.println("--sErrorCode--" + sErrorCode);

            //if (sErrorCode.equalsIgnoreCase("000"))
            //{
            //    if (bEncrypt)
            //    {
            //        string ENReceipt = (string)hmResponse.get("ENRECEIPT");
            //        PTReceipt = decryptReceipt(ENReceipt);
            //    }
            //    else
            //    {
            //        PTReceipt = (string)hmResponse.get("PTRECEIPT");
            //    }

            //    PTReceipt = new string(convertHexToByte(PTReceipt));
            //    System.out.println("convertHexToByte--PTReceipt>>>" + PTReceipt);

            //    if (PTReceipt.length() > 0)
            //    {
            //        string sHash = (string)hmResponse.get("HASH");
            //        string sHash1 = sha256(PTReceipt + Key);

            //        System.out.println("sAction :" + sAction);
            //        if (sAction.equals(SALE_TRANSACTION))
            //        {
            //            int StartPos = PTReceipt.indexOf("<txn_uuid>");
            //            int EndPos = PTReceipt.indexOf("</txn_uuid>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                TxnUuid = PTReceipt.substring(StartPos + "<txn_uuid>".length(), EndPos);
            //                TxnUuid = convertBytesToHex((TxnUuid + "|" + Key).getBytes()); // gayan
            //                hmResponse.put("ipay_out__txn_uuid", TxnUuid);
            //            }

            //            StartPos = PTReceipt.indexOf("<ipg_txn_id>");
            //            EndPos = PTReceipt.indexOf("</ipg_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                IPGTransactionID = PTReceipt.substring(StartPos + "<ipg_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__ipg_txn_id", IPGTransactionID);
            //            }

            //            StartPos = PTReceipt.indexOf("<mer_txn_id>");
            //            EndPos = PTReceipt.indexOf("</mer_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerRefID = PTReceipt.substring(StartPos + "<mer_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_ref_id", MerRefID);
            //            }
            //        }


            //        if (sAction.equals(SALE_TRANSACTION_GROUP))
            //        {
            //            int StartPos = PTReceipt.indexOf("<txn_uuid>");
            //            int EndPos = PTReceipt.indexOf("</txn_uuid>");

            //            System.out.println("----StartPos--" + StartPos);
            //            System.out.println("----EndPos--" + EndPos);

            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                TxnUuid = PTReceipt.substring(StartPos + "<txn_uuid>".length(), EndPos);
            //                TxnUuid = convertBytesToHex((TxnUuid + "|" + Key).getBytes()); // gayan
            //                hmResponse.put("ipay_out__txn_uuid", TxnUuid);
            //            }

            //            StartPos = PTReceipt.indexOf("<ipg_txn_id>");
            //            EndPos = PTReceipt.indexOf("</ipg_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                IPGTransactionID = PTReceipt.substring(StartPos + "<ipg_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__ipg_txn_id", IPGTransactionID);
            //            }

            //            StartPos = PTReceipt.indexOf("<mer_txn_id>");
            //            EndPos = PTReceipt.indexOf("</mer_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerRefID = PTReceipt.substring(StartPos + "<mer_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_ref_id", MerRefID);
            //            }
            //        }


            //        if (sAction.equals(SALE_TRANSACTION_VERIFY))
            //        {
            //            Action
            //            int StartPos = PTReceipt.indexOf("<bank_ref_id>");
            //            int EndPos = PTReceipt.indexOf("</bank_ref_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                BankRefID = PTReceipt.substring(StartPos + "<bank_ref_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__bank_ref_id", BankRefID);
            //            }

            //            StartPos = PTReceipt.indexOf("<cur>");
            //            EndPos = PTReceipt.indexOf("</cur>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                CurrencyCode = PTReceipt.substring(StartPos + "<cur>".length(), EndPos);
            //                hmResponse.put("ipay_out__cur_code", CurrencyCode);
            //            }


            //            StartPos = PTReceipt.indexOf("<ipg_txn_id>");
            //            EndPos = PTReceipt.indexOf("</ipg_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                IPGTransactionID = PTReceipt.substring(StartPos + "<ipg_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__ipg_txn_id", IPGTransactionID);
            //            }


            //            StartPos = PTReceipt.indexOf("<mer_txn_id>");
            //            EndPos = PTReceipt.indexOf("</mer_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerRefID = PTReceipt.substring(StartPos + "<mer_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_ref_id", MerRefID);
            //            }


            //            StartPos = PTReceipt.indexOf("<txn_amt>");
            //            EndPos = PTReceipt.indexOf("</txn_amt>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                TxnAmount = PTReceipt.substring(StartPos + "<txn_amt>".length(), EndPos);
            //                hmResponse.put("ipay_out__txn_amt", TxnAmount);
            //            }

            //            StartPos = PTReceipt.indexOf("<txn_status>");
            //            EndPos = PTReceipt.indexOf("</txn_status>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                TxnStatus = PTReceipt.substring(StartPos + "<txn_status>".length(), EndPos);
            //                hmResponse.put("ipay_out__txn_status", TxnStatus);
            //            }

            //            StartPos = PTReceipt.indexOf("<server_time>");
            //            EndPos = PTReceipt.indexOf("</server_time>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                ServerTime = PTReceipt.substring(StartPos + "<server_time>".length(), EndPos);
            //                hmResponse.put("ipay_out__server_time", ServerTime);
            //            }

            //            Optional
            //            StartPos = PTReceipt.indexOf("<acc_no>");
            //            EndPos = PTReceipt.indexOf("</acc_no>");

            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MaskedAccNo = PTReceipt.substring(StartPos + "<acc_no>".length(), EndPos);
            //                hmResponse.put("ipay_out__masked_acc_number", MaskedAccNo);
            //            }

            //            StartPos = PTReceipt.indexOf("<lang>");
            //            EndPos = PTReceipt.indexOf("</lang>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                LanguageCode = PTReceipt.substring(StartPos + "<lang>".length(), EndPos);
            //                hmResponse.put("ipay_out__lang", LanguageCode);
            //            }

            //            StartPos = PTReceipt.indexOf("<mer_var1>");
            //            EndPos = PTReceipt.indexOf("</mer_var1>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerVar1 = PTReceipt.substring(StartPos + "<mer_var1>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_var_1", MerVar1);
            //            }

            //            StartPos = PTReceipt.indexOf("<mer_var2>");
            //            EndPos = PTReceipt.indexOf("</mer_var2>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerVar2 = PTReceipt.substring(StartPos + "<mer_var2>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_var_2", MerVar2);
            //            }

            //            StartPos = PTReceipt.indexOf("<mer_var3>");
            //            EndPos = PTReceipt.indexOf("</mer_var3>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerVar3 = PTReceipt.substring(StartPos + "<mer_var3>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_var_3", MerVar3);
            //            }

            //            StartPos = PTReceipt.indexOf("<mer_var4>");
            //            EndPos = PTReceipt.indexOf("</mer_var4>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerVar4 = PTReceipt.substring(StartPos + "<mer_var4>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_var_4", MerVar4);
            //            }

            //            StartPos = PTReceipt.indexOf("<name>");
            //            EndPos = PTReceipt.indexOf("</name>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                CustomerName = PTReceipt.substring(StartPos + "<name>".length(), EndPos);
            //                hmResponse.put("ipay_out__card_holder_name", CustomerName);
            //            }

            //            StartPos = PTReceipt.indexOf("<reason>");
            //            EndPos = PTReceipt.indexOf("</reason>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                FailReason = PTReceipt.substring(StartPos + "<reason>".length(), EndPos);
            //                hmResponse.put("ipay_out__fail_reason", FailReason);
            //            }

            //            StartPos = PTReceipt.indexOf("<auth_code>");
            //            EndPos = PTReceipt.indexOf("</auth_code>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                AuthCode = PTReceipt.substring(StartPos + "<auth_code>".length(), EndPos);
            //                hmResponse.put("ipay_out__auth_code", AuthCode);
            //            }
            //        }


            //        if (sAction.equals(ADD_WALLET))
            //        {
            //            Action
            //            int StartPos = PTReceipt.indexOf("<first_name>");
            //            int EndPos = PTReceipt.indexOf("</first_name>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                FirstName = PTReceipt.substring(StartPos + "<first_name>".length(), EndPos);
            //                hmResponse.put("ipay_out__first_name", FirstName);
            //            }

            //            StartPos = PTReceipt.indexOf("<last_name>");
            //            EndPos = PTReceipt.indexOf("</last_name>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                LastName = PTReceipt.substring(StartPos + "<last_name>".length(), EndPos);
            //                hmResponse.put("ipay_out__last_name", LastName);
            //            }

            //            StartPos = PTReceipt.indexOf("<email_address>");
            //            EndPos = PTReceipt.indexOf("</email_address>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                eMailAddress = PTReceipt.substring(StartPos + "<email_address>".length(), EndPos);
            //                hmResponse.put("ipay_out__email_address", eMailAddress);
            //            }

            //            StartPos = PTReceipt.indexOf("<wallet_id>");
            //            EndPos = PTReceipt.indexOf("</wallet_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                WalletID = PTReceipt.substring(StartPos + "<wallet_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__wallet_id", WalletID);
            //            }

            //            Optional
            //            StartPos = PTReceipt.indexOf("<card_out_list>");
            //            EndPos = PTReceipt.indexOf("</card_out_list>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                CardOutList = PTReceipt.substring(StartPos + "<card_out_list>".length(), EndPos);
            //                hmResponse.put("ipay_out__card_out_list", CardOutList);
            //            }

            //            StartPos = PTReceipt.indexOf("<reason>");
            //            EndPos = PTReceipt.indexOf("</reason>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                FailReason = PTReceipt.substring(StartPos + "<reason>".length(), EndPos);
            //                hmResponse.put("ipay_out__fail_reason", FailReason);
            //            }
            //        }

            //        if (sAction.equals(ADD_CARD))
            //        {
            //            Action
            //            int StartPos = PTReceipt.indexOf("<wallet_id>");
            //            int EndPos = PTReceipt.indexOf("</wallet_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                WalletID = PTReceipt.substring(StartPos + "<wallet_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__wallet_id", WalletID);
            //            }

            //            StartPos = PTReceipt.indexOf("<card_out_list>");
            //            EndPos = PTReceipt.indexOf("</card_out_list>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                CardOutList = PTReceipt.substring(StartPos + "<card_out_list>".length(), EndPos);
            //                hmResponse.put("ipay_out__card_out_list", CardOutList);
            //            }


            //            Optional
            //            StartPos = PTReceipt.indexOf("<reason>");
            //            EndPos = PTReceipt.indexOf("</reason>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                FailReason = PTReceipt.substring(StartPos + "<reason>".length(), EndPos);
            //                hmResponse.put("ipay_out__fail_reason", FailReason);
            //            }
            //        }


            //        if (sAction.equals(LIST_CARDS))
            //        {
            //            Action
            //            int StartPos = PTReceipt.indexOf("<wallet_id>");
            //            int EndPos = PTReceipt.indexOf("</wallet_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                WalletID = PTReceipt.substring(StartPos + "<wallet_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__wallet_id", WalletID);
            //            }

            //            StartPos = PTReceipt.indexOf("<card_out_list>");
            //            EndPos = PTReceipt.indexOf("</card_out_list>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                CardOutList = PTReceipt.substring(StartPos + "<card_out_list>".length(), EndPos);
            //                hmResponse.put("ipay_out__card_out_list", CardOutList);
            //            }


            //            Optional
            //            StartPos = PTReceipt.indexOf("<reason>");
            //            EndPos = PTReceipt.indexOf("</reason>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                FailReason = PTReceipt.substring(StartPos + "<reason>".length(), EndPos);
            //                hmResponse.put("ipay_out__fail_reason", FailReason);
            //            }
            //        }


            //        if (sAction.equals(SALE_WALLET_TRANSACTION))
            //        {
            //            Action
            //            int StartPos = PTReceipt.indexOf("<txn_uuid>");
            //            int EndPos = PTReceipt.indexOf("</txn_uuid>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                TxnUuid = PTReceipt.substring(StartPos + "<txn_uuid>".length(), EndPos);
            //                hmResponse.put("ipay_out__txn_uuid", TxnUuid);
            //            }

            //            StartPos = PTReceipt.indexOf("<mer_txn_id>");
            //            EndPos = PTReceipt.indexOf("</mer_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                MerRefID = PTReceipt.substring(StartPos + "<mer_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__mer_ref_id", MerRefID);
            //            }

            //            StartPos = PTReceipt.indexOf("<txn_status>");
            //            EndPos = PTReceipt.indexOf("</txn_status>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                TxnStatus = PTReceipt.substring(StartPos + "<txn_status>".length(), EndPos);
            //                hmResponse.put("ipay_out__txn_status", TxnStatus);
            //            }

            //            StartPos = PTReceipt.indexOf("<server_time>");
            //            EndPos = PTReceipt.indexOf("</server_time>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                ServerTime = PTReceipt.substring(StartPos + "<server_time>".length(), EndPos);
            //                hmResponse.put("ipay_out__server_time", ServerTime);
            //            }

            //            Optional
            //            StartPos = PTReceipt.indexOf("<ipg_txn_id>");
            //            EndPos = PTReceipt.indexOf("</ipg_txn_id>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                IPGTransactionID = PTReceipt.substring(StartPos + "<ipg_txn_id>".length(), EndPos);
            //                hmResponse.put("ipay_out__ipg_txn_id", IPGTransactionID);
            //            }

            //            StartPos = PTReceipt.indexOf("<reason>");
            //            EndPos = PTReceipt.indexOf("</reason>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                FailReason = PTReceipt.substring(StartPos + "<reason>".length(), EndPos);
            //                hmResponse.put("ipay_out__fail_reason", FailReason);
            //            }

            //            StartPos = PTReceipt.indexOf("<auth_code>");
            //            EndPos = PTReceipt.indexOf("</auth_code>");
            //            if ((StartPos > 0) && (EndPos > StartPos))
            //            {
            //                AuthCode = PTReceipt.substring(StartPos + "<auth_code>".length(), EndPos);
            //                hmResponse.put("ipay_out__auth_code", AuthCode);
            //            }
            //        }
            //    }
            //}

            //return hmResponse;
        }

    }
}
