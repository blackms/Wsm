using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wsm.Contracts.Database
{
    public interface IConnection
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; set; }
        /// <summary>
        /// Gets or sets the name of the data base.
        /// </summary>
        /// <value>
        /// The name of the data base.
        /// </value>
        string DataBaseName { get; set; }
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        dynamic DbContext { get;}

    }

}
