using System;
using System.Linq;

using VMware.Vim;

namespace Wsm.VMware
{
    public class VMwareManager : IDisposable
    {

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

            VirtualMachine vm = new VirtualMachine(_client, new ManagedObjectReference());
 
            //mySpec.Customization = new CustomizationSpec();
            //CustomizationSysprep winIdent = (CustomizationSysprep)new CustomizationIdentitySettings();
            //CustomizationFixedName hostname = new CustomizationFixedName();
            //hostname.Name = "TS3-0666";
            //winIdent.UserData.ComputerName = hostname;
            //mySpec.Customization.Identity = winIdent;
        }

        public void Dispose()
        {
            _client.Disconnect();

            var disposable = (_client as IDisposable);

            if (disposable != null)
                disposable.Dispose();

            if (disposable != null)
                disposable.Dispose();
        }
    }
}
