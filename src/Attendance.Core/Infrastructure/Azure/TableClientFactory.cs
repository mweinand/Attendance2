using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Infrastructure.Azure
{
    public interface ITableClientFactory
    {
        CloudTableClient CreateTableClient();
    }

    public class TableClientFactory : ITableClientFactory
    {
        public CloudTableClient CreateTableClient()
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            return storageAccount.CreateCloudTableClient();
        }
    }
}
