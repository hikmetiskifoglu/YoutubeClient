using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YoutubeApi.Entities;

namespace YoutubeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly YoutubeContext _context;
        public ProductsController(YoutubeContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Products.ToList());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var updateProduct = _context.Products.FirstOrDefault(I => I.Id == id);
            updateProduct.Name = product.Name;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetProductByID(int id)
        {
            return Ok(_context.Products.FirstOrDefault(I => I.Id == id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var deletedProduct = _context.Products.FirstOrDefault(I => I.Id == id);
            _context.Remove(deletedProduct);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return Created("", product);
        }
    }
}
