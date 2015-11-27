using System.ComponentModel.Composition;
using Wsm.Contracts;
using Wsm.Contracts.Dal;
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
        /// <param name="dbContext"></param>
        public AccountRepository([Import("DataBaseEntryPoint")] dynamic dbContext)
        {
            DbContext = dbContext;
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
