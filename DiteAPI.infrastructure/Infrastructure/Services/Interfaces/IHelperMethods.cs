using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Services.Interfaces
{
    public interface IHelperMethods
    {
        string GenerateVerificationToken(int tokenSize);
        abstract string Generate6CharString();
        string GenerateRandomNumber(int length);
        string ToAgoFormat(DateTimeOffset dateTimeOffset);
    }
}
