using Wsm.Contracts.Models;

namespace Wsm.Contracts.Dal
{
    public interface IAccountRepository : IRepository<Account>
    {
        string GetPassword();
    }
}