using POS_API.Interfaces;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace POS_API.Repository
{
    public class Dashboard_Repo : I_Dashboard_Interface
    {
        private readonly AppDbContext appDb;

        public Dashboard_Repo(AppDbContext appDb)
        {
            this.appDb = appDb;
        }


        public async Task<int> GetCustomersCount()
        {
            return await appDb.customers.CountAsync();
        }

        public async Task<int> GetOrdersCount()
        {
            return await appDb.orders.CountAsync();
        }

        public int GetProductCount()
        {
            return appDb.products.Sum(p => p.Quantity);
        }

        public async Task<int> GetSalesCount()
        {
            return await appDb.sales.CountAsync();
        }

        public async Task<IEnumerable<Sales>> GetSalesGraph()
        {
            var graphdata = appDb.sales.FromSqlRaw("Select salesid,productid,quantity,Unit_Price, FORMAT(CAST(SalesDate as datetime2), 'MMMM') as SalesDate, sum(CAST(SubTotal as int)) as SubTotal from sales group by SalesDate,SalesID,ProductID,Quantity,Unit_Price;").ToListAsync();
            return await graphdata;
        }
    }
}
