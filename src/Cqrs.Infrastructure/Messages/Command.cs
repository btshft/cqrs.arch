using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cqrs.Infrastructure.Messages
{
    /// <summary>
    /// Базовый класс команды.
    /// </summary>
    public abstract class Command : Message, ICommand
    {
        private readonly Queue<Event> _outputEvents;

        /// <inheritdoc />
        [JsonIgnore]
        public Guid? WorkflowId { get; set; }

        /// <inheritdoc />
        [JsonIgnore]
        public bool HasOutputEvents => _outputEvents.Count > 0;
        
        protected Command()
        {
            _outputEvents = new Queue<Event>();
            WorkflowId = null;
        }

        protected Command(Guid workflowId)
        {
            WorkflowId = workflowId;
            _outputEvents = new Queue<Event>();
        }

        /// <inheritdoc />
        public void EnqueueOutputEvent(Event @event)
        {
            if (@event == null)
                throw new ArgumentException(nameof(@event));

            _outputEvents.Enqueue(@event);
        }
        
        /// <inheritdoc />
        public async Task ProcessOutputEventsAsync(Func<Event, CancellationToken, Task> eventHandler, CancellationToken cancellation)
        {
            var exceptions = new Queue<Exception>();
            while (_outputEvents.TryDequeue(out var @event))
            {
                try
                {
                    await eventHandler(@event, cancellation)
                        .ConfigureAwait(continueOnCapturedContext: false);
                }
                catch (Exception e)
                {
                    var wrapped = new Exception($"Исключение при обработке события {@event.GetType()}", e);
                    exceptions.Enqueue(wrapped);
                }
            }         
                            
            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);
        }
    }
}