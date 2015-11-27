using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wsm.Models
{
    public class User : IModel
    {


        private string _email;
        private string _username;

        private string _lastname;

        public string LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        Guid _id;
        public Guid id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
