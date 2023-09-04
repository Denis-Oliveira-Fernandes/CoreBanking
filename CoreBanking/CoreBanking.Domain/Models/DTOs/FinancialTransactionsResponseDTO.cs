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

        public static List<FinancialTransactionsResponseDTO> Build(List<FinancialTransactions> financialTransactions)
        {
            var financialTransactionsResponseDTO = financialTransactions.ConvertAll(x => new FinancialTransactionsResponseDTO
            {
                TransactionId = x.TransactionId,
                AccountId = x.AccountId,
                Type = x.Type,
                AssetId = x.AssetId,
                Quantity = x.Quantity,
                TotalValue = x.TotalValue,
                Date = x.Date
            });

            return financialTransactionsResponseDTO;
        }
    }
}
