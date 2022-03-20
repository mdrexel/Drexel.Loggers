using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a collection of parameters associated with an event.
    /// </summary>
    public sealed class EventParameters : NonNullList<EventParameter, EventParameters>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters(IReadOnlyCollection<EventParameter> items)
            : base(items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters(IEnumerable<EventParameter> items, int capacity = 0)
            : base(items, capacity)
        {
        }

        protected override EventParameters AsDerived => this;
    }
}
