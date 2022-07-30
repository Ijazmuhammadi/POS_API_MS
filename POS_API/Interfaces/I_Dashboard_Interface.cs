using Microsoft.AspNetCore.Mvc;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Interfaces
{
   public interface I_Dashboard_Interface
    {
        public Task<int> GetCustomersCount();
        public Task<int> GetOrdersCount();
        public Task<int> GetSalesCount();
        public int GetProductCount();
        public Task<IEnumerable<Sales>> GetSalesGraph();
    }
}
