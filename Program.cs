using System;
using System.Collections.Generic;
using System.Linq;

namespace Discount
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Product product = new Product1("Трусы со швом", 100);
      
      DiscountedProduct discountedProduct = new DiscountedProduct(product, 10);
      DiscountedProduct discountedProduct1 = new DiscountedProduct(product, 20);
      DiscountedProduct discountedProduct2 = new DiscountedProduct(product, 30);
      DiscountedProduct discountedProduct3 = new DiscountedProduct(product, 0);
      
      Console.WriteLine(product.Name + " " + product.Price + " " + product.GetDeliveries());
      Console.WriteLine(discountedProduct.Name + " " + discountedProduct.Price + " " + discountedProduct.GetDeliveries());
      Console.WriteLine(discountedProduct1.Name + " " + discountedProduct1.Price + " " + discountedProduct1.GetDeliveries());
      Console.WriteLine(discountedProduct2.Name + " " + discountedProduct2.Price + " " + discountedProduct2.GetDeliveries());
      Console.WriteLine(discountedProduct3.Name + " " + discountedProduct3.Price + " " + discountedProduct3.GetDeliveries());
    }
  }

  public abstract class Product
  {
    public string Name { get; protected set; }
    public float Price { get; protected set; }
    public List<Delivery> Deliveries { get; protected set; } = new List<Delivery>();

    public string GetDeliveries() => 
      string.Join(", ", Deliveries.Select(d => d.Name).ToArray());
  }

  public class Product1 : Product
  {
    public Product1(string name, float price)
    {
      Name = name;
      Price = price;
      Deliveries = new List<Delivery> { new Delivery("Targeted"), new Delivery("Pickup") };
    }
  }

  public abstract class DecoratorProduct : Product
  {
    protected readonly Product Product;
    protected DecoratorProduct(Product product)
    {
      Product = product;
    }
  }
  
  public class DiscountedProduct : DecoratorProduct
  {
    public DiscountedProduct(Product product, float discount) : base(product)
    {
      Name = product.Name;
      Price = Product.Price * (1f - discount / 100f);
      Deliveries = product.Deliveries.Where(d => d.Name != "Targeted").ToList();
    }
  }
  
  public class Delivery
  {
    public Delivery(string name)
    {
      Name = name;
    }

    public string Name { get; }
  }
}