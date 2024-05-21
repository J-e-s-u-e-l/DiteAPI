using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiteAPI.infrastructure.Infrastructure.Utilities.DataExtension
{
    public static class DataExtensions
    {

        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data is not null && data.Any();
        }

    }
}
