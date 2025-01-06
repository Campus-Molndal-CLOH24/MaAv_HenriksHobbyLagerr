using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Repositories
{
    public class MongoDbRepository : IRepository<Product>
    {
        private readonly MongoDbContext _context;

        public MongoDbRepository()
        {
            _context = new MongoDbContext();
            _context.SeedData(); // Initialize databas med 46 produkter if empty
        }

        public IEnumerable<Product> GetAll() => _context.Products.Find(_ => true).ToList();

        public Product GetById(int id) => _context.Products.Find(p => p.Id == id).FirstOrDefault();

        public void Add(Product entity)
        {
            var maxId = _context.Products.AsQueryable().Max(p => p.Id);
            entity.Id = maxId + 1;
            _context.Products.InsertOne(entity);
        }

        public void Update(Product entity)
        {
            _context.Products.ReplaceOne(p => p.Id == entity.Id, entity);
        }

        public void Delete(int id) => _context.Products.DeleteOne(p => p.Id == id);

        public IEnumerable<Product> Search(Func<Product, bool> predicate)
        { 
            return _context.Products.AsQueryable().Where(predicate).ToList();
        }
    }
}
