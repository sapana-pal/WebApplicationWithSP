using System.Data;
using Microsoft.Data.SqlClient;
using WebApplicationWithSP.Models;

namespace WebApplicationWithSP.Data
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int AddEmployee(Employee employee)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_AddEmployee", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Position", employee.Position);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new();
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_GetEmployees", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                    Name = reader["Name"].ToString(),
                    Position = reader["Position"].ToString(),
                    Salary = Convert.ToDecimal(reader["Salary"])
                });
            }
            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_GetEmployeeById", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", id);

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Employee
                {
                    EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                    Name = reader["Name"].ToString(),
                    Position = reader["Position"].ToString(),
                    Salary = Convert.ToDecimal(reader["Salary"])
                };
            }
            return null;
        }

        public void UpdateEmployee(Employee employee)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_UpdateEmployee", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Position", employee.Position);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployee(int id)
        {
            using SqlConnection con = new(_connectionString);
            using SqlCommand cmd = new("sp_DeleteEmployee", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@EmployeeID", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }



}
