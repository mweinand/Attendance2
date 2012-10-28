using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Infrastructure
{
    public interface IUnitOfWork<TEntity> where TEntity : ITableEntity, new()
    {
        void Initialize();
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Execute();
    }

    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : ITableEntity, new()
    {
        private readonly ITableReference<TEntity> _tableReference;
        private TableBatchOperation _batchOperation;

        public UnitOfWork(ITableReference<TEntity> tableReference)
        {
            _tableReference = tableReference;
        }

        public void Initialize()
        {
            if (_batchOperation != null)
            {
                throw new Exception("Batch already started");
            }
            _batchOperation = new TableBatchOperation();
        }

        public void Insert(TEntity entity)
        {
            _batchOperation.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _batchOperation.InsertOrReplace(entity);
        }

        public void Delete(TEntity entity)
        {
            _batchOperation.Delete(entity);
        }

        public void Execute()
        {
            _tableReference.ExecuteBatch(_batchOperation);
            _batchOperation = null;
        }
    }
}
