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
                Console.WriteLine("Your cart is empty!"); Console.ReadKey(); 
                break; 
            }

            for (int i = 0; i < cartCount; i++) Console.WriteLine($"[{i + 1}] {cart[i].Name}");
            Console.Write("Enter the number to remove: ");

            if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= cartCount)
            {
                string removedItemName = cart[index - 1].Name;
                int removedQty = cart[index - 1].Quantity;

                foreach (Product product in products)
                {
                    if (product.Name == removedItemName) product.RemainingStock += removedQty;
                }
             
                for (int i = index - 1; i < cartCount - 1; i++) cart[i] = cart[i + 1];
                cart[cartCount - 1] = null;
                cartCount--;
                Console.WriteLine("Item removed!");
            }
            else Console.WriteLine("Invalid index!");
            Console.ReadKey();
            break;

        case "3":
            if (cartCount == 0) { Console.WriteLine("Cart is empty!"); Console.ReadKey(); break; }
            for (int i = 0; i < cartCount; i++) Console.WriteLine($"[{i + 1}] {cart[i].Name} x{cart[i].Quantity}");

            Console.Write("Enter item number to update: ");
            if (int.TryParse(Console.ReadLine(), out index) && index >= 1 && index <= cartCount)
            {
                Console.Write("Enter new quantity: ");
                if (int.TryParse(Console.ReadLine(), out int newQuantity) && newQuantity >= 1)
                {
                    int diff = newQuantity - cart[index - 1].Quantity;
                    foreach (Product product in products)
                    {
                        if (product.Name == cart[index - 1].Name)
                        {
                            if (diff > 0 && !product.HasEnoughStock(diff)) Console.WriteLine("Not enough stock!");
                            else
                            {
                                product.DeductStock(diff);
                                cart[index - 1].Quantity = newQuantity;
                                Console.WriteLine("Quantity updated!");
                            }
                            break;
                        }
                    }
                }
            }
            else 
            Console.WriteLine("Invalid!");
            Console.ReadKey();
            break;

        case "4":
            Console.WriteLine("Clearing Cart...");
            if (cartCount > 0)
            {
                for (int i = 0; i < cartCount; i++)
                {
                    foreach (Product product in products)
                    {
                        if (product.Name == cart[i].Name)
                        {
                            product.RemainingStock += cart[i].Quantity;
                            break;
                        }
                    }
                }
                cart[i] = null;
            }
            cartCount = 0;
            Console.WriteLine("Cart cleared!");
            Console.ReadKey();
            break;
        case "5":
                   
            Console.WriteLine("Clearing Cart...");
            if (cartCount > 0)
            {
                for (int i = 0; i < cartCount; i++)
                {
                    foreach (Product product in products)
                    {
                        if (product.Name == cart[i].Name)
                        {
                            product.RemainingStock += cart[i].Quantity;
                            break;
                        }
                    }
                }
                cart[i] = null;
            }
            cartCount = 0;
            Console.WriteLine("Cart cleared!");
            Console.ReadKey();
            break;
            Console.Clear();
            if (cartCount == 0)
            {
                Console.WriteLine("Cart is empty!");
                Console.ReadKey();
                break;
            }

            double checkoutTotal = 0;
            for (int i = 0; i < cartCount; i++)
                checkoutTotal += cart[i].TotalPrice;

            double checkoutDiscount = checkoutTotal >= 5000 ? checkoutTotal * 0.10 : 0;
            double finalTotal = checkoutTotal - checkoutDiscount;

            double payment = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("========== Checkout ==========\n");
                Console.WriteLine($"Subtotal          : Php {checkoutTotal:F2}");
                Console.WriteLine($"Discount          : Php {checkoutDiscount:F2}");
                Console.WriteLine($"Total             : Php {finalTotal:F2}");
                Console.WriteLine($"Enter payment amount: Php");

                if(double.TryParse(Console.ReadLine(), out payment))
                {
                    if (payment >= finalTotal)
                    {
                        double change = payment - finalTotal;
                        Console.WriteLine($"Payment accepted! Your change is Php {change:F2}");
                        cart[i] = null;
                        cartCount = 0;
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Insufficient payment! Please enter an amount equal to or greater than the total.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a valid payment amount.");
                    Console.ReadKey();
                }
                break;
            }

            double changeAmount = payment - finalTotal;
            string receiptNumber = receiptCounter.ToString();
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Console.Clear();
            Console.WriteLine("========== Receipt ==========\n");
            Console.WriteLine("Receipt Number: " + receiptNumber);
            Console.WriteLine("Date & Time   : " + dateTime);
            Console.WriteLine("=============================\n");

            for (int i = 0; i < cartCount; i++)
            {
                Console.WriteLine($"{cart[i].Name} x{cart[i].Quantity} - Php {cart[i].TotalPrice:F2}");
            }

            Console.WriteLine("\n=============================");
            Console.WriteLine($"Total Payment  : Php {payment:F2}");
            Console.WriteLine($"Total Discount : Php {checkoutDiscount:F2}");
            Console.WriteLine($"Grand Total    : Php {finalTotal:F2}");
            Console.WriteLine($"Change         : Php {changeAmount:F2}");
            Console.WriteLine("=============================\n");
            Console.WriteLine("Thank you for your purchase!");
            Console.ReadKey();

            Array.Clear(cart, 0, cart.Length);
            cartCount = 0;
            receiptCounter++; 

          
            Console.ReadKey();
            inCartMenu = false;
            break;
        default:
            Console.WriteLine("Invalid choice!");
            Console.ReadKey();
            break;
    }
}
break;
        case "6":
            inCartMenu = false;
            break;
        default:
            Console.WriteLine("Invalid choice!");
            Console.ReadKey();
            break;
                        
    }
}
break;
        

