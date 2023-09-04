using CoreBanking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Repository.Context
{
    public class CoreBankingContext : DbContext
    {
        public CoreBankingContext(DbContextOptions<CoreBankingContext> options) : base(options) { }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<BankAccounts> BankAccounts { get; set; }
        public DbSet<FinancialAssets> FinancialAssets { get; set; }
        public DbSet<FinancialTransactions> FinancialTransactions { get; set; }
    }
}
