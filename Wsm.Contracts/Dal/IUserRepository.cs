using System.ComponentModel.Composition;
using Wsm.Contracts.Models;

namespace Wsm.Contracts.Dal
{
    public interface IUserRepository: IRepository<User>
    {
        User GetById();
    }
}
