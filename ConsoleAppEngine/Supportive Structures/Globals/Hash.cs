using System;
using System.Text;
using System.Security.Cryptography;

namespace ConsoleAppEngine.Globals
{
    public class Hash
    {
        private static readonly Hash _Instance = new Hash();
        public static Hash Instance => _Instance;

        string hash1 = "";

        public string DisplayHash => hash1;

        private Hash()
        {
            hash1 = ComputeSha256Hash(new Random().Next(0, int.MaxValue).ToString());
        }

        public void GenerateNewHash()
        {
            hash1 = ComputeSha256Hash(new Random().Next(0, int.MaxValue).ToString());
        }

        public bool VerifyHashFromConsole(string outerHash)
        {
            if (ComputeSha256Hash(hash1) == outerHash)
                return true;
            return false;
        }

        public static string ComputeSha256Hash(string rawData)
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
