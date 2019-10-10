using System.Collections.Generic;
using Dapper2.Models;
using Dapper2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dapper2.Controllers
{
  public class ProductController : ControllerBase
  {
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
      _productRepository = productRepository;
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<Product> GetById(int id)
    {
      var product = _productRepository.GetById(id);
      return Ok(product);
    }
    [HttpGet]
    public ActionResult<List<Product>> GetAll()
    {
      var product = _productRepository.GetAllProducts();
      return Ok(product);
    }
    [HttpPost]
    public ActionResult AddProduct(Product entity)
    {
      _productRepository.AddProduct(entity);
      return Ok(entity);
    }
    [HttpPut("{id}")]
    public ActionResult<Product> Update(Product entity, int id)
    {
      _productRepository.UpdateProduct(entity, id);
      return Ok(entity);
    }
    [HttpDelete("{id}")]
    public ActionResult<Product> Delete(int id)
    {
      _productRepository.RemoveProduct(id);
      return Ok();
    }
  }
}