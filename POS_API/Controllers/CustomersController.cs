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
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomer_Interface customer_Interface;

        public CustomersController(ICustomer_Interface customer_Interface)
        {
            this.customer_Interface = customer_Interface;
        }
        [HttpGet]
        public async Task<ActionResult> GetCustomer()
        {
            try
            {
                return Ok(await customer_Interface.GetCustomers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var result = await customer_Interface.GetCustomer(id);
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
        public async Task<ActionResult<Customer>> CreateCustomer(Customer Customer)
        {
            try
            {
                if (Customer == null)
                    return BadRequest();

                var emp = await customer_Interface.GetCustomerByEmail(Customer.Email);

                if (emp != null)
                {
                    ModelState.AddModelError("Email", "Customer email already in use");
                    return BadRequest(ModelState);
                }

                var createdCustomer = await customer_Interface.AddCustomer(Customer);

                return CreatedAtAction(nameof(GetCustomer),
                    new { id = createdCustomer.CustomerId }, createdCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Customer record");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
        {
            try
            {
                if (id != customer.CustomerId)
                    return BadRequest("Customer ID mismatch");

                var CustomerToUpdate = await customer_Interface.GetCustomer(id);

                if (CustomerToUpdate == null)
                {
                    return NotFound($"Customer with Id = {id} not found");
                }

                return await customer_Interface.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Customer record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {


                var CustomerDelete = await customer_Interface.GetCustomer(id);

                if (CustomerDelete == null)
                {
                    return NotFound($"Customer with Id = {id} not found");
                }

                await customer_Interface.DeleteCustomer(id);
                return Ok($"Customer with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting Customer record");
            }
        }

    }
}
