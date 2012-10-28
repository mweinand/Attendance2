using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Infrastructure.Azure
{
    public class TableStorageNameAttribute : Attribute
    {
        public string TableName { get; private set; }
        public TableStorageNameAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
