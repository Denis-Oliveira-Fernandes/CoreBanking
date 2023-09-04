using CoreBanking.Domain.Entities;
using CoreBanking.Domain.Models;
using CoreBanking.Domain.Models.DTOs;
using CoreBanking.Repository.Interfaces;
using CoreBanking.Service.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace CoreBanking.Service
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private ICustomersRepository _customersRepository;

        public CustomerAccountService()
        {
        }
        public CustomerAccountService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }
        public async Task<CustomerBalanceResponseDTO> GetCustomerBalanceAsync(int accountId)
        {
            BankAccounts bankAccount = _customersRepository.GetBalance(accountId);
            if (bankAccount is null)
                return null;

            CustomerBalanceResponseDTO customerBalanceResponseDTO = new CustomerBalanceResponseDTO
            {
                Balance = bankAccount.Balance
            };

            return customerBalanceResponseDTO;
        }

        public async Task<FinancialTransactionsResponseDTO> CreateFinancialTransactionAsync(CreateFinancialTransaction createFinancialTransaction)
        {
            // Create a random number generator.
            Random random = new Random();

            var asset = _customersRepository.GetAssetById(createFinancialTransaction.AssetId); // Get asset price
            if (asset is null)
                return null;

            decimal totalValue = asset.Price * createFinancialTransaction.Quantity;

            // Generate a random 5-digit integer.
            int transactionId = random.Next(10000, 99999);

            FinancialTransactions financialTransactions = new FinancialTransactions
            {
                TransactionId = transactionId,
                AccountId = createFinancialTransaction.AccountId,
                Type = createFinancialTransaction.Type,
                AssetId = createFinancialTransaction.AssetId,
                Quantity = createFinancialTransaction.Quantity,
                TotalValue = totalValue,
                Date = DateTime.Now
            };

            _customersRepository.CreateTransactions(financialTransactions);

            FinancialTransactionsResponseDTO createFinancialTransactionResponseDTO = new FinancialTransactionsResponseDTO
            {
                TransactionId = financialTransactions.TransactionId,
                AccountId = financialTransactions.AccountId,
                Type = financialTransactions.Type,
                AssetId = financialTransactions.AssetId,
                Quantity = financialTransactions.Quantity,
                TotalValue = totalValue,
                Date = financialTransactions.Date
            };

            return createFinancialTransactionResponseDTO;
        }

        public async Task<CreateAssetResponseDTO> CreateAssetAsync(CreateAsset createAsset)
        {
            // Create a random number generator.
            Random random = new Random();

            // Generate a random 5-digit integer.
            int assetId = random.Next(10000, 99999);

            FinancialAssets financialAssets = new FinancialAssets
            {
                AssetId = assetId,
                Name = createAsset.Name,
                Price = createAsset.Price
            };

            _customersRepository.CreateAsset(financialAssets);//Add to database

            CreateAssetResponseDTO createAssetResponseDTO = new CreateAssetResponseDTO
            {
                AssetId = financialAssets.AssetId,
                Name = financialAssets.Name,
                Price = financialAssets.Price
            };
 
            return createAssetResponseDTO;
        }

        public async Task<CreateCustomerResponseDTO> CreateCustomerAsync(CreateCustomer createCustomer)
        {
            Guid guid = Guid.NewGuid();
            Customers customers = new Customers
            {
                CustomerId = guid.ToString(),
                Email = createCustomer.Email,
                Name = createCustomer.Name,
                Password = HashPassword(createCustomer.Password)
            };
            
            _customersRepository.AddCustomer(customers);//Database

            CreateCustomerResponseDTO createCustomerResponseDTO = new CreateCustomerResponseDTO
            {
                CustomerId = customers.CustomerId,
                Email = customers.Email,
                Name = customers.Name,
            };
            
            return createCustomerResponseDTO;
        }

        public async Task<CreateAccountResponseDTO> CreateAccountAsync(CreateAccount createAccount)
        {
            // Create a random number generator.
            Random random = new Random();

            // Generate a random 5-digit integer.
            int accountId = random.Next(10000, 99999);

            BankAccounts bankAccount = new BankAccounts
            {
                AccountId = accountId,
                CustomerId = createAccount.CustomerId,
                Balance = createAccount.Balance
            };
            

            _customersRepository.CreateAccount(bankAccount);

            CreateAccountResponseDTO createAccountResponseDTO = new CreateAccountResponseDTO
            {
                CustomerId = bankAccount.CustomerId,
                AccountId = bankAccount.AccountId,
                Balance = bankAccount.Balance
            };
            

            return createAccountResponseDTO;
        }

        public async Task<List<FinancialTransactionsResponseDTO>> GetFinancialTransactionsAsync(int accountId)
        {
            List<FinancialTransactionsResponseDTO> financialTransactionsResponseDTO = new List<FinancialTransactionsResponseDTO>();
            List<FinancialTransactions> financialTransactions = _customersRepository.GetFinancialTransactions(accountId);

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
    }
}
