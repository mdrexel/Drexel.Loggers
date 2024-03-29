﻿using System.Collections.Generic;
using System.Diagnostics;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a collection of suggestions associated with an event.
    /// </summary>
    [DebuggerDisplay("Count = {Count,nq}")]
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
        public EventSuggestions(params EventSuggestion[] itemParams)
            : base(itemParams)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestions(IReadOnlyCollection<EventSuggestion> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        /// <inheritdoc/>
        public EventSuggestions(IEnumerable<EventSuggestion> enumerable, int capacity = 0)
            : base(enumerable, capacity)
        {
        }

        /// <inheritdoc/>
        protected override EventSuggestions AsDerived => this;
    }
}
