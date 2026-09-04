using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace EmployeeManagementSystem
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string not found.");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}