using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class HelperMethods : IHelperMethods
    {
        private readonly ILogger<HelperMethods> _logger;

        public HelperMethods(ILogger<HelperMethods> logger)
        {
            _logger = logger;
        }

        public string GenerateVerificationToken(int tokenSize)
        {
            _logger.LogInformation($"HELPER_METHODS Generate_Verification_Token => Process started");

            byte[] takenByte = new byte[tokenSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(takenByte);
            }

            _logger.LogInformation($"HELPER_METHODS Generate_Verification_Token => Process completed");

            // Convert token bytes to base64 string (URL-safe)
            return Convert.ToBase64String(takenByte).Trim('=');
        }
        public string GenerateUniqueString()
        {
            _logger.LogInformation($"HELPER_METHODS Generate_Unique_String => Process started");
            string uniqueString = Guid.NewGuid().ToString("N");
            uniqueString = uniqueString.Substring(0, 8);
            uniqueString += RandomString(4);

            _logger.LogInformation($"HELPER_METHODS Generate_Unique_String => Process completed");
            return uniqueString;
        }

        public string GenerateRandomNumber(int length)
        {
            _logger.LogInformation($"HELPER_METHODS Generate_Random_Number => Process started");
            var output = new StringBuilder();
            for( int i = 0; i < length; i++)
            {
                output.Append(new Random().Next(10));
            }

            _logger.LogInformation($"HELPER_METHODS Generate_Random_Number => Process completed");
            return output.ToString();
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
