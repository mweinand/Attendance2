using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
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
            var setting = CloudConfigurationManager.GetSetting("StorageConnectionString");
            try
            {
                var storageAccount = CloudStorageAccount.Parse(setting);
                return storageAccount.CreateCloudTableClient();
            }
            catch (Exception e)
            {
            }
            return null;
        }
    }
}
