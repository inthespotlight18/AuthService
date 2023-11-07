using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util;
using System;

using System.Threading;

using AuthModelLib;


namespace GoogleAuth
{
    class googleAuth 
    {
        private static string clientId = "295625733783-5hb28gq3u12meu8lkf2tefv385b8pio9.apps.googleusercontent.com";
        private static string secret = "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";
        private static string[] scopes = { "https://www.googleapis.com/auth/gmail.readonly", "https://www.googleapis.com/auth/youtube" };
        
        public void Info()
        {

        }

       
    }
}


/*
 * 
namespace AuthModel.Interfaces
{
    public interface gAuth
    {
        string Login(string clientId, string Secret, string Scopes);


    }
}
*/