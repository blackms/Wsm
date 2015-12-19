namespace Wsm.Contracts.Database
{
    public interface IDataBaseEntryPoint
    {
        IRepositoryFactory RepositoryFactory { get; set; }
    }
  }
