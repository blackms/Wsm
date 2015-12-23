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
        public CompositionContainer Container => _wsmContainer ?? (_wsmContainer = Init(RegistrationBuilder, ModulairPath));

        /// <summary>
        /// Gets or sets the WSM registration builder.
        /// </summary>
        /// <value>
        /// The WSM registration builder.
        /// </value>
        public RegistrationBuilder RegistrationBuilder => _wsmReflectionBuilder ?? (_wsmReflectionBuilder = new RegistrationBuilder());

        public AggregateCatalog Ac = new AggregateCatalog();


        /// <summary>
        /// Initializes the specified registered exports.
        /// </summary>
        /// <param name="registeredExports">The registered exports.</param>
        /// <param name="modulairPath"></param>
        /// <returns></returns>
        private CompositionContainer Init(ReflectionContext registeredExports, string modulairPath)
        {
            if (string.IsNullOrWhiteSpace(modulairPath))
                throw new ArgumentNullException();
         
            Ac.Catalogs.Add(new DirectoryCatalog(modulairPath, registeredExports));

            var container = new CompositionContainer(Ac, CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe);
            return container;
        }
    }
}
