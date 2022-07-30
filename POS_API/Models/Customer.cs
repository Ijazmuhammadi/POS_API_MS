using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Name must contains at least 2 charcters")]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime Customer_Date { get; set; }


    }
}
