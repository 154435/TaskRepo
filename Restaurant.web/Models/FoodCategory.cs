using Restaurant.web.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.web.Models
{

    [Table(name: "FoodCategory")]
    public class FoodCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int FoodcategoryId { get; set; }



        [Required]
        [StringLength(100)]
        [Display(Name = "Food Category Name")]

        public string FoodcategoryName { get; set; }

        public ICollection<Foodmenu> foodmenus { get; set; }


    }
}