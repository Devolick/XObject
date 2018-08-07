using System;
using System.Collections.Generic;
using System.Text;

namespace XObjectSerializer
{
    public class XObjectException: Exception
    {

        private XObjectException() { }
        internal XObjectException(string message)
            : base(message) { }
        internal XObjectException(string message, Exception innetException)
            : base(message, innetException) { }

    }
}
