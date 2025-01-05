using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HenriksHobbyLager
{

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
            Console.WriteLine("Select Database:");
            Console.WriteLine("1. SQLite");
            Console.WriteLine("2. MongoDB");
            Console.WriteLine("3. Exit");
            var choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    Console.WriteLine("SQLite selected");
                    RunApplication(new SqliteRepository());
                    break;
                case "2":
                    Console.WriteLine("MongoDB selected");
                    RunApplication(new MongoDbRepository());
                    break;
                case "3":
                    Console.WriteLine("Application closed");
                    return;
                default:
                    Console.WriteLine("Invalid choice! Please select 1 for SQLite or 2 for MongoDB:");
                    break;
            }
        }
        static void RunApplication(IRepository<Product> repository)
        {
            // Huvudloopen - Stäng inte av programmet, då försvinner allt!
            while (true)
            {
                Console.Clear();  // Rensar skärmen så det ser proffsigt ut
                Console.WriteLine("=== Henriks HobbyLager™ 2.0 ===");
                Console.WriteLine("1. Visa alla produkter");
                Console.WriteLine("2. Lägg till produkt");
                Console.WriteLine("3. Uppdatera produkt");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("5. Sök produkter");
                Console.WriteLine("6. Avsluta");  // Använd inte denna om du vill behålla datan!
                
                var choice = Console.ReadLine();

                // Switch är tydligen bättre än if-else enligt Google
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
                        return;  // OBS! All data försvinner om du väljer denna!
                    default:
                        Console.WriteLine("Ogiltigt val! Är du säker på att du tryckte på rätt knapp?");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta... (helst inte ESC)");
                Console.ReadKey();
            }
        }

        // Visar alla produkter som finns i "databasen"
        private static void ShowAllProducts(IRepository<Product> _repository)
        {
            Console.WriteLine("=== Alla produkter i lagret ===");
            var products = _repository.GetAll();

            if (!products.Any())
            {
                Console.WriteLine("Inga produkter finns i lagret. Dags att shoppa grossist!");
                return;
            }
            else
            {
                foreach (var product in products)
                {
                    DisplayProduct(product);
                }
            }
        }

        // Lägger till en ny produkt i systemet
        private static void AddProduct(IRepository<Product> _repository)
        {
            Console.WriteLine("=== Lägg till ny produkt ===");
            Console.Write("Namn: ");
            var name = Console.ReadLine();
            while(true)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Namnet får inte vara tomt! Försök igen.");
                    Console.Write("Namn: ");
                    name = Console.ReadLine();
                }
                else
                {
                    break;
                }
            }
            Console.Write("Pris: ");
            decimal price;
            if (decimal.TryParse(Console.ReadLine(), out decimal priceinput))
            {
                if(priceinput <=0)
                {
                    Console.WriteLine("Priset får inte vara 0 eller negativt! Försök igen.");
                    return;
                }
                price = priceinput;
            }
            else
            {
                Console.WriteLine("Ogiltigt pris! Använd punkt istället för komma (lärde mig den hårda vägen)");
                Console.Write("Pris: ");
                return;
            }

            Console.Write("Antal i lager: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Ogiltig lagermängd! Hela tal endast (kan inte sälja halva helikoptrar)");
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
                Created = DateTime.Now  // Automatiskt datum, smooth!
            };

            _repository.Add(product);
            Console.WriteLine("Produkt tillagd!");
        }

        // Uppdaterar en befintlig produkt
        private static void UpdateProduct(IRepository<Product> _repository)
        {
            Console.Write("Ange produkt-ID att uppdatera (finns i listan ovan): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror tack!");
                return;
            }

            var product = _repository.GetById(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte! Är du säker på att du skrev rätt?");
                return;
            }

            // Uppdatera bara det som användaren faktiskt skriver in
            Console.Write("Nytt namn (tryck bara enter om du vill behålla det gamla): ");
            var name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name))
                product.Name = name;

            Console.Write("Nytt pris (tryck bara enter om du vill behålla det gamla): ");
            var priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price))
            {
                if (price <= 0)
                {
                    Console.WriteLine("Priset får inte vara 0 eller negativt! Försök igen.");
                    return;
                }
                product.Price = price;
            }

            Console.Write("Ny lagermängd (tryck bara enter om du vill behålla den gamla): ");
            var stockInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(stockInput) && int.TryParse(stockInput, out int stock))
                product.Stock = stock;

            Console.Write("Ny kategori (tryck bara enter om du vill behålla den gamla): ");
            var category = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(category))
                product.Category = category;

            product.LastUpdated = DateTime.Now;  // Håller koll på när saker ändras
            _repository.Update(product);
            Console.WriteLine("Produkt uppdaterad! Stäng fortfarande inte av datorn!");
        }

        // Ta bort en produkt (använd med försiktighet!)
        private static void DeleteProduct(IRepository<Product> _repository)
        {
            Console.Write("Ange produkt-ID att ta bort (dubbel-check att det är rätt, går inte att ångra!): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Ogiltigt ID! Bara siffror är tillåtna här.");
                return;
            }

            var product = _repository.GetById(id);
            if (product == null)
            {
                Console.WriteLine("Produkt hittades inte! Puh, inget blev raderat av misstag!");
                return;
            }

            _repository.Delete(id);
            Console.WriteLine("Produkt borttagen! (Hoppas det var meningen)");
        }

        // Sökfunktion - Min stolthet! Söker i både namn och kategori
        private static void SearchProducts(IRepository<Product> _repository)
        {
            Console.Write("Sök (namn eller kategori - versaler spelar ingen roll!): ");
            var searchTerm = Console.ReadLine()?.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Söktermen får inte vara tom! Försök igen.");
                return;
            }
            var results = _repository.Search(p => (p.Name?.ToLower().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false) || (p.Category?.ToLower().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ?? false));

            if (!results.Any())
            {
                Console.WriteLine("Inga produkter matchade sökningen. Prova med något annat!");
                return;
            }
            foreach (var product in results)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Category: {product.Category}, Price: {product.Price:C}");
            }
        }

        // Visar en enskild produkt snyggt formaterat
        private static void DisplayProduct(Product product)
        {
            // Snygga streck som separerar produkterna
            Console.WriteLine($"\nID: {product.Id}");
            Console.WriteLine($"Namn: {product.Name}");
            Console.WriteLine($"Pris: {product.Price:C}");  // :C gör att det blir kronor automatiskt!
            Console.WriteLine($"Lager: {product.Stock}");
            Console.WriteLine($"Kategori: {product.Category}");
            Console.WriteLine($"Skapad: {product.Created}");
            if (product.LastUpdated.HasValue)  // Kollar om produkten har uppdaterats någon gång
                Console.WriteLine($"Senast uppdaterad: {product.LastUpdated}");
            Console.WriteLine(new string('-', 40));  // Snyggt streck mellan produkterna
        }
    }
}