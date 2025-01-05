using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using System;
using System.Linq;

namespace HenriksHobbyLager
{
    public class ProgramManager
    {
        public void Run()
        {
            Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
            Console.WriteLine("Select Database:");
            Console.WriteLine("1. SQLite");
            Console.WriteLine("2. MongoDB");
            Console.WriteLine("3. Exit");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("SQLite selected");
                    RunApplication(new SqliteRepository());  // Exempel på SQLite-repository
                    break;
                case "2":
                    Console.WriteLine("MongoDB selected");
                    RunApplication(new MongoDbRepository());  // Exempel på MongoDB-repository
                    break;
                case "3":
                    Console.WriteLine("Application closed");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please select 1 for SQLite or 2 for MongoDB:");
                    break;
            }
        }

        // Huvudloopen som hanterar menysystemet och CRUD-operationerna
        static void RunApplication(IRepository<Product> repository)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök produkter");
                Console.WriteLine("6. Avsluta");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllProducts(repository);
                        break;
                    case "2":
                        AddProduct(repository);
                        break;
                    case "3":
                        UpdateProduct(repository);
                        break;
                    case "4":
                        DeleteProduct(repository);
                        break;
                    case "5":
                        SearchProducts(repository);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val! Försök igen.");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        // Visar alla produkter
        private static void ShowAllProducts(IRepository<Product> repository)
        {
            Console.WriteLine("=== Alla produkter i lagret ===");
            var products = repository.GetAll();

            if (!products.Any())
            {
                Console.WriteLine("Inga produkter finns i lagret.");
                return;
            }

            foreach (var product in products)
            {
                DisplayProduct(product);
            }
        }

        // Lägg till en ny produkt
        private static void AddProduct(IRepository<Product> repository)
        {
            Console.WriteLine("=== Lägg till ny produkt ===");
            Console.Write("Namn: ");
            var name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Namnet får inte vara tomt!");
                Console.Write("Namn: ");
                name = Console.ReadLine();
            }

            Console.Write("Pris: ");
            if (!decimal.TryParse(Console.ReadLine(), out var price) || price <= 0)
            {
                Console.WriteLine("Ogiltigt pris! Priset måste vara ett positivt tal.");
                return;
            }

            Console.Write("Antal i lager: ");
            if (!int.TryParse(Console.ReadLine(), out var stock))
            {
                Console.WriteLine("Ogiltig lagermängd! Endast heltal är tillåtna.");
                return;
            }

            Console.Write("Kategori: ");
            var category = Console.ReadLine();

            var product = new Product
            {
                Name = name,
                Price = price,
                Stock = stock,
                Category = category,
                Created = DateTime.Now
            };

            repository.Add(product);
            Console.WriteLine("Produkt tillagd!");
        }

        // Uppdatera en produkt
        private static void UpdateProduct(IRepository<Product> repository)
        {
            Console.Write("Ange produkt-ID att uppdatera: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            var product = repository.GetById(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte.");
                return;
            }

            Console.Write("Nytt namn: ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                product.Name = name;

            Console.Write("Nytt pris: ");
            if (decimal.TryParse(Console.ReadLine(), out var price) && price > 0)
                product.Price = price;

            Console.Write("Ny lagermängd: ");
            if (int.TryParse(Console.ReadLine(), out var stock))
                product.Stock = stock;

            Console.Write("Ny kategori: ");
            var category = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(category))
                product.Category = category;

            product.LastUpdated = DateTime.Now;
            repository.Update(product);
            Console.WriteLine("Produkt uppdaterad!");
        }

        // Ta bort en produkt
        private static void DeleteProduct(IRepository<Product> repository)
        {
            Console.Write("Ange produkt-ID att ta bort: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Ogiltigt ID!");
                return;
            }

            var product = repository.GetById(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte.");
                return;
            }

            repository.Delete(id);
            Console.WriteLine("Produkt borttagen!");
        }

        // Sök produkter
        private static void SearchProducts(IRepository<Product> repository)
        {
            Console.Write("Sökterm: ");
            var searchTerm = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Söktermen får inte vara tom.");
                return;
            }

            var results = repository.Search(p =>
                (p.Name?.ToLower().Contains(searchTerm) ?? false) ||
                (p.Category?.ToLower().Contains(searchTerm) ?? false));

            if (!results.Any())
            {
                Console.WriteLine("Inga produkter matchade sökningen.");
                return;
            }

            foreach (var product in results)
            {
                DisplayProduct(product);
            }
        }

        // Visar produkt
        private static void DisplayProduct(Product product)
        {
            Console.WriteLine($"\nID: {product.Id}");
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"Pris: {product.Price:C}");
            Console.WriteLine($"Lager: {product.Stock}");
            Console.WriteLine($"Kategori: {product.Category}");
            Console.WriteLine($"Skapad: {product.Created}");
            if (product.LastUpdated.HasValue)
                Console.WriteLine($"Senast uppdaterad: {product.LastUpdated}");
            Console.WriteLine(new string('-', 40));
        }
    }
}
