using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CMS.Data.Access.Interfaces;
using CMS.Data.Models.DTOs;

namespace CMS.Data.Access.Models
{
    [Table("tblProduct")]
    public class Product : IEntity
    {
        [Key]
        [Column("colProductId")]
        public int Id { get; set; }
       
        [Column("colProductName")]
        public string Name { get; set; }

        [Column("colProductPricePerUnit")]
        public decimal PricePerUnit { get; set; }

        [Column("colProductMeasuringUnit")]
        public string MeasuringUnit { get; set; }


        [Column("colDateCreated")]
        public DateTime DateCreated { get; set; }

        [Column("colUserCreated")]
        public string UserCreated { get; set; }

        [Column("colDateModified")]
        public DateTime? DateModified { get; set; }

        [Column("colUserModified")]
        public string? UserModified { get; set; }

        public virtual Item Item { get; set; }

    }
}
