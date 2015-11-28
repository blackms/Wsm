using System;
using Wsm.Contracts.Models;
using Wsm.Contracts.Dal;
using Wsm.Contracts.Logger;

namespace WsmTestApp
{
    public partial class MainWindow
    {
        /// <summary>
        /// The _entry point
        /// </summary>
        private readonly IDataBaseEntryPoint _entryPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="entryPoint">The entry point.</param>
        /// <param name="log">The log.</param>
        public MainWindow(IDataBaseEntryPoint entryPoint, ILogger log)
        {
            InitializeComponent();
            _entryPoint = entryPoint;
            
            var userRepo = _entryPoint.RepositoryFactory.UserRepository;
            userRepo.Create(new User { id = new Guid(), Username = "Wiljan", LastName = "Ruizendaal", Email = "wruizendaal@gmail.com"});

            var accountRepo = _entryPoint.RepositoryFactory.AccountRepository;
            accountRepo.Create(new Account { id = new Guid() });

            log.Info("Repository created");
        }
    }
}
