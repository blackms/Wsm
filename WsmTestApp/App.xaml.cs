﻿using System;
using System.Reflection;
using System.Windows;
using Wsm.ExtentionFramework;
using Wsm.Contracts.Dal;
using Wsm.Contracts.Logger;
using Wsm.Contracts.Models;
using Wsm.Repository;
using Wsm.Repository.MongoDB;

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

            //EntryPoint
            builder.ForType<DataBaseEntryPoint>()
                .ImportProperty<IRepositoryFactory>(config => config.RepositoryFactory)
                .Export<IDataBaseEntryPoint>();

            //RepositoryFactory
            builder.ForType<RepositoryFactory>()
                .ImportProperty<IUserRepository>(config => config.UserRepository)
                .ImportProperty<IAccountRepository>(config => config.AccountRepository)
                .Export<IRepositoryFactory>();

            //program  by contract . no references needed
            builder.ForTypesDerivedFrom<ILogger>().Export<ILogger>();

            //Repositories
            builder.ForTypesMatching(t => t.GetInterface(typeof(IRepository<>).Name) != null).ExportInterfaces();

            var export = bootstrap.wsmContainer.GetExportedValue<MainWindow>();

            //Awesome!!!
            export?.Show();
        }
    }
}
