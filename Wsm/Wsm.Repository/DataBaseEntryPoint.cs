using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsm.Contracts;
using Wsm.Contracts.Dal;

namespace Wsm.Repository
{
    /// <summary>
    /// Responsible for returning a database specific entrypoint
    /// </summary>
    public class DataBaseEntryPoint : IDataBaseEntryPoint
    {
        public IRepositoryFactory RepositoryFactory
        {
            get; set;
        }
    }
}
