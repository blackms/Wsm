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
        public AccountRepository()
        {

        }

        public AccountRepository([Import("DataBaseEntryPoint")] dynamic dbContext)
        {
            DbContext = dbContext;
        }
        public string GetPassword()
        {
            throw new System.NotImplementedException();
        }
    }
}
