using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Order_Name { get; set; }
        public string Quantity { get; set; }
        public string Unit_Price { get; set; }
        public int Sub_Total { get; set; }
        public DateTime Order_Date { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        [NotMapped]
        public int Total_Price_Product { get; set; }
        [NotMapped]
        public int Revenue { get; set; }
        public int ProductID { get; set; }
        [NotMapped]
        public string FullName { get; set; }
        public int CustomerId { get; set; }
        public string Payement { get; set; }
    }
}
