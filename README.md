# Customer Project API - Work With Stored Procedures Only

## 📌 Overview
This API is designed to manage **Customers**, **Projects**, and their relationships using **Stored Procedures** in SQL Server.

The project uses:
- **ASP.NET Core 8 Web API**
- **SQL Server Stored Procedures**
- **Entity Framework Core (for executing raw SQL queries)**
- **Swagger for API Documentation**
- **JWT Authentication for security**

---

## 🚀 API Endpoints
### 🔹 Customers API
| Method | Endpoint | Description |
|--------|-------------|------------------|
| `POST` | `/api/v1/customers` | Add a new customer (Stored Procedure) |
| `PUT` | `/api/v1/customers/{id}` | Update customer details (Stored Procedure) |
| `DELETE` | `/api/v1/customers/{id}` | Delete a customer (Stored Procedure) |
| `GET` | `/api/v1/customers` | Get all customers with department details |

### 🔹 Projects API
| Method | Endpoint | Description |
|--------|-------------|------------------|
| `POST` | `/api/v1/projects` | Add a new project (Stored Procedure) |
| `GET` | `/api/v1/projects` | Get all projects |
| `POST` | `/api/v1/customers/{customerId}/projects/{projectId}` | Assign a project to a customer (Stored Procedure) |

### 🔹 Authentication (JWT)
| Method | Endpoint | Description |
|--------|-------------|------------------|
| `POST` | `/api/v1/auth/login` | Authenticate user and get JWT token |

---

## 🛢️ SQL Server Tables & Stored Procedures

### 🔹 Database Tables
```sql
CREATE TABLE Departments (
    DeptId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL        
);

CREATE TABLE Customers (
    CustomerId INT IDENTITY PRIMARY KEY,  
    Name NVARCHAR(100) NOT NULL,        
    Salary DECIMAL(18, 2) NOT NULL,      
    Password NVARCHAR(50) NOT NULL,      
    DeptId INT NOT NULL,                  
    FOREIGN KEY (DeptId) REFERENCES Departments(DeptId)
);

CREATE TABLE Projects (
    ProjectId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,        
    Description NVARCHAR(MAX) NULL     
);

CREATE TABLE CustomerProjects (
    CustomerId INT NOT NULL,
    ProjectId INT NOT NULL,
    PRIMARY KEY (CustomerId, ProjectId),
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),
    FOREIGN KEY (ProjectId) REFERENCES Projects(ProjectId)
);
```

### 🔹 Stored Procedures

#### 📌 Add a Department
```sql
CREATE PROCEDURE AddDepartment
    @Name NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Departments (Name)
    VALUES (@Name);

    SELECT Name FROM Departments WHERE Name = @Name;
END;
GO
```

#### 📌 Add a New Customer
```sql
CREATE OR ALTER PROCEDURE AddCustomer
    @Name NVARCHAR(100),
    @Salary DECIMAL(18, 2),
    @Password NVARCHAR(50),
    @DeptId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Customers (Name, Salary, Password, DeptId)
    VALUES (@Name, @Salary, @Password, @DeptId);

    DECLARE @NewCustomerId INT = SCOPE_IDENTITY();

    (SELECT CustomerId, Name, Password, DeptId 
    FROM Customers 
    WHERE CustomerId = @NewCustomerId);
END;
GO
```

#### 📌 Get Customer Login
```sql
CREATE PROCEDURE GetCustomerLogin
    @Name NVARCHAR(100),
    @Password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CustomerId, Name
    FROM Customers
    WHERE Name = @Name AND Password = @Password;
END;
GO
```

#### 📌 Update a Customer
```sql
CREATE OR ALTER PROCEDURE UpdateCustomer
    @CustomerId INT,
    @Name NVARCHAR(100) = NULL,
    @Salary DECIMAL(18, 2) = NULL,
    @Password NVARCHAR(50) = NULL,
    @DeptId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerId = @CustomerId)
    BEGIN
        SELECT -1 AS ReturnValue;
        RETURN;
    END

    UPDATE Customers
    SET 
        Name = ISNULL(@Name, Name),
        Salary = ISNULL(@Salary, Salary),
        Password = ISNULL(@Password, Password),
        DeptId = ISNULL(@DeptId, DeptId)
    WHERE CustomerId = @CustomerId;

    SELECT 1 AS ReturnValue;
END;
GO
```

#### 📌 Delete a Customer
```sql
CREATE OR ALTER PROCEDURE DeleteCustomer
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerId = @CustomerId)
    BEGIN
        RETURN -1;
    END

    DELETE FROM Customers
    WHERE CustomerId = @CustomerId;

    RETURN 1;
END;
GO
```

#### 📌 Add a Project
```sql
CREATE OR ALTER PROCEDURE AddProject
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Projects (Name, Description)
    VALUES (@Name, @Description);

    DECLARE @NewProId INT = SCOPE_IDENTITY();

    (SELECT * FROM Projects
     WHERE ProjectId = @NewProId);
END;
GO
```

#### 📌 Assign a Project to a Customer
```sql
CREATE OR ALTER PROCEDURE AddCustomerToProject
    @CustomerId INT,
    @ProjectId INT,
    @ActualReturnValue INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF EXISTS (
            SELECT 1 FROM CustomerProjects 
            WHERE CustomerId = @CustomerId AND ProjectId = @ProjectId
        )
        BEGIN
            SET @ActualReturnValue = -1;
            RETURN;
        END

        INSERT INTO CustomerProjects (CustomerId, ProjectId)
        VALUES (@CustomerId, @ProjectId);

        SET @ActualReturnValue = 1;
    END TRY
    BEGIN CATCH
        SET @ActualReturnValue = 0;
    END CATCH
END;
GO
```

#### 📌 Get All Customer Projects Data
```sql
CREATE OR ALTER PROCEDURE GetAllCustomerProjectsData
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.CustomerId, 
        c.Name AS CustomerName, 
        c.Salary, 
        c.Password, 
        d.DeptId, 
        d.Name AS DepartmentName,
        p.ProjectId, 
        p.Name AS ProjectName, 
        p.Description AS ProjectDescription
    FROM Customers c
    INNER JOIN Departments d ON c.DeptId = d.DeptId
    LEFT JOIN CustomerProjects cp ON c.CustomerId = cp.CustomerId
    LEFT JOIN Projects p ON cp.ProjectId = p.ProjectId;
END;
GO
```

---

## 📄 License
This project is licensed under the **MIT License**.

