using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModel;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("v1/products")]
        public IEnumerable<ListProductViewModel> Get() => _repository.Get();

        [HttpGet]
        [Route("v1/products/{id}")]
        public Product GetProductById(int id) => _repository.GetProductById(id);

        [HttpPost]
        [Route("v1/products")]
        public ResultViewModel Post([FromBody]EditorProductViewModel model)
        {

            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel(
                    false,
                    "Não foi possível cadastrar o produto",
                    model.Notifications);
            }

            var product = new Product
            {
                Title = model.Title,
                CategoryId = model.CategoryId,
                CreateDate = DateTime.Now,
                Description = model.Description,
                Image = model.Image,
                LasUpdateDate = DateTime.Now,
                Price = model.Price,
                Quantity = model.Quantity
            };

            _repository.Save(product);

            return new ResultViewModel
            (
                true,
                "Produto cadastrado com sucesso!",
                product
            );
        }

        [HttpPut]
        [Route("v1/products")]
        public ResultViewModel Put([FromBody]EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new ResultViewModel(
                    false,
                    "Não foi possível atualizar o produto",
                    model.Notifications);
            }

            var product = _repository.GetProductById(model.Id);
            product.Title = model.Title;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.Image = model.Image;
            product.LasUpdateDate = DateTime.Now;
            product.CategoryId = model.CategoryId;

            _repository.Update(product);

            return new ResultViewModel
            (
                true,
                "Produto atualizado com sucesso!",
                product
            );
        }
    }
}