using MulticastProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

            const int McastPort = 4179;

            Multicast mc2 = new Multicast(multicast, 4179, 128);

            while (true) // Loop indefinitely
            {
                Console.WriteLine("Enter input:"); // Prompt
                string line = Console.ReadLine(); // Get string from user

                if (line == "-s")
                {
                    multicast = IPAddress.Parse("230.1.1.1");

                    //get all valid networkinterfaces applicable to multicast
                    var validnics = nics.Where(adapter => MulticastExtentions.ValidMulticastAdapters(adapter)).ToList();

                    //get valid unicast addresses applicable to multicast
                    var unicastAddresses = from nic in validnics
                                           let index = nic.GetIPProperties().GetIPv4Properties().Index
                                           let uc = nic.GetIPProperties().UnicastAddresses.Where(unicastaddress => MulticastExtentions.ValidUnicastAddress(unicastaddress)).ToList()
                                           select new { index, uc };

                    unicastAddresses.ToList().ForEach(ua => ua.uc.PerformMulticastAction(mc =>
                    {
                        //send to all available unicastaddresses
                        mc.Send("123");

                    }, multicast, McastPort, 128, ua.index));

                }
                            
                if (line == "-r")
                {
                    mc2.JoinMulticastGroup();
                    mc2.Start(); //starts listening
                }

                if (line == "-")
                {
                    Console.WriteLine(mc2.messages.Count);
                }

                if (line == "stop") {
                    mc2.stop();
                    mc2.Dispose();
                    break;
                }
            }
        }
    }
}



