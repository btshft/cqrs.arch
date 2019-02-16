using System;
using Cqrs.Infrastructure.Messages;

namespace Cqrs.Infrastructure.Exceptions
{
    /// <summary>
    /// Улучшенное исключение.
    /// </summary>
    [Serializable]
    public class EnrichedException : Exception
    {
        public EnrichedException(IMessage message, Exception inner) 
            : base(message.ToString(), inner) 
        { }
    }
}