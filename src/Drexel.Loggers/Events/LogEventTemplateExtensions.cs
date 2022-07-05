using System;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Extension methods for log event templates.
    /// </summary>
    public static class LogEventTemplateExtensions
    {
        /// <summary>
        /// Creates an instance of <typeparamref name="TEvent"/> using the supplied values.
        /// </summary>
        /// <typeparam name="TEvent">
        /// The type of event produced by the template.
        /// </typeparam>
        /// <param name="template">
        /// The template to use when creating the event.
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
        /// An instance of <typeparamref name="TEvent"/> created using the supplied values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="template"/> is <see langword="null"/>.
        /// </exception>
        public static TEvent Create<TEvent>(
            this ILogEventTemplate<TEvent> template,
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null)
            where TEvent : ILogEvent
        {
            if (template is null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            return template.Create(
                new EventData(
                    reason,
                    suggestions,
                    parameters,
                    innerEvents));
        }

        /// <summary>
        /// Creates an instance of <typeparamref name="TEvent"/> using the supplied values.
        /// </summary>
        /// <typeparam name="TEvent">
        /// The type of event produced by the template.
        /// </typeparam>
        /// <typeparam name="TException">
        /// The type of exception associated with the event occurrence.
        /// </typeparam>
        /// <param name="template">
        /// The template to use when creating the event.
        /// </param>
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
        /// An instance of <typeparamref name="TEvent"/> created using the supplied values.
        /// </returns>
        public static TEvent Create<TEvent, TException>(
            this ILogEventTemplate<TEvent> template,
            TException? exception,
            EventReason? reason = null,
            EventSuggestions? suggestions = null,
            EventParameters? parameters = null,
            EventInnerEvents? innerEvents = null)
            where TEvent : ILogEvent
            where TException : Exception
        {
            if (template is null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            return template.Create(
                new EventData(
                    reason,
                    suggestions,
                    parameters,
                    innerEvents),
                exception);
        }
    }
}
