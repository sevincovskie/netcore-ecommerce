using System.ComponentModel.DataAnnotations;

namespace netcore_ecommerce.DTO;

public class AppUserRegister {
    [Display(Name = "First name")]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName {get;set;}
    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName {get;set;}
    [Display(Name = "User name")]
    [Required(ErrorMessage = "User name is required")]
    public string UserName {get;set;}
    [Display(Name = "City")]
    [Required(ErrorMessage = "City is required")]
    public string City {get;set;}
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required")]
    public string Email {get;set;}
    [Display(Name = "Phone number")]
    [Required(ErrorMessage = "Phone number is required")]
    public string PhoneNumber {get;set;}
    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required")]
    public string Password {get;set;}
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required")]
    public string ConfirmPassword {get;set;}
}