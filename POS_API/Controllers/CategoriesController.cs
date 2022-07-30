using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_API.Interfaces;
using POS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategory_Interface _category;

        public CategoriesController(ICategory_Interface category)
        {
            this._category = category;
        }
        [HttpGet]
        public async Task<ActionResult> GetCategory()
        {
            try
            {
                return Ok(await _category.GetCategorys());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            try
            {
                var result = await _category.GetCategory(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }

            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                    return BadRequest();

                var createdCategory = await _category.AddCategory(category);

                return CreatedAtAction(nameof(GetCategory),
                    new { id = createdCategory.CategoryID }, createdCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Category record");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            try
            {
                if (id != category.CategoryID)
                    return BadRequest("Category ID mismatch");

                var CategoryToUpdate = await _category.GetCategory(id);

                if (CategoryToUpdate == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }

                return await _category.UpdateCategory(category);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Category record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            try
            {

                var CategoryDelete = await _category.GetCategory(id);

                if (CategoryDelete == null)
                {
                    return NotFound($"Category with Id = {id} not found");
                }

                await _category.DeleteCategory(id);
                return Ok($"Category with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting Category record");
            }
        }
    }
}
