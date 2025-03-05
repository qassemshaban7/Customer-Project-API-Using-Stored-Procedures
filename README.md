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
```
The API will be available at: **`http://localhost:5000`** (or the port configured in `launchSettings.json`).

---

## 🚀 API Endpoints

### 🔹 Versioning Support (v1 & v2)
The API follows versioning using **Route Versioning (`v{version:apiVersion}`)**:
- `v1`: Basic functionality
- `v2`: Future enhancements

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

#### 📌 Add a New Customer
```sql
CREATE OR ALTER PROCEDURE AddCustomer
    @Name NVARCHAR(100),
    @Salary DECIMAL(18, 2),
    @Password NVARCHAR(50),
    @DeptId INT
AS
BEGIN
    INSERT INTO Customers (Name, Salary, Password, DeptId)
    VALUES (@Name, @Salary, @Password, @DeptId);

    SELECT SCOPE_IDENTITY() AS CustomerId;
END;
```

#### 📌 Update a Customer
```sql
CREATE OR ALTER PROCEDURE UpdateCustomer
    @CustomerId INT,
    @Name NVARCHAR(100),
    @Salary DECIMAL(18,2),
    @Password NVARCHAR(50),
    @DeptId INT
AS
BEGIN
    UPDATE Customers
    SET Name = @Name, Salary = @Salary, Password = @Password, DeptId = @DeptId
    WHERE CustomerId = @CustomerId;

    SELECT 1 AS ReturnValue;
END;
```

#### 📌 Delete a Customer
```sql
CREATE OR ALTER PROCEDURE DeleteCustomer
    @CustomerId INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerId = @CustomerId)
    BEGIN
        SELECT -1 AS ReturnValue;
        RETURN;
    END

    DELETE FROM Customers WHERE CustomerId = @CustomerId;
    SELECT 1 AS ReturnValue;
END;
```

#### 📌 Get Customers with Departments
```sql
CREATE OR ALTER PROCEDURE GetCustomersWithDepartments
AS
BEGIN
    SELECT c.CustomerId, c.Name, c.Salary, d.Name AS DepartmentName
    FROM Customers c
    INNER JOIN Departments d ON c.DeptId = d.DeptId;
END;
```

---

## 📜 Swagger Documentation
This API uses **Swagger** for documentation.

### 🔹 Enable Swagger UI
Swagger is available at:
- **`http://localhost:5000/swagger`**

### 🔹 Multi-Version Support in Swagger
In `Program.cs`, Swagger is configured for **v1 & v2**:
```csharp
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Project API v1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Customer Project API v2");
});
```

---

## 🔐 Authentication & Security
This API uses **JWT Authentication**. Each request requires a valid token in the **Authorization Header**:
```http
Authorization: Bearer {your-token-here}
```

To generate a token, send a `POST` request to:
```http
POST /api/v1/auth/login
```
with the following JSON body:
```json
{
  "username": "admin",
  "password": "password123"
}
```
---

## 📬 Contributing
Feel free to fork this repository, submit issues, or create pull requests to improve the project. 🙌

---

## 📄 License
This project is licensed under the **MIT License**.

