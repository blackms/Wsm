using MongoDB.Driver;
using Wsm.Contracts.Database;

namespace Wsm.Repository.MongoDB
{

    public class MongoConnection: IConnection
    {
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
        public dynamic DbContext => new MongoClient(ConnectionString).GetDatabase(DataBaseName);
    }
}
