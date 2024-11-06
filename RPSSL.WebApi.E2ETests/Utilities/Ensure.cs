using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSSL.WebApi.E2ETests.Utilities
{
    public static class Ensure
    {
        public static T NotNull<T>(T? value, string parameterName) where T : class
        {
            if (value == null) throw new ArgumentNullException(parameterName);
            return value;
        }
    }
}
