using System;
using Newtonsoft.Json;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс сообщения.
    /// </summary>
    public abstract class Message : IMessage
    {
        /// <inheritdoc />
        [JsonIgnore]
        public Guid Id { get; }
        
        /// <inheritdoc />
        public DateTime Timestamp { get; }

        protected Message()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var separator = $";{Environment.NewLine}";
            return $"Type: {GetType()}{separator} Id: {Id}{separator} Timestamp: {Timestamp:u}";
        }
    }
}