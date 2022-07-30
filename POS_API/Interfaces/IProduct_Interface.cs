using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Interfaces
{
   public interface IProduct_Interface
    {

        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Category>> GetCategory();
        Task<Product> GetProduct(int productId);
        Task<Product> GetProductQuantity(int productId);
        // Task<Product> GetProductByEmail(string email);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> PartialUpdateProduct(int ProductID,int Quantity);
        Task DeleteProduct(int productId);
    }
}
