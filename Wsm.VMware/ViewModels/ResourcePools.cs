using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using VMware.Vim;

namespace Wsm.VMware.ViewModels
{
    public class ResourcePools
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        //selected pool
        public ResourcePool ResourcePool;

        /// <summary>
        /// The _client
        /// </summary>
        private VimClientImpl _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModels.ResourcePools"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public ResourcePools(VimClientImpl client)
        {
            _client = client;

            GetResourcePools();
        }

        private List<ResourcePool> _AvailabelResourcePools;
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
        public void SelectedResourcePool()
        {
            ResourcePool = _AvailabelResourcePools.FirstOrDefault(rp => rp.Name == Name);
        }

        /// <summary>
        /// Gets the virtual machines.
        /// </summary>
        public List<VirtualMachine> getVirtualMachines()
        {
            var vms = new List<VirtualMachine>();

            ResourcePool.Vm.ToList().ForEach(moRef =>
            {
                var vm = (_client.FindEntityView(typeof(VirtualMachine), moRef, null, null) as VirtualMachine);

                if (vm != null)
                    vms.Add(vm);
            });

            return vms;
        }

        public ResourcePool GetResoucePool(string name)
        {
            NameValueCollection filterHost = new NameValueCollection();
            filterHost.Add("Name", name);

            return (_client.FindEntityView(typeof(ResourcePool), null, filterHost, new string[] { "Name" }) as ResourcePool);
        }

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
            var resourcepool = AvailableResourcePools.FirstOrDefault(rp => rp.Name == rp1);

            //GetResoucePool(name);
            if (resourcepool == null)
                Create(resourcepool);

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
