using System;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents a parameter associated with an event.
    /// </summary>
    public interface IEventParameter
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the expected type of the parameter value.
        /// </summary>
        /// <remarks>
        /// When <see cref="Value"/> is <see langword="null"/>, this can be used to discover the expected system type.
        /// The expected type should always match the actual type of <see cref="Value"/>, but if a mismatch occurs, the
        /// actual type should be used to ensure the parameter value is handled safely.
        /// </remarks>
        Type ExpectedType { get; }

        /// <summary>
        /// Gets the value associated with the parameter.
        /// </summary>
        object? Value { get; }
    }

    /// <summary>
    /// Represents a strongly-typed parameter associated with an event.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value associated with the parameter.
    /// </typeparam>
    public interface IEventParameter<out T> : IEventParameter
    {
        /// <summary>
        /// Gets the value associated with the parameter.
        /// </summary>
        new T Value { get; }
    }
}
