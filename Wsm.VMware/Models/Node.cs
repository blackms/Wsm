using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wsm.VMware.Models;

namespace Wsm.VMware
{
    internal class Node
    {
        /// <summary>
        /// The _name
        /// </summary>
        private string _name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The _snapshots
        /// </summary>
        private IList<Snapshot> _snapshots;

        /// <summary>
        /// Gets or sets the snapshots.
        /// </summary>
        /// <value>
        /// The snapshots.
        /// </value>
        public IList<Snapshot> Snapshots
        {
            get { return _snapshots; }
            set { _snapshots = value; }
        }

    }
}
