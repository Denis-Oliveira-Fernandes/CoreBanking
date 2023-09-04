using CoreBanking.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBanking.Domain.Entities
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Customers()
        {
            
        }
    }
}
