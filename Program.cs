using EmployeeManagementSystem;
using System;

class Program
{
    static void Main(string[] args)
    {
        EmployeeService service = new EmployeeService();

        while (true)
        {
            Console.Clear();

            Console.WriteLine("EMPLOYEE MANAGEMENT SYSTEM");
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Update Employee Salary");
            Console.WriteLine("3. Delete Employee");
            Console.WriteLine("4. Find Employee by ID");
            Console.WriteLine("5. View All Employees");
            Console.WriteLine("6. Department Salary Report");
            Console.WriteLine("7. Exit");

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Department: ");
                    string dept = Console.ReadLine();

                    Console.Write("Salary: ");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());

                    service.AddEmployee(name, dept, salary);
                    break;

                case "2":
                    Console.Write("Employee ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("New Salary: ");
                    decimal newSalary = Convert.ToDecimal(Console.ReadLine());

                    service.UpdateEmployeeSalary(id, newSalary);
                    break;

                case "3":
                    Console.Write("Employee ID: ");
                    int deleteId = Convert.ToInt32(Console.ReadLine());

                    service.DeleteEmployee(deleteId);
                    break;

                case "4":
                    Console.Write("Employee ID: ");
                    int findId = Convert.ToInt32(Console.ReadLine());

                    service.GetEmployeeById(findId);
                    break;

                case "5":
                    service.GetAllEmployees();
                    break;

                case "6":
                    service.GetDepartmentSalaryReport();
                    break;

                case "7":
                    return;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}