using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wsm.Wcf.Concrete;
using Wsm.Wcf.Contracts;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ServiceHost selfHost = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Step 1 Create a URI to serve as the base address.
            var baseAddress = new Uri("net.tcp://localhost:8000/MyWebService");

            // Step 2 Create a ServiceHost instance
            ServiceHost selfHost = new ServiceHost(typeof(CalculatorService), baseAddress);

            try
            {
                var binding = new NetTcpBinding();
                binding.Security.Mode = SecurityMode.Message;

                // Step 3 Add a service endpoint.
                selfHost.AddServiceEndpoint(typeof(ICalculator), binding, baseAddress);

                // Step 4 Enable metadata exchange.      
                //var mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
                //selfHost.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, "mex");               

                // Step 5 Start the service.
                selfHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

         
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
            catch (InvalidOperationException ex)
            {

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (selfHost == null)
                return;

            selfHost.Abort(); //yup i know
        }
    }
}
