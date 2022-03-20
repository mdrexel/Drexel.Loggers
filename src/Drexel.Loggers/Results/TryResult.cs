using System;
using System.Collections.Generic;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents a mutable result of an operation.
    /// </summary>
    public class TryResult : ITryResult
    {
        private protected TryResult()
        {
            this.IsSuccess = true;
            this.AllEvents = new List<ILogEvent>();
            this.Errors = new List<ILogEvent>();
            this.Informationals = new List<ILogEvent>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TryResult"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public TryResult(bool isUnsuccessful = false)
            : this()
        {
            this.IsSuccess = !isUnsuccessful;
        }

        public static implicit operator bool(TryResult result) => result.IsSuccess;

        public bool IsSuccess { get; protected set; }

        protected List<ILogEvent> AllEvents;

        protected List<ILogEvent> Errors;

        protected List<ILogEvent> Informationals;

        IReadOnlyList<ILogEvent> ITryResult.AllEvents => this.AllEvents;

        IReadOnlyList<ILogEvent> ITryResult.Errors => this.Errors;

        IReadOnlyList<ILogEvent> ITryResult.Informationals => this.Informationals;

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
        /// Adding an error to this instance will mean <see cref="IsSuccess"/> will be set to <see langword="false"/>.
        /// </remarks>
        public TryResult AddError(ILogEvent error)
        {
            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            this.IsSuccess = false;

            this.AllEvents.Add(error);
            this.Errors.Add(error);

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

            this.AllEvents.Add(informational);
            this.Informationals.Add(informational);

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

            this.AllEvents.AddRange(result.AllEvents);
            this.Errors.AddRange(result.Errors);
            this.Informationals.AddRange(result.Informationals);

            this.IsSuccess &= result.IsSuccess;

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
        /// When this method returns, set to the value contained by <paramref name="result"/>. Note that if
        /// <paramref name="result"/> is unsuccessful, the value contained by <paramref name="result"/> is undefined.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        public TryResult AddResult<T>(ITryResult<T> result, out T value)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            // TODO: only grab value if `result.IsSuccess`?
            value = result.Value;

            this.AllEvents.AddRange(result.AllEvents);
            this.Errors.AddRange(result.Errors);
            this.Informationals.AddRange(result.Informationals);

            this.IsSuccess &= result.IsSuccess;

            return this;
        }
    }

    /// <summary>
    /// Represents a mutable result of an operation that returns a result.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value returned by the operation.
    /// </typeparam>
    public class TryResult<T> : TryResult, ITryResult<T>
    {
        private bool

        /// <summary>
        /// Initializes a new instance of the <see cref="TryResult"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public TryResult(bool isUnsuccessful = false)
            : base(isUnsuccessful)
        {
            // We cannot declare `Value` as `T?` because `T` may be a struct and so cannot be null. 
            this.Value = default!;
        }

        /// <summary>
        /// Gets or sets the value returned by this operation.
        /// </summary>
        /// <value>
        /// The value returned by this operation, or <see langword="default"/> if the operation did not set a result.
        /// </value>
        /// <remarks>
        /// When using the nullable reference types language feature, make sure to declare your nullability correctly.
        /// When an instance of <see cref="TryResult{T}"/> is initialized, the value of this property will be set to
        /// <see langword="default"/>, which is <see langword="null"/> for any <typeparamref name="T"/> that is a
        /// <see langword="class"/>. Because <typeparamref name="T"/> could be a <see langword="struct"/>, the
        /// interface must declare this property to be non-nullable. This means that, if you do not specify
        /// <typeparamref name="T"/> to be nullable, you must make sure you always populate this property before
        /// returning the result object, or else you may unexpectedly return a value of <see langword="null"/>.
        /// </remarks>
        public T Value { get; set; }

        /// <inheritdoc cref="TryResult.AddError(ILogEvent)"/>
        public new TryResult<T> AddError(ILogEvent error)
        {
            base.AddError(error);
            return this;
        }

        /// <inheritdoc cref="TryResult.AddInformational(ILogEvent)"/>
        public new TryResult<T> AddInformational(ILogEvent informational)
        {
            base.AddInformational(informational);
            return this;
        }

        /// <inheritdoc cref="TryResult.AddResult(ITryResult)"/>
        public new TryResult<T> AddResult(ITryResult result)
        {
            base.AddResult(result);
            return this;
        }

        /// <inheritdoc cref="TryResult.AddResult{T}(ITryResult{T}, out T)"/>
        public new TryResult<T> AddResult<TOther>(ITryResult<TOther> result, out TOther value)
        {
            base.AddResult<TOther>(result, out value);
            return this;
        }
    }
}
