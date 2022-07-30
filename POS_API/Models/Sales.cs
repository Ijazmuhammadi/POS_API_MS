using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Models
{
    public class Sales
    {
        public int SalesID { get; set; }
        public int ProductID { get; set; }
        [NotMapped]
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Unit_Price { get; set; }
        public int SubTotal { get; set; }
        public string SalesDate { get; set; }
        public string FullName { get; set; }
    }
}
