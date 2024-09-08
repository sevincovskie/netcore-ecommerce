using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore_ecommerce.Models;

public class Slider {
    [Key]
    public int Id {get;set;}

    public string? Name {get;set;} = string.Empty;
    public string? Header1 {get;set;} = string.Empty;
    public string? Header2 {get;set;} = string.Empty;
    public string? Context {get;set;} = string.Empty;
    public string? Image {get;set;} = string.Empty;

    [NotMapped]
    public IFormFile? ImageUpload {get;set;}
}