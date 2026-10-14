# Employee Management System

A .NET 8 Console Application for managing employees and departments using ADO.NET and SQL Server.

## Technologies Used

- C#
- .NET 8
- SQL Server
- ADO.NET
- Microsoft.Data.SqlClient
- Visual Studio 2022

## Features

1. Add Employee
2. View All Employees
3. View Employee By ID
4. Update Employee
5. Delete Employee
6. Search Employees
7. View Employees By Department
8. View Departments
9. Exit

## Validation

- Employee name is mandatory.
- Email is mandatory and must have a valid format.
- Email must be unique.
- Salary cannot be negative.
- Department must exist.
- Joining date must be valid.
- Employee update prevents duplicate emails.

## Delete Employee

The application uses **Soft Delete**.

Instead of permanently deleting an employee, the `IsActive` column is changed from `1` to `0`.

Inactive employees are excluded from normal employee listings and searches.

## Database

Database name:

`EmployeeManagementDB`

Tables:

- Departments
- Employees

The database creation and sample data script is provided separately as:

`EmployeeManagementDB.sql`

## Database Operations

The application uses:

- ADO.NET
- SQL Server Stored Procedures
- Parameterized SQL
- Async/Await
- SQL JOINs
- Repository pattern
- Service layer
- Exception handling

The main employee operations use stored procedures for:

- Adding employees
- Viewing all employees
- Viewing an employee by ID
- Updating employees
- Soft deleting employees
- Searching employees
- Viewing employees by department

Entity Framework / EF Core is not used.

## Project Structure

```text
Employee_Management_System
│
├── Models
│   ├── Employee.cs
│   └── Department.cs
│
├── Data
│   ├── DatabaseConnection.cs
│   ├── EmployeeRepository.cs
│   └── DepartmentRepository.cs
│
├── Services
│   ├── EmployeeService.cs
│   └── DepartmentService.cs
│
├── Helpers
│   └── ValidationHelper.cs
│
├── Program.cs
└── README.md

## How to Run

1. Open the project in Visual Studio 2022.
2. Make sure SQL Server is running.
3. Execute `EmployeeManagementDB.sql` in SQL Server Management Studio.
4. Verify the connection string in `DatabaseConnection.cs`.
5. Build the project.
6. Run the application.

## Connection

The application uses Windows Authentication to connect to SQL Server.

Update the server name in `DatabaseConnection.cs` if required for another computer.

## Exception Handling

The application includes exception handling to prevent unexpected database or runtime errors from terminating the application without an error message.

## Author

Nirbhay Patil

