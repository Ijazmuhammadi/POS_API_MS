using Microsoft.EntityFrameworkCore;
using POS_API.Interfaces;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Repository
{
    public class Product_Repo: IProduct_Interface
    {
        private readonly AppDbContext appDb;

        public Product_Repo(AppDbContext appDb)
        {
            this.appDb = appDb;
        }

        public async Task<Product> AddProduct(Product product)
        {
            var addprod = await appDb.products.AddAsync(product);
            await appDb.SaveChangesAsync();
            return addprod.Entity;
        }

        public async Task DeleteProduct(int productId)
        {
            var result = await appDb.products
.FirstOrDefaultAsync(e => e.ProductID== productId);

            if (result != null)
            {
                appDb.products.Remove(result);
                await appDb.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Category>> GetCategory()
        {
            var catg = appDb.categories.Select(x => new Category
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName
            }).ToListAsync();
            return await catg;
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await appDb.products.FirstOrDefaultAsync(e => e.ProductID == productId);
        }

        public async Task<Product> GetProductQuantity(int productId)
        {
            var prodquantity = appDb.products.Select(x => new Product
            {
                ProductID = x.ProductID,
                Quantity = x.Quantity
            }).FirstOrDefaultAsync();
            return await prodquantity;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var prodlist = (from x in appDb.products
                             join a in appDb.categories
                             on x.CategoryID equals a.CategoryID
                             
                             select new Product
                             {
                                 ProductID = x.ProductID,
                                 ProductName = x.ProductName,
                                 Unit_Price = x.Unit_Price,
                                 Quantity = x.Quantity,
                                 CategoryName = a.CategoryName,
                                 
                             }).ToListAsync();
            return await prodlist;
        }

        public async Task<Product> PartialUpdateProduct(int ProductID, int Quantity)
        {
            {
                var result = await appDb.products
                     .FirstOrDefaultAsync(e => e.ProductID == ProductID);

                if (result != null)
                {
                    //result.ProductName = product.ProductName;
                    result.Quantity = Quantity;
                    //result.Unit_Price = product.Unit_Price;
                  //  result.CategoryID = product.CategoryID;

                    await appDb.SaveChangesAsync();

                    return result;
                }

                return null;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await appDb.products
                 .FirstOrDefaultAsync(e => e.ProductID == product.ProductID);

            if (result != null)
            {
                result.ProductName = product.ProductName;
                result.Quantity = product.Quantity;
                result.Unit_Price = product.Unit_Price;
                result.CategoryID = product.CategoryID;

                await appDb.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
