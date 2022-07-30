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
    public class SalesController : ControllerBase
    {
        private readonly I_Sales_Interface i_Sales;

        public SalesController(I_Sales_Interface i_Sales)
        {
            this.i_Sales = i_Sales;
        }
        [HttpGet]
        public async Task<ActionResult> GetSales()
        {
            try
            {
                return Ok(await i_Sales.GetSales());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                return Ok(await i_Sales.GetProducts());
            }
            catch (Exception)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Sales>> GetSale(int id)
        {
            try
            {
                var result = await i_Sales.GetSales(id);
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
        public async Task<ActionResult<Sales>> CreateSales(Sales sales)
        {
            try
            {
                if (sales == null)
                    return BadRequest();


                var saleToCreate = await i_Sales.AddSales(sales);

                return CreatedAtAction(nameof(GetSales),
                    new { id = saleToCreate.SalesID }, saleToCreate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Product record");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Sales>> UpdateSale(int id, Sales sales)
        {
            try
            {
                if (id != sales.SalesID)
                    return BadRequest("Sale ID mismatch");

                var saleToUpdate = await i_Sales.GetSales(id);

                if (saleToUpdate == null)
                {
                    return NotFound($"Sale with Id = {id} not found");
                }

                return await i_Sales.UpdateSales(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Product record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSale(int id)
        {
            try
            {


                var SaleDelete = await i_Sales.GetSales(id);

                if (SaleDelete == null)
                {
                    return NotFound($"Sales with Id = {id} not found");
                }

                await i_Sales.DeleteSales(id);
                return Ok($"Sales with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting Product record");
            }
        }
    }
}
