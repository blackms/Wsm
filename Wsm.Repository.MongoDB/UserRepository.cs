using MongoDB.Driver;
using Wsm.Contracts.Models;
using System;
using Wsm.Contracts.Database;

namespace Wsm.Repository.MongoDB
{
    /// <summary>
    /// User specific tasks
    /// </summary>

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbConnection"></param>
        public UserRepository(IConnection dbConnection)
        {
            DbContext = dbConnection.DbContext;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public User GetById()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the name of the by user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public User GetByUserName(string username)
        {
            var filter = Builders<User>.Filter.Eq("firstname", username);
            return null;//DbContext.GetCollection<User>(Collectionname).Find(filter).FirstOrDefaultAsync();       
        }
    }
}
