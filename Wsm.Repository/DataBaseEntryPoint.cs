using Wsm.Contracts.Database;

namespace Wsm.DataBaseEntryPoint
{
    /// <summary>
    /// Responsible for database specific stuff
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
