using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wsm.Contracts.Dispatcher;

namespace Wsm.Dispatcher
{
    public class WPFDispatcher : IDispatcher
    {
        /// <summary>
        /// The _ dispatcher
        /// </summary>
        public System.Windows.Threading.Dispatcher _dispatcher;

        public WPFDispatcher()
        {
            if (System.Windows.Application.Current == null || System.Windows.Application.Current.Dispatcher == null) //testability issues ! :=)
                throw new ArgumentException("Unable to find dispatcher");

            _dispatcher = System.Windows.Application.Current.Dispatcher;
        }

        /// <summary>
        /// Runs the asynchronous.
        /// </summary>
        /// <param name="act">The act.</param>
        /// <returns></returns>
        public Task RunAsync(Action act)
        {
            var tcs = new TaskCompletionSource<object>();
            Action doact = () =>
            {
                act();
                tcs.SetResult(null);
            };
            _dispatcher.BeginInvoke(doact);
            return tcs.Task;
        }

        /// <summary>
        /// Runs the specified act.
        /// </summary>
        /// <param name="act">The act.</param>
        public void Run(Action act)
        {
            _dispatcher.Invoke(act);
        }

        /// <summary>
        /// Evaluates the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="compute">The compute.</param>
        /// <returns></returns>
        public Task<T> EvaluateAsync<T>(Func<T> compute)
        {
            var tcs = new TaskCompletionSource<T>();
            Action doact = () =>
            {
                tcs.SetResult(compute());
            };
            _dispatcher.BeginInvoke(doact);
            return tcs.Task;
        }

        public T Evaluate<T>(Func<T> compute)
        {
            var res = default(T);
            Action Compute = () => res = compute();
            _dispatcher.Invoke(Compute);
            return res;
        }

        public bool IsInContext()
        {
            return _dispatcher.Thread == Thread.CurrentThread;
        }

    }
}
