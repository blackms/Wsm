namespace Wsm.Contracts.Dal
{
    public interface IDataBaseEntryPoint
    {
        IRepositoryFactory RepositoryFactory { get; set; }
    }
  }
