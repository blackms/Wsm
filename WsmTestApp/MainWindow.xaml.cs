using System;
using Wsm.Contracts.Models;
using Wsm.Contracts.Dal;
using Wsm.Contracts.Logger;

namespace WsmTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow
    {
   
        public readonly IDataBaseEntryPoint _entryPoint;
        
        public MainWindow(IDataBaseEntryPoint entryPoint, ILogger log)
        {
            InitializeComponent();
            _entryPoint = entryPoint;

            var userRepo = _entryPoint.RepositoryFactory.UserRepository;
            userRepo.Create(new User { id = new Guid(), Username = "Wiljan", LastName = "Ruizendaal", Email = "wruizendaal@gmail.com" });

            var accountRepo = _entryPoint.RepositoryFactory.AccountRepository;
            accountRepo.Create(new Account { id = new Guid() });

            log.Info("Repository created");
        }
    }
}
