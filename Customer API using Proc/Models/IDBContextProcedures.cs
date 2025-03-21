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
    public partial interface IDBContextProcedures
    {
        Task<List<AddCustomerResult>> AddCustomerAsync(string name, decimal? salary, string password, int? deptId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<AddCustomerToProjectResult>> AddCustomerToProjectAsync(int? customerId, int? projectId, OutputParameter<int?> actualReturnValue, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<AddDepartmentResult>> AddDepartmentAsync(string name, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<AddProjectResult>> AddProjectAsync(string name, string description, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DeleteCustomerAsync(int? customerId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GetAllCustomerProjectsDataResult>> GetAllCustomerProjectsDataAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<GetCustomerLoginResult>> GetCustomerLoginAsync(string name, string password, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<UpdateCustomerResult>> UpdateCustomerAsync(int? customerId, string name, decimal? salary, string password, int? deptId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
