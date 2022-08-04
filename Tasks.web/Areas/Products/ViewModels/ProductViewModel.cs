using Tasks.web.Models;
using System.ComponentModel.DataAnnotations;


namespace Tasks.web.Areas.Products.ViewModels
{
    public class ProductViewModel
        : Product
    {
        [Display(Name = "Product ID")]
        override public int ProductId
        {
            get
            {
                return base.ProductId;
            }
            set
            {
                base.ProductId = value;
            }
        }

        [Display(Name = "Name of the product")]
        [Required(ErrorMessage = "{0} cannot be empty")]
        [MinLength(2, ErrorMessage = "{0} should have at least {1} characters")]
        [MaxLength(100, ErrorMessage = "{0} can have a maximum of {1} characters")]
        public override string Title
        {
            get => base.Title;
            set => base.Title = value;
        }

        [Display(Name = "Number of Product")]
        public override short NumberOfProducts
        {
            get => base.NumberOfProducts;
            set => base.NumberOfProducts = value;
        }

        [Display(Name = "Is Enabled?")]
        public override bool IsEnabled
        {
            get => base.IsEnabled;
            set => base.IsEnabled = value;
        }

        [Display(Name = "Product Category")]
        public override int ProductCategoryId
        {
            get => base.ProductCategoryId;
            set => base.ProductCategoryId = value;
        }

    }
}
