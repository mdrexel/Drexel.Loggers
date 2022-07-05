using System;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents an event template.
    /// </summary>
    public interface ILogEventTemplate
    {
        /// <summary>
        /// Gets the event code associated with this template.
        /// </summary>
        /// <value>
        /// The event code associated with this template. This value is guaranteed to be non-<see langword="null"/>.
        /// </value>
        EventCode Code { get; }

        /// <summary>
        /// Gets the severity associated with this template.
        /// </summary>
        /// <value>
        /// The severity associated with this template.
        /// </value>
        EventSeverity Severity { get; }

        /// <summary>
        /// Gets the message associated with this template.
        /// </summary>
        /// <value>
        /// The message associated with this template. This value is guaranteed to be non-<see langword="null"/>.
        /// </value>
        EventMessage Message { get; }

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
    }

    /// <summary>
    /// Represents an event template.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of data describing an occurrence of an event.
    /// </typeparam>
    /// <typeparam name="TEvent">
    /// The type of event produced by the template.
    /// </typeparam>
    public interface ILogEventTemplate<in TData, out TEvent>
        where TData : IEventData
        where TEvent : ILogEvent
    {
        /// <summary>
        /// Creates an instance of <typeparamref name="TEvent"/> using the supplied values.
        /// </summary>
        /// <param name="data">
        /// The data associated with the event occurrence.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEvent"/> created using the supplied values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        TEvent Create(TData data);

        /// <summary>
        /// Creates an instance of <typeparamref name="TEvent"/> using the supplied values.
        /// </summary>
        /// <typeparam name="TException">
        /// The type of exception associated with the event occurrence.
        /// </typeparam>
        /// <param name="data">
        /// The data associated with the event occurrence.
        /// </param>
        /// <param name="exception">
        /// The exception associated with the event, if one exists; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="TEvent"/> created using the supplied values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="data"/> is <see langword="null"/>.
        /// </exception>
        TEvent Create<TException>(TData data, TException? exception)
            where TException : Exception;
    }

    /// <summary>
    /// Represents an event template.
    /// </summary>
    /// <typeparam name="T">
    /// The type of event produced by the template.
    /// </typeparam>
    public interface ILogEventTemplate<out T> : ILogEventTemplate<IEventData, T>
        where T : ILogEvent
    {
    }
}
