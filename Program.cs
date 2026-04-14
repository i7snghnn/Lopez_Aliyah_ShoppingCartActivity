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

            p1.DisplayProduct();
            p2.DisplayProduct();
            p3.DisplayProduct();
            p4.DisplayProduct();
            p5.DisplayProduct();


            Console.WriteLine("Enter your Product: ");
            Console.ReadLine();
            Console.WriteLine();
            Console.Write("Enter your Product Quantity: ");
            string input = Console.ReadLine();

            Console.WriteLine("\nEnter ProductId: ");
            string Idinput = Console.ReadLine();

            if (!int.TryParse(Idinput, out int id))
            {
                Console.WriteLine("Invalid! You must type numeric ID!");
            }
                
            else
            {
                Console.WriteLine($"Valid! Your chosen ID is: {Id}");
            }

        }
    }
}
