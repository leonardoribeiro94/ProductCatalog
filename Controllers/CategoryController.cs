using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;


namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly StoreDataContext _context;

        public CategoryController(StoreDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("v1/categories")]
        public IEnumerable<Category> Get() =>
            _context.Categories.AsNoTracking().ToList();

        [HttpGet]
        [Route("v1/categories/{id}")]
        public Category GetById(int id) =>
            _context.Categories.AsNoTracking().FirstOrDefault(x => id.Equals(x.Id));

        [HttpGet]
        [Route("v1/categories/{id}/products")]
        public IEnumerable<Product> GetProductsByCategoryId(int id) =>
            _context.Products.AsNoTracking().Where(x => id.Equals(x.Category.Id));

        [HttpPost]
        [Route("v1/categories")]
        public Category Create([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        [HttpPut]
        [Route("v1/categories")]
        public Category Update([FromBody] Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();

            return category;
        }

        [HttpDelete]
        [Route("v1/categories")]
        public Category Delete([FromBody]Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return category;
        }

    }
}