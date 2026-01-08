using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject.Models
{
    public class Customer
    {
       
        [Key]
        public Guid id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [Required, MaxLength(20)]
        [Column("Customer_FirstName", TypeName ="Varchar(20)")]
        public string FirstName { get; set; }


        [Column("Customer_LastName", TypeName = "Varchar(30)")]
        public string LastName { get; set; } = "";

        [Required, MaxLength(50)]
        [EmailAddress]
        [Column("Customer_Email", TypeName = "Varchar(50)")]
        public string Email { get; set; }

        [Required]
        [Column("Customer_Password", TypeName = "Varchar(100)")]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        [Column("Customer_Street", TypeName = "Varchar(50)")]
        public string Street { get; set; } = "";

        [MaxLength(30)]
        [Column("Customer_City", TypeName = "Varchar(30)")]
        public string City { get; set; } = "";

        [MaxLength(10)]
        [Column("Customer_State", TypeName = "Varchar(10)")]
        public string State { get; set; } = "";

        [MaxLength(20)]
        [Column("Customer_Country", TypeName = "Varchar(20)")]
        public string Country { get; set; } = "";

        [MaxLength(15)]
        [Phone]
        [Column("Customer_Mobile", TypeName = "Varchar(15)")]
        public string Mobile { get; set; } = "";

        [MaxLength(20)]
        [Column("Customer_Creditcardnumber", TypeName = "Varchar(20)")]
        public string CreditCardNumber { get; set; } = "";

        [MaxLength(10)]
        [Column("Customer_Creaditcardexpiry", TypeName = "Varchar(20)")]
        public string CreditCardExpiry { get; set; } = "";
    }
}
