using System;

namespace Wsm.Utils
{
    public static class Token
    {         
        public static  string EncryptUserToken(string role, DateTime now, string username)
        {
            return RsaClass.Encrypt(role + ";" + now + ";" + username);
        }

        public static string DecryptUserToken(string token)
        {
            return RsaClass.Decrypt(token);
        }  
    }
}
