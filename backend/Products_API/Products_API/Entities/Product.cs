using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Products_API.Entities
{
    public class Product
    {
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("category_fk")]
        public int CategoryFk { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("batch")]
        public string Batch { get; set; } = string.Empty;

        [Column("entry_date")]
        public DateTime EntryDate { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
