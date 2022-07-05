using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// A simple implementation of <see cref="ILogEvent"/> that represents an event associated with an operation.
    /// </summary>
    [DebuggerDisplay("{ToStringDebug(),nq}")]
    public class LogEvent : ILogEvent
    {
        private static readonly IReadOnlyList<ILogEvent> EmptyInnerEvents = new ILogEvent[0];

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEvent"/> class.
        /// </summary>
        /// <param name="severity">
        /// The severity of the event.
        /// </param>
        /// <param name="info">
        /// The info associated with the event.
        /// </param>
        /// <param name="exception">
        /// The exception associated with the event, if one exists.
        /// </param>
        /// <param name="innerEvents">
        /// The inner events associated with the event, if any exist.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="info"/> is <see langword="null"/>.
        /// </exception>
        public LogEvent(
            EventSeverity severity,
            EventInfo info,
            EventExceptionInfo? exception,
            EventInnerEvents? innerEvents)
        {
            this.Severity = severity;
            this.Info = info ?? throw new ArgumentNullException(nameof(info));
            this.Exception = exception;
            this.InnerEvents = innerEvents ?? EmptyInnerEvents;
        }

        /// <inheritdoc/>
        public virtual EventSeverity Severity { get; }

        /// <inheritdoc/>
        public virtual EventInfo Info { get; }

        /// <inheritdoc/>
        public virtual EventExceptionInfo? Exception { get; }

        /// <inheritdoc/>
        public virtual IReadOnlyList<ILogEvent> InnerEvents { get; }

        /// <summary>
        /// Returns a string representing this instance.
        /// </summary>
        /// <returns>
        /// A string representing this instance.
        /// </returns>
        /// <remarks>
        /// This method is used by the <see cref="DebuggerDisplayAttribute"/> attribute.
        /// </remarks>
        protected virtual string ToStringDebug()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "[{0}] ({1}) {2}",
                this.Info.Code.DebugHumanReadableValue ?? this.Info.Code.ToString(),
                this.Severity,
                this.Info.Message);
        }
    }
}
