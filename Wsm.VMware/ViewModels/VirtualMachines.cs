using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMware.Vim;

namespace Wsm.VMware.ViewModels
{
    public class VirtualMachines
    {
        internal List<VirtualMachine> Vms; // no its not!!

        public VirtualMachines(Func<List<VirtualMachine>, List<VirtualMachine>> virtualMachines)
        {
            virtualMachines(Vms);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void StopAllVms()
        {
            Vms.Where(vm => vm.Name.ToLower().Contains("controller") == false && vm.Name.ToLower().Contains("ctrl") == false).ToList()
                .ForEach(vm => vm.ShutdownGuest());
        }

        /// <summary>
        /// Stops the vm.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public void stopVm(string name)
        {
            var machine = Vms.FirstOrDefault(vm => vm.Name == name);

            if (machine == null)
                throw new ArgumentNullException(Constant.VM_IS_NULL);

            machine.ShutdownGuest();
        }

        /// <summary>
        /// Starts the vm.
        /// </summary>
        /// <param name="name">The name.</param>
        public void StartVm(string name)
        {
            var machine = Vms.FirstOrDefault(vm => vm.Name == name);

            if (machine == null)
                throw new ArgumentNullException(Constant.VM_IS_NULL);

            machine.PowerOnVM(machine.MoRef);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void StartAllVms()
        {
            //host = resourcepool??
            Vms.Where(vm => vm.Name.ToLower().Contains("controller") == false && vm.Name.ToLower().Contains("ctrl") == false).ToList()
                .ForEach(vm => vm.PowerOnVM(vm.MoRef));
        }

        /// <summary>
        /// Reverts this instance.
        /// </summary>
        public void Revert()
        {
            Vms.Where(vm => vm.Name.ToLower().Contains("controller") == false && vm.Name.ToLower().Contains("ctrl") == false).ToList()
                .ForEach(vm => vm.reloadVirtualMachineFromPath(vm.Datastore.First().ToString()));
        }

        /// <summary>
        /// Removesnapshotses this instance.
        /// </summary>
        public void RemoveSnapshots()
        {
            Vms.Where(vm => vm.Name.ToLower().Contains("controller") == false && vm.Name.ToLower().Contains("ctrl") == false).ToList()
                .ForEach(vm => vm.RemoveAllSnapshots(false));
        }
    }
}
