using System.ComponentModel.DataAnnotations;

namespace netcore_ecommerce.Models;

public class Order {
    [Key]
    public string Id {get;set;}

    [Required]
    public long[] ProductId {get;set;}

    [Required]
    public int[] Quantity {get;set;}

    [Length(3, 50), Required]
    public string Name {get;set;}

    [Required]
    public string Email {get;set;}

    [Required]
    [Length(10, 14)]
    public string Phone {get;set;}

    [Required]
    public string Address {get;set;}

    [Required]
    public double GrandTotal {get;set;}
}