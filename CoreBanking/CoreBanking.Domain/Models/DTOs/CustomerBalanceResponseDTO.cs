namespace CoreBanking.Domain.Models.DTOs
{
    public class CustomerBalanceResponseDTO
    {
        public decimal Balance { get; set; }

        public CustomerBalanceResponseDTO()
        {
        }

        public CustomerBalanceResponseDTO(decimal balance)
        {
            Balance = balance;
        }
    }
}