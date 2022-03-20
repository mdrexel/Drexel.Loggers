using System;
using System.Collections.Generic;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents information associated with an event.
    /// </summary>
    public sealed class EventInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventInfo"/> class.
        /// </summary>
        /// <param name="code">
        /// The code associated with the event.
        /// </param>
        /// <param name="message">
        /// The message associated with the event.
        /// </param>
        /// <param name="reason">
        /// The reason associated with the event, or <see langword="null"/> if no reason should be provided.
        /// </param>
        /// <param name="suggestions">
        /// The suggestions associated with the event, or <see langword="null"/> if no suggestions should be provided.
        /// </param>
        /// <param name="parameters">
        /// The parameters associated with the event, or <see langword="null"/> if no parameters should be provided.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="code"/> or <paramref name="message"/> is <see langword="null"/>.
        /// </exception>
        public EventInfo(
            EventCode code,
            EventMessage message,
            EventReason? reason,
            EventSuggestions? suggestions,
            EventParameters? parameters)
        {
            this.Code = code ?? throw new ArgumentNullException(nameof(code));
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.Reason = reason;
            this.Suggestions = suggestions ?? (IReadOnlyList<EventSuggestion>)Array.Empty<EventSuggestion>();
            this.Parameters = parameters ?? (IReadOnlyList<EventParameter>)Array.Empty<EventParameter>();
        }

        /// <summary>
        /// Gets the event code associated with the event.
        /// </summary>
        /// <value>
        /// The event code associated with the event. This value is guaranteed to be non-<see langword="null"/>.
        /// </value>
        public EventCode Code { get; }

        /// <summary>
        /// Gets a message describing the event.
        /// </summary>
        /// <remarks>
        /// An event message gives a high-level description of the event. This value is guaranteed to be
        /// non-<see langword="null"/>.
        /// </remarks>
        public EventMessage Message { get; }

        /// <summary>
        /// Gets a reason describing the event.
        /// </summary>
        /// <value>
        /// An instance of <see cref="EventReason"/> if one is associated with this event; otherwise,
        /// <see langword="null"/>.
        /// </value>
        /// <remarks>
        /// An event reason describes the underlying reason for the event. This differs from the <see cref="Message"/>,
        /// which may not be sufficient for a user to understand why the event was returned.
        /// </remarks>
        public EventReason? Reason { get; }

        /// <summary>
        /// Gets any suggestions associated with the event.
        /// </summary>
        /// <value>
        /// An ordered collection of suggestions. This value is guaranteed to be non-<see langword="null"/>, and all
        /// contained values are guaranteed to be non-<see langword="null"/>.
        /// </value>
        /// <remarks>
        /// Suggestions can be used to help a user understand why the event occurred, and how to prevent reoccurances
        /// (if applicable).
        /// </remarks>
        public IReadOnlyList<EventSuggestion> Suggestions { get; }

        /// <summary>
        /// Gets any parameters associated with the event.
        /// </summary>
        /// <value>
        /// An ordered collection of parameters. This value is guaranteed to be non-<see langword="null"/>, and all
        /// contained values are guaranteed to be non-<see langword="null"/>.
        /// </value>
        /// <remarks>
        /// Parameters describe inputs to the operation that raised the event, or internal state of the system.
        /// </remarks>
        public IReadOnlyList<EventParameter> Parameters { get; }
    }
}
