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
        /// <summary>
        /// Gets or sets the repository factory.
        /// </summary>
        /// <value>
        /// The repository factory.
        /// </value>
        public IRepositoryFactory RepositoryFactory
        {
            get; set;
        }
    }
}
