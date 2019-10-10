namespace Dapper2.Services.Queries
{
  public interface ICommandText
  {
    string GetProducts { get; }
    string GetProductById { get; }
    string AddProduct { get; }
    string UpdateProduct { get; }
    string RemoveProduct { get; }
  }

}