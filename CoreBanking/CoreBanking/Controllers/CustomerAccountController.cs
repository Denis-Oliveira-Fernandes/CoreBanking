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
        private ILogger<CustomerAccountController> _log;

        public CustomerAccountController(ICustomerAccountService customerAccountService, IConfiguration configuration, ILogger<CustomerAccountController> log)
        {
            _customerAccountService = customerAccountService;
            _configuration = configuration;
            _log = log;
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
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateCustomerAsync - Start");

                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var customer = await _customerAccountService.CreateCustomerAsync(createCustomer);
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateCustomerAsync - End");

                    if (customer is not null)
                        return Ok(customer);
                    else
                        return StatusCode(500);
                }
                else
                {
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateCustomerAsync - End - Invalid Token");
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateCustomerAsync - End - Exception: {ex.Message}");
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
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAccountAsync - Start");

                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var account = await _customerAccountService.CreateAccountAsync(createAccount);
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAccountAsync - End");

                    if (account is not null)
                        return Ok(account);
                    else
                        return StatusCode(500);
                }
                else
                {
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAccountAsync - End - Invalid Token");
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAccountAsync - End - Exception: {ex.Message}");
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
                _log.LogInformation($"Controller: CustomerAccountController - Method: GetCustomerBalanceAsync - Start");

                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))// Validate Authorization token bearer
                {
                    var customerBalance = await _customerAccountService.GetCustomerBalanceAsync(accountId); //Call Service
                    _log.LogInformation($"Controller: CustomerAccountController - Method: GetCustomerBalanceAsync - End");

                    if (customerBalance is not null)
                        return Ok(customerBalance);
                    else
                        return StatusCode(404, $"Not found account with accountId = {accountId}");
                }
                else
                {
                    _log.LogInformation($"Controller: CustomerAccountController - Method: GetCustomerBalanceAsync - End - Invalid Token");
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Controller: CustomerAccountController - Method: GetCustomerBalanceAsync - End - Exception: {ex.Message}");
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
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateFinancialTransactionAsync - Start");

                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var financialTransaction = await _customerAccountService.CreateFinancialTransactionAsync(createFinancialTransaction);
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateFinancialTransactionAsync - End");

                    if (financialTransaction is not null)
                        return Ok(financialTransaction);
                    else
                        return StatusCode(500);
                }
                else
                {
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateFinancialTransactionAsync - End - Invalid Token");
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (ApplicationException ex)
            {
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateFinancialTransactionAsync - End - Exception: {ex.Message}");
                return StatusCode(422, ex.Message);
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateFinancialTransactionAsync - End - Exception: {ex.Message}");
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
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAssetAsync - Start");

                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var asset = await _customerAccountService.CreateAssetAsync(createAsset);
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAssetAsync - End");

                    if (asset is not null)
                        return Ok(asset);
                    else
                        return StatusCode(500);
                }
                else
                {
                    _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAssetAsync - End - Invalid Token");
                    return StatusCode(401, "Invalid token.");
                }
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Controller: CustomerAccountController - Method: CreateAssetAsync - End - Exception: {ex.Message}");
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
                _log.LogInformation($"Controller: CustomerAccountController - Method: GetFinancialTransactionsAsync - Start");

                var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
                var apiKey = _configuration.GetSection("apiKey").Value;               

                if (!string.IsNullOrEmpty(authorizationHeader) && _customerAccountService.VerifyAuthorization(authorizationHeader, apiKey))
                {
                    var financialTransactions = await _customerAccountService.GetFinancialTransactionsAsync(accountId);
                    _log.LogInformation($"Controller: CustomerAccountController - Method: GetFinancialTransactionsAsync - End");

                    if (financialTransactions is not null)
                        return Ok(financialTransactions);
                    else
                        return StatusCode(404, "Não foi encontrada conta com essa accountId");
                } else
                {
                    _log.LogInformation($"Controller: CustomerAccountController - Method: GetFinancialTransactionsAsync - End - Invalid Token");
                    return StatusCode(401, "Invalid token.");
                }
                
            }
            catch (Exception ex)
            {
                _log.LogInformation($"Controller: CustomerAccountController - Method: GetFinancialTransactionsAsync - End - Exception: {ex.Message}");
                throw;
            }
        }
    }
}
