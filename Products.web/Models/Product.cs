using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Products.web.Models;

namespace Products.web.Models
{
    [Table(name: "Products")]
    public class Product
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [StringLength(100)]
        public string ProductName { get; set; }


        [Required]
        [DefaultValue(1)]
        public int Quantity { get; set; }

        [Required]
        public DateTime Deliverydate { get; set; }



        #region
        public int PCID { get; set; }
        [ForeignKey(nameof(Product.PCID))]
        public ProductCategory Category { get; set; }


        #endregion
        #region
        public int CustomerID { get; set; }
        [ForeignKey(nameof(Product.CustomerID))]
        [Display(Name = "Delivery TO")]
        public Customer DeliveryDetails { get; set; }
        #endregion
    }
}