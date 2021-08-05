using System;

namespace AdminArea_IdentityBase.Models.Exceptions
{
    public class SendException : Exception
    {
        public SendException() : base($"Couldn't send the message")
        {
        }
    }
}