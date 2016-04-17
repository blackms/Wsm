using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using VMware.Vim;

namespace Wsm.VMware.ViewModels
{
    public class ResourcePools
    {
        //selected pool
        /// <summary>
        /// The selected resource pool
        /// </summary>
        public ResourcePool SelectedResourcePool { get; set; }

        /// <summary>
        /// The virtual machines
        /// </summary>
        public VirtualMachines VirtualMachines { get; set; }

        /// <summary>
        /// The _client
        /// </summary>
        private VimClientImpl _client { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModels.ResourcePools"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ResourcePools(VimClientImpl client)
        {
            _client = client;
            GetResourcePools();
        }

        /// <summary>
        /// The _ availabel resource pools
        /// </summary>
        private List<ResourcePool> _AvailabelResourcePools { get; set; }

        /// <summary>
        /// The _resource pool
        /// </summary>
        public List<ResourcePool> AvailableResourcePools
        {
            get { return _AvailabelResourcePools; }
            set { _AvailabelResourcePools = value; }
        }

        /// <summary>
        /// Gets the resource pools.
        /// </summary>
        public void GetResourcePools()
        {
            _AvailabelResourcePools = _client.FindEntityViews(typeof(ResourcePool), null, null, null).Cast<ResourcePool>().ToList();

        }

        /// <summary>
        /// Selecteds the resource pool.
        /// </summary>
        public ResourcePool GetResourcePool(string name)
        {
            var resourcePool = _AvailabelResourcePools.FirstOrDefault(rp => rp.Name == name);

            if (resourcePool == null)
                throw new ArgumentException(Constant.RESOURCEPOOL_IS_NULL);

            GetVirtualMachinesFromResourcePool(resourcePool);

            return resourcePool;
        }

        public void GetVirtualMachinesFromResourcePool(ResourcePool rsp)
        {
            VirtualMachines = new VirtualMachines((vms) =>
            {
                rsp.Vm.ToList().ForEach(moRef =>
                {
                    var vm = (_client.FindEntityView(typeof(VirtualMachine), moRef, null, null) as VirtualMachine);

                    if (vm != null)
                        vms.Add(vm);
                });
                return vms;
            });
        }

        /// <summary>
        /// Gets the resouce pool.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ResourcePool GetResourcePoolFromEsx(string name)
        {
            NameValueCollection filterHost = new NameValueCollection() { { "Name", name } };

            var resourcePool = (_client.FindEntityView(typeof(ResourcePool), null, filterHost, new string[] { "Name" }) as ResourcePool);

            if (resourcePool == null)
                throw new ArgumentException(Constant.RESOURCEPOOL_IS_NULL);

            GetVirtualMachinesFromResourcePool(resourcePool);

            return resourcePool;
        }

        /// <summary>
        /// Gets the host.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        public void GetHost(string hostName)
        {
            NameValueCollection filterHost = new NameValueCollection();
            filterHost.Add("Name", hostName);
            HostSystem hosRef = (HostSystem)_client.FindEntityView(typeof(HostSystem), null, filterHost, new string[] { "Name" });
            HostSystem host = (_client.GetView(hosRef.MoRef, null) as HostSystem);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public void Clone(string rp1, string toBeCloned)
        {
            var resourcepool = GetResourcePoolFromEsx(rp1);

            if (resourcepool == null)
                Create(resourcepool);

            var PoolToBeCloned = GetResourcePoolFromEsx(toBeCloned);

            if (PoolToBeCloned == null)
                throw new ArgumentException(Constant.RESOURCEPOOL_IS_NULL);

            VirtualMachines.StopAllVms();
            VirtualMachines.Revert();

            VirtualMachines.Vms.ForEach(clone =>
             {
                 var folder = (_client.FindEntityView(typeof(Folder), clone.MoRef, null, null) as Folder);
                 var cloneSpec = new VirtualMachineCloneSpec();

                 clone.CloneVM(folder.MoRef, clone.Name + "Clone", cloneSpec);
             });
        }

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Create(ResourcePool rp)
        {
            ResourceConfigSpec ResourceConfigSpec = new ResourceConfigSpec();
            SharesInfo sharesInfo = new SharesInfo();
            sharesInfo.Level = SharesLevel.normal;
            sharesInfo.Shares = 0;

            ResourceAllocationInfo cpu = new ResourceAllocationInfo();
            cpu.Limit = -1;
            // cpu.limitSpecified = true;
            cpu.Shares = sharesInfo;
            cpu.Reservation = -1;
            //cpu.reservationSpecified = true;
            cpu.ExpandableReservation = true;
            // cpu.expandableReservationSpecified = true;
            ResourceAllocationInfo mem = new ResourceAllocationInfo();
            mem.Limit = -1;
            //mem.limitSpecified = true;
            mem.Shares = sharesInfo;
            mem.Reservation = 0;
            // mem.reservationSpecified = true;

            mem.ExpandableReservation = true;
            //mem.expandableReservationSpecified = true;

            var ResourcePoolSpec = new ResourceConfigSpec();
            ResourcePoolSpec.CpuAllocation = cpu;
            ResourcePoolSpec.MemoryAllocation = mem;

            rp.CreateResourcePool(Name, ResourcePoolSpec);
        }
    }
}
