using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.IO;
using System.Reflection;

namespace CardObtainer.Authentication
{
    public class SampleConfiguration
    {
        public PublicClientApplicationOptions PublicClientApplicationOptions { get; set; }

        // Base URL for Microsoft Graph (it varies depending on whether the application is ran
        // in Microsoft Azure public clouds or national / sovereign clouds
        public string MicrosoftGraphBaseEndpoint { get; set; }

        // Reads the configuration from a json file
        public static SampleConfiguration ReadFromJsonFile(string path)
        {
            IConfigurationRoot Configuration;

            var builder = new ConfigurationBuilder()
             .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
            .AddJsonFile(path);

            Configuration = builder.Build();

            // Read the auth and graph endpoint config
            SampleConfiguration config = new SampleConfiguration()
            {
                PublicClientApplicationOptions = new PublicClientApplicationOptions()
            };
            Configuration.Bind("Authentication", config.PublicClientApplicationOptions);
            config.MicrosoftGraphBaseEndpoint = Configuration.GetValue<string>("WebAPI:MicrosoftGraphBaseEndpoint");
            return config;
        }
    }
}
