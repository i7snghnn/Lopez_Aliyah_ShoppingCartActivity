# Product.cs

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp3
{
    internal class Product
        {
        private int _id;
        private string name;
        private double price;
        private int remainingStock;
        private string category;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

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

#Order.cs

using System;
using System.Collections.Generic;
using System.Text;

namespace part_2
{
    internal class Order
    {
        public int ReceiptNumber;
        public string DateTime;
        public double finalTotal;

        public Order(int receiptNumber, string dateTime, double finalTotal)
        {
            ReceiptNumber = receiptNumber;
            DateTime = dateTime;
            finalTotal = finalTotal;
        }   
    }
}


# MAIN
using ConsoleApp3;
using System.Security.Cryptography;

internal class Program
{
    static int receiptCounter = 1;
    static Order[] orderHistory = new Order[100];
    static int orderCount = 0;
    private static int i;

    private static void Main(string[] args)
    {
        Product selectedProduct = null;

        Product p1 = new Product(12, "String Instruments", "Guitar", 960, 38);
        Product p2 = new Product(45, "String Instruments", "Ukulele", 660, 12);
        Product p3 = new Product(67, "Keyboard Instruments", "Piano", 1120, 24);
        Product p4 = new Product(88, "Percussion Instruments", "Drums", 2160, 83);
        Product p5 = new Product(99, "Wind Instruments", "Flute", 580, 9);

        Product[] products = new Product[] { p1, p2, p3, p4, p5 };
        CartItem[] cart = new CartItem[10];
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
                    Console.WriteLine("\nEnter ProductId: ");
                    string idInput = Console.ReadLine();
                    if (!int.TryParse(idInput, out int id))
                    {
                        Console.WriteLine("Invalid! You must type numeric ID!");
                        Console.ReadKey(); continue;
                    }
                    if (id <= 0)
                    {
                        Console.WriteLine("Invalid Product ID! Please enter a positive number.");
                        Console.ReadKey(); continue;
                    }

                    selectedProduct = null;
                    foreach (Product product in products)
                    {
                        if (product.Id == id) { selectedProduct = product; break; }
                    }

                    if (selectedProduct == null)
                    {
                        Console.WriteLine("Product ID is not found!");
                        Console.ReadKey(); continue;
                    }
                    if (selectedProduct.RemainingStock == 0)
                    {
                        Console.WriteLine($"{selectedProduct.Name} is out of stock!");
                        Console.ReadKey(); continue;
                    }

                    Console.WriteLine("Product Selected: " + selectedProduct.Name);
                    Console.WriteLine("\nEnter Product Quantity: ");
                    string qtyInput = Console.ReadLine();
                    if (!int.TryParse(qtyInput, out int qty))
                    {
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
                            existingIndex = i;
                            break;
                        }
                    }

                    if (existingIndex >= 0)
                    {
                        cart[existingIndex].Quantity += qty;
                        cart[existingIndex].TotalPrice += selectedProduct.GetItemTotal(qty);
                        Console.WriteLine($"Updated {selectedProduct.Name} in cart!");
                    }
                    else
                    {
                        if (cartCount >= 10)
                        {
                            Console.WriteLine("Cart is full!");
                            Console.ReadKey();
                            break;
                        }
                        cart[cartCount] = new CartItem(selectedProduct.Name, qty, selectedProduct.GetItemTotal(qty));
                        cartCount++;
                        Console.WriteLine($"Added {qty} {selectedProduct.Name}(s) to cart!");
                    }

                    selectedProduct.DeductStock(qty);

                    Console.ReadKey();
                    break;
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
                        Console.Write("Choose: ");
                        string cartChoice = Console.ReadLine();

                        int index;

                        switch (cartChoice)
                        {
                            case "1":
                                Console.WriteLine("\nViewing Cart: ");
                                if (cartCount == 0) Console.WriteLine("Your cart is empty!");
                                else
                                {
                                    for (int i = 0; i < cartCount; i++) cart[i].DisplayCartItem();
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
                                        cart[i] = null;
                                    }
                                }
                                cartCount = 0;
                                Console.WriteLine("Cart cleared!");
                                Console.ReadKey();
                                break;
                            case "5":
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
                                    Console.WriteLine($"Subtotal          : PHP {checkoutTotal:F2}");
                                    Console.WriteLine($"Discount          : PHP {checkoutDiscount:F2}");
                                    Console.WriteLine($"Final Total       : PHP {finalTotal:F2}");
                                    Console.Write("Enter payment: PHP ");

                                    if (!double.TryParse(Console.ReadLine(), out payment) || payment <= 0)
                                    {
                                        Console.WriteLine("Invalid payment!");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    if (payment < finalTotal)
                                    {
                                        Console.WriteLine("Insufficient payment! Try again.");
                                        Console.ReadKey();
                                        continue;
                                    }
                                    break;
                                }

                                double changeAmount = payment - finalTotal;
                                string receiptNumber = receiptCounter.ToString("D4");
                                string dateTime = DateTime.Now.ToString("MMMM dd, yyyy h:mm tt");

                                Console.Clear();
                                Console.WriteLine("========================================");
                                Console.WriteLine($"        RECEIPT NO: {receiptNumber}");
                                Console.WriteLine($"        Date: {dateTime}");
                                Console.WriteLine("========================================");
                                for (int i = 0; i < cartCount; i++)
                                    Console.WriteLine($"{cart[i].Name,-15} x{cart[i].Quantity}   PHP {cart[i].TotalPrice:F2}");
                                Console.WriteLine("----------------------------------------");
                                Console.WriteLine($"{"Subtotal:",-25} PHP {checkoutTotal:F2}");
                                if (checkoutDiscount > 0)
                                    Console.WriteLine($"{"Discount (10%):",-25} PHP {checkoutDiscount:F2}");
                                Console.WriteLine($"{"Final Total:",-25} PHP {finalTotal:F2}");
                                Console.WriteLine($"{"Payment:",-25} PHP {payment:F2}");
                                Console.WriteLine($"{"Change:",-25} PHP {changeAmount:F2}");
                                Console.WriteLine("========================================");
                                Console.WriteLine("      Thank you for shopping!");
                                Console.WriteLine("========================================");

                                orderHistory[orderCount] = new Order(receiptNumber, dateTime, finalTotal);
                                orderCount++;
                                receiptCounter++;

                                Console.WriteLine("\n--- LOW STOCK ALERT ---");
                                bool hasLow = false;
                                foreach (Product product in products)
                                {
                                    if (product.RemainingStock <= 5)
                                    {
                                        Console.WriteLine($"{product.Name} has only {product.RemainingStock} left!");
                                        hasLow = true;
                                    }
                                }
                                if (!hasLow) Console.WriteLine("All products have sufficient stock.");

                                Array.Clear(cart, 0, cart.Length);
                                cartCount = 0;

                                Console.ReadKey();
                                inCartMenu = false;
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
                case "5":
                    Console.Clear();
                    Console.WriteLine("========== ORDER HISTORY ==========");
                    if (orderCount == 0)
                    {
                        Console.WriteLine("No orders yet.");
                    }
                    else
                    {
                        for (int i = 0; i < orderCount; i++)
                        {
                            Console.WriteLine($"Receipt #{orderHistory[i].ReceiptNumber} | {orderHistory[i].DateTime} | PHP {GetFinalTotal(i):F2}");
                        }
                    }
                    Console.ReadKey();
                    break;
                 case "6":
                    isShopping = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static double GetFinalTotal(int i)
    {
        return orderHistory[i].finalTotal;
    }
}
