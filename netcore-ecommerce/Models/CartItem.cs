namespace netcore_ecommerce.Models;

public class CartItem {
    public long ProductId {get;set;}
    public string ProductName {get;set;}
    public int Quantity {get;set;}
    public Decimal Price {get;set;}
    public string Image {get;set;}

    public decimal Total {
        get {return Quantity * Price;}
    }

    public CartItem() {}

    public CartItem(Product products) {
        ProductId = products.ProductId;
        ProductName = products.Name;
        Quantity = 1;
        Price = Convert.ToDecimal(products.Price);
        Image = products.Picture;
    }
}