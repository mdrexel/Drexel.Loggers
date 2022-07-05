using System.Collections.Generic;
using System.Diagnostics;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents an ordered collection of parameters associated with an event.
    /// </summary>
    [DebuggerDisplay("Count = {Count,nq}")]
    public sealed class EventParameters : NonNullList<IEventParameter, EventParameters>
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
        public EventParameters(params IEventParameter[] itemParams)
            : base(itemParams)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters(IReadOnlyCollection<IEventParameter> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventParameters(IEnumerable<IEventParameter> enumerable, int capacity = 0)
            : base(enumerable, capacity)
        {
        }

        /// <inheritdoc/>
        protected override EventParameters AsDerived => this;
    }
}
