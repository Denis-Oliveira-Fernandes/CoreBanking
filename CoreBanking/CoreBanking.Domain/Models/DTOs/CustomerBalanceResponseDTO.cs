using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBanking.Domain.Models.DTOs
{
    public class CustomerBalanceResponseDTO
    {
        public decimal Balance { get; set; }

        public CustomerBalanceResponseDTO()
        {
        }
    }
}