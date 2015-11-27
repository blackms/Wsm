using System.Windows;
using ServiceStack;
using Wsm.ExtentionFramework;
using Wsm.Contracts.Dal;
using Wsm.Contracts.Models;
using Wsm.Repository;
using Wsm.Repository.MongoDB;

namespace WsmTestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrap = new WsmBootstrap();
            var builder = bootstrap.WsmRegistrationBuilder;

            //Mainwindow
            builder.ForType<MainWindow>()
                .Export<MainWindow>();

            //EntryPoint
            builder.ForType<DataBaseEntryPoint>()
                .ImportProperty<IRepositoryFactory>(config => config.RepositoryFactory)
                .Export<IDataBaseEntryPoint>();

            //RepositoryFactory
            builder.ForType<RepositoryFactory>()
                .ImportProperty<IUserRepository>(config => config.UserRepository)
                .ImportProperty<IRepository<Account>>(config => config.AccountRepository)
                .Export<IRepositoryFactory>();

            //Repositories
            builder.ForTypesMatching(t => t.GetInterface(typeof(IRepository<>).Name) != null).ExportInterfaces();
            
            var export = bootstrap.wsmContainer.GetExportedValue<MainWindow>();

            //Awesome!!!
            export?.Show();
        }
    }
}
