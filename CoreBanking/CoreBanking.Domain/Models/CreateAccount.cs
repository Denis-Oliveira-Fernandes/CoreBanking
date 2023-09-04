namespace CoreBanking.Domain.Models
{
    public class CreateAccount
    {
        public string CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
