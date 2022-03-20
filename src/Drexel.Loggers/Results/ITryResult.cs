using System.Collections.Generic;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    public interface ITryResult
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the operation was successful; otherwise, <see langword="false"/>.
        /// </value>
        bool IsSuccess { get; }

        /// <summary>
        /// Gets all events that occurred during the operation, in the order that they occurred.
        /// </summary>
        /// <value>
        /// All events that occurred during the operation. This value is guaranteed to be non-<see langword="null"/>,
        /// and all contained values are guaranteed to be non-<see langword="null"/>.
        /// </value>
        /// <remarks>
        /// This property contains all events included in <see cref="Errors"/> and <see cref="Informationals"/>.
        /// </remarks>
        IReadOnlyList<ILogEvent> AllEvents { get; }

        /// <summary>
        /// Gets all errors that occurred during the operation, in the order that they occurred.
        /// </summary>
        /// <value>
        /// All errors that occurred during the operation. This value is guaranteed to be non-<see langword="null"/>,
        /// and all contained values are guaranteed to be non-<see langword="null"/>.
        /// </value>
        /// <remarks>
        /// An error is an event that caused the operation to abort prematurely. When <see cref="IsSuccess"/> is
        /// <see langword="true"/>, this list is guaranteed to be empty.
        /// </remarks>
        IReadOnlyList<ILogEvent> Errors { get; }

        /// <summary>
        /// Gets all informational events that occurred during the operation, in the order that they occurred.
        /// </summary>
        /// <value>
        /// All informational events that occurred during the operation. This value is guaranteed to be
        /// non-<see langword="null"/>, and all contained values are guaranteed to be non-<see langword="null"/>.
        /// </value>
        /// <remarks>
        /// An informational event is a non-error event that occurred during the operation.
        /// </remarks>
        IReadOnlyList<ILogEvent> Informationals { get; }
    }

    /// <summary>
    /// Represents the result of an operation that returns a value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of returned value.
    /// </typeparam>
    public interface ITryResult<out T> : ITryResult
    {
        T Value { get; }

        // TODO: Prevent caller from accessing `Value` without the result being successful/populated?
        bool TryGetValue(out T value);
    }
}
