using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMware.Vim;

namespace Wsm.VMware.ViewModels
{
    public static class VirtualMachineExtentions
    {

        /// <summary>
        /// Launches the power shell.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <param name="client">The client.</param>
        /// <param name="arguments">The arguments.</param>
        public static void LaunchPowerShell(this VirtualMachine vm, VimClientImpl client, string username, string password, string arguments)
        {
            //arguments = "-command \"c:\\temp\\policy.ps1\"";
            ExecuteTaskInGuest(client, username, password, (opm, authm, filem, procm, npa) =>
            {
                GuestProgramSpec spec = new GuestProgramSpec();
                spec.ProgramPath = "C:\\Windows\\SysWOW64\\WindowsPowerShell\\v1.0\\powershell.exe";
                spec.Arguments = arguments;
                procm.StartProgramInGuest(vm.MoRef, npa, spec);
            });
        }

        /// <summary>
        /// Transfers the file to virtual machine.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <param name="client">The client.</param>
        /// <param name="filetoCopy">The fileto copy.</param>
        public static void TransferFileToVirtualMachine(this VirtualMachine vm, VimClientImpl client, string username, string password, string filetoCopy)
        {
            ExecuteTaskInGuest(client, username, password, (opm, authm, filem, procm, npa) =>
            {
                System.IO.FileInfo FileToTransfer = new System.IO.FileInfo(filetoCopy);
                GuestFileAttributes GFA = new GuestFileAttributes()
                {
                    AccessTime = FileToTransfer.LastAccessTimeUtc,
                    ModificationTime = FileToTransfer.LastWriteTimeUtc
                };
                filem.InitiateFileTransferToGuest(vm.MoRef, npa, filetoCopy, GFA, FileToTransfer.Length, false);
            });
        }

        /// <summary>
        /// Launches the power shell.
        /// </summary>
        /// <param name="vm">The vm.</param>
        /// <param name="client">The client.</param>
        /// <param name="execute">The execute.</param>
        private static void ExecuteTaskInGuest(VimClientImpl client, string username, string password, Action<GuestOperationsManager, GuestAuthManager, GuestFileManager, GuestProcessManager, NamePasswordAuthentication> execute)
        {
            var operationManager = new GuestOperationsManager(client, client.ServiceContent.GuestOperationsManager);
            var authManager = new GuestAuthManager(client, client.ServiceContent.AuthorizationManager);
            var processManager = new GuestProcessManager(client, client.ServiceContent.GuestOperationsManager);
            var FileManager = new GuestFileManager(client, client.ServiceContent.FileManager);

            NamePasswordAuthentication npa = new NamePasswordAuthentication
            {
                Username = username,
                Password = password
            };

            execute(operationManager, authManager, FileManager, processManager, npa);
        }


    }
}
