using Tasks.web.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Tasks.web.Models
{
    [Table(name: "Customers")]

    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }


        [Required]
        [Column(TypeName = "varchar(50)")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Don't Leave Customer Address")]
        [Column(TypeName = "varchar(200)")]
        [Display(Name = "Delivery Address")]
        [StringLength(200)]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Don't leave Mobile Number")]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^(\d{10})$")]
        public string MobileNUmber { get; set; }

        [Required(ErrorMessage = "Don't leave Email Address")]
        [Display(Name = "EMAIL")]
        [DataType(DataType.EmailAddress)]
        public string Eamil { get; set; }





        public ICollection<Product> Products { get; set; }



    }

}
