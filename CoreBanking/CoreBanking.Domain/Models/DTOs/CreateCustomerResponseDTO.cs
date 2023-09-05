using CoreBanking.Domain.Entities;

namespace CoreBanking.Domain.Models.DTOs
{
    public class CreateCustomerResponseDTO
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CreateCustomerResponseDTO()
        {

        }

        public CreateCustomerResponseDTO(Customers customers)
        {
            CustomerId = customers.CustomerId;
            Name = customers.Name;
            Email = customers.Email;
        }
    }
}
