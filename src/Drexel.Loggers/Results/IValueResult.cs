using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents the result of an operation that returns a value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value returned by the operation.
    /// </typeparam>
    /// 
    public interface IValueResult<out T> : ITryResult
    {
        /// <summary>
        /// Gets a value indicating whether this result contains a value.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if this result contains a value; otherwise, <see langword="false"/>.
        /// </value>
        bool HasValue { get; }

        /// <summary>
        /// Gets the value returned by the operation, if this result contains a value. Otherwise, undefined.
        /// </summary>
        T Value { get; }
    }

    /// <summary>
    /// Represents the result of an operation that returns a value with strongly-typed events.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event returned by the operation.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of value returned by the operation.
    /// </typeparam>
    public interface IValueResult<out TEvent, out TValue> : IValueResult<TValue>, ITryResult<TEvent>
        where TEvent : ILogEvent
    {
    }
}
