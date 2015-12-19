namespace Wsm.Contracts.Models
{
    public class Token
    {
        public string EncodedTokenValue { get; set; }
        public string DecodedTokenValue { get; set; }

        public void ValidateToken()
        {
        }
    }
}
