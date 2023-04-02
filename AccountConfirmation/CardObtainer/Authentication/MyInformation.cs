using CardObtainer.Dto;
using Microsoft.Identity.Client;
using System.Security;

namespace CardObtainer.Authentication
{
    public class MyInformation
    {
        public MyInformation(IPublicClientApplication app, HttpClient client, string microsoftGraphBaseEndpoint)
        {
            tokenAcquisitionHelper = new PublicAppUsingUsernamePassword(app);
            MicrosoftGraphBaseEndpoint = microsoftGraphBaseEndpoint;
        }

        protected PublicAppUsingUsernamePassword tokenAcquisitionHelper;

        // Scopes to request access to the protected Web API (here Microsoft Graph)
        private static string[] Scopes { get; set; } = new string[] { "User.Read", "User.ReadBasic.All" };

        // Base endpoint for Microsoft Graph
        private string MicrosoftGraphBaseEndpoint { get; set; }

        // Calls the Web API and displays its information
        public async Task<AuthenticationStatuses> DisplayMeAndMyManagerRetryingWhenWrongCredentialsAsync(CredentialsModel model)
        {
            try
            {
                string username = model.Login;
                SecureString password = SecurePassword(model.Password);

                AuthenticationStatuses authenticationStatus = await tokenAcquisitionHelper.GetTokenForWebApiUsingUsernamePasswordAsync(Scopes, username, password);

                return authenticationStatus;
            }
            catch (ArgumentException ex) when (ex.Message.StartsWith("U/P"))
            {
                return AuthenticationStatuses.Error;
            }
        }

        private static SecureString SecurePassword(string strPassword)
        {
            var secureStr = new SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword)
                {
                    secureStr.AppendChar(c);
                }
            }
            return secureStr;
        }
    }
}
