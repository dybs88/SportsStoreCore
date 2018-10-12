using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Exceptions
{
    public class ParameterNotExistException : Exception
    {
        public ParameterNotExistException(string message)
            :base(message)
        { }
    }
}
