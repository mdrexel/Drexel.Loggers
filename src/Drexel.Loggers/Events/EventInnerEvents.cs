using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a collection of inner events associated with an event.
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
        public EventInnerEvents(IReadOnlyCollection<ILogEvent> items)
            : base(items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventInnerEvents"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventInnerEvents(IEnumerable<ILogEvent> items, int capacity = 0)
            : base(items, capacity)
        {
        }

        protected override EventInnerEvents AsDerived => this;
    }
}
