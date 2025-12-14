using System;
using System.Collections.Generic;

// =========================== ADDRESS CLASS ===========================
class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.Trim().ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }
}

// =========================== CUSTOMER CLASS ===========================
class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }

    public string GetName()
    {
        return name;
    }

    public string GetAddressString()
    {
        return address.GetFullAddress();
    }
}

// =========================== PRODUCT CLASS ===========================
class Product
{
    private string name;
    private string productId;
    private double pricePerUnit;
    private int quantity;

    public Product(string name, string productId, double pricePerUnit, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.pricePerUnit = pricePerUnit;
        this.quantity = quantity;
    }

    public double GetTotalCost()
    {
        return pricePerUnit * quantity;
    }

    public string GetName() { return name; }
    public string GetProductId() { return productId; }
}

// =========================== ORDER CLASS ===========================
class Order
{
    private List<Product> products = new List<Product>();
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public string GetPackingLabel()
    {
        string label = "PACKING LABEL:\n";
        foreach (Product p in products)
        {
            label += $"{p.GetName()}  (ID: {p.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"SHIPPING LABEL:\n{customer.GetName()}\n{customer.GetAddressString()}";
    }

    public double GetTotalPrice()
    {
        double productTotal = 0;

        foreach (Product p in products)
        {
            productTotal += p.GetTotalCost();
        }

        double shippingCost = customer.LivesInUSA() ? 5 : 35;

        return productTotal + shippingCost;
    }
}

// =========================== MAIN PROGRAM ===========================
class Program
{
    static void Main(string[] args)
    {
        // ---------------- ORDER 1 ----------------
        Address a1 = new Address("123 Maple St", "Dallas", "TX", "USA");
        Customer c1 = new Customer("James Smith", a1);

        Order order1 = new Order(c1);
        order1.AddProduct(new Product("USB Cable", "A100", 5.99, 2));
        order1.AddProduct(new Product("Wireless Mouse", "B230", 15.50, 1));

        // ---------------- ORDER 2 ----------------
        Address a2 = new Address("55 Queen St", "Toronto", "ON", "Canada");
        Customer c2 = new Customer("Maria Lopez", a2);

        Order order2 = new Order(c2);
        order2.AddProduct(new Product("Laptop Stand", "C450", 32.00, 1));
        order2.AddProduct(new Product("Keyboard", "D900", 27.99, 1));
        order2.AddProduct(new Product("HDMI Adapter", "E130", 9.50, 2));

        // ---------------- DISPLAY RESULTS ----------------
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.GetTotalPrice():0.00}");
        Console.WriteLine("\n------------------------------------------\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.GetTotalPrice():0.00}");
    }
}
