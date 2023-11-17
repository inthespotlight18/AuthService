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
        
        public async Task<string> GetProfile()
        {
            try
            {
                await AuthLogin();
                Console.ReadLine();

                //==============================================================
                Console.WriteLine("googleAuth->GetProfile started");

                if (_credentials.Token.IsExpired(SystemClock.Default))
                    _credentials.RefreshTokenAsync(CancellationToken.None).Wait();
                
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credentials
                });

                var profile = service.Users.GetProfile("tugit@fity.ca").Execute();
                Console.WriteLine(profile.MessagesTotal);

                string funcName = "Result of google->GetProfile : ";
                string st = (profile != null) ? funcName + "OK" : "FAIL|error";

                Console.WriteLine(funcName + st);

                Console.ReadLine();

                return st;
            }
            catch (Exception ex)
            {
                string resultMessage = "googleAuth->GetProfile(): [" + ex.Message + "]";
                Console.WriteLine(resultMessage);
                return resultMessage;
            }
        }

        public async Task<string> AuthLogin()
        {
            Console.WriteLine("googleAuth->AuthLogin STARTED");
            try
            {
               var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
               new ClientSecrets
               {
                   ClientId = clientId,
                   ClientSecret = secret,
               },
               scopes, "testClientGS", CancellationToken.None).Result;
                //Console.WriteLine("googleAuth->AuthLoginTest : c[{0}]", credentials.Token.IssuedUtc.ToString());

                string resultMessage = "googleAuth->AuthLoginTest : OK";
                Console.WriteLine(resultMessage);

                _credentials = credentials;
                return resultMessage;
            } 
            catch (Exception ex) 
            {
                string resultMessage = "googleAuth->AuthLogin(): [" + ex.Message + "]";
                Console.WriteLine(resultMessage);
                return resultMessage;
            }
            
        }
        public string ServiceTest()
        {
            return "googleAuth-> ServiceTest";
        }


        public async Task<string> SendSMS(string receiverNumber, string message)
        {
            return "This functionality is not implemented in GoogleAuth class";

        }

        public async Task<string> SendEmail(string adresantEmail, string subject, string body)
        {
            return "This functionality is not implemented in GoogleAuth class";

        }
    }
}

