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

        public Product(int id, string name, double price, int remainingStock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = remainingStock;
        }
        
        public void DisplayProduct()
        {
            Console.WriteLine("Name             : " + Name);
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


# MAIN
using ConsoleApp3;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {
        Product selectedProduct = null;

        Product p1 = new Product(12, "Guitar", 960, 38);
        Product p2 = new Product(45, "Ukulele", 660, 12);
        Product p3 = new Product(67, "Piano", 1120, 24);
        Product p4 = new Product(88, "Drums", 2160, 83);
        Product p5 = new Product(99, "Flute", 580, 9);

        
        Product[] products = new Product[] { p1, p2, p3, p4, p5 };
        CartItem[] cart = new CartItem [3];
        int cartCount = 0;

            
            bool isShopping = true;
            while (isShopping)
            {
                Console.Clear();
                Console.WriteLine("========== Available Products: ==========");
                Console.WriteLine();
                foreach (Product product in products)
                {
                    product.DisplayProduct();
                }
                Console.WriteLine("\nEnter ProductId: ");
                string Idinput = Console.ReadLine();
                if (!int.TryParse(Idinput, out int id))
                {
                    Console.WriteLine("Invalid! You must type numeric ID!");
                    Console.ReadKey();
                    continue;
                }
                if (id <= 0)
                {
                     Console.WriteLine("Invalid Product ID! Please enter a positive number.");
                     Console.ReadKey();
                     continue;
                }
                if (id != 12 && id != 45 && id != 67 && id != 88 && id != 99)
                {
                    Console.WriteLine("Product ID is not found!");
                    Console.ReadKey();
                    continue;
                }
                foreach (Product product in products)
                {
                    if (product.Id == id)
                    {
                        selectedProduct = product;
                        break;
                    }
                }
                if (selectedProduct.RemainingStock == 0)
                {
                    Console.WriteLine($"{selectedProduct.Name} is out of stock!");
                    Console.ReadKey();
                    continue;
                }
                
                Console.WriteLine("Product Selected: " + selectedProduct.Name);
                    
                Console.WriteLine("\nEnter Product Quantity: ");
                string qtyinput = Console.ReadLine();
                
                if (!int.TryParse(qtyinput, out int qty))
                {
                Console.WriteLine("Invalid! You must type numeric Quantity!");
                Console.ReadKey();
                continue;
                }
                if (qty <= 0)
                {
                    Console.WriteLine("Invalid quantity. Please enter a positive number.");
                    Console.ReadKey();
                    continue;
                }
                if (!selectedProduct.HasEnoughStock(qty))
                {
                    Console.WriteLine($"Sorry, only {selectedProduct.RemainingStock} {selectedProduct.Name}(s) left in stock.");
                    Console.ReadKey();
                    continue;
                }


                int existingIndex = -1;
                for (int i = 0; i < cartCount; i++)
                {
                    if (cartItems[i] == selectedProduct.Name)
                    {
                        existingIndex = i;
                        break;
                    }    
                }
                if (existingIndex >= 0)
                {
                    cartQty[existingIndex] += qty;
                    cartPrice[existingIndex] += selectedProduct.Price * qty;
                    Console.WriteLine($"Updated {selectedProduct.Name} in cart! New qty: {cartQty[existingIndex]}");
                    selectedProduct.RemainingStock -= qty;
                }

                if (existingIndex == -1)
                {
                    if (cartCount >= 3)
                    {
                        Console.WriteLine("Cart is full! Cannot add more items.");
                        Console.ReadKey();
                    }
                    else
                    {
                    cart[cartCount] = new CartItem(
                        selectedProduct.Name 
                        qty,
                        selectedProduct.GetItemTotal(qty)
                        );
                        cartCount ++;
                        Console.WriteLine($"Added {qty} {selectedProduct.Name}(s) to cart!");
                    }
            }



            Console.WriteLine("\nDo you want to continue shopping? (Y/N)");
            string continueShopping = Console.ReadLine();
            if (continueShopping.ToUpper() != "Y")
            {
                isShopping = false;
            }
        }

        Console.WriteLine();
        Console.WriteLine("===== Here is your Receipt =====");
        Console.WriteLine("\nYour Cart:");
        double total = 0;
        for (int i = 0; i < cartCount; i++)
        {
            Console.WriteLine($"{cartItems[i]} - Qty: {cartQty[i]} - Price: ${cartPrice[i]:0.00}");
            total += cartPrice[i];
        }
        Console.WriteLine($"\nTotal: ${total:0.00}");
        Console.WriteLine("Thank you for shopping with us!");
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
