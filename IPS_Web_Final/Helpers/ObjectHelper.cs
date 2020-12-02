using IPS_Web_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPS_Web_Final.Helpers
{
    public class ObjectHelper
    {
        Dictionary<string, string> imageList = new Dictionary<string, string>();

        public ObjectHelper()
        {
            imageList.Add("img0", "product-2.jpg");
            imageList.Add("img1", "product-6.jpg");
            imageList.Add("img2", "product-7.jpg");
            imageList.Add("img3", "product-8.jpg");
        }

      
        Random random = new Random();
        public Cart GetFilledCard() {
          

            Cart obj = new Cart();
            obj.Id = 1;
            obj.SubTotal = 0;
            obj.NetTotal = 0;
            obj.Items = new List<CartDetails>();

            for (int i = 0; i < 4; i++)
            {
                CartDetails details = new CartDetails();
                details.Id = i;
                details.image = imageList["img" + i.ToString()];
                details.Pro_Name = "Product " + i.ToString();
                details.Cprice = random.Next(10,100);
                details.Qty = random.Next(1, 10);
                details.Total = details.Cprice * details.Qty;
                obj.SubTotal += details.Total;
                obj.Items.Add(details);
            }
            obj.NetTotal = obj.SubTotal;

            return obj;
        }

    }
}