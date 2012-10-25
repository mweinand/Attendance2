using Attendance.Core.Domain;
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
        public IEnumerable<Employee> GetEmployeesInCompany(string companyId)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var tableClient = storageAccount.CreateCloudTableClient();

            tableClient.CreateTableIfNotExist("employees");

            // Get the data service context
            TableServiceContext serviceContext = tableClient.GetDataServiceContext();

            CloudTableQuery<Employee> partitionQuery =
                (from e in serviceContext.CreateQuery<Employee>("employees")
                 where e.PartitionKey == companyId
                 select e).AsTableServiceQuery<Employee>();

            return partitionQuery.AsEnumerable();
        }
    }
}
