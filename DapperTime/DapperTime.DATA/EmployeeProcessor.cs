using System.Data.SqlClient;
using Dapper;

namespace DapperTime.DATA
{
  public interface IEmployeeProcessor
  {
    void Create(Employee employee);
    void Update(Employee employee);
    void Delete(int employeeId);
  }

  class EmployeeProcessor : IEmployeeProcessor
  {
    private readonly string connString;

    public EmployeeProcessor(string connString)
    {
      this.connString = connString;
    }
 
    public void Create(Employee emp)
    {
      using (var conn = new SqlConnection(connString))
      {
        // connection.Execute("INSERT INTO Employee (first_name, last_name, address, home_phone, cell_phone) VALUES (@FirstName, @LastName, @Address, @HomePhone, @CellPhone)",
        // new { employee.FirstName, employee.LastName, employee.Address, employee.HomePhone, employee.CellPhone });
        conn.Execute("INSERT INTO dbo.Employee(first_name,last_name,address,home_phone,cell_phone) VALUES (@FirstName, @LastName, @Address, @HomePhone, @CellPhone)",
                      new { emp.FirstName, emp.LastName, emp.Address, emp.HomePhone, emp.CellPhone });          
      }
    }
    
    public void Delete(int employeeId)
    {
      throw new System.NotImplementedException();
    }

    public void Update(Employee employee)
    {
      throw new System.NotImplementedException();
    }
  }
}