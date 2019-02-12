using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModel;

namespace ProductCatalog.Repositories
{
    public class ProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }


        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
             .Include(x => x.Category)
             .Select(x => new ListProductViewModel
             {
                 Id = x.Id,
                 Title = x.Title,
                 Price = x.Price,
                 Category = x.Category.Title,
                 CategoryId = x.CategoryId
             })
             .AsNoTracking()
             .ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                      .FirstOrDefault(x => id.Equals(x.Id));
        }

        public void Save(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}