using CoreBanking.Domain.Entities;
using CoreBanking.Repository.Context;
using CoreBanking.Repository.Interfaces;

namespace CoreBanking.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private CoreBankingContext _coreBankingContext;

        public CustomersRepository() { }
        public CustomersRepository(CoreBankingContext coreBankingContext)
        {
            _coreBankingContext = coreBankingContext;
        }

        public void AddCustomer(Customers customers)
        {
            _coreBankingContext.Customers.Add(customers);
            _coreBankingContext.SaveChanges();
        }

        public void CreateAccount(BankAccounts bankAccounts)
        {
            _coreBankingContext.BankAccounts.Add(bankAccounts);
            _coreBankingContext.SaveChanges();
        }

        public void CreateAsset(FinancialAssets financialAssets)
        {
            _coreBankingContext.FinancialAssets.Add(financialAssets);
            _coreBankingContext.SaveChanges();
        }

        public bool CreateTransactions(FinancialTransactions financialTransactions)
        {
            _coreBankingContext.FinancialTransactions.Add(financialTransactions);
            _coreBankingContext.SaveChanges();

            return true;
        }

        public BankAccounts GetBalance(int accountId)
        {
            BankAccounts account = _coreBankingContext.BankAccounts.Where(_ => _.AccountId == accountId).FirstOrDefault();
            return account;
        }

        public void UpdateBalance(BankAccounts bankAccount)
        {
            _coreBankingContext.BankAccounts.Update(bankAccount);
            _coreBankingContext.SaveChanges();
        }

        public List<FinancialTransactions> GetFinancialTransactions(int accountId)
        {
            List<FinancialTransactions> financialTransactions = _coreBankingContext.FinancialTransactions.Where(_ => _.AccountId == accountId).ToList();
            return financialTransactions;
        }

        public FinancialAssets GetAssetById(int assetId)
        {
            FinancialAssets financialAsset = _coreBankingContext.FinancialAssets.Where(_ => _.AssetId == assetId).FirstOrDefault();
            return financialAsset;
        }
    }
}
