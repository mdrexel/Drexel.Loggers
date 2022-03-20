using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.FormattableString;

namespace Drexel.Loggers.Events
{
    [DebuggerDisplay("{ToStringDebug,nq}")]
    public class LogEvent : ILogEvent
    {
        public LogEvent(
            EventSeverity severity,
            EventInfo info,
            EventExceptionInfo? exception,
            EventInnerEvents? innerEvents)
        {
            this.Severity = severity;
            this.Info = info ?? throw new ArgumentNullException(nameof(info));
            this.Exception = exception;
            this.InnerEvents = innerEvents ?? (IReadOnlyList<ILogEvent>)Array.Empty<ILogEvent>();
        }

        public virtual EventSeverity Severity { get; }

        public virtual EventInfo Info { get; }

        public virtual EventExceptionInfo? Exception { get; }

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
            return Invariant($"[{this.Info.Code.DebugHumanReadableValue ?? this.Info.Code.ToString()}] ({this.Severity}) {this.Info.Message}");
        }
    }
}
