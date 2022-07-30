using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Interfaces
{
   public interface ICategory_Interface
    {

        Task<IEnumerable<Category>> GetCategorys();
        Task<Category> GetCategory(int categoryId);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
