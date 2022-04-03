﻿using System;
using System.Collections.Generic;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents a mutable result of an operation.
    /// </summary>
    public sealed class TryResult : ITryResult
    {
        private readonly List<IResultEvent> allEvents;
        private readonly List<IResultEvent> errors;
        private readonly List<IResultEvent> informationals;

        /// <summary>
        /// Initializes a new instance of the <see cref="TryResult"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public TryResult(bool isUnsuccessful = false)
        {
            this.Success = !isUnsuccessful;
            this.allEvents = new List<IResultEvent>();
            this.errors = new List<IResultEvent>();
            this.informationals = new List<IResultEvent>();
        }

        public static implicit operator bool(TryResult result) => result.Success;

        public static bool operator !(TryResult result) => !result.Success;

        public bool Success { get; private set; }

        public IReadOnlyList<IResultEvent> AllEvents => this.allEvents;

        public IReadOnlyList<IResultEvent> Errors => this.errors;

        public IReadOnlyList<IResultEvent> Informationals => this.informationals;

        /// <summary>
        /// Adds the specified event to this instance as an error.
        /// </summary>
        /// <param name="error">
        /// The error to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="error"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Adding an error to this instance will mean <see cref="Success"/> will be set to <see langword="false"/>.
        /// </remarks>
        public TryResult AddError(ILogEvent error)
        {
            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            this.Success = false;

            ResultEvent @event = new ResultEvent(error, isError: true);
            this.allEvents.Add(@event);
            this.errors.Add(@event);

            return this;
        }

        /// <summary>
        /// Adds the specified event to this instance as an informational event.
        /// </summary>
        /// <param name="informational">
        /// The event to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="informational"/> is <see langword="null"/>.
        /// </exception>
        public TryResult AddInformational(ILogEvent informational)
        {
            if (informational is null)
            {
                throw new ArgumentNullException(nameof(informational));
            }

            ResultEvent @event = new ResultEvent(informational, isError: false);
            this.allEvents.Add(@event);
            this.informationals.Add(@event);

            return this;
        }

        /// <summary>
        /// Adds the specified result to this instance.
        /// </summary>
        /// <param name="result">
        /// The result to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that adding a result that is unsuccessful, means this instance will become unsuccessful.
        /// </remarks>
        public TryResult AddResult(ITryResult result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            this.allEvents.AddRange(result.AllEvents);
            this.errors.AddRange(result.Errors);
            this.informationals.AddRange(result.Informationals);

            this.Success &= result.Success;

            return this;
        }

        /// <summary>
        /// Adds the specified result to this instance.
        /// </summary>
        /// <typeparam name="T">
        /// The type of value returned by the operation that produced <paramref name="result"/>.
        /// </typeparam>
        /// <param name="result">
        /// The result to add.
        /// </param>
        /// <param name="value">
        /// When this method returns, if <paramref name="result"/> contained a value, set to the value contained by
        /// <paramref name="result"/>. Otherwise, if <paramref name="result"/> does not contain a value, set to
        /// <see langword="default"/>.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that <paramref name="value"/> may be <see langword="null"/> if <paramref name="result"/> does not
        /// contain a value. Because the <see cref="IValueResult{T}"/> interface declares the value of the
        /// <see cref="IValueResult{T}.Value"/> property to be undefined when <see cref="IValueResult{T}.HasValue"/> is
        /// <see langword="false"/>, accessing <see cref="IValueResult{T}.Value"/> could do something unexpected, like
        /// throw an exception. To avoid unexpected exceptions, a default value is used instead. If you're using the
        /// C# nullable reference feature, make sure <typeparamref name="T"/> is declared correctly to avoid
        /// <see langword="null"/> escaping.
        /// </remarks>
        public TryResult AddResult<T>(IValueResult<T> result, out T value)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            value = result.HasValue ? result.Value : default!;

            this.allEvents.AddRange(result.AllEvents);
            this.errors.AddRange(result.Errors);
            this.informationals.AddRange(result.Informationals);

            this.Success &= result.Success;

            return this;
        }
    }

    /// <summary>
    /// Represents a mutable result of an operation.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event returned by the operation.
    /// </typeparam>
    public sealed class TryResult<TEvent> : ITryResult<TEvent>
        where TEvent : ILogEvent
    {
        private readonly List<IResultEvent<TEvent>> allEvents;
        private readonly List<IResultEvent<TEvent>> errors;
        private readonly List<IResultEvent<TEvent>> informationals;

        /// <summary>
        /// Initializes a new instance of the <see cref="TryResult"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public TryResult(bool isUnsuccessful = false)
        {
            this.Success = !isUnsuccessful;
            this.allEvents = new List<IResultEvent<TEvent>>();
            this.errors = new List<IResultEvent<TEvent>>();
            this.informationals = new List<IResultEvent<TEvent>>();
        }

        public static implicit operator bool(TryResult<TEvent> result) => result.Success;

        public static bool operator !(TryResult<TEvent> result) => !result.Success;

        public bool Success { get; private set; }

        public IReadOnlyList<IResultEvent<TEvent>> AllEvents => this.allEvents;

        public IReadOnlyList<IResultEvent<TEvent>> Errors => this.errors;

        public IReadOnlyList<IResultEvent<TEvent>> Informationals => this.informationals;

        IReadOnlyList<IResultEvent> ITryResult.AllEvents => this.allEvents;

        IReadOnlyList<IResultEvent> ITryResult.Errors => this.errors;

        IReadOnlyList<IResultEvent> ITryResult.Informationals => this.informationals;

        /// <summary>
        /// Adds the specified event to this instance as an error.
        /// </summary>
        /// <param name="error">
        /// The error to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="error"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Adding an error to this instance will mean <see cref="Success"/> will be set to <see langword="false"/>.
        /// </remarks>
        public TryResult<TEvent> AddError(TEvent error)
        {
            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            this.Success = false;

            ResultEvent<TEvent> @event = new ResultEvent<TEvent>(error, isError: true);
            this.allEvents.Add(@event);
            this.errors.Add(@event);

            return this;
        }

        /// <summary>
        /// Adds the specified event to this instance as an informational event.
        /// </summary>
        /// <param name="informational">
        /// The event to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="informational"/> is <see langword="null"/>.
        /// </exception>
        public TryResult<TEvent> AddInformational(TEvent informational)
        {
            if (informational is null)
            {
                throw new ArgumentNullException(nameof(informational));
            }

            ResultEvent<TEvent> @event = new ResultEvent<TEvent>(informational, isError: false);
            this.allEvents.Add(@event);
            this.informationals.Add(@event);

            return this;
        }

        /// <summary>
        /// Adds the specified result to this instance.
        /// </summary>
        /// <param name="result">
        /// The result to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that adding a result that is unsuccessful, means this instance will become unsuccessful.
        /// </remarks>
        public TryResult<TEvent> AddResult(ITryResult<TEvent> result)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            this.allEvents.AddRange(result.AllEvents);
            this.errors.AddRange(result.Errors);
            this.informationals.AddRange(result.Informationals);

            this.Success &= result.Success;

            return this;
        }

        /// <summary>
        /// Adds the specified result to this instance.
        /// </summary>
        /// <typeparam name="T">
        /// The type of event returned by the operation that produced <paramref name="result"/>.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of value returned by the operation that produced <paramref name="result"/>.
        /// </typeparam>
        /// <param name="result">
        /// The result to add.
        /// </param>
        /// <param name="value">
        /// When this method returns, if <paramref name="result"/> contained a value, set to the value contained by
        /// <paramref name="result"/>. Otherwise, if <paramref name="result"/> does not contain a value, set to
        /// <see langword="default"/>.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that <paramref name="value"/> may be <see langword="null"/> if <paramref name="result"/> does not
        /// contain a value. Because the <see cref="IValueResult{T}"/> interface declares the value of the
        /// <see cref="IValueResult{T}.Value"/> property to be undefined when <see cref="IValueResult{T}.HasValue"/> is
        /// <see langword="false"/>, accessing <see cref="IValueResult{T}.Value"/> could do something unexpected, like
        /// throw an exception. To avoid unexpected exceptions, a default value is used instead. If you're using the
        /// C# nullable reference feature, make sure <typeparamref name="TValue"/> is declared correctly to avoid
        /// <see langword="null"/> escaping.
        /// </remarks>
        public TryResult<TEvent> AddResult<TValue>(IValueResult<TEvent, TValue> result, out TValue value)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            value = result.HasValue ? result.Value : default!;

            this.allEvents.AddRange(result.AllEvents);
            this.errors.AddRange(result.Errors);
            this.informationals.AddRange(result.Informationals);

            this.Success &= result.Success;

            return this;
        }
    }
}
