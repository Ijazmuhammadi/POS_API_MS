using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly I_Dashboard_Interface i_Dashboard;

        public DashboardsController(I_Dashboard_Interface i_Dashboard)
        {
            this.i_Dashboard = i_Dashboard;
        }
        [HttpGet]
        public async Task<ActionResult> GetCustomerCount()
        {
            try
            {
                return Ok(await i_Dashboard.GetCustomersCount());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetOrderCount()
        {
            try
            {
                return Ok(await i_Dashboard.GetOrdersCount());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public  ActionResult GetProductCount()
        {
            try
            {
                return Ok(i_Dashboard.GetProductCount());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetSalesCount()
        {
            try
            {
                return Ok(await i_Dashboard.GetSalesCount());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetSalesGraph()
        {
            try
            {
                return Ok(await i_Dashboard.GetSalesGraph());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
    

