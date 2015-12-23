
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Wsm.Contracts.Database;
using Wsm.Mef;

namespace Wsm.Test.Web.Mvc
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var bootstrap = new WsmBootstrap { ModulairPath = "../lib" };
            var builder = bootstrap.RegistrationBuilder;

            bootstrap.Ac.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly(), Init(builder)));

            var db = bootstrap.Container.GetExportedValue<IConnection>();
            db.ConnectionString = "mongodb://admin:test123@127.0.0.1/WsmDb";
            db.DataBaseName = "WsmDb";

            ControllerBuilder.Current.SetControllerFactory(new WsmControllerFactory(bootstrap.Container));
        }

        private static RegistrationBuilder Init(RegistrationBuilder builder)
        {
            //Database -Stuff
            builder.ForTypesMatching(t => t.Name == "DataBaseEntryPoint")
               .ImportProperties(prop => prop.Name == "RepositoryFactory")
               .ExportInterfaces();

            //Logger
            builder.ForTypesMatching(t => t.Name == "NLogger")
                   .ExportInterfaces();

            //RepositoryFactory
            builder.ForTypesMatching(t => t.Name == "RepositoryFactory")
                   .ImportProperties(prop => prop.Name.EndsWith("Repository"))
                   .ExportInterfaces();

            //Repositories
            builder.ForTypesMatching(t => t.GetInterface(typeof(IRepository<>).Name) != null)
                   .ExportInterfaces();

            //Controller
            builder.ForTypesDerivedFrom<Controller>()
                   .SetCreationPolicy(CreationPolicy.NonShared)
                   .Export();

            builder.ForTypesMatching(t => t.Name == "HomeController")
                   .Export();

            //Database Connection
            builder.ForTypesMatching(t => t.Name == "MongoConnection").ExportInterfaces();

            return builder;
        }

        public MvcApplication()
        {
            AppDomain.CurrentDomain.AssemblyResolve += MyResolveEventHandler;
        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            const string rootDir = @"C:\Wsm\Lib\";
            var dllPath = System.IO.Path.Combine(rootDir, args.Name.Split(',')[0] + ".dll");
            return System.IO.File.Exists(dllPath) ? Assembly.LoadFrom(dllPath) : null;
        }
    }

}
