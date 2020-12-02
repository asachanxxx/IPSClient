using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Web.ViewModels
{
    public class PostBackVM
    {
		public string BankRefID { get; set; }
		public string CurrencyCode { get; set; }
		public string IPGTransactionID { get; set; }
		public string MerRefID { get; set; }
		public string TxnAmount { get; set; }
		public string TxnStatus { get; set; }
		public string ServerTime { get; set; }
		public string MaskedAccNo { get; set; }
		public string LanguageCode { get; set; }
		public string MerVar1 { get; set; }
		public string MerVar2 { get; set; }
		public string MerVar3 { get; set; }
		public string MerVar4 { get; set; }
		public string CustomerName { get; set; }
		public string FailReason { get; set; }
		public string AuthCode { get; set; }

	}
}