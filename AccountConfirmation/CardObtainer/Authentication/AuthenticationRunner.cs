using CardObtainer.Dto;
using Microsoft.Identity.Client;

namespace CardObtainer.Authentication
{
    public class AuthenticationRunner
    {
        public static async Task<AuthenticationStatuses> RunAsync(CredentialsModel model)
        {
            SampleConfiguration config = SampleConfiguration.ReadFromJsonFile("appsettings.json");
            var appConfig = config.PublicClientApplicationOptions;
            var app = PublicClientApplicationBuilder.CreateWithApplicationOptions(appConfig)
                                                    .Build();
            var httpClient = new HttpClient();

            MyInformation myInformation = new MyInformation(app, httpClient, config.MicrosoftGraphBaseEndpoint);
            return await myInformation.DisplayMeAndMyManagerRetryingWhenWrongCredentialsAsync(model);
        }
    }
}
