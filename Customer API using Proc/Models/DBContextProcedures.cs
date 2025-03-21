﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Company.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Company.Models
{
    public partial class DBContext
    {
        private IDBContextProcedures _procedures;

        public virtual IDBContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new DBContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public IDBContextProcedures GetProcedures()
        {
            return Procedures;
        }
    }

    public partial class DBContextProcedures : IDBContextProcedures
    {
        private readonly DBContext _context;

        public DBContextProcedures(DBContext context)
        {
            _context = context;
        }

        public virtual async Task<List<AddCustomerResult>> AddCustomerAsync(string name, decimal? salary, string password, int? deptId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "Name",
                    Size = 200,
                    Value = name ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Salary",
                    Precision = 18,
                    Scale = 2,
                    Value = salary ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Decimal,
                },
                new SqlParameter
                {
                    ParameterName = "Password",
                    Size = 100,
                    Value = password ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "DeptId",
                    Value = deptId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<AddCustomerResult>("EXEC @returnValue = [dbo].[AddCustomer] @Name = @Name, @Salary = @Salary, @Password = @Password, @DeptId = @DeptId", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<AddCustomerToProjectResult>> AddCustomerToProjectAsync(int? customerId, int? projectId, OutputParameter<int?> actualReturnValue, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterActualReturnValue = new SqlParameter
            {
                ParameterName = "ActualReturnValue",
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = actualReturnValue?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Int,
            };
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "CustomerId",
                    Value = customerId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "ProjectId",
                    Value = projectId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterActualReturnValue,
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<AddCustomerToProjectResult>("EXEC @returnValue = [dbo].[AddCustomerToProject] @CustomerId = @CustomerId, @ProjectId = @ProjectId, @ActualReturnValue = @ActualReturnValue OUTPUT", sqlParameters, cancellationToken);

            actualReturnValue?.SetValue(parameterActualReturnValue.Value);
            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<AddDepartmentResult>> AddDepartmentAsync(string name, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "Name",
                    Size = 100,
                    Value = name ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<AddDepartmentResult>("EXEC @returnValue = [dbo].[AddDepartment] @Name = @Name", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<AddProjectResult>> AddProjectAsync(string name, string description, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "Name",
                    Size = 200,
                    Value = name ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Description",
                    Size = -1,
                    Value = description ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<AddProjectResult>("EXEC @returnValue = [dbo].[AddProject] @Name = @Name, @Description = @Description", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<int> DeleteCustomerAsync(int? customerId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "CustomerId",
                    Value = customerId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[DeleteCustomer] @CustomerId = @CustomerId", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<GetAllCustomerProjectsDataResult>> GetAllCustomerProjectsDataAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<GetAllCustomerProjectsDataResult>("EXEC @returnValue = [dbo].[GetAllCustomerProjectsData]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<GetCustomerLoginResult>> GetCustomerLoginAsync(string name, string password, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "Name",
                    Size = 200,
                    Value = name ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Password",
                    Size = 100,
                    Value = password ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<GetCustomerLoginResult>("EXEC @returnValue = [dbo].[GetCustomerLogin] @Name = @Name, @Password = @Password", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        public virtual async Task<List<UpdateCustomerResult>> UpdateCustomerAsync(int? customerId, string name, decimal? salary, string password, int? deptId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "CustomerId",
                    Value = customerId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "Name",
                    Size = 200,
                    Value = name ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Salary",
                    Precision = 18,
                    Scale = 2,
                    Value = salary ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Decimal,
                },
                new SqlParameter
                {
                    ParameterName = "Password",
                    Size = 100,
                    Value = password ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                },
                new SqlParameter
                {
                    ParameterName = "DeptId",
                    Value = deptId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<UpdateCustomerResult>("EXEC @returnValue = [dbo].[UpdateCustomer] @CustomerId = @CustomerId, @Name = @Name, @Salary = @Salary, @Password = @Password, @DeptId = @DeptId", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
