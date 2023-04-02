using CardObtainer.Authentication;
using CardObtainer.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Text;

namespace CardObtainer.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountConfirmationController
{
    [HttpGet]
    public Task<string> GetCardToken([FromQuery] CredentialsModel model)
    {
        AuthenticationStatuses status = AuthenticationStatuses.Undefined;
        try
        {
            status = AuthenticationRunner.RunAsync(model).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        string reply;

        reply = status switch
        {
            AuthenticationStatuses.AuthentificationPass => "token-" + model.Login.Split('@')[0], //dA==bw==aw==ZQ==bg==
            AuthenticationStatuses.Error => "error", //ZQ==cg==cg==bw==cg==
            AuthenticationStatuses.Undefined => "00000", //MA==MA==MA==MA==MA==
        };


        return Task.FromResult(reply);
    }
}
