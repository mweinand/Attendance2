using Attendance.Core.Domain;
using Attendance.Core.Infrastructure;
using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
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
            For<ITableReference<Employee>>().Use(ctx => new TableReference<Employee>(ctx.GetInstance<CloudTableClient>()));
            For<IUnitOfWork<Employee>>().Use(ctx => new UnitOfWork<Employee>(ctx.GetInstance<ITableReference<Employee>>()));
        }
    }
}
