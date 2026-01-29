using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Transactions_API.Entities
{
    public class Transaction
    {

        [Key]
        [Column("transaction_id")]
        public int TransactionId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("type_fk")]
        public int Type { get; set; } // "compra" 1 | "venta" 2

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("date")]
        public DateTime TransactionDate { get; set; }

        [Column("detail")]
        public string Detail { get; set; } = string.Empty;

        [Column("unit_price")]
        public decimal UnitPrice {  get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }
        
        [NotMapped]
        public string TypeName { get; set; } = string.Empty;


    }
}
