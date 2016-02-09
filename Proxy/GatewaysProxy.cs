using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatewaysExecutableHandler
{
    public class GatewaysProxy : MarshalByRefObject
    {

        /// <summary>
        /// The _monitor
        /// </summary>
        private Monitor _monitor;

        /// <summary>
        /// Gets the gateways monitor.
        /// </summary>
        /// <value>
        /// The gateways monitor.
        /// </value>
        public Monitor GatewaysMonitor
        {
            get
            {
                return _monitor ?? (_monitor = new Monitor());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GatewaysProxy"/> class.
        /// </summary>
        public GatewaysProxy()
        {

        }
    }
}
