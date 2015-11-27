using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using MefContrib.Hosting.Generics;

namespace Wsm.ExtentionFramework
{
    public class WsmBootstrap
    {
        private static CompositionContainer _wsmContainer;
        private static RegistrationBuilder _wsmReflectionBuilder;

        public CompositionContainer wsmContainer => _wsmContainer ?? (_wsmContainer = Init(WsmRegistrationBuilder));

        public RegistrationBuilder WsmRegistrationBuilder => _wsmReflectionBuilder ?? (_wsmReflectionBuilder = new RegistrationBuilder());

        private static CompositionContainer Init(ReflectionContext registeredExports)
        {
            var ac = new AggregateCatalog();

            var program = new AssemblyCatalog(Assembly.GetEntryAssembly(), registeredExports);
            var appCatalog = new AssemblyCatalog(Assembly.GetCallingAssembly(), registeredExports);
            var libCatalog = new DirectoryCatalog("../../../lib", registeredExports);
            
            ac.Catalogs.Add(program);
            ac.Catalogs.Add(appCatalog);
            ac.Catalogs.Add(libCatalog);

            var container = new CompositionContainer(ac, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);

            return container;
        }
    }
}
