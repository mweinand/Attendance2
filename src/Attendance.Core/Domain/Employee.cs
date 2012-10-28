using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Domain
{
    [TableStorageName("employees")]
    public class Employee : TableEntity
    {
        public Employee() { }

        public string CompanyId
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string SerialNumber
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
