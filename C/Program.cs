using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Wsm.Wcf.Contracts;

namespace C
{
    class Program
    {
        static async Task CallCalculator()
        {
            try
            {
                var baseAddress = new Uri("net.tcp://localhost:8000/MyWebService");

                var binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.Message;

                var proxy = new ChannelFactory<ICalculator>(binding, baseAddress.ToString()).CreateChannel();

                var task = await proxy.DivideAsync(10, 1);

                proxy.WaitBlocking();
        
                Console.WriteLine("Result: " + task);
            }
            catch (FaultException<CalculatorFault> ex)
            {
                Console.Error.WriteLine("Error: " + ex.Detail.Message);
            }
        }



        static void Main(string[] args)
        {
            try
            {
                CallCalculator().Wait();

                


            }
            catch (Exception ex)
            {

            }
        }
    }
}
