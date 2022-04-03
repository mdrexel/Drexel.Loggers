using System;
using System.Collections.Generic;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents a mutable result of an operation.
    /// </summary>
    /// <typeparam name="TValue">
    /// The type of value returned by the operation.
    /// </typeparam>
    /// <remarks>
    /// When using the nullable reference types language feature, make sure to declare your nullability correctly.
    /// When an instance of <see cref="ValueResult{T}"/> is initialized, the value contained by this result will be set
    /// to <see langword="default"/>, which is <see langword="null"/> for any <typeparamref name="T"/> that is a
    /// <see langword="class"/>. Because <typeparamref name="T"/> could be a <see langword="struct"/>, the
    /// interface must declare the value to be non-nullable. This means that, if you do not specify
    /// <typeparamref name="T"/> to be nullable, you must make sure you always populate this property before
    /// returning the result object, or else you may unexpectedly return a value of <see langword="null"/>.
    /// </remarks>
    public sealed class ValueResult<TValue> : IValueResult<TValue>
    {
        private readonly List<IResultEvent> allEvents;
        private readonly List<IResultEvent> errors;
        private readonly List<IResultEvent> informationals;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueResult{T}"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public ValueResult(bool isUnsuccessful = false)
        {
            this.Value = default!;
            this.HasValue = false;

            this.Success = !isUnsuccessful;
            this.allEvents = new List<IResultEvent>();
            this.errors = new List<IResultEvent>();
            this.informationals = new List<IResultEvent>();
        }

        public bool HasValue { get; private set; }

        public TValue Value { get; private set; }

        public static implicit operator bool(ValueResult<TValue> result) => result.Success;

        public static bool operator !(ValueResult<TValue> result) => !result.Success;

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
        public ValueResult<TValue> AddError(ILogEvent error)
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
        public ValueResult<TValue> AddInformational(ILogEvent informational)
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
        public ValueResult<TValue> AddResult(ITryResult result)
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
        public ValueResult<TValue> AddResult<T>(IValueResult<T> result, out T value)
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

        /// <summary>
        /// Adds the specified value to this result, throwing an exception if this result already has a value.
        /// </summary>
        /// <param name="value">
        /// The value to add to this result.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this result already has a value.
        /// </exception>
        public void AddValue(TValue value)
        {
            if (this.HasValue)
            {
                throw new InvalidOperationException("Result already contains a value.");
            }

            this.Value = value;
            this.HasValue = true;
        }

        /// <summary>
        /// Removes the value contained by this result.
        /// </summary>
        /// <param name="value">
        /// When this method returns, set to the value that was contained by this result, if one was contained by this
        /// result. Otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was removed from this result; otherwise, <see langword="false"/>.
        /// </returns>
        public bool RemoveValue(out TValue value)
        {
            bool hadValue = this.HasValue;
            this.HasValue = false;
            value = this.Value;
            this.Value = default!;

            return hadValue;
        }

        /// <summary>
        /// Sets the value of this result to the specified value.
        /// </summary>
        /// <param name="newValue">
        /// The value this result should contain.
        /// </param>
        /// <param name="oldValue">
        /// The value previously contained by this result, if one existed; otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was already contained by this result; otherwise, <see langword="false"/>.
        /// </returns>
        public bool SetValue(TValue newValue, out TValue oldValue)
        {
            bool hadValue = this.RemoveValue(out oldValue);
            this.Value = newValue;
            this.HasValue = true;

            return hadValue;
        }

        /// <summary>
        /// Tries to add the specified value to this result.
        /// </summary>
        /// <param name="value">
        /// The value to try to add.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the result now contains the specified value; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryAddValue(TValue value)
        {
            if (this.HasValue)
            {
                return false;
            }

            this.Value = value;
            return true;
        }
    }

    /// <summary>
    /// Represents a mutable result of an operation.
    /// </summary>
    /// <typeparam name="TEvent">
    /// The type of event returned by the operation.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of value returned by the operation.
    /// </typeparam>
    /// <remarks>
    /// When using the nullable reference types language feature, make sure to declare your nullability correctly.
    /// When an instance of <see cref="ValueResult{T}"/> is initialized, the value contained by this result will be set
    /// to <see langword="default"/>, which is <see langword="null"/> for any <typeparamref name="T"/> that is a
    /// <see langword="class"/>. Because <typeparamref name="T"/> could be a <see langword="struct"/>, the
    /// interface must declare the value to be non-nullable. This means that, if you do not specify
    /// <typeparamref name="T"/> to be nullable, you must make sure you always populate this property before
    /// returning the result object, or else you may unexpectedly return a value of <see langword="null"/>.
    /// </remarks>
    public sealed class ValueResult<TEvent, TValue> : IValueResult<TEvent, TValue>
        where TEvent : ILogEvent
    {
        private readonly List<IResultEvent<TEvent>> allEvents;
        private readonly List<IResultEvent<TEvent>> errors;
        private readonly List<IResultEvent<TEvent>> informationals;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueResult{T}"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public ValueResult(bool isUnsuccessful = false)
        {
            this.Value = default!;
            this.HasValue = false;

            this.Success = !isUnsuccessful;
            this.allEvents = new List<IResultEvent<TEvent>>();
            this.errors = new List<IResultEvent<TEvent>>();
            this.informationals = new List<IResultEvent<TEvent>>();
        }

        public bool HasValue { get; private set; }

        public TValue Value { get; private set; }

        public static implicit operator bool(ValueResult<TEvent, TValue> result) => result.Success;

        public static bool operator !(ValueResult<TEvent, TValue> result) => !result.Success;

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
        public ValueResult<TEvent, TValue> AddError(TEvent error)
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
        public ValueResult<TEvent, TValue> AddInformational(TEvent informational)
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
        public ValueResult<TEvent, TValue> AddResult(ITryResult<TEvent> result)
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
        /// <typeparam name="TOtherValue">
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
        /// C# nullable reference feature, make sure <typeparamref name="TOtherValue"/> is declared correctly to avoid
        /// <see langword="null"/> escaping.
        /// </remarks>
        public ValueResult<TEvent, TValue> AddResult<TOtherValue>(
            IValueResult<TEvent, TOtherValue> result,
            out TOtherValue value)
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

        /// <summary>
        /// Adds the specified value to this result, throwing an exception if this result already has a value.
        /// </summary>
        /// <param name="value">
        /// The value to add to this result.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this result already has a value.
        /// </exception>
        public void AddValue(TValue value)
        {
            if (this.HasValue)
            {
                throw new InvalidOperationException("Result already contains a value.");
            }

            this.Value = value;
            this.HasValue = true;
        }

        /// <summary>
        /// Removes the value contained by this result.
        /// </summary>
        /// <param name="value">
        /// When this method returns, set to the value that was contained by this result, if one was contained by this
        /// result. Otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was removed from this result; otherwise, <see langword="false"/>.
        /// </returns>
        public bool RemoveValue(out TValue value)
        {
            bool hadValue = this.HasValue;
            this.HasValue = false;
            value = this.Value;
            this.Value = default!;

            return hadValue;
        }

        /// <summary>
        /// Sets the value of this result to the specified value.
        /// </summary>
        /// <param name="newValue">
        /// The value this result should contain.
        /// </param>
        /// <param name="oldValue">
        /// The value previously contained by this result, if one existed; otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was already contained by this result; otherwise, <see langword="false"/>.
        /// </returns>
        public bool SetValue(TValue newValue, out TValue oldValue)
        {
            bool hadValue = this.RemoveValue(out oldValue);
            this.Value = newValue;
            this.HasValue = true;

            return hadValue;
        }

        /// <summary>
        /// Tries to add the specified value to this result.
        /// </summary>
        /// <param name="value">
        /// The value to try to add.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the result now contains the specified value; otherwise, <see langword="false"/>.
        /// </returns>
        public bool TryAddValue(TValue value)
        {
            if (this.HasValue)
            {
                return false;
            }

            this.Value = value;
            return true;
        }
    }
}
