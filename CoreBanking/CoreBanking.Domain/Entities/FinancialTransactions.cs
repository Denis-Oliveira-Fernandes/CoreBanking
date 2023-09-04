using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBanking.Domain.Entities
{
    [Table("FinancialTransactions")]
    public class FinancialTransactions
    {
        [Key]
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime Date { get; set; }

        public FinancialTransactions()
        {

        }
    }
}
