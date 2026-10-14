
IF DB_ID('EmployeeManagementDB') IS NULL
BEGIN
    CREATE DATABASE EmployeeManagementDB;
END
GO
use EmployeeManagementDB;

IF OBJECT_ID('Departments', 'U') IS NULL
BEGIN
    CREATE TABLE Departments
    (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name VARCHAR(100) NOT NULL UNIQUE
    );
END
GO
IF OBJECT_ID('Employees', 'U') IS NULL
BEGIN
    CREATE TABLE Employees
    (    Id INT PRIMARY KEY IDENTITY(1,1), Name VARCHAR(100) NOT NULL, Email VARCHAR(150) NOT NULL UNIQUE,Salary DECIMAL(10,2) NOT NULL,DepartmentId INT NOT NULL,  JoiningDate DATE NOT NULL, IsActive BIT NOT NULL DEFAULT 1,
  CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
    );
END
GO
IF NOT EXISTS (SELECT 1 FROM Departments WHERE Name = 'IT')
    INSERT INTO Departments (Name) VALUES ('IT');

IF NOT EXISTS (SELECT 1 FROM Departments WHERE Name = 'HR')
    INSERT INTO Departments (Name) VALUES ('HR');

IF NOT EXISTS (SELECT 1 FROM Departments WHERE Name = 'Finance')
    INSERT INTO Departments (Name) VALUES ('Finance');
GO
IF NOT EXISTS (SELECT 1 FROM Employees WHERE Email = 'john@test.com')
BEGIN
    INSERT INTO Employees
        (Name, Email, Salary, DepartmentId, JoiningDate, IsActive)
    VALUES
        ('John', 'john@test.com', 50000,
         (SELECT Id FROM Departments WHERE Name = 'IT'),
         '2025-01-15', 1);
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Email = 'david@test.com')
BEGIN
    INSERT INTO Employees
        (Name, Email, Salary, DepartmentId, JoiningDate, IsActive)
    VALUES
        ('David', 'david@test.com', 60000,
         (SELECT Id FROM Departments WHERE Name = 'HR'),
         '2024-06-10', 1);
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Email = 'robert@test.com')
BEGIN
    INSERT INTO Employees
        (Name, Email, Salary, DepartmentId, JoiningDate, IsActive)
    VALUES
        ('Robert', 'robert@test.com', 45000,
         (SELECT Id FROM Departments WHERE Name = 'Finance'),
         '2023-03-20', 1);
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Email = 'priya@test.com')
BEGIN
    INSERT INTO Employees
        (Name, Email, Salary, DepartmentId, JoiningDate, IsActive)
    VALUES
        ('Priya', 'priya@test.com', 55000,
         (SELECT Id FROM Departments WHERE Name = 'IT'),
         '2025-02-01', 1);
END

IF NOT EXISTS (SELECT 1 FROM Employees WHERE Email = 'rahul@test.com')
BEGIN
    INSERT INTO Employees
        (Name, Email, Salary, DepartmentId, JoiningDate, IsActive)
    VALUES
        ('Rahul', 'rahul@test.com', 48000,
         (SELECT Id FROM Departments WHERE Name = 'HR'),
         '2024-09-12', 1);
END
GO

SELECT * FROM Departments;
GO

SELECT * FROM Employees;
GO

CREATE OR ALTER PROCEDURE sp_GetAllEmployees
AS
BEGIN
    SELECT  e.Id, e.Name, e.Email, e.Salary,e.DepartmentId,d.Name AS DepartmentName, e.JoiningDate,  e.IsActive
    FROM Employees e INNER JOIN Departments d ON e.DepartmentId = d.Id WHERE e.IsActive = 1  ORDER BY e.Id;
END
GO
CREATE OR ALTER PROCEDURE sp_GetEmployeeById
    @Id INT
AS
BEGIN
    SELECT
        e.Id,
        e.Name,
        e.Email,
        e.Salary,
        e.DepartmentId,
        d.Name AS DepartmentName,
        e.JoiningDate,
        e.IsActive
    FROM Employees e
    INNER JOIN Departments d
        ON e.DepartmentId = d.Id
    WHERE e.Id = @Id
      AND e.IsActive = 1;
END
GO
CREATE OR ALTER PROCEDURE sp_AddEmployee
    @Name VARCHAR(100),
    @Email VARCHAR(150),
    @Salary DECIMAL(10,2),
    @DepartmentId INT,
    @JoiningDate DATE
AS
BEGIN
    INSERT INTO Employees
    (
        Name,
        Email,
        Salary,
        DepartmentId,
        JoiningDate,
        IsActive
    )
    VALUES
    (
        @Name,
        @Email,
        @Salary,
        @DepartmentId,
        @JoiningDate,
        1
    );
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS EmployeeId;
END
GO
CREATE OR ALTER PROCEDURE sp_UpdateEmployee
    @Id INT,
    @Name VARCHAR(100),
    @Email VARCHAR(150),
    @Salary DECIMAL(10,2),
    @DepartmentId INT,
    @JoiningDate DATE
AS
BEGIN
    UPDATE Employees
    SET
        Name = @Name,
        Email = @Email,
        Salary = @Salary,
        DepartmentId = @DepartmentId,
        JoiningDate = @JoiningDate
    WHERE Id = @Id
      AND IsActive = 1;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEmployee
    @Id INT
AS
BEGIN
    UPDATE Employees
    SET IsActive = 0
    WHERE Id = @Id
      AND IsActive = 1;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

CREATE OR ALTER PROCEDURE sp_DeleteEmployee
    @Id INT
AS
BEGIN
    UPDATE Employees
    SET IsActive = 0
    WHERE Id = @Id
      AND IsActive = 1;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

CREATE OR ALTER PROCEDURE sp_GetEmployeesByDepartment
    @DepartmentId INT
AS
BEGIN
    SELECT
        e.Id,
        e.Name,
        e.Email,
        e.Salary,
        e.DepartmentId,
        d.Name AS DepartmentName,
        e.JoiningDate,
        e.IsActive
    FROM Employees e
    INNER JOIN Departments d
        ON e.DepartmentId = d.Id
    WHERE e.DepartmentId = @DepartmentId
      AND e.IsActive = 1
    ORDER BY e.Id;
END
GO

USE EmployeeManagementDB;
GO

CREATE OR ALTER PROCEDURE sp_SearchEmployees
    @Name VARCHAR(100)
AS
BEGIN
    SELECT
        e.Id,
        e.Name,
        e.Email,
        e.Salary,
        e.DepartmentId,
        d.Name AS DepartmentName,
        e.JoiningDate,
        e.IsActive
    FROM Employees e
    INNER JOIN Departments d
        ON e.DepartmentId = d.Id
    WHERE e.Name LIKE '%' + @Name + '%'
      AND e.IsActive = 1
    ORDER BY e.Id;
END
GO

SELECT name
FROM sys.procedures
WHERE name = 'sp_SearchEmployees';

