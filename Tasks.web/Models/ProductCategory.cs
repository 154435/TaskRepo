using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasks.web.Models
{
    [Table(name: "Categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50)]

        public string CategoryName { get; set; }

        #region Navigation Properties to the Book Model

        public ICollection<Product> Products { get; set; }


        #endregion
    }
    /**************
    *      CREATE TABLE [Categories]
    *      (
    *          [CategoryId] int NOT NULL IDENTITY (1,1)
    *          , [CategoryName] varchar(50) NOT NULL
    *          
    *          , CONSTRAINT [PK_Categories] PRIMARY KEY ( [CategoryId] ASC )
    *      )
    *****/
}


/****************
 * CREATE TABLE [Categories]
 * (
 *      []
 */
