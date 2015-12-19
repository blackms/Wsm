using System;
using Wsm.Contracts.Models;
using Wsm.Contracts.Database;
using Wsm.Contracts.Logger;

namespace WsmTestApp
{
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="entryPoint">The entry point.</param>
        /// <param name="log">The log.</param>
        public MainWindow(IDataBaseEntryPoint entryPoint, ILogger log)
        {
            InitializeComponent();

            var userRepo = entryPoint.RepositoryFactory.UserRepository;

            userRepo.Create(new User
            {
                id = new Guid(),
                Username = "Wiljan",
                LastName = "Ruizendaal",
                Email = "wruizendaal@gmail.com"
            });

            var accountRepo = entryPoint.RepositoryFactory.AccountRepository;
            accountRepo.Create(new Account { id = new Guid() });

            log.Info("Repository created");
        }
    }
}
