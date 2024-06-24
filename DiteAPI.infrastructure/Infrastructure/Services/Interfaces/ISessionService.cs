using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.Infrastructure.Infrastructure.Services.Interfaces
{
    public interface ISessionService
    {
        public void SetStringInSession(string key, string value);
        public string GetStringFromSession(string value);
    }
}
