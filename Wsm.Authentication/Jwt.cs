using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Wsm.Authentication
{
    /// <summary>
    /// Json Stateless authentication based on jwt protocol
    /// </summary>
    public class Jwt : IAuthentication
    {
        public enum JwtHashAlgorithm
        {
            HS256,
            HS384,
            HS512
        }

        private readonly Dictionary<string, JwtHashAlgorithm> _getHashAlgorithm = new Dictionary<string, JwtHashAlgorithm>
        {
            {"HS256", JwtHashAlgorithm.HS256},
            {"HS384", JwtHashAlgorithm.HS384},
            {"HS512", JwtHashAlgorithm.HS512}
        };

        private static readonly IDictionary<JwtHashAlgorithm, Func<byte[], byte[], byte[]>> HashAlgorithms;

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        static Jwt()
        {
            HashAlgorithms = new Dictionary<JwtHashAlgorithm, Func<byte[], byte[], byte[]>>
            {
                { JwtHashAlgorithm.HS256, (key, value) => { using (var sha = new HMACSHA256(key)) { return sha.ComputeHash(value); } } },
                { JwtHashAlgorithm.HS384, (key, value) => { using (var sha = new HMACSHA384(key)) { return sha.ComputeHash(value); } } },
                { JwtHashAlgorithm.HS512, (key, value) => { using (var sha = new HMACSHA512(key)) { return sha.ComputeHash(value); } } }
            };
        }

        private readonly string _secretKey;
        
        public Jwt(string secretKey)
        {
            _secretKey = secretKey;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public Token Decode(string token, byte[] key)
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Token must consist from 3 delimited by dot parts");
            }

            var header = parts[0];
            var payload = parts[1];
            var crypto = Base64UrlDecode(parts[2]);

            var headerJson = Encoding.UTF8.GetString(Base64UrlDecode(header));
            var payloadJson = Encoding.UTF8.GetString(Base64UrlDecode(payload));

            var headerData = JsonConvert.DeserializeObject<Dictionary<string, string>>(headerJson);

            //var bytesToSign = Encoding.UTF8.GetBytes(string.Concat(header, ".", payload));
            //var algorithm = headerData["alg"];

            //var signature = HashAlgorithms[_getHashAlgorithm[algorithm]](key, bytesToSign);
            //var decodedCrypto = Convert.ToBase64String(crypto);
            //var decodedSignature = Convert.ToBase64String(signature);
            //Verify(decodedCrypto, decodedSignature, payloadJson);

            return new Token();
        }

        public Token Encode(IDictionary<string, object> extraHeaders, object payload, byte[] key, JwtHashAlgorithm algorithm)
        {
            var segments = new List<string>();
            var header = new Dictionary<string, object>(extraHeaders) { { "type", "JWT" }, { "alg", algorithm.ToString() } };

            var headerBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(header));
            var payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload));

            segments.Add(Base64UrlEncode(headerBytes));
            segments.Add(Base64UrlEncode(payloadBytes));

            var stringToSign = string.Join(".", segments.ToArray());
            var bytesToSign = Encoding.UTF8.GetBytes(stringToSign);

            segments.Add(Base64UrlEncode(HashAlgorithms[algorithm](key, bytesToSign)));

            return new Token { EncodedTokenValue = string.Join(".", segments.ToArray()) };
        }

        public bool Verify(string decodedCrypto, string decodedSignature, string payloadJson)
        {
            if (decodedCrypto != decodedSignature)
                return false;

            var payloadData = JsonConvert.DeserializeObject<Dictionary<string, string>>(payloadJson);

            if (!payloadData.ContainsKey("exp") || payloadData["exp"] == null)
                return false;

            int exp;
            int.TryParse(payloadData["exp"], out exp);
            return !(Math.Round((DateTime.UtcNow - UnixEpoch).TotalSeconds) >= exp);
        }

        // from JWT spec
        private string Base64UrlEncode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
            return output;
        }

        // from JWT spec
        private byte[] Base64UrlDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: output += "=="; break; // Two pad chars
                case 3: output += "="; break;  // One pad char
                default: throw new Exception("Illegal base64url string!");
            }
            var converted = Convert.FromBase64String(output); // Standard base64 decoder
            return converted;
        }

        public Token Encode()
        {
            throw new NotImplementedException();
        }

        public Token Decode()
        {
            throw new NotImplementedException();
        }

        public bool Verify()
        {
            throw new NotImplementedException();
        }
    }
}
