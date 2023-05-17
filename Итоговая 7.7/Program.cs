// See https://aka.ms/new-console-template for more information
public abstract class Order
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public DateTime OrderDate { get; set; }
    public abstract decimal CalculateTotal();
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    private decimal _price;
    public decimal Price
    {
        get { return _price; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
            _price = value;
        }
    }
}
public class Address
{
    public Address(string street, string city, string zipCode)
    {
        Street = street;
        City = city;
        ZipCode = zipCode;
    }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}
public class HomeDelivery : Order
{
    public HomeDelivery(Address deliveryAddress, string courierCompany)
    {
        DeliveryAddress = deliveryAddress;
        CourierCompany = courierCompany;
    }

    public Address DeliveryAddress { get; set; }
    public string CourierCompany { get; set; }

    public override decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var product in Products)
        {
            total += product.Price;
        }
        return total + 10;
    }
}
public class PickPointDelivery : Order
{
    public PickPointDelivery(Address pickPointAddress, string pickPointCompany)
    {
        PickPointAddress = pickPointAddress;
        PickPointCompany = pickPointCompany;
    }
    public Address PickPointAddress { get; set; }
    public string PickPointCompany { get; set; }
    public override decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var product in Products)
        {
            total += product.Price;
        }
        return total + 5;
    }
}
public class ShopDelivery : Order
{
    public ShopDelivery(Address shopAddress, string shopName)
    {
        ShopAddress = shopAddress;
        ShopName = shopName;
    }

    public Address ShopAddress { get; set; }
    public string ShopName { get; set; }

    public override decimal CalculateTotal()
    {
        decimal total = 0;
        foreach (var product in Products)
        {
            total += product.Price;
        }
        return total;
    }
}
public static class OrderHelper
{
    public static void PrintOrderDetails(Order order)
    {
        Console.WriteLine($"Order ID: {order.Id}");
        Console.WriteLine($"Order Date: {order.OrderDate}");
        Console.WriteLine($"Total: {order.CalculateTotal()}");
    }

    public static T CreateOrder<T>(int id, List<Product> products, DateTime orderDate) where T : Order, new ()
    {
        return new T { Id = id, Products = products, OrderDate = orderDate};
    }
}
public class Program
{
    public static void Main()
    {
        var homeDelivery = new HomeDelivery(new Address("Улица", "Город", "00000"), "Компания доставки")
        {
            Id = 1,
            Products = new List<Product>
        {
            new Product { Id = 1, Name = "Товар 1", Price = 100 },
            new Product { Id = 2, Name = "Товар 2", Price = 200 }
        },
            OrderDate = DateTime.Now,
        };

        Console.WriteLine($"Home Delivery Total: {homeDelivery.CalculateTotal()}");

        OrderHelper.PrintOrderDetails(homeDelivery);

        var pickPointDelivery = new PickPointDelivery(new Address("Улица", "Город", "11111"), "Пункт выдачи")
        {
            Id = 2,
            Products = new List<Product>
        {
            new Product { Id = 3, Name = "Товар 3", Price = 300 },
            new Product { Id = 4, Name = "Товар 4", Price = 400 }
        },
            OrderDate = DateTime.Now,
        };

        Console.WriteLine($"PickPoint Delivery Total: {pickPointDelivery.CalculateTotal()}");

        OrderHelper.PrintOrderDetails(pickPointDelivery);

        var shopDelivery = new ShopDelivery(new Address("Улица", "Город", "22222"), "Название магазина")
        {
            Id = 3,
            Products = new List<Product>
        {
            new Product { Id = 5, Name = "Товар 5", Price = 500 },
            new Product { Id = 6, Name = "Товар 6", Price = 600 }
        },
            OrderDate = DateTime.Now,
        };

        Console.WriteLine($"Shop Delivery Total: {shopDelivery.CalculateTotal()}");

        OrderHelper.PrintOrderDetails(shopDelivery);
    }
}