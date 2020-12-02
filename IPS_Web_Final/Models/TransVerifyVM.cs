using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Web_Final.Models
{
    public class TransVerifyVM
    {
        public string Action { get; set; }
        public string MerRefID { get; set; }
        public string TxnUUID { get; set; }
    }
}