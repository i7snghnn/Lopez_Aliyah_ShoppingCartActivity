# class 
  using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace ConsoleApp2
{
    internal class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Remaining_Stock;

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            Remaining_Stock = stock;
        }

       
        public void DisplayProduct() 
        {
            string status = Remaining_Stock == 0 ? "OUT OF STOCK" : $"Stock: {Remaining_Stock}";
            Console.WriteLine($"{Id}. {Name,-10} | ₱{Price,7:N2} | {status}");
        }
    }
}

