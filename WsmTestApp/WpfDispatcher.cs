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
        private System.Windows.Threading.Dispatcher _Dispatcher;

        public WPFDispatcher(System.Windows.Threading.Dispatcher iDispatcher)
        {
            _Dispatcher = iDispatcher;
        }

        public Task RunAsync(Action act)
        {
            var tcs = new TaskCompletionSource<object>();
            Action doact = () =>
            {
                act();
                tcs.SetResult(null);
            };
            _Dispatcher.BeginInvoke(doact);
            return tcs.Task;
        }

        public void Run(Action act)
        {
            _Dispatcher.Invoke(act);
        }

        public Task<T> EvaluateAsync<T>(Func<T> compute)
        {
            var tcs = new TaskCompletionSource<T>();
            Action doact = () =>
            {
                tcs.SetResult(compute());
            };
            _Dispatcher.BeginInvoke(doact);
            return tcs.Task;
        }

        public T Evaluate<T>(Func<T> compute)
        {
            var res = default(T);
            Action Compute = () => res = compute();
            _Dispatcher.Invoke(Compute);
            return res;
        }

        public bool IsInContext()
        {
            return _Dispatcher.Thread == Thread.CurrentThread;
        }

    }
}
