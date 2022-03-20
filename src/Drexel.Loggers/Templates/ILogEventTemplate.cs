using System;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Templates
{
    /// <summary>
    /// Represents an event template.
    /// </summary>
    /// <typeparam name="T">
    /// The type of event produced by the template.
    /// </typeparam>
    public interface ILogEventTemplate<out T>
        where T : ILogEvent
    {
        /// <summary>
        /// Gets the event code associated with this template.
        /// </summary>
        /// <value>
        /// The event code associated with this template. This value is guaranteed to be non-<see langword="null"/>.
        /// </value>
        EventCode Code { get; }

        /// <summary>
        /// Gets the message associated with this template.
        /// </summary>
        /// <value>
        /// The message associated with this template. This value is guaranteed to be non-<see langword="null"/>.
        /// </value>
        EventMessage Message { get; }

        /// <summary>
        /// Gets the severity associated with this template.
        /// </summary>
        /// <value>
        /// The severity associated with this template.
        /// </value>
        EventSeverity Severity { get; }

        /// <summary>
        /// Gets the default reason associated with this template.
        /// </summary>
        /// <value>
        /// If this template has a default reason, an <see cref="EventReason"/> instance; otherwise,
        /// <see langword="null"/>.
        /// </value>
        EventReason? DefaultReason { get; }

        /// <summary>
        /// Gets the default suggestions associated with this template.
        /// </summary>
        /// <value>
        /// If this template has default suggestions, an <see cref="EventSuggestions"/> instance; otherwise,
        /// <see langword="null"/>.
        /// </value>
        EventSuggestions? DefaultSuggestions { get; }

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> using the supplied values.
        /// </summary>
        /// <param name="reason">
        /// The reason associated with the event, if one exists; otherwise, <see langword="null"/> if the default
        /// reason should be used.
        /// </param>
        /// <param name="suggestions">
        /// The suggestions associated with the event, if any exist; otherwise, <see langword="null"/> if the default
        /// suggestions should be used.
        /// </param>
        /// <param name="parameters">
        /// The parameters associated with the event, if any exist; otherwise <see langword="null"/>.
        /// </param>
        /// <param name="innerEvents">
        /// The inner events associated with the event, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> created using the supplied values.
        /// </returns>
        T Create(
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null);

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> using the supplied values.
        /// </summary>
        /// <param name="exception">
        /// The exception associated with the event, if one exists; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="reason">
        /// The reason associated with the event, if one exists; otherwise, <see langword="null"/> if the default
        /// reason should be used.
        /// </param>
        /// <param name="suggestions">
        /// The suggestions associated with the event, if any exist; otherwise, <see langword="null"/> if the default
        /// suggestions should be used.
        /// </param>
        /// <param name="parameters">
        /// The parameters associated with the event, if any exist; otherwise <see langword="null"/>.
        /// </param>
        /// <param name="innerEvents">
        /// The inner events associated with the event, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> created using the supplied values.
        /// </returns>
        T Create<TException>(
            TException? exception,
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null)
            where TException : Exception;
    }
}
