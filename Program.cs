using Employee_Management_System.Data;
using Employee_Management_System.Helpers;
using Employee_Management_System.Models;
using Employee_Management_System.Services;

DatabaseConnection databaseConnection = new DatabaseConnection();
EmployeeRepository employeeRepository =
    new EmployeeRepository(databaseConnection);
DepartmentRepository departmentRepository =
    new DepartmentRepository(databaseConnection);
EmployeeService employeeService =new EmployeeService(employeeRepository);
DepartmentService departmentService =
    new DepartmentService(departmentRepository);

Console.WriteLine("=================================");
Console.WriteLine("   EMPLOYEE MANAGEMENT SYSTEM");
Console.WriteLine("=================================");
try
{
    while (true)
    {
        Console.Clear();+
027
        Console.WriteLine("=================================");
        Console.WriteLine("   EMPLOYEE MANAGEMENT SYSTEM");
        Console.WriteLine("=================================");
        Console.WriteLine("1. Add Employee");
        Console.WriteLine("2. View All Employees");
        Console.WriteLine("3. View Employee By Id");
        Console.WriteLine("4. Update Employee");
        Console.WriteLine("5. Delete Employee");
        Console.WriteLine("6. Search Employees");
        Console.WriteLine("7. View Employees By Department");
        Console.WriteLine("8. View Departments");
        Console.WriteLine("9. Exit");

        Console.WriteLine();
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        if (choice == "9")
        {
            Console.WriteLine("Thank you!");
            break;
        }
        if (choice == "1")
        {
            Console.Clear();


            Console.WriteLine("========== ADD EMPLOYEE ==========");

            Console.Write("Enter Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name is required.");
                Console.ReadLine();
                continue;
            }

            Console.Write("Enter Email: ");
            string? email = Console.ReadLine();

            if (!ValidationHelper.IsValidEmail(email ?? ""))
            {
                Console.WriteLine("Please enter a valid email.");
                Console.ReadLine();
                continue;
            }

            if (await employeeService.EmailExistsAsync(email!))
            {
                Console.WriteLine("Email already exists.");
                Console.ReadLine();
                continue;
            }

            Console.Write("Enter Salary: ");
            string? salaryInput = Console.ReadLine();

            if (!decimal.TryParse(salaryInput, out decimal salary) ||
                !ValidationHelper.IsValidSalary(salary))
            {
                Console.WriteLine("Please enter a valid salary.");
                Console.ReadLine();
                continue;
            }
            Console.WriteLine();
            Console.WriteLine("Available Departments:");

            List<Department> departments =
                await departmentService.GetAllDepartmentsAsync();

            foreach (Department department in departments)
            {
                Console.WriteLine($"{department.Id} - {department.Name}");
            }

            Console.Write("Enter Department ID: ");
            string? departmentInput = Console.ReadLine();

            if (!int.TryParse(departmentInput, out int departmentId))
            {
                Console.WriteLine("Invalid Department ID.");
                Console.ReadLine();
                continue;
            }

            if (!await departmentService.DepartmentExistsAsync(departmentId))
            {
                Console.WriteLine("Department does not exist.");
                Console.ReadLine();
                continue;
            }
            Console.Write("Enter Joining Date (yyyy-MM-dd): ");
            string? joiningDateInput = Console.ReadLine();

            if (!DateTime.TryParse(joiningDateInput, out DateTime joiningDate))
            {
                Console.WriteLine("Invalid joining date.");
                Console.ReadLine();
                continue;
            }

            Employee employee = new Employee
            {
                Name = name,
                Email = email!,
                Salary = salary,
                DepartmentId = departmentId,
                JoiningDate = joiningDate,
                IsActive = true
            };
            int employeeId = await employeeService.AddEmployeeAsync(employee);

            Console.WriteLine();
            Console.WriteLine($"Employee added successfully. Employee ID: {employeeId}");

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }
        if (choice == "2")
        {
            Console.Clear();

            Console.WriteLine("========== ALL EMPLOYEES ==========");

            List<Employee> employees =
                await employeeService.GetAllEmployeesAsync();

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                foreach (Employee employee in employees)
                {
                    Console.WriteLine(
                        $"ID: {employee.Id} | " +
                        $"Name: {employee.Name} | " +
                        $"Email: {employee.Email} | " +
                        $"Salary: {employee.Salary} | " +
                        $"Department: {employee.DepartmentName} | " +
                        $"Joining Date: {employee.JoiningDate:yyyy-MM-dd}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }
        if (choice == "3")
        {
            Console.Clear();

            Console.WriteLine("========== VIEW EMPLOYEE ==========");

            Console.Write("Enter Employee ID: ");
            string? idInput = Console.ReadLine();

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Invalid Employee ID.");
                Console.ReadLine();
                continue;
            }

            Employee? employee = await employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
            }
            else
            {
                Console.WriteLine($"ID: {employee.Id}");
                Console.WriteLine($"Name: {employee.Name}");
                Console.WriteLine($"Email: {employee.Email}");
                Console.WriteLine($"Salary: {employee.Salary}");
                Console.WriteLine($"Department: {employee.DepartmentName}");
                Console.WriteLine($"Joining Date: {employee.JoiningDate:yyyy-MM-dd}");
                Console.WriteLine($"Active: {employee.IsActive}");
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }
        if (choice == "7")
        {
            Console.Clear();

            Console.WriteLine("========== EMPLOYEES BY DEPARTMENT ==========");

            Console.WriteLine();
            Console.WriteLine("Available Departments:");

            List<Department> departments =
                await departmentService.GetAllDepartmentsAsync();

            foreach (Department department in departments)
            {
                Console.WriteLine($"{department.Id} - {department.Name}");
            }
            Console.WriteLine();
            Console.Write("Enter Department ID: ");
            string? departmentInput = Console.ReadLine();

            if (!int.TryParse(departmentInput, out int departmentId))
            {
                Console.WriteLine("Invalid Department ID.");
                Console.ReadLine();
                continue;
            }

            if (!await departmentService.DepartmentExistsAsync(departmentId))
            {
                Console.WriteLine("Department does not exist.");
                Console.ReadLine();
                continue;
            }

            List<Employee> employees =
                await employeeService.GetEmployeesByDepartmentAsync(departmentId);

            Console.WriteLine();

            if (employees.Count == 0)
            {
                Console.WriteLine("No active employees found in this department.");
            }
            else
            {
                Console.WriteLine("Employees:");

                foreach (Employee employee in employees)
                {
                    Console.WriteLine(
                        $"ID: {employee.Id} | " +
                        $"Name: {employee.Name} | " +
                        $"Email: {employee.Email} | " +
                        $"Salary: {employee.Salary} | " +
                        $"Department: {employee.DepartmentName} | " +
                        $"Joining Date: {employee.JoiningDate:yyyy-MM-dd}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }
        if (choice == "6")
        {
            Console.Clear();

            Console.WriteLine("========== SEARCH EMPLOYEES ==========");

            Console.Write("Enter employee name to search: ");
            string? searchName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Search name cannot be empty.");
                Console.ReadLine();
                continue;
            }
            List<Employee> employees =
                await employeeService.SearchEmployeesAsync(searchName);

            if (employees.Count == 0)
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Search Results:");

                foreach (Employee employee in employees)
                {
                    Console.WriteLine(
                        $"ID: {employee.Id} | " +
                        $"Name: {employee.Name} | " +
                        $"Email: {employee.Email} | " +
                        $"Salary: {employee.Salary} | " +
                        $"Department: {employee.DepartmentName} | " +
                        $"Joining Date: {employee.JoiningDate:yyyy-MM-dd}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }
        if (choice == "5")
        {
            Console.Clear();

            Console.WriteLine("========== DELETE EMPLOYEE ==========");

            Console.Write("Enter Employee ID: ");
            string? idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int employeeId))
            {
                Console.WriteLine("Invalid Employee ID.");
                Console.ReadLine();
                continue;
            }

            Employee? employee =
                await employeeService.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine();
            Console.WriteLine($"Employee: {employee.Name}");
            Console.Write("Are you sure you want to delete this employee? (Y/N): ");
            string? confirmation = Console.ReadLine();

            if (confirmation?.ToUpper() != "Y")
            {
                Console.WriteLine("Delete operation cancelled.");
                Console.ReadLine();
                continue;
            }

            bool deleted =
                await employeeService.DeleteEmployeeAsync(employeeId);

            if (deleted)
            {
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee could not be deleted.");
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }
        if (choice == "4")
        {
            Console.Clear();

            Console.WriteLine("========== UPDATE EMPLOYEE ==========");


            Console.Write("Enter Employee ID: ");
            string? idInput = Console.ReadLine();

            if (!int.TryParse(idInput, out int employeeId))
            {
                Console.WriteLine("Invalid Employee ID.");
                Console.ReadLine();
                continue;
            }

            Employee? existingEmployee =
                await employeeService.GetEmployeeByIdAsync(employeeId);

            if (existingEmployee == null)
            {
                Console.WriteLine("Employee not found.");
                Console.ReadLine();
                continue;
            }

            Console.Write("Enter Name: ");
            string? name = Console.ReadLine();

            if (!ValidationHelper.IsValidName(name ?? ""))
            {
                Console.WriteLine("Name is required.");
                Console.ReadLine();
                continue;
            }

            Console.Write("Enter Email: ");
            string? email = Console.ReadLine();

            if (!ValidationHelper.IsValidEmail(email ?? ""))
            {
                Console.WriteLine("Please enter a valid email.");
                Console.ReadLine();
                continue;
            }
            if (await employeeService.EmailExistsForOtherEmployeeAsync(email!, employeeId))
            {
                Console.WriteLine("Email already exists for another employee.");
                Console.ReadLine();
                continue;
            }
            Console.Write("Enter Salary: ");
            string? salaryInput = Console.ReadLine();

            if (!decimal.TryParse(salaryInput, out decimal salary) ||
                !ValidationHelper.IsValidSalary(salary))
            {
                Console.WriteLine("Please enter a valid salary.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine();
            Console.WriteLine("Available Departments:");

            List<Department> departments =
                await departmentService.GetAllDepartmentsAsync();

            foreach (Department department in departments)
            {
                Console.WriteLine($"{department.Id} - {department.Name}");
            }

            Console.Write("Enter Department ID: ");
            string? departmentInput = Console.ReadLine();

            if (!int.TryParse(departmentInput, out int departmentId))
            {
                Console.WriteLine("Invalid Department ID.");
                Console.ReadLine();
                continue;
            }
            if (!await departmentService.DepartmentExistsAsync(departmentId))
            {
                Console.WriteLine("Department does not exist.");
                Console.ReadLine();
                continue;
            }

            Console.Write("Enter Joining Date (yyyy-MM-dd): ");
            string? joiningDateInput = Console.ReadLine();

            if (!DateTime.TryParse(joiningDateInput, out DateTime joiningDate))
            {
                Console.WriteLine("Invalid joining date.");
                Console.ReadLine();
                continue;
            }

            existingEmployee.Name = name;
            existingEmployee.Email = email!;
            existingEmployee.Salary = salary;
            existingEmployee.DepartmentId = departmentId;
            existingEmployee.JoiningDate = joiningDate;

            bool updated =
                await employeeService.UpdateEmployeeAsync(existingEmployee);

            if (updated)
            {
                Console.WriteLine();
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Employee could not be updated.");
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }
        if (choice == "8")
        {
            Console.Clear();

            Console.WriteLine("========== DEPARTMENTS ==========");

            List<Department> departments =
                await departmentService.GetAllDepartmentsAsync();

            foreach (Department department in departments)
            {
                Console.WriteLine($"{department.Id} - {department.Name}");
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            continue;
        }

        Console.WriteLine();
        Console.WriteLine("This option will be implemented next.");

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
catch (Exception ex)
{
    Console.Clear();

    Console.WriteLine("=================================");
    Console.WriteLine("        UNEXPECTED ERROR");
    Console.WriteLine("=================================");
    Console.WriteLine($"Error: {ex.Message}");

    Console.WriteLine();
    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
}