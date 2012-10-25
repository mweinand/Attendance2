using Microsoft.WindowsAzure.StorageClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Domain
{
    public class Employee : TableServiceEntity
    {
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
