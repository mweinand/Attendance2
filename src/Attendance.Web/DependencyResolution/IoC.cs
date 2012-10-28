using StructureMap;
using Attendance.Core.Configuration;

namespace Attendance.Web {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.AddRegistry<InfrastructureRegistry>();
                            x.AddRegistry<RepositoryRegistry>();
                        });
            return ObjectFactory.Container;
        }
    }
}