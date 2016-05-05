using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsm.Contracts.Dispatcher
{
    public interface IDispatcher
    {
        /// <summary>
        /// Run on a safe thread the corresponding action
        /// </summary>
        /// <param name="act">
        /// Action to be executed
        /// </param>
        /// <returns>
        /// the corresponding task
        ///</returns>
        Task RunAsync(Action act);

        /// <summary>
        /// Run on a safe thread the corresponding action and block untill completion
        /// </summary>
        /// <param name="act">
        /// Action to be executed
        /// </param>
        void Run(Action act);

        /// <summary>
        /// Compute a function on a safe thread
        /// </summary>
        /// <param name="compute">
        /// Function to be executed
        /// </param>
        /// <returns>
        /// the corresponding task
        ///</returns>
        Task<T> EvaluateAsync<T>(Func<T> compute);

        /// <summary>
        /// Compute a function on a safe thread and wait for result
        /// </summary>
        /// <param name="compute">
        /// Function to be executed
        /// </param>
        T Evaluate<T>(Func<T> compute);

        /// <summary>
        /// True if current thread is the dispacther thread
        /// </summary>
        bool IsInContext();
    }
}
