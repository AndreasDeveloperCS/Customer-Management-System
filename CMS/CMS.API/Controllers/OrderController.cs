using CMS.Data.Models.DTOs;
using CMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{
    /// <summary>
    /// Controller responsible for the Orders' Operations
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ApiControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly ICustomerService _customers;
        private readonly IOrderService _orders;

        public OrderController(ILogger<OrderController> logger, ICustomerService customers, IOrderService orders)
        {
            _logger = logger;
            _customers = customers;
            _orders = orders;
        }

        /// <summary>
        /// Retreives All Orders Collection storing in the database
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>Orders Collection retrieved from DataBase</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            try
            {
                var order = await _orders.GetAllOrdersAsync(token);
                if (order == null)
                {
                    _logger.LogWarning($"Any orders are absent in current database");
                    return NotFound();
                }
                _logger.LogInformation($"Orders have been successfully retreived");
                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of orders retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retreives Orders Collection by passed Date Range
        /// </summary>
        /// <param name="start">Start date of interval</param>
        /// <param name="end">End date of interval</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Orders Collection retrieved by Date Range</returns>
        [HttpGet]
        [Route(@"daterange/{start}/{end}")]
        public async Task<IActionResult> GetOrdersByDateRange
        (
            [FromQuery] DateTime start,
            [FromQuery] DateTime end, 
            CancellationToken token
        )
        {
            try
            {
                var orders = await _orders.GetOrdersByDateRangeAsync(start, end, token);
                if (orders == null)
                {
                    _logger.LogWarning($"Requested orders in date range from {start} to {end} are absent in the database");
                    return NotFound();
                }
                _logger.LogInformation($"Orders have been successfully retreived");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of orders retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retreives Orders Collection by passed Customer Id and Date Range
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="start">Start date of interval</param>
        /// <param name="end">End date of interval</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Orders Collection retrieved by Customer Id and Date Range</returns>
        [HttpGet]
        [Route(@"customer/{id}/daterange/{start}/{end}")]
        public async Task<IActionResult> GetOrdersByCustomerDateRangeAsync
        (
            int customerId, 
            [FromQuery] DateTime start,
            [FromQuery] DateTime end,
            CancellationToken token
        )
        {
            try
            {
                var customer = await _customers.GetCustomerByIdAsync(customerId, token);
                var orders = await _orders.GetOrdersByCustomerDateRangeAsync(customer, start, end, token);
                if (orders == null)
                {
                    _logger.LogWarning($"Requested orders with customer ID = {customerId} and in date range from {start} to {end} are absent in the database");
                    return NotFound();
                }
                _logger.LogInformation($"Orders have been successfully retreived");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of orders retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrives Order By Id 
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Order retrieved by Id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token)
        {
            try
            {
                var orders = await _orders.GetOrderByIdAsync(id, token);
                if (orders == null)
                {
                    _logger.LogWarning($"Order with id = {id} is absent in current database");
                    return NotFound();
                }
                _logger.LogInformation($"Order with id = {id} has been successfully retreived");
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of orders retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates new Order in the database
        /// </summary>
        /// <param name="order">Order Entoty</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Created Order Id</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OrderDto order, CancellationToken token)
        {
            try
            {
                var isSuccess = await _orders.CreateOrderAsync(order, token);
                if (isSuccess > 0)
                {
                    return Ok($"New Order Id={isSuccess} Has been created ");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of orders retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates new Order in the database
        /// </summary>
        /// <param name="order">Order Entoty</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Created Order Id</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] OrderDto order, CancellationToken token)
        {
            try
            {
                var isSuccess = await _orders.UpdateOrderAsync(order, token);
                if (isSuccess > 0)
                {
                    return Ok($"New Order Id={isSuccess} Has been updated");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of orders retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes new Order in the database
        /// </summary>
        /// <param name="order">Order Entoty</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Deletes Order Id</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] OrderDto order, CancellationToken token)
        {
            try
            {
                var isSuccess = await _orders.DeleteOrderAsync(order, token);
                if (isSuccess > 0)
                {
                    return Ok($"New Order Id={isSuccess} Has been updated");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of orders retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}