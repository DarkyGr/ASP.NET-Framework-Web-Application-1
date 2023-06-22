using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Security.Cryptography;
using System.Text;

namespace ASP.NET_Web_App_NetFramework_1.Services
{
    public static class UtilityService
    {
        // Password Encryption Method
        public static string ConvertSHA256(string text)
        {
            string hash = string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                // Get Hash of the text
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Convert the array byte to string
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }


        // Generate Token Method
        public static string GenerateToken()
        {
            // Get Unique Code
            string token = Guid.NewGuid().ToString("N");
            return token;
        }
    }
}