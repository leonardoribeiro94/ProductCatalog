using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories;

namespace ProductCatalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("v1/categories")]
        [ResponseCache(Duration = 5)] // Cache-Control: public, max-age: 60
        public IEnumerable<Category> Get() =>
            _repository.Get();

        [HttpGet]
        [Route("v1/categories/{id}")]
        public Category GetById(int id) =>
            _repository.GetById(id);

        [HttpGet]
        [Route("v1/categories/{id}/products")]
        public IEnumerable<Product> GetProductsByCategoryId(int id) =>
            _repository.GetProductsByCategoryId(id);

        [HttpPost]
        [Route("v1/categories")]
        public Category Create([FromBody] Category category)
        {
            _repository.Save(category);

            return category;
        }

        [HttpPut]
        [Route("v1/categories")]
        public Category Update([FromBody] Category category)
        {
            _repository.Update(category);

            return category;
        }

        [HttpDelete]
        [Route("v1/categories")]
        public Category Delete([FromBody]Category category)
        {
            _repository.Delete(category);

            return category;
        }

    }
}