using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Infrastructure.Azure
{
    public interface ITableStorageCreator
    {
        void CreateTablesIfNotExist();
    }

    public class TableStorageCreator : ITableStorageCreator
    {
        private readonly CloudTableClient _tableClient;

        public TableStorageCreator(CloudTableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public void CreateTablesIfNotExist()
        {
            _tableClient.GetTableReference("employees").CreateIfNotExists();
        }
    }
}
