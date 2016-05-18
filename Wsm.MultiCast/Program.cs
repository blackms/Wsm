using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace MulticastProject
{
    public static class Program
    {
        static NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        static IPAddress multicast;

        const int McastPort = 4179;

        public static void Main(string[] args)
        {
            if (args[0] == "-s")
            {
                multicast = IPAddress.Parse("230.1.1.1");

                //get all valid networkinterfaces applicable to multicast
                var validnics = nics.Where(adapter => MulticastExtentions.ValidMulticastAdapters(adapter)).ToList();

                //get valid unicast addresses applicable to multicast
                var unicastAddresses = from nic in validnics
                                       let index = nic.GetIPProperties().GetIPv4Properties().Index
                                       let uc = nic.GetIPProperties().UnicastAddresses.Where(unicastaddress => MulticastExtentions.ValidUnicastAddress(unicastaddress)).ToList()
                                       select new { index, uc };

                //validnics.Select(nic => new { nic.GetIPProperties().GetIPv4Properties().Index ,
                //                            nic.GetIPProperties().UnicastAddresses.Where(unicastaddress => MulticastExtentions.ValidUnicastAddress(unicastaddress)).ToList()});

                unicastAddresses.ToList().ForEach(ua => ua.uc.PerformMulticastAction(mc =>
                {

                    //send to all available unicastaddresses
                    mc.Send("123");

                }, multicast, McastPort, 128));

            }


            if (args[0] == "-r")
            {
                Multicast mc = new Multicast(multicast, 4179, 128);
                while (System.Console.KeyAvailable)
                {
                    mc.Receive();
                }
            }

        }
    }
}



