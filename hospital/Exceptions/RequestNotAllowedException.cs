using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Exceptions
{
    public class RequestNotAllowedException : Exception
    {
        public RequestNotAllowedException(string message) : base(message)
        {

        }
    }
}
