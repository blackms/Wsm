using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wsm.Contracts.Database
{
    public interface IConnection
    {
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }
        dynamic DbContext { get;}

    }

}
