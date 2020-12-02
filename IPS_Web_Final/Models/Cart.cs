using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Web_Final.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public decimal NetTotal { get; set; }
        public List<CartDetails> Items { get; set; }

    }

    public class CartDetails
    {
        public int Id { get; set; }
        public string Pro_Name { get; set; }
        public string image { get; set; }
        public decimal Cprice { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }

    }
}