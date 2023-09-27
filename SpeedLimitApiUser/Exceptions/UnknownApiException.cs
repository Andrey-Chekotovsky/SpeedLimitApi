using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedLimitApiUser.Exceptions
{
    internal class UnknownApiException : Exception
    {
        public UnknownApiException(string message) : base(message) { }
    }
}
