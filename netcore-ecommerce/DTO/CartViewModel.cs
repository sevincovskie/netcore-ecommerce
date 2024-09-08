using netcore_ecommerce.Models;

namespace netcore_ecommerce.DTO;

public class CartViewModel {
    public List<CartItem> Items {get;set;}
    public decimal GrandTotal {get;set;}
}