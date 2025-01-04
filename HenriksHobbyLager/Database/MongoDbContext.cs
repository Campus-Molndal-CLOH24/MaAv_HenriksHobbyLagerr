using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb+srv://avinashbodduluri:0143@cluster0.zgftr.mongodb.net/");
            _database = client.GetDatabase("ProductInventory");
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");

        public void SeedData()
        {
            if (!Products.AsQueryable().Any())
            {
                Products.InsertMany(new List<Product>
                {
                new Product
                {
                    Id = 1,
                    Name = "Smallcar",
                    Price = 65,
                    Stock = 1,
                    Category = "Bilar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 2,
                    Name = "SUV",
                    Price = 513.06M,
                    Stock = 17,
                    Category = "Bilar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 3,
                    Name = "Truck",
                    Price = 1000,
                    Stock = 3,
                    Category = "Bilar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 4,
                    Name = "Doll",
                    Price = 100,
                    Stock = 5,
                    Category = "Dockor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 5,
                    Name = "Teddybear",
                    Price = 150,
                    Stock = 2,
                    Category = "Björnar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 6,
                    Name = "Lego",
                    Price = 200,
                    Stock = 10,
                    Category = "Leksaker",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 7,
                    Name = "Puzzle",
                    Price = 50,
                    Stock = 4,
                    Category = "Pussel",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 8,
                    Name = "Game",
                    Price = 300,
                    Stock = 6,
                    Category = "Spel",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 9,
                    Name = "Book",
                    Price = 150,
                    Stock = 8,
                    Category = "Böcker",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 10,
                    Name = "Paint",
                    Price = 200,
                    Stock = 3,
                    Category = "Målarböcker",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 11,
                    Name = "Pencil",
                    Price = 10,
                    Stock = 20,
                    Category = "Pennor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 12,
                    Name = "Paper",
                    Price = 20,
                    Stock = 30,
                    Category = "Papper",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 13,
                    Name = "Scissors",
                    Price = 30,
                    Stock = 10,
                    Category = "Saxar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 14,
                    Name = "Glue",
                    Price = 40,
                    Stock = 5,
                    Category = "Lim",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 15,
                    Name = "Ruler",
                    Price = 15,
                    Stock = 10,
                    Category = "Linjaler",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 16,
                    Name = "Eraser",
                    Price = 5,
                    Stock = 20,
                    Category = "Suddgummin",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 17,
                    Name = "Backpack",
                    Price = 100,
                    Stock = 10,
                    Category = "Ryggsäckar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 18,
                    Name = "Lunchbox",
                    Price = 50,
                    Stock = 15,
                    Category = "Lunchlådor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 19,
                    Name = "Waterbottle",
                    Price = 30,
                    Stock = 20,
                    Category = "Vattenflaskor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 20,
                    Name = "Thermos",
                    Price = 70,
                    Stock = 5,
                    Category = "Termosar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 21,
                    Name = "Umbrella",
                    Price = 80,
                    Stock = 10,
                    Category = "Paraplyer",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 22,
                    Name = "Raincoat",
                    Price = 150,
                    Stock = 5,
                    Category = "Regnkläder",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 23,
                    Name = "Boots",
                    Price = 200,
                    Stock = 3,
                    Category = "Stövlar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 24,
                    Name = "Shoes",
                    Price = 100,
                    Stock = 10,
                    Category = "Skor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 25,
                    Name = "Socks",
                    Price = 20,
                    Stock = 30,
                    Category = "Strumpor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 26,
                    Name = "Underwear",
                    Price = 50,
                    Stock = 20,
                    Category = "Underkläder",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 27,
                    Name = "T-shirt",
                    Price = 100,
                    Stock = 10,
                    Category = "T-shirts",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 28,
                    Name = "Pants",
                    Price = 150,
                    Stock = 5,
                    Category = "Byxor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 29,
                    Name = "Dress",
                    Price = 200,
                    Stock = 3,
                    Category = "Klänningar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 30,
                    Name = "Jacket",
                    Price = 300,
                    Stock = 10,
                    Category = "Jackor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 31,
                    Name = "Hat",
                    Price = 50,
                    Stock = 20,
                    Category = "Hattar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 32,
                    Name = "Gloves",
                    Price = 30,
                    Stock = 10,
                    Category = "Handskar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 33,
                    Name = "Scarf",
                    Price = 40,
                    Stock = 5,
                    Category = "Scarfs",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 34,
                    Name = "Belt",
                    Price = 20,
                    Stock = 30,
                    Category = "Bälten",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 35,
                    Name = "Sunglasses",
                    Price = 100,
                    Stock = 10,
                    Category = "Solglasögon",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 36,
                    Name = "Watch",
                    Price = 150,
                    Stock = 5,
                    Category = "Klockor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 37,
                    Name = "Necklace",
                    Price = 200,
                    Stock = 3,
                    Category = "Halsband",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 38,
                    Name = "Bracelet",
                    Price = 300,
                    Stock = 10,
                    Category = "Armband",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 39,
                    Name = "Earrings",
                    Price = 50,
                    Stock = 20,
                    Category = "Örhängen",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 40,
                    Name = "Ring",
                    Price = 30,
                    Stock = 10,
                    Category = "Ringar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 41,
                    Name = "Bag",
                    Price = 40,
                    Stock = 5,
                    Category = "Väskor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 42,
                    Name = "Wallet",
                    Price = 20,
                    Stock = 30,
                    Category = "Plånböcker",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 43,
                    Name = "Keychain",
                    Price = 100,
                    Stock = 10,
                    Category = "Nyckelringar",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 44,
                    Name = "Notebook",
                    Price = 150,
                    Stock = 5,
                    Category = "Anteckningsböcker",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 45,
                    Name = "Pen",
                    Price = 200,
                    Stock = 3,
                    Category = "Pennor",
                    Created = DateTime.Now
                },
                new Product
                {
                    Id = 46,
                    Name = "Calendar",
                    Price = 300,
                    Stock = 10,
                    Category = "Kalendrar",
                    Created = DateTime.Now
                }
            });
            }
        }
    }
}
