using Wsm.Contracts.Database;
using Wsm.Contracts.Models;

namespace Wsm.Repository.MongoDB
{
    /// <summary>
    /// Account specific tasks
    /// </summary>
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="dbConnection"></param>
        public AccountRepository(IConnection dbConnection)
        {
            DbContext = dbConnection.DbContext;
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetPassword()
        {
            throw new System.NotImplementedException();
        }
    }
}
