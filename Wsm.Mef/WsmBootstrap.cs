using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

namespace Wsm.Mef
{
    public class WsmBootstrap
    {
        /// <summary>
        /// The _WSM container
        /// </summary>
        private static CompositionContainer _wsmContainer;

        /// <summary>
        /// The _WSM reflection builder
        /// </summary>
        private static RegistrationBuilder _wsmReflectionBuilder;

        public string ModulairPath { get; set; }
        
        /// <summary>
        /// Gets or sets the WSM container.
        /// </summary>
        /// <value>
        /// The WSM container.
        /// </value>
        public CompositionContainer wsmContainer => _wsmContainer ?? (_wsmContainer = Init(WsmRegistrationBuilder, ModulairPath));

        /// <summary>
        /// Gets or sets the WSM registration builder.
        /// </summary>
        /// <value>
        /// The WSM registration builder.
        /// </value>
        public RegistrationBuilder WsmRegistrationBuilder => _wsmReflectionBuilder ?? (_wsmReflectionBuilder = new RegistrationBuilder());

        /// <summary>
        /// Initializes the specified registered exports.
        /// </summary>
        /// <param name="registeredExports">The registered exports.</param>
        /// <param name="modulairPath"></param>
        /// <returns></returns>
        private static CompositionContainer Init(ReflectionContext registeredExports, string modulairPath)
        {
            if (string.IsNullOrWhiteSpace(modulairPath))
                throw new ArgumentNullException();

            var ac = new AggregateCatalog();
            var program = new AssemblyCatalog(Assembly.GetEntryAssembly(), registeredExports);
            var appCatalog = new AssemblyCatalog(Assembly.GetCallingAssembly(), registeredExports);
            var libCatalog = new DirectoryCatalog(modulairPath, registeredExports);

            ac.Catalogs.Add(program);
            ac.Catalogs.Add(appCatalog);
            ac.Catalogs.Add(libCatalog);

            var container = new CompositionContainer(ac, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
            return container;
        }
    }
}
