using MulticastProject;
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
    public static class Program
    {       
        
        static NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        static IPAddress multicast;

        const int McastPort = 4179;

        public static void Main(string[] args)
        {
            multicast = IPAddress.Parse("230.1.1.1");

            //get all valid networkinterfaces applicable to multicast
            var validnics = nics.Where(adapter => MulticastExtentions.ValidMulticastAdapters(adapter)).ToList();

            //get valid unicast addresses applicable to multicast
            var unicastAddresses = validnics.Select(nic => nic.GetIPProperties().UnicastAddresses.Where(unicastaddress => MulticastExtentions.ValidUnicastAddress(unicastaddress))).ToList();

            unicastAddresses.ForEach(ua => ua.PerformMulticastAction(mc => {
                //send to all available unicastaddresses
                mc.Send("123");               

            }, multicast, McastPort, 128));
        }
    }
}



