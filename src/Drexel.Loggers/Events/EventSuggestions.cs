using System;
using System.Collections;
using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a collection of suggestions associated with an event.
    /// </summary>
    public sealed class EventSuggestions : IReadOnlyList<EventSuggestion>
    {
        private List<EventSuggestion> suggestions;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        public EventSuggestions()
        {
            this.suggestions = new List<EventSuggestion>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSuggestions"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The initial capacity of the suggestions collection.
        /// </param>
        public EventSuggestions(int capacity)
        {
            this.suggestions = new List<EventSuggestion>(capacity);
        }

        public EventSuggestion this[int index] => this.suggestions[index];

        public int Count => this.suggestions.Count;

        /// <summary>
        /// Adds the specified suggestion to this collection.
        /// </summary>
        /// <param name="suggestion">
        /// The suggestion to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="suggestion"/> is <see langword="null"/>.
        /// </exception>
        public EventSuggestions Add(EventSuggestion suggestion)
        {
            this.suggestions.Add(suggestion ?? throw new ArgumentNullException(nameof(suggestion)));
            return this;
        }

        public IEnumerator<EventSuggestion> GetEnumerator() => this.suggestions.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
