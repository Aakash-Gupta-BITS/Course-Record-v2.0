using System;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;

namespace DriveQuickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] scopes = new string[] { Oauth2Service.Scope.UserinfoEmail };
            Console.WriteLine("Requesting Authentication");
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = @"99450014535-93f37a216juu2jtenue0ikintb74pal1.apps.googleusercontent.com",
                    ClientSecret = @"DG8dQkcGyb-71YJW-u_ABGRB"
                },
                scopes,
                "user",
                CancellationToken.None).Result;
            Console.WriteLine("Done");
            if (credential.Token.IsExpired(credential.Flow.Clock))
            {
                Console.WriteLine("Access Token is expired. Refreshing it.");
                if (credential.RefreshTokenAsync(CancellationToken.None).Result)
                    Console.WriteLine("Access Token is now refreshed.");
                else
                    Console.WriteLine("Access Token is expired but we couldn't refresh it.");
            }
            else
                Console.WriteLine("Access token is Ok. Continuing");

            Oauth2Service oauth2Service = new Oauth2Service(
                new BaseClientService.Initializer { HttpClientInitializer = credential });
            Userinfoplus userinfo = oauth2Service.Userinfo.Get().ExecuteAsync().Result;
            Console.WriteLine("Email : " + userinfo.Email);
            credential.RevokeTokenAsync(CancellationToken.None).Wait();
        }
    }
}