using Microsoft.EntityFrameworkCore;
using POS_API.Interfaces;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Repository
{
    public class Category_Repo: ICategory_Interface
    {
        private readonly AppDbContext appDb;

        public Category_Repo(AppDbContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<Category> AddCategory(Category category)
        {

            var result = await appDb.categories.AddAsync(category);
            await appDb.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var result = await appDb.categories
            .FirstOrDefaultAsync(e => e.CategoryID == categoryId);

            if (result != null)
            {
                appDb.categories.Remove(result);
                await appDb.SaveChangesAsync();
            }
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await appDb.categories
               .FirstOrDefaultAsync(e => e.CategoryID == categoryId);
        }

        public async Task<IEnumerable<Category>> GetCategorys()
        {
            return await appDb.categories.ToListAsync();
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var result = await appDb.categories
                  .FirstOrDefaultAsync(e => e.CategoryID == category.CategoryID);

            if (result != null)
            {
                result.CategoryName = category.CategoryName;
                
                await appDb.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
