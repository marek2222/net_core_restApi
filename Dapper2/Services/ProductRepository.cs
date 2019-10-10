using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper2.Models;
using Dapper2.Services.Queries;
using Microsoft.Extensions.Configuration;

namespace Dapper2.Services
{
  public class ProductRepository : IProductRepository
  {
    private readonly ICommandText _cmd;
    private readonly string _connStr;
    public ProductRepository(IConfiguration config, ICommandText cmd)
    {
      _cmd = cmd;
      _connStr = config.GetConnectionString("DefaultConnection");
    }


    public List<Product> GetAllProducts()
    {
      // using (var conn = new SqlConnection(connStr))
      // {
      //     conn.Open();
      //     task(conn => conn.Query<Product>(_cmd.GetAllProducts)).ToList;
      // }
      var query = ExecuteCommand(_connStr, conn => conn.Query<Product>(_cmd.GetAllProducts)).ToList();
      return query;
    }

    public Product GetById(int id)
    {
      var product = ExecuteCommand<Product>(_connStr, conn =>
          conn.Query<Product>(_commandText.GetProductById, new { @Id = id }).SingleOrDefault());
      return product;
    }
    public void AddProduct(Product entity)
    {
      ExecuteCommand(_connStr, conn =>
      {
        var query = conn.Query<Product>(_commandText.AddProduct,
            new { Name = entity.Name, Cost = entity.Cost, CreatedDate = entity.CreatedDate });
      });
    }
    public void UpdateProduct(Product entity, int id)
    {
      ExecuteCommand(_connStr, conn =>
      {
        var query = conn.Query<Product>(_commandText.UpdateProduct,
                  new { Name = entity.Name, Cost = entity.Cost, Id = id });
      });
    }

    public void RemoveProduct(int id)
    {
      ExecuteCommand(_connStr, conn =>
      {
        var query = conn.Query<Product>(_commandText.RemoveProduct, new { Id = id });
      });
    }


    #region Helpers

    private void ExecuteCommand(string connStr, Action<SqlConnection> task)
    {
      using (var conn = new SqlConnection(connStr))
      {
        conn.Open();

        task(conn);
      }
    }
    private T ExecuteCommand<T>(string connStr, Func<SqlConnection, T> task)
    {
      using (var conn = new SqlConnection(connStr))
      {
        conn.Open();

        return task(conn);
      }
    }
    #endregion
  }
}