using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Products.web.Models
{
    [Table(name: "ProductCategories")]

    public class ProductCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PCId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50)]
        public string ProductCategoryName { get; set; }



        #region Navigation Property
        public ICollection<Product> Products { get; set; }

        #endregion
    }
}
