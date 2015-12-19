using System;
using System.Reflection;
using System.Windows;
using Wsm.Contracts.Database;
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
            var builder = bootstrap.WsmRegistrationBuilder;

            //Mainwindow
            builder.ForType<MainWindow>().Export<MainWindow>();

            // Design by contract no project ~ references needed
            builder.ForTypesMatching(t => t.Name == "DataBaseEntryPoint")
                   .ImportProperties(prop => prop.Name == "RepositoryFactory")
                   .ExportInterfaces();

            builder.ForTypesMatching(t => t.Name == "NLogger")
                   .ExportInterfaces();

            builder.ForTypesMatching(t => t.Name == "RepositoryFactory")
                   .ImportProperties(prop => prop.Name.EndsWith("Repository"))
                   .ExportInterfaces();

            //Repositories
            builder.ForTypesMatching(t => t.GetInterface(typeof(IRepository<>).Name) != null).ExportInterfaces();

            //Database Connection
            builder.ForTypesMatching(t => t.Name == "MongoConnection").ExportInterfaces();
            
            var db = bootstrap.wsmContainer.GetExportedValue<IConnection>();
            
            db.ConnectionString = "mongodb://admin:test123@127.0.0.1/WsmDb";
            db.DataBaseName = "WsmDb";
            
            var export = bootstrap.wsmContainer.GetExportedValue<MainWindow>();
            
            //Awesome!!!
            export?.Show();
        }
    }
}
