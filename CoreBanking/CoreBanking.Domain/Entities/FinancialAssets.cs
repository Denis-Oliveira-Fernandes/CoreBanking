using CoreBanking.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBanking.Domain.Entities
{
    [Table("FinancialAssets")]
    public class FinancialAssets
    {
        [Key]
        public int AssetId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public FinancialAssets()
        {

        }

        public FinancialAssets(CreateAsset createAsset, int assetId)
        {
            AssetId = assetId;
            Name = createAsset.Name;
            Price = createAsset.Price;
        }
    }
}
