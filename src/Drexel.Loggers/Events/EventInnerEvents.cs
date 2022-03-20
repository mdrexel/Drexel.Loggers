using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents an ordered collection of inner events associated with an event.
    /// </summary>
    public sealed class EventInnerEvents : NonNullList<ILogEvent, EventInnerEvents>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventInnerEvents"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventInnerEvents()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventInnerEvents"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventInnerEvents(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventInnerEvents"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventInnerEvents(params ILogEvent[] itemParams)
            : base(itemParams)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventInnerEvents"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventInnerEvents(IReadOnlyCollection<ILogEvent> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventInnerEvents"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventInnerEvents(IEnumerable<ILogEvent> enumerable, int capacity = 0)
            : base(enumerable, capacity)
        {
        }

        protected override EventInnerEvents AsDerived => this;
    }
}
