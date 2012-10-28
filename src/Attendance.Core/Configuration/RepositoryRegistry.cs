using Attendance.Core.Repositories;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance.Core.Configuration
{
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry ()
        {
            Scan(s =>
            {
                s.TheCallingAssembly();
                s.AssemblyContainingType<IEmployeeRepository>();
                s.WithDefaultConventions();
            });
	    }
    }
}
