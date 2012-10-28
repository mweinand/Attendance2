using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Attendance.Web.StructuremapMvc), "Start")]

namespace Attendance.Web {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}