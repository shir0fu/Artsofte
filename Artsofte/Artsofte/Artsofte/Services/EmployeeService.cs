using Microsoft.EntityFrameworkCore;
using Artsofte.Models;
using Artsofte.DTO;
using System.Data.Common;

namespace Artsofte.Services;

public interface IEmployeeService
{
    public List<EmployeeDTO> GetEmployees();
    public bool DeleteEmployee(int employeeId);
    public bool UpdateEmployee(Employee employee);
    public bool AddEmployee(EmployeeDTO employee);
}

public class EmployeeService : IEmployeeService
{
    ApplicationContext db;

    public EmployeeService(ApplicationContext context)
    {
        db = context;
    }

    public bool AddEmployee(EmployeeDTO employee)
    {

    }

    public bool DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }

    public List<EmployeeDTO> GetEmployees()
    {
        List<EmployeeDTO> employees = new List<EmployeeDTO>();
        var conn = db.Database.GetDbConnection();
        try
        {
            conn.Open();
            using (var command = conn.CreateCommand())
            {
                string query = "GetAllEmployees";
                command.CommandText = query;
                DbDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new EmployeeDTO { Name = reader.GetString(0), Surname = reader.GetString(1), Age = reader.GetInt32(2), Departament = reader.GetString(3), PogrammingLanguage = reader.GetString(4) };
                        employees.Add(row);
                    }
                }
                reader.Dispose();
            }
        }
        finally
        {
            conn.Close();
        }
        
        return employees;
    }

    public bool UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }
}
