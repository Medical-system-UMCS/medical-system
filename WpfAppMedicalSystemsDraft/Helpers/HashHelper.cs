using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMedicalSystemsDraft.Helpers
{
    internal static class HashHelper
    {
        private static SHA256 sHA256 = SHA256.Create();

        public static string GenerateHash(string input)
        {
            byte[] inputBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(inputBytes).Replace("-", string.Empty).ToLower();
        }
    }
}
