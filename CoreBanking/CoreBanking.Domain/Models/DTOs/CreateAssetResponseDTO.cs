using CoreBanking.Domain.Entities;

namespace CoreBanking.Domain.Models.DTOs
{
    public class CreateAssetResponseDTO
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public CreateAssetResponseDTO()
        {
           
        }

        public CreateAssetResponseDTO(FinancialAssets financialAssets)
        {
            AssetId = financialAssets.AssetId;
            Name = financialAssets.Name;
            Price = financialAssets.Price;
        }
    }
}
