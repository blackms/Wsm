using System;
using Wsm.Contracts;
using Wsm.Contracts.Database;
using Wsm.Contracts.Models;

namespace Wsm.Repository.Redis
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class Repository<TModel> : IRepository<TModel> where TModel : class, IModel
    {
        /// <summary>
        /// The _DB manager
        /// </summary>
        protected dynamic DbContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TModel}"/> class.
        /// </summary>
        public Repository()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TModel}"/> class.
        /// </summary>
        public Repository(IConnection dbCon)
        {
            DbContext = dbCon.DbContext;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Create(TModel model)
        {
            DbContext.GetClient().As<TModel>().Store(model);
        }

        /// <summary>
        /// Reads the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Read(TModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(TModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Delete(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}
