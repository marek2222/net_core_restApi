using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace DapperTime.DATA
{

  public class Employee
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string HomePhone { get; set; }
    public string CellPhone { get; set; }
  }

  public interface IEmployeeProvider
  {
    IEnumerable<Employee> Get();
  }

  public class EmployeeProvider : IEmployeeProvider
  {
    private readonly string connectionString;

    public EmployeeProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public IEnumerable<Employee> Get()
    {
      IEnumerable<Employee> employee = null;

      using (var connection = new SqlConnection(connectionString))
      {
        employee = connection.Query<Employee>("select id, first_name as FirstName, last_name as LastName, address, home_phone as HomePhone, cell_phone as CellPhone from Employee");
      }

      return employee;
    }
  }
}