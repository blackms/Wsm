using System;
using System.Linq;
using Wsm.VMware.ViewModels;
using VMware.Vim;

namespace Wsm.VMware
{
    public class VMwareManager : IDisposable
    {
        /// <summary>
        /// The _resource pool
        /// </summary>
        public ResourcePools ResourcePools;      

        /// <summary>
        /// The _client
        /// </summary>
        private VimClientImpl _client;

        /// <summary>
        /// The host
        /// </summary>
        public string host, username, password;

        public VMwareManager(string host, string username, string password, string resourcepoolName)
        {
            //Create Client
            _client = new VimClientImpl();

            var sc = _client.Connect(host);
            var us = _client.Login(username, password);

            //Retrieve rpools
            ResourcePools = new ResourcePools(_client);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            _client.Disconnect();

            var disposable = (_client as IDisposable);

            if (disposable != null)
                disposable.Dispose();

            disposable = (ResourcePools as IDisposable);

            if (disposable != null)
                disposable.Dispose();

            disposable = (VirtualMachines as IDisposable);

            if (disposable != null)
                disposable.Dispose();
        }
    }
}
