using System.Collections.Generic;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents the result of an operation.
    /// </summary>
    public interface IActionResult
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the operation was successful; otherwise, <see langword="false"/>.
        /// </value>
        bool Success { get; }

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
        IReadOnlyList<IResultEvent> AllEvents { get; }

        /// <summary>
        /// Gets all errors that occurred during the operation, in the order that they occurred.
        /// </summary>
        /// <value>
        /// All errors that occurred during the operation. This value is guaranteed to be non-<see langword="null"/>,
        /// and all contained values are guaranteed to be non-<see langword="null"/>.
        /// </value>
        /// <remarks>
        /// An error is an event that caused the operation to abort prematurely. When <see cref="Success"/> is
        /// <see langword="true"/>, this list is guaranteed to be empty.
        /// </remarks>
        IReadOnlyList<IResultEvent> Errors { get; }

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
        IReadOnlyList<IResultEvent> Informationals { get; }
    }

    /// <summary>
    /// Represents the result of an operation with strongly-typed events.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event returned by the operation.
    /// </typeparam>
    public interface ITryResult<out TEvent> : IActionResult
        where TEvent : ILogEvent
    {
        /// <inheritdoc cref="IActionResult.AllEvents"/>
        new IReadOnlyList<IResultEvent<TEvent>> AllEvents { get; }

        /// <inheritdoc cref="IActionResult.Errors"/>
        new IReadOnlyList<IResultEvent<TEvent>> Errors { get; }

        /// <inheritdoc cref="IActionResult.Informationals"/>
        new IReadOnlyList<IResultEvent<TEvent>> Informationals { get; }
    }
}
