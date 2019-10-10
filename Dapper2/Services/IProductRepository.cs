using System.Collections.Generic;
using Dapper2.Models;

namespace Dapper2.Services
{
  public interface IProductRepository
  {
    Product GetById(int id);
    void AddProduct(Product entity);
    void UpdateProduct(Product entity, int id);
    void RemoveProduct(int id);
    List<Product> GetAllProducts();
  }
}