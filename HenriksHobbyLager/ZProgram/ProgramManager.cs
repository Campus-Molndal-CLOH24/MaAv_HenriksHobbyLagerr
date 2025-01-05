using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using HenriksHobbyLager.Repositories;
using System;

namespace HenriksHobbyLager.ProgramManager
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
                    return;  // Avslutar programmet
                default:
                    Console.WriteLine("Invalid choice! Please select 1 for SQLite or 2 for MongoDB:");
                    break;
            }
        }

        // Metoden för att köra själva applikationen.
        static void RunApplication(IRepository<Product> repository)
        {
            Console.WriteLine("Running application with repository: " + repository.GetType().Name);
            // Här kan du lägga till din meny och logik för att hantera produkter.
        }
    }
}
