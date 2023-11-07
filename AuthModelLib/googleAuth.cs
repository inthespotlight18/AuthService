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
        /*  GS
         *  
         */ private static string clientId = "532374526064-uorsuiai1d4nvtsr8fr01h5keubolqjs.apps.googleusercontent.com";
            private static string secret = "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";
         /*/
        /*  DZ
         */

        /*
        private static string clientId = "295625733783-5hb28gq3u12meu8lkf2tefv385b8pio9.apps.googleusercontent.com";
             private static string secret = "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";
        // */


        //private static string clientId = "1016139361797-pt8scol5cahrsh81a7q594c24bt8unia.apps.googleusercontent.com";
        //private static string secret = "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";
        private static string[] scopes = { "https://www.googleapis.com/auth/gmail.readonly", "https://www.googleapis.com/auth/youtube" };
    

        public string AuthLogin()
        {

            var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
               new ClientSecrets
               {
                   ClientId = clientId,
                   ClientSecret = secret,
               },
               scopes, "user", CancellationToken.None).Result;

            if (credentials.Token.IsExpired(SystemClock.Default))
                credentials.RefreshTokenAsync(CancellationToken.None).Wait();

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });

            var profile = service.Users.GetProfile("tugit@fity.ca").Execute();
            Console.WriteLine(profile.MessagesTotal);

            Console.ReadLine();

            string funcName = "google->AuthLogin : ";

            return (profile != null) ? funcName + "OK" : "FAIL|error";
        }


        public static async Task<string> AuthLoginTest()
        {
            try
            {
                var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                   new ClientSecrets
                   {
                       ClientId = clientId,
                       ClientSecret = secret,
                   },
                   scopes, "user", CancellationToken.None).Result;
                Console.WriteLine("AuthLoginTest : c[{0}]", credentials.Token.IssuedUtc.ToString());
                Console.ReadLine();

                if (credentials.Token.IsExpired(SystemClock.Default))
                    credentials.RefreshTokenAsync(CancellationToken.None).Wait();

                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credentials
                });

                var profile = service.Users.GetProfile("tugit@fity.ca").Execute();
                Console.WriteLine(profile.MessagesTotal);

                Console.ReadLine();

                string funcName = "TESTING google->AuthLogin : ";

                string st = (profile != null) ? funcName + "OK" : "FAIL|error";

                Console.WriteLine(funcName + st);
                //Console.WriteLine("OK");
                return "OK";
            }
            catch (Exception ex)
            {
                //Console.WriteLine(("FAIL|{0}]", ex.Message);
                return string.Format("FAIL|{0}]", ex.Message);
            }
        }


        public string ServiceTest()
        {
            return "googleAuth-> ServiceTest";
        }
    }
}


/*
 * static void Main(string[] args)
        {
            var googleAuth = new googleAuth();

            var credentials = AuthCheck(clientId, secret, scopes);

            string res = googleAuth.Login(clientId, secret, scopes);

            Console.WriteLine(res);
           
            if (credentials.Token.IsExpired(SystemClock.Default))
                credentials.RefreshTokenAsync(CancellationToken.None).Wait();

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });

            var profile = service.Users.GetProfile("dzlenko0922@gmail.com").Execute();
            Console.WriteLine(profile.MessagesTotal);

            Console.ReadLine();
        }
        private static UserCredential AuthCheck(string clientId, string Secret, string[] Scopes)
        {
            var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
               new ClientSecrets
               {
                   ClientId = clientId,
                   ClientSecret = secret,
               },
               scopes, "user", CancellationToken.None).Result;

            return credentials;
        }

        public string Login(string ClientId, string Secret, string[] Scopes)
        {
            var credentials = AuthCheck(ClientId, Secret, Scopes);

            return (credentials != null) ? "ok" : "login failed";
           
        }
*/