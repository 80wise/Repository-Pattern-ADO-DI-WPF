using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository.Exceptions
{
    public class EmptyMessageException : Exception
    {
        public EmptyMessageException(string message = "you can't sent an empty message"):
            base(message)
        {
            
        }
    }
}
