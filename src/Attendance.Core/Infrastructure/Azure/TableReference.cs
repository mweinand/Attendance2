using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Infrastructure.Azure
{
    public interface ITableReference<TEntity> where TEntity : ITableEntity, new()
    {
        void Execute(TableOperation operation, TableRequestOptions requestOptions = null, OperationContext operationContext = null);
        void ExecuteBatch(TableBatchOperation operation, TableRequestOptions requestOptions = null, OperationContext operationContext = null);
        IEnumerable<TEntity> ExecuteQuery(TableQuery<TEntity> query, TableRequestOptions requestOptions = null, OperationContext operationContext = null);
    }

    public class TableReference<TEntity> : ITableReference<TEntity> where TEntity : ITableEntity, new()
    {
        private readonly CloudTable _table;

        public TableReference(CloudTableClient tableClient)
        {
            var attributes = typeof(TEntity).GetCustomAttributes(typeof(TableStorageNameAttribute), false);
            var attribute = attributes.Cast<TableStorageNameAttribute>().FirstOrDefault();
            if (attribute == null)
            {
                throw new ArgumentException("Model should have the TableStorageName Attribute");
            }

            _table = tableClient.GetTableReference(attribute.TableName);
        }

        public void Execute(TableOperation operation, TableRequestOptions requestOptions = null, OperationContext operationContext = null)
        {
            _table.Execute(operation, requestOptions, operationContext);
        }

        public void ExecuteBatch(TableBatchOperation operation, TableRequestOptions requestOptions = null, OperationContext operationContext = null)
        {
            _table.ExecuteBatch(operation, requestOptions, operationContext);
        }

        public IEnumerable<TEntity> ExecuteQuery(TableQuery<TEntity> query, TableRequestOptions requestOptions = null, OperationContext operationContext = null)
        {
            return _table.ExecuteQuery<TEntity>(query, requestOptions, operationContext);
        }
    }
}
