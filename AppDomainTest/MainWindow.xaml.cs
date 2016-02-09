using System;
using System.Threading.Tasks;
using System.Windows;
using GatewaysExecutableHandler;

namespace AppDomainTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        GatewaysProxy proxy = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            var handler = new ExecutableHandler();

            handler.CreateAppDomain();
            proxy = handler.GetObject<GatewaysProxy>();

            Console.WriteLine(proxy.GatewaysMonitor.i);
            
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                var result = WaitFor(() =>
                {
                    Console.WriteLine(AppDomain.CurrentDomain.FriendlyName + " " + proxy.GatewaysMonitor.i);
                    System.Threading.Thread.Sleep(5000);
                    return true;
                }, 60, 60);
            });
      
        }

        public TResult WaitFor<TResult>(Func<TResult> action, int timeout, int pollingInterval)
        {
            do
            {
                action();             

            } while (proxy.GatewaysMonitor.i < 60);

            Console.WriteLine("Done");

            return default(TResult);
        }
    }
}
