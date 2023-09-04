namespace CoreBanking.Domain.Models.DTOs
{
    public class CreateAccountResponseDTO
    {
        public int AccountId { get; set; }
        public string CustomerId { get; set; }
        public decimal Balance { get; set; }
    }
}
