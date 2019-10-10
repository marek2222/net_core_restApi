using System.Data.SqlClient;
using System.Text;
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
        StringBuilder sb = new StringBuilder();
        sb.Append("INSERT INTO dbo.Employee(first_name,last_name,address,home_phone,cell_phone) ");
        sb.Append("VALUES (@FirstName, @LastName, @Address, @HomePhone, @CellPhone)");
        conn.Execute(sb.ToString(),
          new { emp.FirstName, emp.LastName, emp.Address, emp.HomePhone, emp.CellPhone });
      }
    }

    public void Delete(int employeeId)
    {
      using (var conn = new SqlConnection(connString))
      {
        StringBuilder sb = new StringBuilder("DELETE FROM Employee WHERE id=@Id");
        conn.Execute(sb.ToString(),
            new { Id = employeeId });
      }
    }

    public void Update(Employee employee)
    {
      using (var conn = new SqlConnection(connString))
      {
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE Employee ");
        sb.Append("SET first_name=@FirstName, last_name=@LastName, address=@Address,");
        sb.Append("     home_phone=@HomePhone, cell_phone=@CellPhone ");
        sb.Append("WHERE id=@Id");
        conn.Execute(sb.ToString(),
            new { employee.FirstName, employee.LastName, employee.Address, employee.HomePhone, employee.CellPhone, employee.Id });
      }
    }
  }
}