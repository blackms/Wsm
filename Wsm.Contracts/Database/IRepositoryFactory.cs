namespace Wsm.Contracts.Database
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Gets or sets the account repository.
        /// </summary>
        /// <value>
        /// The account repository.
        /// </value>
        IAccountRepository AccountRepository { get; set; }
        /// <summary>
        /// Gets or sets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        IUserRepository UserRepository { get; set; }
    }
}