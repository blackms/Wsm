using Wsm.Contracts.Dal;

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
