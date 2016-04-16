using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsm.VMware.Models
{
    internal class Snapshot
    {

        /// <summary>
        /// The _description
        /// </summary>
        private string _description;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Snapshot"/> class.
        /// </summary>
        public Snapshot()
        {

        }

        public Snapshot(string desc)
        {
            _description = desc;
        }


    }
}
