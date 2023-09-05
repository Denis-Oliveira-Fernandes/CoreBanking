using CoreBanking.Domain.Entities;

namespace CoreBanking.Domain.Models.DTOs
{
    public class FinancialTransactionsResponseDTO
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime Date { get; set; }

        public FinancialTransactionsResponseDTO()
        {

        }

        public FinancialTransactionsResponseDTO(FinancialTransactions financialTransactions)
        {
            TransactionId = financialTransactions.TransactionId;
            AccountId = financialTransactions.AccountId;
            Type = financialTransactions.Type;
            AssetId = financialTransactions.AssetId;
            Quantity = financialTransactions.Quantity;
            TotalValue = financialTransactions.TotalValue;
            Date = financialTransactions.Date;
        }
    }
}
