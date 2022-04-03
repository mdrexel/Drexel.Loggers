using System;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents an event associated with an operation's result.
    /// </summary>
    public interface IResultEvent
    {
        /// <summary>
        /// Gets the event. This value is guaranteed to be non-<see langword="null"/>.
        /// </summary>
        ILogEvent Event { get; }

        /// <summary>
        /// Invokes the appropriate delegate on this instance.
        /// </summary>
        /// <param name="onError">
        /// The delegate invoked when this event represents an error.
        /// </param>
        /// <param name="onInformational">
        /// The delegate invoked when this event represents an informational.
        /// </param>
        void Operation(
            Action<IResultEvent> onError,
            Action<IResultEvent> onInformational);

        /// <summary>
        /// Invokes the appropriate delegate on this instance, returning the value returned by the delegate.
        /// </summary>
        /// <typeparam name="T">
        /// The type of value returned by the delegate.
        /// </typeparam>
        /// <param name="onError">
        /// The delegate invoked when this event represents an error.
        /// </param>
        /// <param name="onInformational">
        /// The delegate invoked when this event represents an informational.
        /// </param>
        /// <returns></returns>
        T Operation<T>(
            Func<IResultEvent, T> onError,
            Func<IResultEvent, T> onInformational);
    }

    /// <summary>
    /// Represents a strongly-typed event associated with an operation's result.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event contained by the result.
    /// </typeparam>
    public interface IResultEvent<out TEvent> :
        IResultEvent
        where TEvent : ILogEvent
    {
        /// <inheritdoc cref="IResultEvent.Event"/>
        new TEvent Event { get; }

        /// <inheritdoc cref="IResultEvent.Operation(Action{IResultEvent}, Action{IResultEvent})"/>
        void Operation(
            Action<IResultEvent<TEvent>> onError,
            Action<IResultEvent<TEvent>> onInformational);

        /// <inheritdoc cref="IResultEvent.Operation{T}(Func{IResultEvent, T}, Func{IResultEvent, T})"/>
        T Operation<T>(
            Func<IResultEvent<TEvent>, T> onError,
            Func<IResultEvent<TEvent>, T> onInformational);
    }
}
