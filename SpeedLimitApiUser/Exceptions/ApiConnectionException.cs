using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SpeedLimitApiUser.Exceptions
{
    internal class ApiConnectionException : Exception
    {
        public ApiConnectionException(string message) : base(message) {
        
        } 
    }
}
