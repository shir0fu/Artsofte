using Microsoft.EntityFrameworkCore;
using Artsofte.Models;
using Artsofte.DTO;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Artsofte.Services;

public interface IEmployeeService
{
    public Task<List<EmployeeDTO>> GetEmployeesAsync();
    public bool DeleteEmployee(int employeeId);
    public bool UpdateEmployee(Employee employee);
    public Task<bool> AddEmployeeAsync(CreateEmployeeDTO employee);
}

public class EmployeeService : IEmployeeService
{
    ApplicationContext db;

    public EmployeeService(ApplicationContext context)
    {
        db = context;
    }

    public async Task<bool> AddEmployeeAsync(CreateEmployeeDTO employee)
    {
        List<SqlParameter> parms = new List<SqlParameter>
        {
            new SqlParameter { ParameterName = "@name", Value = employee.Name },
            new SqlParameter { ParameterName = "@surName", Value = employee.Surname },
            new SqlParameter { ParameterName = "@age", Value = employee.Age },
            new SqlParameter { ParameterName = "@gender", Value = employee.Gender },
            new SqlParameter { ParameterName = "@deparnamentName", Value = employee.Departament },
            new SqlParameter { ParameterName = "@progLang", Value = employee.PogrammingLanguage }
        };
        try
        {
            var res = db.Database.ExecuteSqlRaw("EXEC AddEmployee @name, @surName, @age, @gender, @deparnamentName, @progLang", parms.ToArray());
            if (res == 1)
            {
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<EmployeeDTO>> GetEmployeesAsync()
    {
        List<EmployeeDTO> employees = new List<EmployeeDTO>();
        var conn = db.Database.GetDbConnection();
        try
        {
            await conn.OpenAsync();
            using (var command = conn.CreateCommand())
            {
                string query = "GetAllEmployees";
                command.CommandText = query;
                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new EmployeeDTO { Id = Convert.ToInt32(reader.GetString(0)), Name = reader.GetString(1), Surname = reader.GetString(2), Age = reader.GetInt32(3), Departament = reader.GetString(4), PogrammingLanguage = reader.GetString(5) };
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
