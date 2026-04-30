# Product.cs

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3
{
    internal class Product
        {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;
        public string Category;

         public Product(int id, string category, string name, double price, int remainingStock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = remainingStock;
            Category = category;
        }

        public void DisplayProduct()
        {
            Console.WriteLine("Name             : " + Name);
            Console.WriteLine("Category         : " + Category);
            Console.WriteLine("Id               : " + Id);
            Console.WriteLine("Price            : " + Price);
            Console.WriteLine("Remaining Stock  : " + RemainingStock);
        }

        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        public bool HasEnoughStock(int quantity)
        {
            return RemainingStock >= quantity;

        }

        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }

    }
}

# CartItem.cs

internal class CartItem
{
    public string Name;
    public int Quantity;
    public double TotalPrice;

    public CartItem(string name, int quantity, double totalPrice)
    {
        Name = name;
        Quantity = quantity;
        TotalPrice = totalPrice;
    }
}


# MAIN
using ConsoleApp3;

Product selectedProduct = null;

Product p1 = new Product(12, "String Instruments", "Guitar", 960, 38);
Product p2 = new Product(45, "String Instruments", "Ukulele", 660, 12);
Product p3 = new Product(67, "Keyboard Instruments", "Piano", 1120, 24);
Product p4 = new Product(88, "Percussion Instruments", "Drums", 2160, 83);
Product p5 = new Product(99, "Wind Instruments", "Flute", 580, 9);


        Product[] products = new Product[] { p1, p2, p3, p4, p5 };
        CartItem[] cart = new CartItem[3];
        int cartCount = 0;

        bool isShopping = true;
while (isShopping)
{
    Console.Clear();
    Console.WriteLine("========== MAIN MENU ==========");
    Console.WriteLine("[1] Browse Products");
    Console.WriteLine("[2] Search by Name");
    Console.WriteLine("[3] Filter by Category");
    Console.WriteLine("[4] Cart Menu");
    Console.WriteLine("[5] Exit");
    Console.Write("Choose: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("========== Available Products: ==========\n");
            foreach (Product product in products)
                product.DisplayProduct();
            Console.ReadKey(); continue;
        case "2":
            Console.WriteLine("\nEnter product name to search: ");
            string nameInput = Console.ReadLine();

            bool found = false;
            foreach (Product product in products)
            {
                if (product.Name.Contains(nameInput, StringComparison.CurrentCultureIgnoreCase))
                {
                    product.DisplayProduct();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Product not found!");
            }
            Console.ReadKey();
            break;
        case "3":
            Console.WriteLine("\nFilter by category");
            string categoryInput = Console.ReadLine();

            bool foundCategory = false;
            foreach (Product product in products)
            {
                if (product.Category.Equals(categoryInput, StringComparison.CurrentCultureIgnoreCase))
                {
                    product.DisplayProduct();
                    foundCategory = true;
                }
            }
            if (!foundCategory)
            {
                Console.WriteLine("No products found in this category!");
                Console.ReadKey(); continue;
            }
            Console.ReadKey(); continue;
        case "4":
            bool inCartMenu = true;
            while (inCartMenu)
            {
                Console.Clear();
                Console.WriteLine("========== Cart Menu ==========\n");

                Console.WriteLine("[1] View Cart");
                Console.WriteLine("[2] Remove Item from Cart");
                Console.WriteLine("[3] Update item quantity");
                Console.WriteLine("[4] Clear Cart");
                Console.WriteLine("[5] Checkout");
                Console.WriteLine("[6] Back to Shopping");
                 string cartChoice = Console.ReadLine();

                switch (cartChoice)
                {
                    case "1":
                        Console.WriteLine("Viewing Cart: ");
                        if (cartCount == 0)
                        {
                            Console.WriteLine("Your cart is empty!");
                        }
                        else
                        {
                                for (int i = 0; i < cartCount; i++)
                                {
                                    cart[i].DisplayCartItem();
                                }
                        }
                        Console.ReadKey();
                        break;
                case "2":
                    if (cartCount == 0)
                    {
                        Console.WriteLine("Your cart is empty!");
                        Console.ReadKey();
                        break;
                    }
                     for (int i = 0; i < cartCount; i++)
                    {
                        Console.WriteLine($"[ {i + 1} ] {cart[i].Name}");
                    }
                    Console.WriteLine("Enter the number of the item to remove: ");
                    if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > cartCount)
                    {
                        Console.WriteLine("Invalid!");
                        Console.ReadKey();
                        break;
                    }
                    for (int i = index - 1; i < cartCount - 1; i++)
                    {
                        cart[i] = cart[i + 1];
                    }
                    cart[cartCount - 1] = null;
                    cartCount--;

                    foreach (Product product in products)
                    {
                        if (product.Name == cart[index - 1].Name)
                            product.RemainingStock += cart[index - 1].Quantity;
                    }
                    Console.WriteLine("Item removed!");
                    Console.ReadKey();
                    break;


        

