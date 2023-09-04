namespace CoreBanking.Domain.Models.DTOs
{
    public class CreateAssetResponseDTO
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
