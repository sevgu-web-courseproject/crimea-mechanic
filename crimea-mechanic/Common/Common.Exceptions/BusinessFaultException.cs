using System;

namespace Common.Exceptions
{
    public class BusinessFaultException : Exception
    {
        public BusinessFaultException(string message) : base(message) { }
    }
}
