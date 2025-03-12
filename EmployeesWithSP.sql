-- tbl 
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Position NVARCHAR(100) NOT NULL,
    Salary DECIMAL(18,2) NOT NULL
);

select * from Employees

--Create Procedure
CREATE PROCEDURE sp_AddEmployee  
    @Name NVARCHAR(100),  
    @Position NVARCHAR(100),  
    @Salary DECIMAL(18,2)  
AS  
BEGIN  
    INSERT INTO Employees (Name, Position, Salary)  
    VALUES (@Name, @Position, @Salary);  

    SELECT SCOPE_IDENTITY() AS EmployeeID;  
END  

-- Read Procedure

CREATE PROCEDURE sp_GetEmployees  
AS  
BEGIN  
    SELECT * FROM Employees;  
END  


-- Read by ID

CREATE PROCEDURE sp_GetEmployeeById  
    @EmployeeID INT  
AS  
BEGIN  
    SELECT * FROM Employees WHERE EmployeeID = @EmployeeID;  
END  

-- Update Procedure

CREATE PROCEDURE sp_UpdateEmployee  
    @EmployeeID INT,  
    @Name NVARCHAR(100),  
    @Position NVARCHAR(100),  
    @Salary DECIMAL(18,2)  
AS  
BEGIN  
    UPDATE Employees  
    SET Name = @Name, Position = @Position, Salary = @Salary  
    WHERE EmployeeID = @EmployeeID;  
END  

-- Delete Procedure

CREATE PROCEDURE sp_DeleteEmployee  
    @EmployeeID INT  
AS  
BEGIN  
    DELETE FROM Employees WHERE EmployeeID = @EmployeeID;  
END  


