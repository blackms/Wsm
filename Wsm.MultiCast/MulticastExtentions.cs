using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MulticastProject
{
    public static class MulticastExtentions
    {
        /// <summary>
        /// The valid multicast adapters
        /// </summary>
        public static Func<NetworkInterface, bool> ValidMulticastAdapters = adapter =>
        {
            return adapter.GetIPProperties().MulticastAddresses.Any() && adapter.SupportsMulticast &&
                   OperationalStatus.Up == adapter.OperationalStatus && adapter.GetIPProperties().GetIPv4Properties() != null;
        };

        /// <summary>
        /// The valid unicast address
        /// </summary>
        public static Func<UnicastIPAddressInformation, bool> ValidUnicastAddress = unicastAddres =>
        {
            var bmsLoopBackAddress = IPAddress.Parse("169.254.25.129");
            return IPAddress.IsLoopback(unicastAddres.Address) == false && (unicastAddres.Address.AddressFamily == AddressFamily.InterNetwork) && unicastAddres.Address.Equals(bmsLoopBackAddress) == false;
        };


        /// <summary>
        /// Executes an action using a specif address
        /// </summary>
        /// <param name="unicastAddresses">The unicast addresses.</param>
        /// <param name="perform">The perform.</param>
        public static void PerformMulticastAction(this IEnumerable<UnicastIPAddressInformation> unicastAddresses, Action<Multicast> perform, IPAddress multicast, int port, int ttl)
        {
            unicastAddresses.ToList().ForEach(uni =>
            {
                Multicast mc = new Multicast(multicast, uni.Address, port, ttl);
                mc.JoinMulticastGroup();
                perform(mc);
                mc.Dispose();  
            });
        }
    }
}
