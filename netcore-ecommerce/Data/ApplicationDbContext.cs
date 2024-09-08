using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Data;

public class ApplicationDbContext: IdentityDbContext<AppUser, AppRole, int> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}
    public DbSet<Category> Categories {get;set;}
    public DbSet<Product> Products {get;set;}
    public DbSet<Slider> Sliders {get;set;}
    public DbSet<Order> Orders {get;set;}
}