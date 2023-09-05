using CoreBanking.Domain.Entities;

namespace CoreBanking.Domain.Models.DTOs
{
    public class CreateAccountResponseDTO
    {
        public int AccountId { get; set; }
        public string CustomerId { get; set; }
        public decimal Balance { get; set; }

        public CreateAccountResponseDTO()
        {

        }

        public CreateAccountResponseDTO(BankAccounts bankAccount)
        {
            CustomerId = bankAccount.CustomerId;
            AccountId = bankAccount.AccountId;
            Balance = bankAccount.Balance;
        }
    }
}
