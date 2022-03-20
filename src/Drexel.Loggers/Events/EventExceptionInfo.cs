using System;

namespace Drexel.Loggers.Events
{
    /// <summary>
    /// Represents information associated with an event describing an exception.
    /// </summary>
    public abstract class EventExceptionInfo
    {
        private protected EventExceptionInfo()
        {
        }

        /// <summary>
        /// Gets the type of the underlying exception.
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// Gets the message of the underlying exception.
        /// </summary>
        public abstract string Message { get; }

        /// <summary>
        /// Gets the stacktrace of the underlying exception.
        /// </summary>
        public abstract string StackTrace { get; }

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
            return new EventExceptionInfo<T>(exception ?? throw new ArgumentNullException(nameof(exception)));
        }
    }

    /// <summary>
    /// Represents information associated with an event describing an exception.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the underlying exception.
    /// </typeparam>
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
        {
            this.Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        /// <summary>
        /// Gets the underlying exception.
        /// </summary>
        public T Exception { get; }

        public override Type Type => typeof(T);

        public override string Message => this.Exception.Message;

        public override string StackTrace => this.Exception.StackTrace;
    }
}
