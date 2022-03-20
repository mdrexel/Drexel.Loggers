using System;
using System.Collections;
using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a collection of parameters associated with an event.
    /// </summary>
    public sealed class EventParameters : IReadOnlyList<EventParameter>
    {
        private List<EventParameter> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        public EventParameters()
        {
            this.parameters = new List<EventParameter>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameters"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The initial capacity of the parameters collection.
        /// </param>
        public EventParameters(int capacity)
        {
            this.parameters = new List<EventParameter>(capacity);
        }

        public EventParameter this[int index] => this.parameters[index];

        public int Count => this.parameters.Count;

        /// <summary>
        /// Adds the specified parameter to this collection.
        /// </summary>
        /// <param name="parameter">
        /// The parameter to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="parameter"/> is <see langword="null"/>.
        /// </exception>
        public EventParameters Add(EventParameter parameter)
        {
            this.parameters.Add(parameter ?? throw new ArgumentNullException(nameof(parameter)));
            return this;
        }

        public IEnumerator<EventParameter> GetEnumerator() => this.parameters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
