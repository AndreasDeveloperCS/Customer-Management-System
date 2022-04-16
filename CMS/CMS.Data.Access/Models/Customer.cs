using CMS.Data.Access.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Data.Access.Models
{
    [Table("tblCustomer")]
    public class Customer : IEntity
    {
        [Key]
        [Column("colCustomerId")]
        public int Id { get; set; }

        [Column("colFirstName")]
        public string FirstName { get; set; }

        [Column("colLastName")]
        public string LastName { get; set; }

        [Column("colAddress")]
        public string Address { get; set; }

        [Column("colPostalCode")]
        public string PostalCode { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        [Column("colDateCreated")]
        public DateTime DateCreated { get; set; }

        [Column("colUserCreated")]
        public string UserCreated  { get; set; }

        [Column("colDateModified")]
        public DateTime? DateModified { get; set; }

        [Column("colUserModified")]
        public string? UserModified { get; set; }
    }
}
