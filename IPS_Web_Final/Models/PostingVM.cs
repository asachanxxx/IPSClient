using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Web_Final.Models
{
    public class PostingVM
    {
		public string TxnAmount { get; set; }
		public string Action { get; set; }
		public string CurrencyCode { get; set; }
		public string LanguageCode { get; set; }
		public string MerRefID { get; set; }
		public string ItemList { get; set; }
		public string ReturnURL { get; set; }
		public string MerVar1 { get; set; }
		public string MerVar2 { get; set; }
		public string MerVar3 { get; set; }
		public string MerVar4 { get; set; }
		
	}
}