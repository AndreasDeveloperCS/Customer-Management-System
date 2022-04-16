using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CMS.Data.Access.Interfaces;

namespace CMS.Data.Access.Models
{
    [Table("tblOrder")]
    public class Order : IEntity
    {
        [Key]
        [Column("colOrderId")]
        public int Id { get; set; }

        [Column("colOrderDateTime")]
        public DateTime OrderDate { get; set; }

        [Column("colOrderTotalPrice")]
        public decimal TotalPrice { get; set; }

        [ForeignKey(nameof(Customer))]
        [Column("colOrderCustomerId")]
        public int OrderCustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual IEnumerable<Item> Items { get; set; }

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
