using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Text;

namespace Attendance.Core.Infrastructure.Azure
{
    public interface IServiceContext
    {
        DataServiceQuery<TModel> CreateQuery<TModel>();
    }

    public class ServiceContext : IServiceContext
    {
        private readonly TableServiceContext _tableServiceContext;

        public ServiceContext(TableServiceContext tableServiceContext)
        {
            _tableServiceContext = tableServiceContext;
        }

        public DataServiceQuery<TModel> CreateQuery<TModel>()
        {
            var attributes = typeof(TModel).GetCustomAttributes(typeof(TableStorageNameAttribute), false);
            var attribute = attributes.Cast<TableStorageNameAttribute>().FirstOrDefault();
            if (attribute == null)
            {
                throw new ArgumentException("Model should have the TableStorageName Attribute");
            }

            return _tableServiceContext.CreateQuery<TModel>(attribute.TableName);
        }
    }
}
