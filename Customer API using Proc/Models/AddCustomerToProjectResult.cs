﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public partial class AddCustomerToProjectResult
    {
        public int CustomerId { get; set; }
        [StringLength(100)]
        public string CustomerName { get; set; }
        [Column("Salary", TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
    }
}
