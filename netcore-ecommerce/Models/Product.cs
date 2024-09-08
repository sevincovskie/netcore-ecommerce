using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace netcore_ecommerce.Models;

public class Product {
    [Key]
    public int ProductId {get;set;}

    [Display(Name = "Name")]
    public string? Name {get;set;} = string.Empty;

    [Display(Name = "Code")]
    public int? Code {get;set;}
    
    [Display(Name = "Stock")]
    public int? Stock {get;set;}

    [Display(Name = "Description")]
    public string? Description {get;set;} = string.Empty;

    [Display(Name = "Picture")]
    [ValidateNever]
    public string? Picture {get;set;} = string.Empty;

    [Display(Name = "Price")]
    public int? Price {get;set;}

    [Display(Name = "Category")]
    public int? CategoryId {get;set;}

    public virtual Category? Category {get;set;}

    [NotMapped]
    [ValidateNever]
    public IFormFile? ImageUpload {get;set;}
}