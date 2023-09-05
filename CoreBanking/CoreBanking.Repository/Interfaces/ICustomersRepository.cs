using CoreBanking.Domain.Entities;

namespace CoreBanking.Repository.Interfaces
{
    public interface ICustomersRepository
    {
        public void AddCustomer(Customers customers);
        public void CreateAccount(BankAccounts bankAccounts);
        public void CreateAsset(FinancialAssets financialAssets);
        public bool CreateTransactions(FinancialTransactions financialTransactions);
        public BankAccounts GetBalance(int accountId);
        public List<FinancialTransactions> GetFinancialTransactions(int accountId);
        public FinancialAssets GetAssetById(int assetId);
        public void UpdateBalance(BankAccounts bankAccount);
    }
}
