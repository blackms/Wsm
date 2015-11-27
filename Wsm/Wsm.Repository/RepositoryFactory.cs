using System;
using Wsm.Contracts;
using System.Linq;
using Wsm.Contracts.Models;
using Wsm.Contracts.Dal;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Wsm.Repository
{

    public class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// Gets or sets the account repository.
        /// </summary>
        /// <value>
        /// The account repository.
        /// </value>
        public IAccountRepository AccountRepository
        {
            get; set;
        }

        /// <summary>
        /// Users the repository.
        /// </summary>
        /// <returns></returns>
        public IUserRepository UserRepository
        {
            get; set;
        }

    }
}
