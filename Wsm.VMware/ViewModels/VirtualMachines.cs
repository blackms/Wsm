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
        private List<VirtualMachine> _vms;

        public VirtualMachines(Func<List<VirtualMachine>, List<VirtualMachine>> virtualMachines)
        {
            virtualMachines(_vms);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void StopAllVms()
        {
            _vms.ForEach(vm => vm.PowerOffVM());
        }

        /// <summary>
        /// Stops the vm.
        /// </summary>
        /// <param name="vm">The vm.</param>
        public void stopVm(string name)
        {
            var machine = _vms.FirstOrDefault(vm => vm.Name == name);

            if (machine == null)
                throw new ArgumentNullException(Constant.VM_IS_NULL);

            machine.PowerOffVM();
        }

        /// <summary>
        /// Starts the vm.
        /// </summary>
        /// <param name="name">The name.</param>
        public void StartVm(string name)
        {
            var machine = _vms.FirstOrDefault(vm => vm.Name == name);

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
            _vms.ForEach(vm => vm.PowerOnVM(vm.MoRef));
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete()
        {
            _vms.ForEach(vm => vm.Destroy());
        }

        /// <summary>
        /// Reverts this instance.
        /// </summary>
        public void Revert()
        {
            _vms.ForEach(vm => vm.reloadVirtualMachineFromPath(vm.Datastore.First().ToString()));
        }

        /// <summary>
        /// Removesnapshotses this instance.
        /// </summary>
        public void RemoveSnapshots()
        {
            _vms.ForEach(vm => vm.RemoveAllSnapshots(false));
        }
    }
}
