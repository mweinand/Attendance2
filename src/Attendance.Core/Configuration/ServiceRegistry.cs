using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attendance.Core.Services;
using StructureMap.Configuration.DSL;

namespace Attendance.Core.Configuration
{
	public class ServiceRegistry : Registry
	{
		public ServiceRegistry()
        {
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.AssemblyContainingType<ITardyService>();
                s.WithDefaultConventions();
            });
	    }
	}
}
