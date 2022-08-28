using Microsoft.EntityFrameworkCore;
using Artsofte.Models;
using Artsofte.DTO;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Artsofte.Services;

public interface IEmployeeService
{
    public Task<UpdateEmployeeDTO> GetEmployeeByIdAsync(int id);
    public Task<List<EmployeeDTO>> GetEmployeesAsync();
    public Task<bool> DeleteEmployeeAsync(int employeeId);
    public Task<bool> UpdateEmployeeAsync(UpdateEmployeeDTO employee);
    public Task<bool> AddEmployeeAsync(CreateEmployeeDTO employee);
}

public class EmployeeService : IEmployeeService
{
    ApplicationContext db;

    public EmployeeService(ApplicationContext context)
    {
        db = context;
    }

    public async Task<UpdateEmployeeDTO> GetEmployeeByIdAsync(int id)
    {
        UpdateEmployeeDTO employee = new UpdateEmployeeDTO();
        var conn = db.Database.GetDbConnection();
        try
        {
            await conn.OpenAsync();
            using (var command = conn.CreateCommand())
            {
                string query = $"GetEmployeeById @identy = {id}";
                command.CommandText = query;
                DbDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new UpdateEmployeeDTO { Id = reader.GetInt32(0), Name = reader.GetString(1), Surname = reader.GetString(2), Age = reader.GetInt32(3), Gender = reader.GetString(4), Departament = reader.GetString(5), PogrammingLanguage = reader.GetString(6) };
                        employee = row;
                    }
                }
                reader.Dispose();
            }
        }
        finally
        {
            conn.Close();
        }

        return employee;
    }
    public async Task<bool> AddEmployeeAsync(CreateEmployeeDTO employee)
    {
        Type check = employee.Age.GetType();
        if (!check.Equals(typeof(int)))
        {
            return false;
        }

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

    public async Task<bool> DeleteEmployeeAsync(int employeeId)
    {
        SqlParameter sqlParameter = new SqlParameter { ParameterName = "@identy", Value = employeeId };
        try
        {
            var res = db.Database.ExecuteSqlRaw("EXEC DeleteEmployee @identy", sqlParameter);
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
                        var row = new EmployeeDTO { Id = reader.GetInt32(0), Name = reader.GetString(1), Surname = reader.GetString(2), Age = reader.GetInt32(3), Departament = reader.GetString(4), PogrammingLanguage = reader.GetString(5) };
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

    public async Task<bool> UpdateEmployeeAsync(UpdateEmployeeDTO employee)
    {

        List<SqlParameter> parms = new List<SqlParameter>
        {
            new SqlParameter { ParameterName = "@identy", Value = employee.Id },
            new SqlParameter { ParameterName = "@name", Value = employee.Name },
            new SqlParameter { ParameterName = "@surName", Value = employee.Surname },
            new SqlParameter { ParameterName = "@age", Value = employee.Age },
            new SqlParameter { ParameterName = "@gender", Value = employee.Gender },
            new SqlParameter { ParameterName = "@deparnamentName", Value = employee.Departament },
            new SqlParameter { ParameterName = "@progLang", Value = employee.PogrammingLanguage }
        };
        try
        {
            var res = db.Database.ExecuteSqlRaw("EXEC UpdateEmployee @identy, @name, @surName, @age, @gender, @deparnamentName, @progLang", parms.ToArray());
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

}
