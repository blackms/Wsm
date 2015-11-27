using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using Wsm.Contracts.Models;

namespace Wsm.Contracts.Dal
{
    public interface IRepositoryFactory
    {
        IAccountRepository AccountRepository { get; set; }
        IUserRepository UserRepository { get; set; }
    }
}