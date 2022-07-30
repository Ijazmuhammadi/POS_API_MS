using Microsoft.EntityFrameworkCore;
using POS_API.Interfaces;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Repository
{
    public class Sales_Repo : I_Sales_Interface
    {
        private readonly AppDbContext appDb;

        public Sales_Repo(AppDbContext appDb)
        {
            this.appDb = appDb;
        }

        public async Task<Sales> AddSales(Sales sales)
        {
            var addsale = await appDb.sales.AddAsync(sales);
            await appDb.SaveChangesAsync();
            return addsale.Entity;
        }

        public async Task DeleteSales(int saleId)
        {
            var result = await appDb.sales
        .FirstOrDefaultAsync(e => e.SalesID == saleId);

            if (result != null)
            {
                appDb.sales.Remove(result);
                await appDb.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var pro = appDb.products.Select(x => new Product
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName
            }).ToListAsync();
            return await pro;
        }

        public async Task<Sales> GetSales(int salesId)
        {
            return await appDb.sales.FirstOrDefaultAsync(e => e.SalesID == salesId);
        }

        public async Task<IEnumerable<Sales>> GetSales()
        {
            var prodlist = (from x in appDb.sales
                            join a in appDb.products
                            on x.ProductID equals a.ProductID

                            select new Sales
                            {
                                SalesID = x.SalesID,
                                Unit_Price = x.Unit_Price,
                                Quantity = x.Quantity,
                                SubTotal=x.SubTotal,
                                SalesDate = x.SalesDate,
                                ProductName = a.ProductName,
                                
                            }).ToListAsync();
            return await prodlist;
        }

        public async Task<Sales> UpdateSales(Sales sales)
        {
            var result = await appDb.sales
                 .FirstOrDefaultAsync(e => e.SalesID == sales.SalesID);

            if (result != null)
            { 
                result.Quantity = sales.Quantity;
                result.Unit_Price = sales.Unit_Price;
                result.SubTotal = sales.SubTotal;
                result.ProductID = sales.ProductID;

                await appDb.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
