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

        public Product(int id, string name, double proce, int remainingStock)
        {
            Id = Id;
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
    }
}


# MAIN
namespace ConsoleApp3
                                {
    internal class Program
    {
        public static object Id { get; private set; }

        static void Main(string[] args)
        {
            Product p1 = new Product(12, "Guitar", 960, 38);
            Product p2 = new Product(45, "Ukulele", 660, 12);
            Product p3 = new Product(67, "Piano", 1120, 24);
            Product p4 = new Product(88, "Drums", 2160, 83);
            Product p5 = new Product(99, "Flute", 580, 9);

            Product [] products = new Product [] { p1, p2, p3, p4, p5 };
            
            int[] cartQty = cartPrice = new int[5];
            double[] cartPrice = new double[5];
            
            bool isShopping = true;
            while (isShopping)
            {
                Console.WriteLine("Available Products: ");
                foreach (Product product in products)
                {
                    product.DisplayProduct();
                }
                Console.WriteLine("\nEnter ProductId: ");
                string Idinput = Console.ReadLine();

                if (!int.TryParse(Idinput, out int id))
                {
                    Console.WriteLine("Invalid! You must type numeric ID!");
                    continue;
                }
                if (id != 12 && && id != 45 && id != 67 && id != 88 && id != 99)
                {
                    Console.WriteLine("Product ID is not found!");
                    continue;
                }
                Console.WriteLine("Enter Product Quantity: ");
                string qtyinput = Console.ReadLine();
                if (!(int.TryParse(qtyinput, result: out int qty) && qty > 0))
                {
                    Console.WriteLine("Invalid Quantity!");
                }
                if (qty <= 0)
                {
                    Console.WriteLine("Invalid quantity. Please enter a positive number.");
                    continue;
                }
                if (qty > ((Product)selectedProduct).RemainingStock)
                {
                    Console.WriteLine("Not enough stock available.");
                    continue;
                }

                
            }
         
        }
    }
}
