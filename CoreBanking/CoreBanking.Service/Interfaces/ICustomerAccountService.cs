using CoreBanking.Domain.Models;
using CoreBanking.Domain.Models.DTOs;

namespace CoreBanking.Service.Interfaces
{
    public interface ICustomerAccountService
    {
        public Task<CustomerBalanceResponseDTO> GetCustomerBalanceAsync(int accountId);
        public Task<FinancialTransactionsResponseDTO> CreateFinancialTransactionAsync(CreateFinancialTransaction createFinancialTransaction);
        public Task<CreateAssetResponseDTO> CreateAssetAsync(CreateAsset createAsset);
        public Task<CreateCustomerResponseDTO> CreateCustomerAsync(CreateCustomer createCustomer);
        public Task<CreateAccountResponseDTO> CreateAccountAsync(CreateAccount createAccount);
        public Task<List<FinancialTransactionsResponseDTO>> GetFinancialTransactionsAsync(int accountId);
        public bool VerifyAuthorization(string authorizationHeader, string apiKey);
    }
}
