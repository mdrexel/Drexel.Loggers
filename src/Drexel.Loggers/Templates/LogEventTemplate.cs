using System;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Templates
{
    /// <summary>
    /// A simple implementation of <see cref="ILogEventTemplate{T}"/> that handles checking for <see langword="null"/>
    /// values and replacing them with default values if applicable, and throwing an appropriate exception otherwise.
    /// </summary>
    /// <typeparam name="T">
    /// The type of event created by this template.
    /// </typeparam>
    public abstract class LogEventTemplate<T> : ILogEventTemplate<T>
        where T : ILogEvent
    {
        public LogEventTemplate(
            EventCode code,
            EventMessage message,
            EventReason? defaultReason = null,
            EventSuggestions? defaultSuggestions = null)
        {
            this.Code = code ?? throw new ArgumentNullException(nameof(code));
            this.Message = message ?? throw new ArgumentNullException(nameof(message));
            this.DefaultReason = defaultReason;
            this.DefaultSuggestions = defaultSuggestions;
        }

        public EventCode Code { get; }

        public EventMessage Message { get; }

        public EventSeverity Severity { get; }

        public virtual EventReason? DefaultReason { get; }

        public virtual EventSuggestions? DefaultSuggestions { get; }

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> using the supplied values.
        /// </summary>
        /// <returns>
        /// An instance of <typeparamref name="T"/> created using the supplied values.
        /// </returns>
        /// <inheritdoc/>
        public T Create(
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null)
        {
            return
                this.CreateInternal(
                    this.Code ?? throw new InvalidOperationException("Event template code illegally null."),
                    this.Message ?? throw new InvalidOperationException("Event template message illegally null."),
                    this.Severity,
                    reason ?? this.DefaultReason,
                    suggestions ?? this.DefaultSuggestions,
                    parameters,
                    innerEvents)
                ?? throw new InvalidOperationException("Event template illegally produced a null event.");
        }

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> using the supplied values.
        /// </summary>
        /// <returns>
        /// An instance of <typeparamref name="T"/> created using the supplied values.
        /// </returns>
        /// <inheritdoc/>
        public T Create<TException>(
            TException? exception,
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null)
            where TException : Exception
        {
            return
                this.CreateInternal<TException>(
                    exception,
                    this.Code ?? throw new InvalidOperationException("Event template code illegally null."),
                    this.Message ?? throw new InvalidOperationException("Event template message illegally null."),
                    this.Severity,
                    reason ?? this.DefaultReason,
                    suggestions ?? this.DefaultSuggestions,
                    parameters,
                    innerEvents)
                ?? throw new InvalidOperationException("Event template illegally produced a null event.");
        }

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> using the supplied values.
        /// </summary>
        /// <param name="code">
        /// The event code associated with the event. This value is guaranteed to be non-<see langword="null"/>.
        /// </param>
        /// <param name="message">
        /// The message associated with the event. This value is guaranteed to be non-<see langword="null"/>.
        /// </param>
        /// <param name="severity">
        /// The severity associated with the event.
        /// </param>
        /// <param name="reason">
        /// The reason associated with the event. May be <see langword="null"/> if no reason was supplied and the
        /// template does not have a default reason.
        /// </param>
        /// <param name="suggestions">
        /// The suggestions associated with the event. Guaranteed to be non-<see langword="null"/>.
        /// </param>
        /// <param name="parameters">
        /// The parameters associated with the event, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="innerEvents">
        /// The inner events associated with the event, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> created using the supplied values. Must be
        /// non-<see langword="null"/>.
        /// </returns>
        protected abstract T CreateInternal(
            EventCode code,
            EventMessage message,
            EventSeverity severity,
            EventReason? reason,
            EventSuggestions? suggestions,
            EventParameters? parameters,
            EventInnerEvents? innerEvents);

        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> using the supplied values.
        /// </summary>
        /// <param name="exception">
        /// The exception associated with the event, if one exists; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="code">
        /// The event code associated with the event. This value is guaranteed to be non-<see langword="null"/>.
        /// </param>
        /// <param name="message">
        /// The message associated with the event. This value is guaranteed to be non-<see langword="null"/>.
        /// </param>
        /// <param name="severity">
        /// The severity associated with the event.
        /// </param>
        /// <param name="reason">
        /// The reason associated with the event. May be <see langword="null"/> if no reason was supplied and the
        /// template does not have a default reason.
        /// </param>
        /// <param name="suggestions">
        /// The suggestions associated with the event, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="parameters">
        /// The parameters associated with the event, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <param name="innerEvents">
        /// The inner events associated with the event, if any exist; otherwise, <see langword="null"/>.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> created using the supplied values. Must be
        /// non-<see langword="null"/>.
        /// </returns>
        protected abstract T CreateInternal<TException>(
            TException? exception,
            EventCode code,
            EventMessage message,
            EventSeverity severity,
            EventReason? reason,
            EventSuggestions? suggestions,
            EventParameters? parameters,
            EventInnerEvents? innerEvents)
            where TException : Exception;
    }
    /// <summary>
    /// A simple implementation of <see cref="ILogEventTemplate{T}"/>.
    /// </summary>
    public sealed class LogEventTemplate : LogEventTemplate<ILogEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventTemplate"/> class.
        /// </summary>
        /// <param name="code">
        /// The code associated with the event template.
        /// </param>
        /// <param name="message">
        /// The message associated with the event template.
        /// </param>
        /// <param name="defaultReason">
        /// The default reason associated with the event template, or <see langword="null"/> if there is no default
        /// reason.
        /// </param>
        /// <param name="defaultSuggestions">
        /// The default suggestions associated with the event template, or <see langword="null"/> if there are no
        /// default suggestions.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="code"/> or <paramref name="message"/> is <see langword="null"/>.
        /// </exception>
        public LogEventTemplate(
            EventCode code,
            EventMessage message,
            EventReason? defaultReason = null,
            EventSuggestions? defaultSuggestions = null)
            : base(
                  code,
                  message,
                  defaultReason,
                  defaultSuggestions)
        {
        }

        protected override ILogEvent CreateInternal(
            EventCode code,
            EventMessage message,
            EventSeverity severity,
            EventReason? reason,
            EventSuggestions? suggestions,
            EventParameters? parameters,
            EventInnerEvents? innerEvents)
        {
            return new LogEvent(
                severity,
                new EventInfo(
                    code,
                    message,
                    reason,
                    suggestions,
                    parameters),
                null,
                innerEvents);
        }

        protected override ILogEvent CreateInternal<TException>(
            TException? exception,
            EventCode code,
            EventMessage message,
            EventSeverity severity,
            EventReason? reason,
            EventSuggestions? suggestions,
            EventParameters? parameters,
            EventInnerEvents? innerEvents)
            where TException : class
        {
            return new LogEvent(
                severity,
                new EventInfo(
                    code,
                    message,
                    reason,
                    suggestions,
                    parameters),
                exception is null ? null : EventExceptionInfo.Create(exception),
                innerEvents);
        }
    }
}
