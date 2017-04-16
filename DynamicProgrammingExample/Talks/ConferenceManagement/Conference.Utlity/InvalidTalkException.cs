using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Utility
{
    public class InvalidTalkException : Exception
    {
        public InvalidTalkException(string message) : base(message)
        {
        }
    }   
}
