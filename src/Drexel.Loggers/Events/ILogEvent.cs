using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents an event associated with an operation.
    /// </summary>
    public interface ILogEvent
    {
        /// <summary>
        /// Gets the severity of the event.
        /// </summary>
        EventSeverity Severity { get; }

        /// <summary>
        /// Gets information associated with the event.
        /// </summary>
        /// <value>
        /// An <see cref="EventInfo"/> instance. This value is guaranteed to be non-<see langword="null"/>.
        /// </value>
        EventInfo Info { get; }

        /// <summary>
        /// Gets the exception associated with the event, if one exists.
        /// </summary>
        /// <value>
        /// An <see cref="EventExceptionInfo"/> instance associated with the event, or <see langword="null"/> if the
        /// event is not associated with an exception.
        /// </value>
        EventExceptionInfo? Exception { get; }

        /// <summary>
        /// Gets an ordered collection of inner events, if any exist.
        /// </summary>
        /// <value>
        /// An ordered collection of inner events. This value is guaranteed to be non-<see langword="null"/>, and all
        /// contained values are guaranteed to be non-<see langword="null"/>.
        /// </value>
        IReadOnlyList<ILogEvent> InnerEvents { get; }
    }
}
