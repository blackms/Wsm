using System;
using System.Reflection;
using System.Windows;
using Wsm.Contracts.Dal;
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
            var bootstrap = new WsmBootstrap();
            var builder = bootstrap.WsmRegistrationBuilder;


            //Mainwindow
            builder.ForType<MainWindow>()
                .Export<MainWindow>();

            // Design by contract no project ~ references needed
            builder.ForTypesMatching(t => t.Name == "DataBaseEntryPoint")
                   .ImportProperties(prop => prop.Name =="RepositoryFactory")
                   .ExportInterfaces();
             
            builder.ForTypesMatching(t => t.Name == "NLogger")
                   .ExportInterfaces();

            builder.ForTypesMatching(t => t.Name == "RepositoryFactory")
                   .ImportProperties(prop => prop.Name.EndsWith("Repository"))
                   .ExportInterfaces();

            //Repositories
            builder.ForTypesMatching(t => t.GetInterface(typeof(IRepository<>).Name) != null)
                   .ExportInterfaces();

            var export = bootstrap.wsmContainer.GetExportedValue<MainWindow>();

            //Awesome!!!
            export?.Show();
        }
    }
}
