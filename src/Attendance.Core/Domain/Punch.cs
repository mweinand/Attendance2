using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Domain
{
    [TableStorageName("punches")]
    public class Punch : TableEntity
    {
        public string SerialNumber
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
    }
}
