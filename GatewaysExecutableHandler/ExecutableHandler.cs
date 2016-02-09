using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GatewaysExecutableHandler
{
    public sealed class ExecutableHandler : IDisposable
    {
        private AppDomain _domain;

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetObject<T>() where T : MarshalByRefObject
        {
            return (T)_domain.CreateInstanceAndUnwrap(typeof(T).Assembly.FullName, typeof(T).FullName);
        }

        /// <summary>
        /// Creates the application domain.
        /// </summary>
        public void CreateAppDomain()
        {
            _domain = AppDomain.CreateDomain("Gateways:" + Guid.NewGuid(), null, AppDomain.CurrentDomain.SetupInformation);

            _domain.AssemblyResolve += GatewaysAssemblyResolve;
        }

        /// <summary>
        /// Gatewayses the assembly resolve.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        static Assembly GatewaysAssemblyResolve(object source, ResolveEventArgs e)
        {
            var assembly = Assembly.Load(e.Name);
            if (assembly != null)
                return assembly;

            var EliasFileDir = @"C:\program files(86)\c2sc\bin";
            var target = Path.Combine(EliasFileDir, e.Name);

            return Assembly.Load(target) ?? null;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (_domain != null)
            {
                AppDomain.Unload(_domain);

                _domain = null;
            }
        }
    }
}
