using CMS.Data.Models.DTOs;
using CMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{
    /// <summary>
    /// Controller responsilbe for the Customer's CRUD Operations
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowOrigins")]
    public class CustomerController : ApiControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customers;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customers)
        {
            _logger = logger;
            _customers = customers;
        }

        /// <summary>
        /// Retrieves all existing Customers in the database
        /// </summary>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Customers Collection</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken token)
        {
            try
            {
                var customers = await _customers.GetAllCustomersAsync(token);

                if (customers == null)
                {
                    _logger.LogWarning($"Anyone customer has not been found in data");
                    return NotFound();
                }
                return Ok(customers);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in time of customers retreiving {ex.Message}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves Customers Requested by Id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Requested Customer</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken token)
        {
            try
            {
                var customer = await _customers.GetCustomerByIdAsync(id, token);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in time of customer with ID = {id} retreiving { ex.Message }", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates customer
        /// </summary>
        /// <param name="customer">Customer Entity</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Created Customer Id</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerDto customer, CancellationToken token)
        {
            try
            {
                customer.UserCreated = string.IsNullOrEmpty(customer.UserCreated) ?  CurrentUserName : customer.UserCreated;
                var isSuccess = await _customers.CreateCustomerAsync(customer, token);
                if (isSuccess > 0)
                {
                    _logger.LogInformation($"New Customer ID = {isSuccess} Has been created.");
                    return Ok($"New Customer ID = { isSuccess } Has been created.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error { ex.Message }", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates customer
        /// </summary>
        /// <param name="customer">Customer Entity</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Updated Customer Id</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomerDto customer, CancellationToken token)
        {
            try
            {
                customer.UserModified = string.IsNullOrEmpty(customer.UserModified) ? CurrentUserName : customer.UserModified;
                var isSuccess = await _customers.UpdateCustomerAsync(customer, token);
                if (isSuccess > 0)
                {
                    _logger.LogInformation($"Customer Id = {isSuccess} Has been updated.");
                    return Ok($"Customer Id = {isSuccess} Has been updated.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error { ex.Message }", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes customer
        /// </summary>
        /// <param name="customer">Customer Entity</param>
        /// <param name="token">Cancellation Token</param>
        /// <returns>Removed Customer Id</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] CustomerDto customer, CancellationToken token)
        {
            try
            {
                var isSuccess = await _customers.DeleteCustomerAsync(customer, token);
                if (isSuccess > 0)
                {
                    _logger.LogInformation($"Customer Id = {isSuccess} Has been deleted.");
                    return Ok($"Customer Id = {isSuccess} Has been deleted.");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error { ex.Message }", ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}