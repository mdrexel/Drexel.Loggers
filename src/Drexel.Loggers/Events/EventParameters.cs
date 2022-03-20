using System.Collections.Generic;
using System.Diagnostics;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents an ordered collection of parameters associated with an event.
    /// </summary>
    [DebuggerDisplay("Count = {Count,nq}")]
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
        public EventParameters(params EventParameter[] itemParams)
            : base(itemParams)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters(IReadOnlyCollection<EventParameter> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters(IEnumerable<EventParameter> enumerable, int capacity = 0)
            : base(enumerable, capacity)
        {
        }

        protected override EventParameters AsDerived => this;
    }
}
