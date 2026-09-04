using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EmployeeManagementSystem
{
    public class EmployeeService
    {
        private readonly DatabaseConnection _db;

        public EmployeeService()
        {
            _db = new DatabaseConnection();
        }


        public void AddEmployee(string fullName, string department, decimal salary)
        {
            using (SqlConnection connection = _db.GetConnection())
            {
                string query = @"INSERT INTO Employee (FullName, Department, Salary)
                         VALUES (@FullName, @Department, @Salary)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Department", department);
                command.Parameters.AddWithValue("@Salary", salary);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} employee added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding employee:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void UpdateEmployeeSalary(int employeeId, decimal newSalary)
        {
            using (SqlConnection connection = _db.GetConnection())
            {
                string query = @"UPDATE Employee
                         SET Salary = @Salary
                         WHERE EmployeeId = @EmployeeId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Salary", newSalary);
                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Employee salary updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error updating employee salary:");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            using (SqlConnection connection = _db.GetConnection())
            {
                string query = @"DELETE FROM Employee
                         WHERE EmployeeId = @EmployeeId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Employee deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error deleting employee:");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void GetEmployeeById(int employeeId)
        {
            using (SqlConnection connection = _db.GetConnection())
            {
                string query = @"SELECT EmployeeId, FullName, Department, Salary, DateCreated
                         FROM Employee
                         WHERE EmployeeId = @EmployeeId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string id = reader["EmployeeId"].ToString();
                            string name = reader["FullName"].ToString();
                            string department = reader["Department"].ToString();
                            string salary = Convert.ToDecimal(reader["Salary"]).ToString("F2");
                            string dateCreated =
                                Convert.ToDateTime(reader["DateCreated"])
                                .ToString("dd/MM/yyyy HH:mm");

                            int idWidth = Math.Max(2, id.Length);
                            int nameWidth = Math.Max(4, name.Length);
                            int deptWidth = Math.Max(10, department.Length);
                            int salaryWidth = Math.Max(10, salary.Length);
                            int dateWidth = Math.Max(12, dateCreated.Length);

                            Console.WriteLine();

                            Console.WriteLine(
                                $"{"ID".PadRight(idWidth)}  " +
                                $"{"Name".PadRight(nameWidth)}  " +
                                $"{"Department".PadRight(deptWidth)}  " +
                                $"{"Salary".PadLeft(salaryWidth)}  " +
                                $"{"Date Created".PadRight(dateWidth)}"
                            );

                            int totalWidth =
                                idWidth +
                                nameWidth +
                                deptWidth +
                                salaryWidth +
                                dateWidth +
                                8;

                            Console.WriteLine(new string('-', totalWidth));

                            Console.WriteLine(
                                $"{id.PadRight(idWidth)}  " +
                                $"{name.PadRight(nameWidth)}  " +
                                $"{department.PadRight(deptWidth)}  " +
                                $"{salary.PadLeft(salaryWidth)}  " +
                                $"{dateCreated.PadRight(dateWidth)}"
                            );
                        }
                        else
                        {
                            Console.WriteLine("Employee not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving employee:");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void GetAllEmployees()
        {
            using (SqlConnection connection = _db.GetConnection())
            {
                string query = @"SELECT EmployeeId, FullName, Department, Salary
                         FROM Employee";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Store data first
                        var employees = new List<(int Id, string Name, string Dept, decimal Salary)>();

                        while (reader.Read())
                        {
                            employees.Add((
                                Convert.ToInt32(reader["EmployeeId"]),
                                reader["FullName"].ToString(),
                                reader["Department"].ToString(),
                                Convert.ToDecimal(reader["Salary"])
                            ));
                        }

                        // Calculate max widths
                        int idWidth = Math.Max(2, employees.Max(e => e.Id.ToString().Length));
                        int nameWidth = Math.Max(4, employees.Max(e => e.Name.Length));
                        int deptWidth = Math.Max(10, employees.Max(e => e.Dept.Length));
                        int salaryWidth = 12; // fixed for money

                        // Header
                        Console.WriteLine(
                            $"{"ID".PadRight(idWidth)}  {"Name".PadRight(nameWidth)}  {"Department".PadRight(deptWidth)}  {"Salary".PadLeft(salaryWidth)}"
                        );

                        Console.WriteLine(new string('-', idWidth + nameWidth + deptWidth + salaryWidth + 8));

                        // Rows
                        foreach (var e in employees)
                        {
                            Console.WriteLine(
                                $"{e.Id.ToString().PadRight(idWidth)}  {e.Name.PadRight(nameWidth)}  {e.Dept.PadRight(deptWidth)}  {e.Salary.ToString("F2").PadLeft(salaryWidth)}"
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error retrieving employees:");
                    Console.WriteLine(ex.Message);
                }
            }
        }


        public void GetDepartmentSalaryReport()
        {
            using (SqlConnection connection = _db.GetConnection())
            {
                string query = @"SELECT Department, SUM(Salary) AS TotalSalary
                         FROM Employee
                         GROUP BY Department";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(table);

                    if (table.Rows.Count == 0)
                    {
                        Console.WriteLine("No data found.");
                        return;
                    }

                    // Calculate dynamic widths
                    int deptWidth = Math.Max(
                        "Department".Length,
                        table.AsEnumerable().Max(r => r["Department"].ToString().Length)
                    );

                    int salaryWidth = Math.Max(
                        "Total Salary".Length,
                        table.AsEnumerable().Max(r =>
                            Convert.ToDecimal(r["TotalSalary"]).ToString("F2").Length)
                    );

                    // Header
                    Console.WriteLine();
                    Console.WriteLine(
                        $"{"Department".PadRight(deptWidth)}  {"Total Salary".PadLeft(salaryWidth)}"
                    );

                    Console.WriteLine(new string('-', deptWidth + salaryWidth + 2));

                    // Rows
                    foreach (DataRow row in table.Rows)
                    {
                        string dept = row["Department"].ToString();
                        string totalSalary = Convert.ToDecimal(row["TotalSalary"]).ToString("F2");

                        Console.WriteLine(
                            $"{dept.PadRight(deptWidth)}  {totalSalary.PadLeft(salaryWidth)}"
                        );
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error generating report:");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

}