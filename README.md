# Employee Management System (ADO.NET)

A console-based Employee Management System built with C# and ADO.NET.  
This project demonstrates CRUD operations, SQL Server integration, and clean database handling using parameterized queries.

## Features

- Add Employee
- Update Employee Salary
- Delete Employee
- Find Employee by ID
- View All Employees (formatted table output)
- Department Salary Report (grouped data)

## Tech Stack

- C# (.NET 8)
- ADO.NET
- SQL Server
- Console Application

## Concepts Demonstrated

- SqlConnection
- SqlCommand
- SqlDataReader
- SqlDataAdapter
- DataTable
- Parameterized Queries (SQL Injection Prevention)
- Exception Handling
- Clean Code Structure

## Database Setup

Run this SQL:

```sql
CREATE DATABASE CompanyDB;
GO

USE CompanyDB;
GO

CREATE TABLE Employee (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100),
    Department NVARCHAR(50),
    Salary DECIMAL(18,2),
    DateCreated DATETIME DEFAULT GETDATE()
);