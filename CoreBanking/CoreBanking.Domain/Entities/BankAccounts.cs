using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBanking.Domain.Entities
{
    [Table("BankAccounts")]
    public class BankAccounts
    {
        [Key]
        public int AccountId { get; set; }
        public string CustomerId { get; set; }
        public decimal Balance { get; set; }

        public BankAccounts()
        {

        }
    }
}
