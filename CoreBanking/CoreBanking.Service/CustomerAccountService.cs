using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Models;
using CoreBanking.Domain.Models.DTOs;
using CoreBanking.Repository.Interfaces;
using CoreBanking.Service.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace CoreBanking.Service
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private ICustomersRepository _customersRepository;
        private ILogger<CustomerAccountService> _log;

        public CustomerAccountService()
        {
        }
        public CustomerAccountService(ICustomersRepository customersRepository, ILogger<CustomerAccountService> log)
        {
            _customersRepository = customersRepository;
            _log = log;
        }
        public async Task<CustomerBalanceResponseDTO> GetCustomerBalanceAsync(int accountId)
        {
            BankAccounts bankAccount = _customersRepository.GetBalance(accountId);//Get balance from database
            if (bankAccount is null)//Validation - If nothing is found
                return null;

            CustomerBalanceResponseDTO customerBalanceResponseDTO = new CustomerBalanceResponseDTO(bankAccount.Balance);
            return customerBalanceResponseDTO;
        }

        public async Task<FinancialTransactionsResponseDTO> CreateFinancialTransactionAsync(CreateFinancialTransaction createFinancialTransaction)
        {
            ValidatePayload(createFinancialTransaction);

            // Organizing required data
            Random random = new Random();// Create a random number generator.
            var asset = _customersRepository.GetAssetById(createFinancialTransaction.AssetId); // Get asset price
            if (asset is null)
                return null;

            _log.LogInformation($"Service: CustomerAccountService - Method: CreateFinancialTransactionAsync - Asset found!");
            decimal totalValue = asset.Price * createFinancialTransaction.Quantity;

            BankAccounts bankAccount = _customersRepository.GetBalance(createFinancialTransaction.AccountId);

            // Funds validations
            if (bankAccount is null)
                return null;

            if (createFinancialTransaction.Type.ToLower() == "buy" && totalValue > bankAccount.Balance)
                throw new ApplicationException("Not enough funds.");

            int transactionId = random.Next(10000, 99999); // Generate a random 5-digit integer.

            // Converting/Adapting object
            FinancialTransactions financialTransactions = new FinancialTransactions(createFinancialTransaction, transactionId, totalValue, DateTime.Now);

            // Inserting in the database
            _log.LogInformation($"Service: CustomerAccountService - Method: CreateFinancialTransactionAsync - Inserting transaction in the database");
            bool createTransactions = _customersRepository.CreateTransactions(financialTransactions);

            // If inserted successfully, update balance in BankAccount
            if(createTransactions)
            {
                if (createFinancialTransaction.Type.ToLower() == "buy")
                    bankAccount.Balance -= totalValue;
                else
                    bankAccount.Balance += totalValue;

                _customersRepository.UpdateBalance(bankAccount);
            } else
            {
                return null;
            }

            // Converting/Adapting object to return it as response
            FinancialTransactionsResponseDTO createFinancialTransactionResponseDTO = new FinancialTransactionsResponseDTO(financialTransactions);
            return createFinancialTransactionResponseDTO;
        }

        public async Task<CreateAssetResponseDTO> CreateAssetAsync(CreateAsset createAsset)
        {
            // Create a random number generator.
            Random random = new Random();

            // Generate a random 5-digit integer.
            int assetId = random.Next(10000, 99999);

            // Converting/Adapting object
            FinancialAssets financialAssets = new FinancialAssets(createAsset, assetId);

            // Inserting in the database
            _log.LogInformation($"Service: CustomerAccountService - Method: CreateAssetAsync - Inserting asset in the database");
            _customersRepository.CreateAsset(financialAssets);//Add to database

            // Converting/Adapting object to return it as response
            CreateAssetResponseDTO createAssetResponseDTO = new CreateAssetResponseDTO(financialAssets);
            return createAssetResponseDTO;
        }

        public async Task<CreateCustomerResponseDTO> CreateCustomerAsync(CreateCustomer createCustomer)
        {
            Guid guid = Guid.NewGuid();
            // Converting/Adapting object
            Customers customers = new Customers(createCustomer, guid.ToString(), HashPassword(createCustomer.Password));

            // Inserting in the database
            _log.LogInformation($"Service: CustomerAccountService - Method: CreateCustomerAsync - Inserting new customer in the database");
            _customersRepository.AddCustomer(customers);//SQL Server call to add data

            // Converting/Adapting object to return it as response
            CreateCustomerResponseDTO createCustomerResponseDTO = new CreateCustomerResponseDTO(customers);
            return createCustomerResponseDTO;
        }

        public async Task<CreateAccountResponseDTO> CreateAccountAsync(CreateAccount createAccount)
        {
            // Create a random number generator.
            Random random = new Random();

            // Generate a random 5-digit integer.
            int accountId = random.Next(10000, 99999);

            // Converting/Adapting object
            BankAccounts bankAccount = new BankAccounts(createAccount, accountId);

            // Inserting in the database
            _log.LogInformation($"Service: CustomerAccountService - Method: CreateAccountAsync - Inserting new account in the database");
            _customersRepository.CreateAccount(bankAccount);

            // Converting/Adapting object to return it as response
            CreateAccountResponseDTO createAccountResponseDTO = new CreateAccountResponseDTO(bankAccount);
            return createAccountResponseDTO;
        }

        public async Task<List<FinancialTransactionsResponseDTO>> GetFinancialTransactionsAsync(int accountId)
        {
            List<FinancialTransactionsResponseDTO> financialTransactionsResponseDTO = new List<FinancialTransactionsResponseDTO>();
            List<FinancialTransactions> financialTransactions = _customersRepository.GetFinancialTransactions(accountId);

            _log.LogInformation($"Service: CustomerAccountService - Method: GetFinancialTransactionsAsync - financialTransactions found: {financialTransactions.Count()}");
            financialTransactionsResponseDTO = financialTransactions.ConvertAll(x => new FinancialTransactionsResponseDTO
            {
                TransactionId = x.TransactionId,
                AccountId = x.AccountId,
                Type = x.Type,
                AssetId = x.AssetId,
                Quantity = x.Quantity,
                TotalValue = x.TotalValue,
                Date = x.Date
            });

            return financialTransactionsResponseDTO;
        }

        private string HashPassword(string password)
        {
            // verify hash
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed; 
        }

        public bool VerifyAuthorization(string authorizationHeader, string apiKey)
        {
            var bearerToken = authorizationHeader.Substring("Bearer ".Length);

            var stream = bearerToken;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "apiKey").Value;

            return jti == apiKey;
        }

        private void ValidatePayload(CreateFinancialTransaction createFinancialTransaction)
        {
            if (createFinancialTransaction.Type.ToLower() != "buy" && createFinancialTransaction.Type.ToLower() != "sell")
                throw new ApplicationException("Invalid transaction type");
        }
    }
}
