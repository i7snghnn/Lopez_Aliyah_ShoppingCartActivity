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
                Console.WriteLine("Invalid! You must type numeric Quantity!");
                Console.ReadKey(); continue;
            }
            if (qty <= 0)
            {
                Console.WriteLine("Invalid quantity. Please enter a positive number.");
                Console.ReadKey(); continue;
            }
            if (!selectedProduct.HasEnoughStock(qty))
            {
                Console.WriteLine($"Sorry, only {selectedProduct.RemainingStock} {selectedProduct.Name}(s) left in stock.");
                Console.ReadKey(); continue;
            }

            
            int existingIndex = -1;
            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Name == selectedProduct.Name)
                {
                    existingIndex = i; break;
                }
            }

            if (existingIndex >= 0)
            {
               
                cart[existingIndex].Quantity += qty;
                cart[existingIndex].TotalPrice += selectedProduct.GetItemTotal(qty);
                Console.WriteLine($"Updated {selectedProduct.Name} in cart! New qty: {cart[existingIndex].Quantity}");
            }
            else
            {
                if (cartCount >= 3)
                {
                    Console.WriteLine("Cart is full! Cannot add more items.");
                    Console.ReadKey(); continue;
                }
                
                cart[cartCount] = new CartItem(
                    selectedProduct.Name,
                    qty,
                    selectedProduct.GetItemTotal(qty)  
                );
                cartCount++;
                Console.WriteLine($"Added {qty} {selectedProduct.Name}(s) to cart!");
            }

           
            selectedProduct.DeductStock(qty);

            Console.WriteLine("\nDo you want to continue shopping? (Y/N)");
            string cont = Console.ReadLine();
            if (!cont.Equals("Y", StringComparison.CurrentCultureIgnoreCase)) isShopping = false;
        }

        
        Console.WriteLine("\n===== Here is your Receipt =====\n");
        double total = 0;
        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{cart[i].Name} - Qty: {cart[i].Quantity} - Price: ${cart[i].TotalPrice:0.00}");
            total += cart[i].TotalPrice;
        }

        Console.WriteLine($"\nTotal: ${total:0.00}");
        Console.ReadKey();

        if (total >= 5000)
        {
            double discount = total * 0.10;
            double finalTotal = total - discount;
            Console.WriteLine($"Grand Total: ${total:0.00}");
            Console.WriteLine($"Discount (10%): ${discount:0.00}");
            Console.WriteLine($"Final Total: ${finalTotal:0.00}");
        }
        else
        {
            Console.WriteLine($"Total: ${total:0.00}");
        }

        Console.WriteLine("\nUpdated Product Stock:");
        foreach (Product product in products)
        {
            string stockStatus = product.RemainingStock == 0 ? "OUT OF STOCK" : $"{product.RemainingStock} left";
            Console.WriteLine($"{product.Name,-10} : {stockStatus}");
        }

        Console.WriteLine("Thank you for purchasing in our store!");
    }
}

