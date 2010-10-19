using System;
using System.Threading;

namespace DataScraper.Tools
{
    public class ThreadUtil
    {

        /// <summary>
        /// Delegate to wrap another delegate and its arguments
        /// </summary>
        delegate void DelegateWrapper(Delegate d, object[] args);

        /// <summary>
        /// An instance of DelegateWrapper which calls InvokeWrappedDelegate,
        /// which in turn calls the DynamicInvoke method of the wrapped
        /// delegate.
        /// </summary>
        static DelegateWrapper wrapperInstance = new DelegateWrapper(InvokeWrappedDelegate);

        /// <summary>
        /// Callback used to call <code>EndInvoke</code> on the asynchronously
        /// invoked DelegateWrapper.
        /// </summary>
        static AsyncCallback callback = new AsyncCallback(EndWrapperInvoke);

        /// <summary>
        /// Executes the specified delegate with the specified arguments
        /// asynchronously on a thread pool thread.
        /// </summary>
        public static void FireAndForget(Delegate d, params object[] args)
        {
            // Invoke the wrapper asynchronously, which will then
            // execute the wrapped delegate synchronously (in the
            // thread pool thread)
            wrapperInstance.BeginInvoke(d, args, callback, null);
        }

        /// <summary>
        /// Invokes the wrapped delegate synchronously
        /// </summary>
        static void InvokeWrappedDelegate(Delegate d, object[] args)
        {
            d.DynamicInvoke(args);
        }

        /// <summary>
        /// Calls EndInvoke on the wrapper and Close on the resulting WaitHandle
        /// to prevent resource leaks.
        /// </summary>
        static void EndWrapperInvoke(IAsyncResult ar)
        {
            wrapperInstance.EndInvoke(ar);
            ar.AsyncWaitHandle.Close();
        }
    }
}
