﻿using Attendance.Core.Domain;
using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployeesInCompany(string companyId);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ITableReference<Employee> _tableReference;

        public EmployeeRepository(ITableReference<Employee> tableReference)
        {
            _tableReference = tableReference;
        }

        public IEnumerable<Employee> GetEmployeesInCompany(string companyId)
        {
            TableQuery<Employee> partitionQuery = new TableQuery<Employee>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, companyId));

            return _tableReference.ExecuteQuery(partitionQuery);
        }
    }
}
