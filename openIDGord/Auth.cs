using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Identity.Client;

//using Microsoft.Identity.Web;

using Azure.Identity;
using static System.Formats.Asn1.AsnWriter;
//using Tavis.UriTemplates;
//using Microsoft.Extensions.DependencyInjection;







//using Microsoft.Kiota.Abstractions.Authentication;
//using static System.Formats.Asn1.AsnWriter;

//https://stackoverflow.com/questions/75604903/delegateauthenticationprovider-not-found-after-updating-microsoft-graph
//https://github.com/microsoftgraph/msgraph-sdk-dotnet/blob/feature/5.0/docs/upgrade-to-v5.md#authentication



namespace openIDGord;

internal class Auth

{

    const string tenantId = "tugit.onmicrosoft.com";

    const string clientId = "d22e80d0-6e1d-4998-84a8-251485588156";

    const string secret = "V6M8Q~7L_9OVzvH~GjzMVb.BbR4FIh-3--Zaoc1A";



    static readonly string[] _scopes = new string[] { "https://graph.microsoft.com/.default" };





    public static async Task AuthTokenCSCAsync()
    {
        try
        {
            var options = new ClientSecretCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var csc = new ClientSecretCredential(tenantId, clientId, secret, options);
            Console.WriteLine("AuthToken : csc[{0}]", csc.GetToken);


            var gsc = new GraphServiceClient(csc, _scopes);
            var users = await gsc.Users.GetAsync();
            Console.WriteLine("AuthTokenCSCAsync : user.count[{0}]", users.Value.Count);
        } catch (Exception ex) 
        {
            Console.WriteLine("AuthTokenCSCAsync : [{0}]", ex.ToString());
        }
        
    }
}



