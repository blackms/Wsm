using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewaysExecutableHandler
{
    [Serializable]
    public class Monitor
    {
        public int i = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Monitor"/> class.
        /// </summary>
        public Monitor()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    int count = 0;

                    while (i < 60)
                    {
                        i = count++;
                        System.Threading.Thread.Sleep(500);
                    }
                });

            }
            catch (Exception)
            {
                //ignore
            }


        }


    }
}
