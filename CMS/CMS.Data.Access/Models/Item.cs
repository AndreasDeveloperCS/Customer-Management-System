using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CMS.Data.Access.Interfaces;

namespace CMS.Data.Access.Models
{
    [Table("tblItem")]
    public class Item : IEntity
    {
        [Key]
        [Column("colItemId")]
        public int Id { get; set; }

        [Column("colItemQuantity")]
        public decimal Quantity { get; set; }

        [ForeignKey(nameof(Order))]
        [Column("colItemOrderId")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(Product))]
        [Column("colItemProductId")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        [Column("colDateCreated")]
        public DateTime DateCreated { get; set; }

        [Column("colUserCreated")]
        public string UserCreated { get; set; }

        [Column("colDateModified")]
        public DateTime? DateModified { get; set; }

        [Column("colUserModified")]
        public string? UserModified { get; set; }
    }
}
