using Attendance.Core.Infrastructure.Azure;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Web
{
    public class AzureSetup
    {
        private readonly ITableStorageCreator _tableStorageCreator;

        public AzureSetup()
        {
            _tableStorageCreator = ObjectFactory.GetInstance<ITableStorageCreator>();
        }

        public void Start()
        {
            _tableStorageCreator.CreateTablesIfNotExist();
        }
    }
}