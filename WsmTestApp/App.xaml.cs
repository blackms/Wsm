using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Reflection;
using System.Windows;
using Wsm.Contracts.Database;
using Wsm.Contracts.Dispatcher;
using Wsm.Mef;

namespace WsmTestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += MyResolveEventHandler;
        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            const string rootDir = @"C:\Wsm\Lib\";
            var dllPath = System.IO.Path.Combine(rootDir, args.Name.Split(',')[0] + ".dll");
            return System.IO.File.Exists(dllPath) ? Assembly.LoadFrom(dllPath) : null;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrap = new WsmBootstrap { ModulairPath = "../../../lib" };
            var builder = bootstrap.RegistrationBuilder;

            bootstrap.Ac.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly(), Init(builder)));

            //Mainwindow
            builder.ForType<MainWindow>().Export<MainWindow>();

            var db = bootstrap.Container.GetExportedValue<IConnection>();
            db.ConnectionString = "mongodb://admin:test123@127.0.0.1/WsmDb";
            db.DataBaseName = "WsmDb";

            Lazy<MainWindow> export = bootstrap.Container.GetExport<MainWindow>(); 

            export.Value.Show();
                      
        }

        /// <summary>
        /// Initializes the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
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

            //Dispatcher
            builder.ForTypesMatching(t => t.Name == "WPFDispatcher").ExportInterfaces();

            builder.ForTypesMatching(t => t.Name == "Dispatcher").Export();

            //Database Connection
            builder.ForTypesMatching(t => t.Name == "MongoConnection").ExportInterfaces();

            return builder;
        }
    }
}
