using System;
using System.Diagnostics;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a parameter associated with an event.
    /// </summary>
    public abstract class EventParameter : IEventParameter
    {
        private protected EventParameter(string name, Type expectedType)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.ExpectedType = expectedType ?? throw new ArgumentNullException(nameof(expectedType));
        }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public Type ExpectedType { get; }

        /// <inheritdoc/>
        public object? Value => this.GetValue();

        /// <summary>
        /// Returns an instance of <see cref="IEventParameter{T}"/> created using the supplied <paramref name="name"/>
        /// and <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of value associated with the parameter.
        /// </typeparam>
        /// <param name="name">
        /// The name of the parameter.
        /// </param>
        /// <param name="value">
        /// The value associated with the parameter.
        /// </param>
        /// <returns>
        /// An instance of <see cref="IEventParameter{T}"/> created using the supplied <paramref name="name"/> and
        /// <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="name"/> is <see langword="null"/>.
        /// </exception>
        public static IEventParameter<T> Create<T>(string name, T value) =>
            new EventParameter<T>(name, value);

        /// <summary>
        /// Returns the value contained by this instance.
        /// </summary>
        /// <returns>
        /// The value contained by this instance.
        /// </returns>
        protected abstract object? GetValue();
    }

    /// <summary>
    /// Represents a parameter associated with an event.
    /// </summary>
    [DebuggerDisplay("[{Name,nq}] {Value ?? \"null\"}")]
    internal sealed class EventParameter<T> : EventParameter, IEventParameter<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventParameter{T}"/> class.
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
        public EventParameter(string name, T value)
            : base(name, typeof(T))
        {
            this.Value = value;
        }

        /// <inheritdoc/>
        public new T Value { get; }

        object? IEventParameter.Value => this.Value;

        protected override object? GetValue() => this.Value;
    }
}
