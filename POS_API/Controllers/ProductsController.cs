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
    public class ProductsController : ControllerBase
    {
        private readonly IProduct_Interface _product;

        public ProductsController(IProduct_Interface product) 
        {
            _product = product;
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                return Ok(await _product.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetCategoryNameId()
        {
            try
            {
                return Ok(await _product.GetCategory());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var result = await _product.GetProduct(id);
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
        //get product quanttiiy
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProductQuantity(int id)
        {
            try
            {
                var result = await _product.GetProductQuantity(id);
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
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

          
                var createdProduct = await _product.AddProduct(product);

                return CreatedAtAction(nameof(GetProduct),
                    new { id = createdProduct.ProductID }, createdProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Product record");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            try
            {
                if (id != product.ProductID)
                    return BadRequest("Product ID mismatch");

                var ProductToUpdate = await _product.GetProduct(id);

                if (ProductToUpdate == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                return await _product.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Product record");
            }
        }
        [HttpPut("{ProductID:int}")]
        public async Task<ActionResult<Product>> PartialUpdateProduct(int ProductID, int Quantity)
        {
            try
            {
                //if (ProductID !=ProductID)
                //    return BadRequest("Product ID mismatch");

                var ProductToUpdate = await _product.GetProduct(ProductID);

                if (ProductToUpdate == null)
                {
                    return NotFound($"Product with Id = {ProductID} not found");
                }

                return await _product.PartialUpdateProduct(ProductID,Quantity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Product record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
            {
            try
            {


                var ProductDelete = await _product.GetProduct(id);

                if (ProductDelete == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }

                await _product.DeleteProduct(id);
                return Ok($"Product with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting Product record");
            }
        }
    }
}
