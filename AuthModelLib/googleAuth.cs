using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util;
using System;

using System.Threading;

namespace AuthModelLib 
{
    public class googleAuth : iGAuth
    {     
        const string clientId = "532374526064-70e174f85sid96ttojf20k06fjf3kp0s.apps.googleusercontent.com";
        const string secret = "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";
        private  UserCredential _credentials { get; set; }

        private static string[] scopes = { "https://www.googleapis.com/auth/gmail.readonly", "https://www.googleapis.com/auth/youtube" };
        
        public async Task<string> AuthLogin()
        {
            try
            {
                await GetProfile();
                Console.ReadLine();

                //==============================================================
                Console.WriteLine("googleAuth->AuthLogin started");

                if (_credentials.Token.IsExpired(SystemClock.Default))
                    _credentials.RefreshTokenAsync(CancellationToken.None).Wait();
                
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credentials
                });

                var profile = service.Users.GetProfile("tugit@fity.ca").Execute();
                Console.WriteLine(profile.MessagesTotal);

                Console.ReadLine();

                string funcName = "Result of google->AuthLogin : ";

                string st = (profile != null) ? funcName + "SUCCESS" : "FAIL|error";

                Console.WriteLine(funcName + st);

                return "OK";
            }
            catch (Exception ex)
            {
                //Console.WriteLine(("FAIL|{0}]", ex.Message);
                return string.Format("FAIL|{0}]", ex.Message);
            }
        }

        public async Task GetProfile()
        {
            Console.WriteLine("googleAuth->GetProfile STARTED");
            try
            {
                var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
               new ClientSecrets
               {
                   ClientId = clientId,
                   ClientSecret = secret,
               },
               scopes, "testClientGS", CancellationToken.None).Result;

                Console.WriteLine("CHECKPOINT");
                Console.WriteLine("AuthLoginTest : c[{0}]", credentials.Token.IssuedUtc.ToString());

                _credentials = credentials;
                Console.WriteLine("googleAuth->GetProfile FINISHED");
            } 
            catch (Exception ex) 
            {
                Console.WriteLine(string.Format("FAIL|{0}]", ex.Message));
            }
            
        }
        public string ServiceTest()
        {
            return "googleAuth-> ServiceTest";
        }
    }
}

