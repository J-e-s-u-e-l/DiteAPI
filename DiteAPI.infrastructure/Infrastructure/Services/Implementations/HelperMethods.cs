using DiteAPI.infrastructure.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Implementations
{
    public class HelperMethods : IHelperMethods
    {
        public string GenerateUniqueString()
        {
            string uniqueString = Guid.NewGuid().ToString("N");
            uniqueString = uniqueString.Substring(0, 8);
            uniqueString += RandomString(4);

            return uniqueString;
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
