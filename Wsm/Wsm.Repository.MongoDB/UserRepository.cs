using MongoDB.Driver;
using System.Threading.Tasks;
using Wsm.Contracts;
using Wsm.Contracts.Dal;
using Wsm.Contracts.Models;
using System;
using System.ComponentModel.Composition;

namespace Wsm.Repository.MongoDB
{
    /// <summary>
    /// User specific tasks
    /// </summary>

    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository()
        {
            
        }

        public UserRepository([Import("DataBaseEntryPoint")] dynamic dbContext)
        {
            DbContext = dbContext;
        }

        public User GetById()
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string username)
        {
            var filter = Builders<User>.Filter.Eq("firstname", username);
            return null;//DbContext.GetCollection<User>(Collectionname).Find(filter).FirstOrDefaultAsync();       
        }
    }
}
