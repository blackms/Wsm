using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using Wsm.Contracts.Database;

namespace Wsm.Repository.Redis
{
   public class RedisConnection: IConnection
    {
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
        public dynamic DbContext => new RedisManagerPool(ConnectionString);

    }
}
