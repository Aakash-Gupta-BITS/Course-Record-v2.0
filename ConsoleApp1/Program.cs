using System;
using System.IO;
using System.Threading;
using System.Text;
using System.Security.Cryptography;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;

namespace CourseRecordAuthenticator
{
    static class Program
    {
        static readonly string LocalStoragePath = Path.Combine(Environment.ExpandEnvironmentVariables("%AppData%"), @"Google.Apis.Auth");

        static void Main()
        {
            UserCredential credential;

            if (!IsAuthenticated())
            {
                Console.WriteLine("Login using BITS ID only. Other Logins will not be considered.");
                credential = Authenticate();
            }
            else
            {
                string[] scopes = new string[] { Oauth2Service.Scope.UserinfoEmail };
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
                {
                    ClientId = @"99450014535-bel11jg1ft872p8qvt84nk3qoij5bh5q.apps.googleusercontent.com",
                    ClientSecret = @"aTaj6A8kq38r5FkKogPAZb5q"
                }, scopes, "user", CancellationToken.None).Result;
                UpdateTokens(credential);
            }

            if (!File.Exists(Path.Combine(LocalStoragePath, "Hash.bin")))
                GenerateKeyToConsole();


        Menu:

            Console.Clear();
            Console.WriteLine("1. Generate Unregistration ID");
            Console.WriteLine("2. Remove Authentication");

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index < 3)
            {
                switch (index)
                {
                    case 1:
                        string hash1 = File.ReadAllText(Path.Combine(LocalStoragePath, "Hash.bin"));
                        Console.WriteLine("{0}\t\t:\t{1}", "Key", hash1);
                        Console.WriteLine("{0}\t:\t{1}", "Unregister Id", ComputeSha256Hash(hash1));
                        break;

                    case 2:
                        credential.RevokeTokenAsync(CancellationToken.None).Wait();
                        File.Delete(Path.Combine(LocalStoragePath, "Hash.bin"));
                        Console.Clear();
                        Main();
                        return;
                }

                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
            goto Menu;
        }

        static bool IsAuthenticated()
        {
            if (File.Exists(Path.Combine(LocalStoragePath, @"Google.Apis.Auth.OAuth2.Responses.TokenResponse-user")))
                return true;
            return false;
        }

        static UserCredential Authenticate()
        {
            Console.WriteLine("Requesting Authentication");
            string[] scopes = new string[] { Oauth2Service.Scope.UserinfoEmail };
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = @"99450014535-bel11jg1ft872p8qvt84nk3qoij5bh5q.apps.googleusercontent.com",
                ClientSecret = @"aTaj6A8kq38r5FkKogPAZb5q"
            }, scopes, "user", CancellationToken.None).Result;

            Oauth2Service oauth2Service = new Oauth2Service(
                new BaseClientService.Initializer { HttpClientInitializer = credential });
            Userinfoplus userinfo = oauth2Service.Userinfo.Get().ExecuteAsync().Result;

            if (!userinfo.Email.EndsWith(@"@pilani.bits-pilani.ac.in"))
            {
                credential.RevokeTokenAsync(CancellationToken.None).Wait();
                Console.WriteLine("\nInvalid BITS ID : {0}\n\n", userinfo.Email);
                return Authenticate();
            }

            return credential;
        }

        static void UpdateTokens(UserCredential credential)
        {
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
        }

        static void GenerateKeyToConsole()
        {
            Console.Write("Enter Id : ");
            string hash1 = Console.ReadLine();

            if (hash1.Length != 64)
            {
                GenerateKeyToConsole();
                return;
            }

            string hash2 = ComputeSha256Hash(hash1);

            File.WriteAllText(Path.Combine(LocalStoragePath, "Hash.bin"), hash2);

            Console.WriteLine("Paste the key to App : {0}\n", hash2);
            Console.WriteLine("Press any key to continue...");
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