using Attendance.Core.Infrastructure;
using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure.StorageClient;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Configuration
{
    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.AssemblyContainingType<InfrastructureMarker>();
                    s.WithDefaultConventions();
                });

            For<CloudTableClient>().Use(ctx => ctx.GetInstance<ITableClientFactory>().CreateTableClient());
            For<TableServiceContext>().Use(ctx => ctx.GetInstance<CloudTableClient>().GetDataServiceContext());
        }
    }
}
