using Attendance.Core.Domain;
using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
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
        private readonly IServiceContext _serviceContext;

        public EmployeeRepository(IServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public IEnumerable<Employee> GetEmployeesInCompany(string companyId)
        {
            CloudTableQuery<Employee> partitionQuery =
                (from e in _serviceContext.CreateQuery<Employee>()
                 where e.PartitionKey == companyId
                 select e).AsTableServiceQuery<Employee>();

            return partitionQuery.AsEnumerable();
        }
    }
}
