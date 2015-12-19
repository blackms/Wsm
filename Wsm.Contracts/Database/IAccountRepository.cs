using Wsm.Contracts.Models;

namespace Wsm.Contracts.Database
{
    public interface IAccountRepository : IRepository<Account>
    {
        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <returns></returns>
        string GetPassword();
    }
}