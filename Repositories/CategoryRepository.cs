using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Get() =>
            _context.Categories.AsNoTracking().ToList();

        public Category GetById(int id) =>
            _context.Categories.AsNoTracking().FirstOrDefault(x => id.Equals(x.Id));


        public IEnumerable<Product> GetProductsByCategoryId(int id) =>
            _context.Products.AsNoTracking().Where(x => id.Equals(x.Category.Id));


        public void Save(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}