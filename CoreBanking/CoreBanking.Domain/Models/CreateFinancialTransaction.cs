namespace CoreBanking.Domain.Models
{
    public class CreateFinancialTransaction
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
    }
}
