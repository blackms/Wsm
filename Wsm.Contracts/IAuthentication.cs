using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Wsm.Contracts.Models;

namespace Wsm.Contracts
{
    public interface IAuthentication
    {
        Token Encode();
        Token Decode();

        bool Verify();
    }
}
