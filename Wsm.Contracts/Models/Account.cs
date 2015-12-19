using System;

namespace Wsm.Contracts.Models
{

    public class Account : IModel
    {
        public Guid id
        {
            get; set;
        }

        public string username { get; set; }

        public string email
        {
            get; set;
        }
    }
}
