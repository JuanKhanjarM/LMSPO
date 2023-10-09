using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSPO.UseCase.Exceptions.GroupEX
{
    public class InvalidGroupIdException : Exception
    {
        public InvalidGroupIdException()
        {
        }

        public InvalidGroupIdException(string message)
            : base(message)
        {
        }

        public InvalidGroupIdException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
