using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Interfaces
{
    public interface IOrder_Interface
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrdersRevenue();
        Task<IEnumerable<Customer>> GetCustomers();
        Task<IEnumerable<Product>> GetProducts();
        Task<Order> GetOrder(int OrderId);
        Task <Product> GetOrderUnitPrice(int productID);
        // Task<Order> GetOrderByEmail(string email);
        Task<Order> AddOrder(Order Order);
        Task<Order> UpdateOrder(Order Order);
        Task DeleteOrder(int OrderId);
    
}
}
