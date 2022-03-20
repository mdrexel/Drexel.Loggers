using System;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a parameter associated with an event.
    /// </summary>
    public sealed class EventParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameter"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter.
        /// </param>
        /// <param name="value">
        /// The value of the parameter.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="name"/> is <see langword="null"/>.
        /// </exception>
        public EventParameter(string name, object? value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the value of the parameter.
        /// </summary>
        public object? Value { get; }
    }
}
