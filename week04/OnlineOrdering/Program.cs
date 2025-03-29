using System;
using System.Collections.Generic;

// Address class
class Address
{
    // Private member variables
    private string _street;
    private string _city;
    private string _stateProvince;
    private string _country;

    // Constructor
    public Address(string street, string city, string stateProvince, string country)
    {
        _street = street;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    // Method to check if address is in USA
    public bool IsInUSA()
    {
        return _country.ToUpper() == "USA";
    }

    // Method to return the full address as a string
    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
    }
}

// Customer class
class Customer
{
    // Private member variables
    private string _name;
    private Address _address;

    // Constructor
    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Getter for name
    public string GetName()
    {
        return _name;
    }

    // Getter for address
    public Address GetAddress()
    {
        return _address;
    }

    // Method to check if customer lives in USA
    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}

// Product class
class Product
{
    // Private member variables
    private string _name;
    private string _productId;
    private double _price;
    private int _quantity;

    // Constructor
    public Product(string name, string productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    // Getters
    public string GetName()
    {
        return _name;
    }

    public string GetProductId()
    {
        return _productId;
    }

    // Method to calculate total cost of the product
    public double CalculateTotalCost()
    {
        return _price * _quantity;
    }
}

// Order class
class Order
{
    // Private member variables
    private List<Product> _products;
    private Customer _customer;

    // Constructor
    public Order(Customer customer)
    {
        _products = new List<Product>();
        _customer = customer;
    }

    // Method to add a product to the order
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Method to calculate total cost of the order
    public double CalculateTotalCost()
    {
        double totalProductCost = 0;
        
        // Calculate sum of all product costs
        foreach (Product product in _products)
        {
            totalProductCost += product.CalculateTotalCost();
        }
        
        // Add shipping cost based on customer location
        double shippingCost = _customer.LivesInUSA() ? 5.0 : 35.0;
        
        return totalProductCost + shippingCost;
    }

    // Method to generate packing label
    public string GetPackingLabel()
    {
        string packingLabel = "PACKING LABEL:\n";
        
        foreach (Product product in _products)
        {
            packingLabel += $"Product: {product.GetName()}, ID: {product.GetProductId()}\n";
        }
        
        return packingLabel;
    }

    // Method to generate shipping label
    public string GetShippingLabel()
    {
        string shippingLabel = "SHIPPING LABEL:\n";
        shippingLabel += $"Customer: {_customer.GetName()}\n";
        shippingLabel += $"Address:\n{_customer.GetAddress().GetFullAddress()}";
        
        return shippingLabel;
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Cosmetics Ordering System");
        Console.WriteLine("=========================");

        // Create first customer with address
        Address address1 = new Address("123 Beauty Lane", "New York", "NY", "USA");
        Customer customer1 = new Customer("Dorothy Mclauren", address1);

        // Create second customer with address
        Address address2 = new Address("Rua Malheiro Nº 22 1ºAndar", "Barreiro", "Portugal", "PT");
        Customer customer2 = new Customer("Megan Maxinho", address2);

        // Create cosmetic products
        Product product1 = new Product("Moisturizing Cream", "SKIN101", 24.99, 2);
        Product product2 = new Product("Lipstick", "MAKE202", 15.50, 1);
        Product product3 = new Product("Facial Cleanser", "SKIN103", 18.75, 1);
        Product product4 = new Product("Mascara UltraXXL", "MAKE205", 12.99, 2);
        Product product5 = new Product("Sunscreen", "SKIN110", 22.50, 1);

        // Create first order
        Console.WriteLine("\n--- ORDER 1 ---");
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        // Display order information
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"\nTotal Cost: ${order1.CalculateTotalCost():F2}");

        // Create second order
        Console.WriteLine("\n--- ORDER 2 ---");
        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        // Display order information
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"\nTotal Cost: ${order2.CalculateTotalCost():F2}");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}