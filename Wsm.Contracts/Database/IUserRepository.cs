using Wsm.Contracts.Models;
 namespace Wsm.Contracts.Database
{
    public interface IUserRepository: IRepository<User>
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <returns></returns>
        User GetById();
    }
}
