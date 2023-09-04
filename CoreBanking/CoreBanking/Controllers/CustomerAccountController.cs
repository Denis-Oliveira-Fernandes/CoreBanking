using CoreBanking.Domain.Models;
using CoreBanking.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoreBanking.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CustomerAccountController : ControllerBase
    {
        private ICustomerAccountService _customerAccountService;
        private IConfiguration _configuration;

        public CustomerAccountController(ICustomerAccountService customerAccountService, IConfiguration configuration)
        {
            _customerAccountService = customerAccountService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("createCustomer")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateCustomerAsync(
                [FromBody] CreateCustomer createCustomer
            )
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var customer = await _customerAccountService.CreateCustomerAsync(createCustomer);
                    if (customer is not null)
                        return Ok(customer);
                    else
                        return StatusCode(500);
                }
                else
                {
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createAccount")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateAccountAsync(
                [FromBody] CreateAccount createAccount
            )
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var account = await _customerAccountService.CreateAccountAsync(createAccount);
                    if (account is not null)
                        return Ok(account);
                    else
                        return StatusCode(500);
                }
                else
                {
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getCustomerBalance")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCustomerBalanceAsync(
                [FromQuery] int accountId
            )
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var customerBalance = await _customerAccountService.GetCustomerBalanceAsync(accountId);
                    if (customerBalance is not null)
                        return Ok(customerBalance);
                    else
                        return StatusCode(404, $"Not found account with accountId = {accountId}");
                }
                else
                {
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createFinancialTransaction")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateFinancialTransactionAsync(
                [FromBody] CreateFinancialTransaction createFinancialTransaction
            )
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var financialTransaction = await _customerAccountService.CreateFinancialTransactionAsync(createFinancialTransaction);
                    if (financialTransaction is not null)
                        return Ok(financialTransaction);
                    else
                        return StatusCode(500);
                }
                else
                {
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("createAsset")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateAssetAsync(
                [FromBody] CreateAsset createAsset
            )
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var asset = await _customerAccountService.CreateAssetAsync(createAsset);
                    if (asset is not null)
                        return Ok(asset);
                    else
                        return StatusCode(500);
                }
                else
                {
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getFinancialTransactions")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFinancialTransactionsAsync(
                [FromQuery] int accountId
            )
        {
            try
            {
                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;               

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var financialTransactions = await _customerAccountService.GetFinancialTransactionsAsync(accountId);
                    if (financialTransactions is not null)
                        return Ok(financialTransactions);
                    else
                        return StatusCode(404, "Não foi encontrada conta com essa accountId");
                } else
                {
                    return StatusCode(401, "Invalid token.");
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
