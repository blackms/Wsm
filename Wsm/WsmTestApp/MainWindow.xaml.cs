using System;
using Wsm.Contracts.Models;
using Wsm.Contracts.Dal;

namespace WsmTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    public partial class MainWindow
    {
   
        public readonly IDataBaseEntryPoint _entryPoint;
        
        public MainWindow(IDataBaseEntryPoint entryPoint)
        {
            InitializeComponent();
            _entryPoint = entryPoint;

            var userRepo = _entryPoint.RepositoryFactory.UserRepository;
            userRepo.Create(new User { id = new Guid(), Username = "Wiljan", LastName = "Ruizendaal", Email = "wruizendaal@gmail.com" });

            var accountRepo = _entryPoint.RepositoryFactory.AccountRepository;
            accountRepo.Create(new Account { id = new Guid() });
        }
    }
}
