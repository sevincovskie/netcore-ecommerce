using System.ComponentModel.DataAnnotations;

namespace netcore_ecommerce.Models;

public class Category {
    [Key]
    public int Id {get;set;}

    [Display(Name = "Name")]
    [Required(ErrorMessage = "This field is required")]
    public string? Name {get;set;}

    public virtual List<Product>? Products {get;set;}
}