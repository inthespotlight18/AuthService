
using Microsoft.Graph;
using Azure.Identity;

//https://stackoverflow.com/questions/75604903/delegateauthenticationprovider-not-found-after-updating-microsoft-graph
//https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/feature/5.0/docs/upgrade-to-v5.md#authentication
namespace AuthModelLib;
public class graphAuth : iGAuth
{

    const string tenantId = "tugit.onmicrosoft.com";
    const string clientId = "d22e80d0-6e1d-4998-84a8-251485588156";
    const string secret = "V6M8Q~7L_9OVzvH~GjzMVb.BbR4FIh-3--Zaoc1A";

    private ClientSecretCredential csc;

    static readonly string[] _scopes = new string[]
    {
        "https://graph.microsoft.com/.default"
    };

    public async Task<string> AuthLogin()
    {
        await GetProfile();
        var v = csc.GetToken;
        var gsc = new GraphServiceClient(csc, _scopes);

        //var users = gsc.Users.GetAsync();
        //Console.WriteLine("AuthTokenCSCAsync : user.count[{0}]", users.Value.Count);

        string funcName = "graphAuth->AuthLogin: ";

        return (gsc != null) ? (funcName + "OK") : (funcName + "FAIL");
    }

    public async Task GetProfile()
    {
        var options = new ClientSecretCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        };
        csc = new ClientSecretCredential(tenantId, clientId, secret, options);
    }

    public string ServiceTest()
    {
        return "graphAuth-> ServiceTest";
    }
}