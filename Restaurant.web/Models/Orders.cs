using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.web.Model;

namespace Restaurant.web.Models
{
    [Table(name: "Orders")]
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Order ID")]

        public int OrderId { get; set; }

     
        [Required]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50)]
        [Display(Name = "Order Name")]

        public string OrderName { get; set; }

        [Required]
        [DefaultValue(1)]
        

        public short Quantity { get; set; }

        #region Navigation Properties to the Book Model
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(Orders.CustomerId))]
        public Customers Customers { get; set; }

        [Display(Name = "Food ID")]

        public int FoodId { get; set; }

        [ForeignKey(nameof(Orders.FoodId))]
        [Display(Name = "Food Menu")]
        public Foodmenu Foodmenu { get; set; }

       





        #endregion
    }
}