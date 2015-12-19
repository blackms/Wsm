using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsmTestApp
{

    public class WsmConnection
    {
        [Export("DataBaseEntryPoint")]
        public dynamic dataBaseEntryPoint
        {
            get
            {
                const string connectionString = "mongodb://admin:test123@127.0.0.1/WsmDb";
                return new MongoClient(connectionString).GetDatabase("WsmDb");
            }
        }
    }
}
