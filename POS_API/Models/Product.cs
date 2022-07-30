using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Models
{
    public class Product
    {
        public int ProductID  { get; set; }
        public string ProductName { get; set; }
        public string Unit_Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public DateTime Product_Date { get; set; }


    }
}
