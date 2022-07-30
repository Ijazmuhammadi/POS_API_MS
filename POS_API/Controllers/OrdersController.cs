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
    public class OrdersController : ControllerBase
    {
        private readonly IOrder_Interface order;

        public OrdersController(IOrder_Interface order)
        {
            this.order = order;
        }
        [HttpGet]
        public async Task<ActionResult> GetOrder()
        {
            try
            {
                return Ok(await order.GetOrders());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetOrderRevenue()
        {
            try
            {
                return Ok(await order.GetOrdersRevenue());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetCustomerNameId()
        {
            try
            {
                return Ok(await order.GetCustomers());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetOrderNameId()
        {
            try
            {
                return Ok(await order.GetProducts());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var result = await order.GetOrder(id);
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetOrderUnitPrice(int id)
        {
            try
            {
                var result = await order.GetOrderUnitPrice(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order Order)
        {
            try
            {
                if (Order == null)
                    return BadRequest();

                //var emp = await order.GetOrderByEmail(Order.Email);

                //if (emp != null)
                //{
                //    ModelState.AddModelError("Email", "Order email already in use");
                //    return BadRequest(ModelState);
                //}

                var createdOrder = await order.AddOrder(Order);

                return CreatedAtAction(nameof(GetOrder),
                    new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Order record");
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order Order)
        {
            try
            {
                if (id != Order.OrderId)
                    return BadRequest("Order ID mismatch");

                var OrderToUpdate = await order.GetOrder(id);

                if (OrderToUpdate == null)
                {
                    return NotFound($"Order with Id = {id} not found");
                }

                return await order.UpdateOrder(Order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating Order record");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {


                var OrderDelete = await order.GetOrder(id);

                if (OrderDelete == null)
                {
                    return NotFound($"Order with Id = {id} not found");
                }

                await order.DeleteOrder(id);
                return Ok($"Order with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Deleting Order record");
            }
        }
    }
}
