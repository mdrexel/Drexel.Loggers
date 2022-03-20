using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a collection of suggestions associated with an event.
    /// </summary>
    public sealed class EventSuggestions : NonNullList<EventSuggestion, EventSuggestions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestions(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestions(IReadOnlyCollection<EventSuggestion> items)
            : base(items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestions(IEnumerable<EventSuggestion> items, int capacity = 0)
            : base(items, capacity)
        {
        }

        protected override EventSuggestions AsDerived => this;
    }
}
