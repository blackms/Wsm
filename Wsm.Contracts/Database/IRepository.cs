namespace Wsm.Contracts.Database
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    ///-
    public interface IRepository<in TModel> where TModel: class, IModel
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Create(TModel model);
        
        /// <summary>
        /// Reads the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Read(TModel model);
        
        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(TModel model);
        
        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
         void Delete(TModel model);

    }
}
