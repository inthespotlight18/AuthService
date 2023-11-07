
using Microsoft.Graph;
using Azure.Identity;

//https://stackoverflow.com/questions/75604903/delegateauthenticationprovider-not-found-after-updating-microsoft-graph
//https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/feature/5.0/docs/upgrade-to-v5.md#authentication
namespace AuthModelLib;
public class Auth 
{


const string tenantId = "tugit.onmicrosoft.com";
    const string clientId = "d22e80d0-6e1d-4998-84a8-251485588156";
    const string secret = "V6M8Q~7L_9OVzvH~GjzMVb.BbR4FIh-3--Zaoc1A";
    static readonly string[] _scopes = new string[]
    {
        "https://graph.microsoft.com/.default"
    };
    public static async Task AuthTokenCSCAsync()
    {
        var options = new ClientSecretCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
        };
        var csc = new ClientSecretCredential(tenantId, clientId, secret, options);
        Console.WriteLine("AuthToken : csc[{0}]", csc.GetToken);
        var v = csc.GetToken;
        string s = v.ToString();
        Console.WriteLine("EEEEEEEE  " + s);


        var gsc = new GraphServiceClient(csc, _scopes);
        var users = await gsc.Users.GetAsync();
        Console.WriteLine("AuthTokenCSCAsync : user.count[{0}]", users.Value.Count);

        Console.ReadLine();
    }

    public string AuthLogin()
    {
        throw new NotImplementedException();
    }

    public string ServiceTest()
    {
        throw new NotImplementedException();
    }

   /*
    public static async Task<string> AuthTokenAquireAsync()
    {
        Console.WriteLine("AuthToken : ");
        IConfidentialClientApplication app;
        app = ConfidentialClientApplicationBuilder.Create(clientId).WithClientSecret(secret).WithTenantId(tenantId)
            //.WithAuthority(new Uri(config.Authority))
            .Build();
        //var authProvider = new ClientCredentialProvider(app, _scopes);
        var authResult = await app.AcquireTokenForClient(_scopes).ExecuteAsync();
        var token = authResult.AccessToken;
        GraphServiceClient gsc = new GraphServiceClient(authResult, 
       
  




            //AuthenticationResult result = null;
            //try
            //{
            //    result = await app.AcquireTokenForClient(_scopes)
            //        .ExecuteAsync();
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine("Token acquired");
            //    Console.ResetColor();
            //}
            //catch (MsalServiceException ex) when (ex.Message.Contains("AADSTS70011"))
            //{
            //    // Invalid scope. The scope has to be of the form "https://resourceurl/.default"
            //    // Mitigation: change the scope to be as expected
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Scope provided is not supported");
            //    Console.ResetColor();
            //}
            //var httpClient = new HttpClient();
            //var apiCaller = new ProtectedApiCallHelper(httpClient);
            //await apiCaller.CallWebApiAndProcessResultASync($"{config.ApiUrl}v1.0/users",
            //    result.AccessToken, Display);
            return token.ToString();
    }
    
    */




    //public class TokenProvider : IAccessTokenProvider
    //{
    //    public Task<string> GetAuthorizationTokenAsync(Uri uri, Dictionary<string, object> additionalAuthenticationContext = default,
    //        CancellationToken cancellationToken = default)
    //    {
    //        var token = "token";
    //        // get the token and return it in your own way
    //        AuthenticationResult result = await app.AcquireTokenForClient(scopes)
    //                        .ExecuteAsync();
    //        return Task.FromResult(token);
    //    }
    //    public AllowedHostsValidator AllowedHostsValidator { get; }
    //}
    //    public static async IConfidentialClientApplication AuthLogin()
    //    {
    //        Console.WriteLine("AuthLogin : ");
    //        IConfidentialClientApplication app;
    //        app = ConfidentialClientApplicationBuilder.Create(clientId)
    //                                                  .WithClientSecret(secret)
    //                                                  .WithTenantId(tenantId)
    //                                                  //.WithAuthority(new Uri(config.Authority))
    //                                                  .Build();
    //        //Console.WriteLine("AuthLogin : app[{0}]", app.Authority.ToString());
    //        app.AddInMemoryTokenCache();
    //        //var auth = await app.AcquireTokenForClient(_scopes).ExecuteAsync();
    //        //var authResult = await app.AcquireTokenSilent(_scopes).ExecuteAsync();
    //        //var authenticationProvider = new BaseBearerTokenAuthenticationProvider(new TokenProvider());
    //        //var graphServiceClient = new GraphServiceClient(authenticationProvider);
    //        //Console.WriteLine("Authogin.AquireTokenForClient : {0}", auth.AuthenticationResultMetadata.TokenSource);
    //        return app;
    //    }
}