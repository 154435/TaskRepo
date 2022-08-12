using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.web.Models;

namespace Restaurant.web.Model
{
    [Table(name: "FoodMenu")]
    public class Foodmenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Food ID")]

        public int FoodId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Food Name")]

        public string FoodName { get; set; }

        [Display(Name = "Food Category ID")]
        public int FoodcategoryId { get; set; }

        [ForeignKey(nameof(Foodmenu.FoodcategoryId))]

        [Display(Name = "Food Category")]

        public FoodCategory FoodCategory { get; set; }

        [Required]
        [DefaultValue(1)]

        public short Quantity { get; set; }

        [Required]
        [DefaultValue(false)]
        [Display(Name ="Available")]
        public bool Confirmed { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<Orders> Orders { get; set; }




    }
}