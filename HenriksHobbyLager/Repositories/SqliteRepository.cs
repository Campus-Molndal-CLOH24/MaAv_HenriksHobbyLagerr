using HenriksHobbyLager.Database;
using HenriksHobbyLager.Interfaces;
using HenriksHobbyLager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenriksHobbyLager.Repositories
{
    public class SqliteRepository : IRepository<Product>
    {
        private readonly AppDbContext _context;

        public SqliteRepository()
        {
            _context = new AppDbContext();
            _context.Database.EnsureCreated();
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAll() => _context.Products.ToList();

        public Product GetById(int id) => _context.Products.Find(id);

        public void Add(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> Search(Func<Product, bool> predicate)
        {
            return _context.Products.Where(predicate).ToList();
        }
    }
}
