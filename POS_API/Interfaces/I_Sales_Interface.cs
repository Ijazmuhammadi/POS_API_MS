using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Interfaces
{
   public interface I_Sales_Interface
    {
        Task<IEnumerable<Sales>> GetSales();
        Task<IEnumerable<Product>> GetProducts();
        Task<Sales> GetSales(int salesId);
        // Task<Product> GetProductByEmail(string email);
        Task<Sales> AddSales(Sales sales);
        Task<Sales> UpdateSales(Sales sales);
        Task DeleteSales(int salesId);
    }
}
