using System;
using System.Diagnostics;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents information associated with an event describing an exception.
    /// </summary>
    [DebuggerDisplay("[{Type,nq}] {Message,nq}")]
    public abstract class EventExceptionInfo
    {
        private protected EventExceptionInfo(
            Exception exception)
        {
            this.Exception = exception;
            this.Type = exception.GetType();
        }

        /// <summary>
        /// Gets the exception associated with the event.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets the type of the exception.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Creates an instance of <see cref="EventExceptionInfo{T}"/> derived from the specified
        /// <paramref name="exception"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the exception.
        /// </typeparam>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// An instance of <see cref="EventExceptionInfo{T}"/> derived from the specified <paramref name="exception"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="exception"/> is <see langword="null"/>.
        /// </exception>
        public static EventExceptionInfo<T> Create<T>(T exception)
            where T : Exception
        {
            return new EventExceptionInfo<T>(exception);
        }
    }

    /// <summary>
    /// Represents information associated with an event describing an exception.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the underlying exception.
    /// </typeparam>
    [DebuggerDisplay("[{Type,nq}] {Message,nq}")]
    public sealed class EventExceptionInfo<T> : EventExceptionInfo
        where T : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventExceptionInfo{T}"/> class.
        /// </summary>
        /// <param name="exception">
        /// The underlying exception.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="exception"/> is <see langword="null"/>.
        /// </exception>
        public EventExceptionInfo(T exception)
            : base(exception ?? throw new ArgumentNullException(nameof(exception)))
        {
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the underlying exception.
        /// </summary>
        public new T Exception { get; }
    }
}
