using System;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents an event contained by a result.
    /// </summary>
    public sealed class ResultEvent : IResultEvent
    {
        private readonly bool isError;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultEvent"/> class.
        /// </summary>
        /// <param name="event">
        /// The event contained by this result.
        /// </param>
        /// <param name="isError">
        /// <see langword="true"/> if the event is an error; otherwise, <see langword="false"/> if the event is an
        /// informational.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="event"/> is <see langword="null"/>.
        /// </exception>
        public ResultEvent(ILogEvent @event, bool isError)
        {
            this.Event = @event ?? throw new ArgumentNullException(nameof(@event));
            this.isError = isError;
        }

        public ILogEvent Event { get; }

        /// <inheritdoc cref="IResultEvent.Operation(Action{IResultEvent}, Action{IResultEvent})"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="onError"/> or <paramref name="onInformational"/> is <see langword="null"/>.
        /// </exception>
        public void Operation(
            Action<IResultEvent> onError,
            Action<IResultEvent> onInformational)
        {
            if (onError is null)
            {
                throw new ArgumentNullException(nameof(onError));
            }
            else if (onInformational is null)
            {
                throw new ArgumentNullException(nameof(onInformational));
            }

            if (this.isError)
            {
                onError.Invoke(this);
            }
            else
            {
                onInformational.Invoke(this);
            }
        }

        /// <inheritdoc cref="IResultEvent.Operation{T}(Func{IResultEvent, T}, Func{IResultEvent, T})"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="onError"/> or <paramref name="onInformational"/> is <see langword="null"/>.
        /// </exception>
        public T Operation<T>(
            Func<IResultEvent, T> onError,
            Func<IResultEvent, T> onInformational)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents an event contained by a result.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event contained by the result.
    /// </typeparam>
    public sealed class ResultEvent<TEvent> : IResultEvent<TEvent>
        where TEvent : ILogEvent
    {
        private readonly bool isError;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultEvent{T}"/> class.
        /// </summary>
        /// <param name="event">
        /// The event contained by the result.
        /// </param>
        /// <param name="isError">
        /// <see langword="true"/> if the event is an error; otherwise, <see langword="false"/> if the event is an
        /// informational.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="event"/> is <see langword="null"/>.
        /// </exception>
        public ResultEvent(TEvent @event, bool isError)
        {
            this.Event = @event ?? throw new ArgumentNullException(nameof(@event));
            this.isError = isError;
        }

        public TEvent Event { get; }

        ILogEvent IResultEvent.Event => this.Event;

        /// <inheritdoc cref="IResultEvent{T}.Operation(Action{IResultEvent{T}}, Action{IResultEvent{T}})"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="onError"/> or <paramref name="onInformational"/> is <see langword="null"/>.
        /// </exception>
        public void Operation(
            Action<IResultEvent<TEvent>> onError,
            Action<IResultEvent<TEvent>> onInformational)
        {
            if (onError is null)
            {
                throw new ArgumentNullException(nameof(onError));
            }
            else if (onInformational is null)
            {
                throw new ArgumentNullException(nameof(onInformational));
            }

            if (this.isError)
            {
                onError.Invoke(this);
            }
            else
            {
                onInformational.Invoke(this);
            }
        }

        /// <inheritdoc cref="IResultEvent{TEvent}.Operation{T}(Func{IResultEvent{TEvent}, T}, Func{IResultEvent{TEvent}, T})"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="onError"/> or <paramref name="onInformational"/> is <see langword="null"/>.
        /// </exception>
        public T Operation<T>(
            Func<IResultEvent<TEvent>, T> onError,
            Func<IResultEvent<TEvent>, T> onInformational)
        {
            if (onError is null)
            {
                throw new ArgumentNullException(nameof(onError));
            }
            else if (onInformational is null)
            {
                throw new ArgumentNullException(nameof(onInformational));
            }

            if (this.isError)
            {
                return onError.Invoke(this);
            }
            else
            {
                return onInformational.Invoke(this);
            }
        }

        /// <inheritdoc cref="IResultEvent.Operation(Action{IResultEvent}, Action{IResultEvent})"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="onError"/> or <paramref name="onInformational"/> is <see langword="null"/>.
        /// </exception>
        void IResultEvent.Operation(
            Action<IResultEvent> onError,
            Action<IResultEvent> onInformational)
        {
            if (onError is null)
            {
                throw new ArgumentNullException(nameof(onError));
            }
            else if (onInformational is null)
            {
                throw new ArgumentNullException(nameof(onInformational));
            }

            if (this.isError)
            {
                onError.Invoke(this);
            }
            else
            {
                onInformational.Invoke(this);
            }
        }

        /// <inheritdoc cref="IResultEvent.Operation{T}(Func{IResultEvent, T}, Func{IResultEvent, T})"/>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="onError"/> or <paramref name="onInformational"/> is <see langword="null"/>.
        /// </exception>
        T IResultEvent.Operation<T>(
            Func<IResultEvent, T> onError,
            Func<IResultEvent, T> onInformational)
        {
            if (onError is null)
            {
                throw new ArgumentNullException(nameof(onError));
            }
            else if (onInformational is null)
            {
                throw new ArgumentNullException(nameof(onInformational));
            }

            if (this.isError)
            {
                return onError.Invoke(this);
            }
            else
            {
                return onInformational.Invoke(this);
            }
        }
    }
}
