using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Extension
    {
        public static IEnumerable<T> CheckNull<T>(this IEnumerable<T> list)
        {
            return list == null ? new List<T>(0) : list;
        }
    }
}
