using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsm.VMware.Models;
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
        /// The virtual machines
        /// </summary>
        public VirtualMachines VirtualMachines;

        /// <summary>
        /// The _client
        /// </summary>
        private VimClientImpl _client;

        /// <summary>
        /// The host
        /// </summary>
        public string host, username, password;

        public VMwareManager(string host, string username, string password)
        {
            _client = new VimClientImpl();
            var sc = _client.Connect(host);
            var us = _client.Login(username, password);

            //Retrieve rp
            ResourcePools = new ViewModels.ResourcePools(_client);
            ResourcePools.Name = "Test";
            ResourcePools.SelectedResourcePool();

            //Retrieve VMS
            VirtualMachines = new VirtualMachines((vms) =>
            {
                ResourcePools.ResourcePool.Vm.ToList().ForEach(moRef =>
                {
                    var vm = (_client.FindEntityView(typeof(VirtualMachine), moRef, null, null) as VirtualMachine);

                    if (vm != null)
                        vms.Add(vm);
                });
                return vms;
            });
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
