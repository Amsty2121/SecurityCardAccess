using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HostedSupervizer.Configurations
{
    public class AuthOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            if (SecretKey == null)
            {
                string errorMessage = "Login failed - invalid SecretKey";

                throw new ArgumentNullException(errorMessage);
            }

            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}
