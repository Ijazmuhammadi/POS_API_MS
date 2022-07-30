using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS_API.Interfaces;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Repository
{
    public class Order_Repo : IOrder_Interface
    {
        private readonly AppDbContext appDb;

        public Order_Repo(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<Order> AddOrder(Order order)
        {

            var result = await appDb.orders.AddAsync(order);
            await appDb.SaveChangesAsync();
            return result.Entity;
        }
     
        public async Task DeleteOrder(int orderId)
        {
            var result = await appDb.orders
   .FirstOrDefaultAsync(e => e.OrderId == orderId);

            if (result != null)
            {
                appDb.orders.Remove(result);
                await appDb.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var cust = appDb.customers.Select(x => new Customer
            {
                CustomerId = x.CustomerId,
                FullName = x.FullName
            }).ToListAsync();
            return await cust;
        }

        public async Task<Order> GetOrder(int OrderId)
        {
            return await appDb.orders
               .FirstOrDefaultAsync(e => e.OrderId == OrderId);
        }

        //public async Task<Order> GetOrderByEmail(string email)
        //{
        //    return await ;
        //    //return await appDb.orders
        //    //    .FirstOrDefaultAsync(e => e. == email);
        //}
      
        public async Task<IEnumerable<Order>> GetOrders()
        {
           
            var orderlist = ( from x in appDb.orders
                             join a in appDb.products
                             on x.ProductID equals a.ProductID
                             join c in appDb.customers
                             on x.CustomerId equals c.CustomerId
                select new Order
            {
                OrderId = x.OrderId,
                Order_Name = x.Order_Name,
                Quantity = x.Quantity,
                Unit_Price = x.Unit_Price,
                Sub_Total = x.Sub_Total,
                Order_Date = x.Order_Date,
               ProductName = a.ProductName,
               FullName=c.FullName,
                Payement = x.Payement,
                }).ToListAsync();
            return await orderlist;
        }

        public async Task<IEnumerable<Order>> GetOrdersRevenue()
        {

            var orderlistrev = (from x in appDb.orders
                             join a in appDb.products
                             on x.ProductID equals a.ProductID
                             join c in appDb.customers
                             on x.CustomerId equals c.CustomerId
                             select new Order
                             {
                                 OrderId = x.OrderId,
                                 Order_Name = x.Order_Name,
                                 Quantity = x.Quantity,
                                 Unit_Price = x.Unit_Price,
                                 Sub_Total = x.Sub_Total,
                                 Order_Date = x.Order_Date,
                                 ProductName = a.ProductName,
                                 Total_Price_Product = int.Parse(a.Unit_Price) * a.Quantity,
                                 Revenue = x.Total_Price_Product - x.Sub_Total,
                                 FullName = c.FullName,
                                 
                             }).ToListAsync();
            return await orderlistrev;
        }

        public async Task<Product> GetOrderUnitPrice(int productID)
        {
            var cust = appDb.products.Where(x=>x.ProductID== productID).FirstOrDefaultAsync();
            return await cust;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var prod = appDb.products.Select(x => new Product
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName
            }).ToListAsync();
            return await prod;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            var result = await appDb.orders
                  .FirstOrDefaultAsync(e => e.OrderId == order.OrderId);

            if (result != null)
            {
                result.Order_Name = order.Order_Name;
                result.Quantity = order.Quantity;
                result.Unit_Price = order.Unit_Price;
                result.Sub_Total = order.Sub_Total;
                result.Order_Date = order.Order_Date;
                result.ProductID = order.ProductID;
                result.Payement = order.Payement;
                //result.ProductName = order.ProductName;
                await appDb.SaveChangesAsync();

                return result;
            }

            return null;
        }

       
    }
}
