using System;
using System.Threading;
using System.Text;
using System.Security.Cryptography;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;

namespace DriveQuickstart
{
    class Program
    {
        static void Main(params string[] args)
        {
            Console.WriteLine("Login using BITS ID only. Other Logins will not be considered.");

            string[] scopes = new string[] { Oauth2Service.Scope.UserinfoEmail };
            Console.WriteLine("Requesting Authentication");
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = @"99450014535-bel11jg1ft872p8qvt84nk3qoij5bh5q.apps.googleusercontent.com",
                    ClientSecret = @"aTaj6A8kq38r5FkKogPAZb5q"
                },
                scopes,
                "user",
                CancellationToken.None).Result;

            Oauth2Service oauth2Service = new Oauth2Service(
                new BaseClientService.Initializer { HttpClientInitializer = credential });
            Userinfoplus userinfo = oauth2Service.Userinfo.Get().ExecuteAsync().Result;
            Console.WriteLine("Email : " + userinfo.Email);
            credential.RevokeTokenAsync(CancellationToken.None).Wait();
            if (!userinfo.Email.EndsWith(@"@pilani.bits-pilani.ac.in"))
            {
                Console.WriteLine("Invalid BITS ID\n\n");
                Main();
                return;
            }

            Console.WriteLine("Enter Id : ");
            string hash1 = Console.ReadLine();
            Console.WriteLine("Paste the key to App : {0}\n", ComputeSha256Hash(hash1));
            Console.ReadKey();

        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}