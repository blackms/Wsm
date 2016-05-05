using System;
using Wsm.Contracts.Models;
using Wsm.Contracts.Database;
using Wsm.Contracts.Logger;
using Wsm.Contracts.Dispatcher;
using System.Threading.Tasks;

namespace WsmTestApp
{
    public partial class MainWindow
    {
        /// <summary>
        /// The _dispatcher
        /// </summary>
        IDispatcher _dispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="entryPoint">The entry point.</param>
        /// <param name="log">The log.</param>
        public MainWindow(IDataBaseEntryPoint entryPoint, ILogger log, IDispatcher dispatcher)
        {
            InitializeComponent();

            _dispatcher = dispatcher;

            _dispatcher.Run(() => {

                while (true) {

                    System.Diagnostics.Debug.WriteLine("test");

                    System.Threading.Thread.Sleep(1000);

                }
            });
            


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

        public async Task testDispatcher()
        {
            await _dispatcher.RunAsync(() => { System.Diagnostics.Debug.WriteLine("test"); });
        }

    }
}
