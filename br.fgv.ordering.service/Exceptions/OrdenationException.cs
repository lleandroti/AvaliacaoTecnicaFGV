using System;

namespace br.fgv.ordering.service.Exceptions
{
    public class OrdenationException : Exception
    {
        public OrdenationException(string message) : base(message)
        {
            // nothing
        }
    }
}