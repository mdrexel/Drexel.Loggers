using System;
using System.Collections.Generic;
using Drexel.Loggers.Events;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents a mutable result of an operation.
    /// </summary>
    public class ActionResult : IActionResult
    {
        private readonly List<IResultEvent> allEvents;
        private readonly List<IResultEvent> errors;
        private readonly List<IResultEvent> informationals;

        private protected ActionResult()
        {
            this.allEvents = new List<IResultEvent>();
            this.errors = new List<IResultEvent>();
            this.informationals = new List<IResultEvent>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResult"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public ActionResult(bool isUnsuccessful = false)
            : this()
        {
            this.Success = !isUnsuccessful;
        }

        /// <inheritdoc/>
        public static implicit operator bool(ActionResult result) => result.Success;

        /// <inheritdoc/>
        public static bool operator !(ActionResult result) => !result.Success;

        /// <inheritdoc/>
        public bool Success { get; protected set; }

        /// <inheritdoc/>
        public IReadOnlyList<IResultEvent> AllEvents => this.allEvents;

        /// <inheritdoc/>
        public IReadOnlyList<IResultEvent> Errors => this.errors;

        /// <inheritdoc/>
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
        /// Adding an error to this instance means <see cref="Success"/> will be set to <see langword="false"/>.
        /// </remarks>
        public ActionResult AddError(ILogEvent error)
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
        public ActionResult AddInformational(ILogEvent informational)
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
        /// <param name="categories">
        /// The categories of inner event contained by <paramref name="result"/> to add to this instance.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that adding a result that is unsuccessful means this instance will become unsuccessful.
        /// </remarks>
        public ActionResult AddResult(
            IActionResult result,
            EventCategories categories = EventCategories.All)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (categories == EventCategories.All)
            {
                this.allEvents.AddRange(result.AllEvents);
                this.errors.AddRange(result.Errors);
                this.informationals.AddRange(result.Informationals);
            }
            else if (categories == EventCategories.Errors)
            {
                this.allEvents.AddRange(result.Errors);
                this.errors.AddRange(result.Errors);
            }
            else if (categories == EventCategories.Informationals)
            {
                this.allEvents.AddRange(result.Informationals);
                this.informationals.AddRange(result.Informationals);
            }

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
        /// <param name="categories">
        /// The categories of inner event contained by <paramref name="result"/> to add to this instance.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that <paramref name="value"/> may be <see langword="null"/> if <paramref name="result"/> does not
        /// contain a value. Because the <see cref="IFuncResult{T}"/> interface declares the value of the
        /// <see cref="IReadOnlyValueContainer{T}.Value"/> property to be undefined when
        /// <see cref="IReadOnlyValueContainer{T}.HasValue"/> is
        /// <see langword="false"/>, accessing <see cref="IReadOnlyValueContainer{T}.Value"/> could do something
        /// unexpected, like throw an exception. To avoid unexpected exceptions, a default value is used instead. If
        /// you're using the C# nullable reference feature, make sure <typeparamref name="T"/> is declared correctly to
        /// avoid <see langword="null"/> escaping.
        /// </remarks>
        public ActionResult AddResult<T>(
            IFuncResult<T> result,
            out T value,
            EventCategories categories = EventCategories.All)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            value = result.HasValue ? result.Value : default!;

            if (categories == EventCategories.All)
            {
                this.allEvents.AddRange(result.AllEvents);
                this.errors.AddRange(result.Errors);
                this.informationals.AddRange(result.Informationals);
            }
            else if (categories == EventCategories.Errors)
            {
                this.allEvents.AddRange(result.Errors);
                this.errors.AddRange(result.Errors);
            }
            else if (categories == EventCategories.Informationals)
            {
                this.allEvents.AddRange(result.Informationals);
                this.informationals.AddRange(result.Informationals);
            }

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
    public class ActionResult<TEvent> : ITryResult<TEvent>
        where TEvent : ILogEvent
    {
        private readonly List<IResultEvent<TEvent>> allEvents;
        private readonly List<IResultEvent<TEvent>> errors;
        private readonly List<IResultEvent<TEvent>> informationals;

        private protected ActionResult()
        {
            this.allEvents = new List<IResultEvent<TEvent>>();
            this.errors = new List<IResultEvent<TEvent>>();
            this.informationals = new List<IResultEvent<TEvent>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionResult"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public ActionResult(bool isUnsuccessful = false)
            : this()
        {
            this.Success = !isUnsuccessful;
        }

        /// <inheritdoc/>
        public static implicit operator bool(ActionResult<TEvent> result) => result.Success;

        /// <inheritdoc/>
        public static bool operator !(ActionResult<TEvent> result) => !result.Success;

        /// <inheritdoc/>
        public bool Success { get; protected set; }

        /// <inheritdoc/>
        public IReadOnlyList<IResultEvent<TEvent>> AllEvents => this.allEvents;

        /// <inheritdoc/>
        public IReadOnlyList<IResultEvent<TEvent>> Errors => this.errors;

        /// <inheritdoc/>
        public IReadOnlyList<IResultEvent<TEvent>> Informationals => this.informationals;

        IReadOnlyList<IResultEvent> IActionResult.AllEvents => this.allEvents;

        IReadOnlyList<IResultEvent> IActionResult.Errors => this.errors;

        IReadOnlyList<IResultEvent> IActionResult.Informationals => this.informationals;

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
        public ActionResult<TEvent> AddError(TEvent error)
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
        public ActionResult<TEvent> AddInformational(TEvent informational)
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
        /// <param name="categories">
        /// The categories of inner event contained by <paramref name="result"/> to add to this instance.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that adding a result that is unsuccessful means this instance will become unsuccessful.
        /// </remarks>
        public ActionResult<TEvent> AddResult(
            ITryResult<TEvent> result,
            EventCategories categories = EventCategories.All)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (categories == EventCategories.All)
            {
                this.allEvents.AddRange(result.AllEvents);
                this.errors.AddRange(result.Errors);
                this.informationals.AddRange(result.Informationals);
            }
            else if (categories == EventCategories.Errors)
            {
                this.allEvents.AddRange(result.Errors);
                this.errors.AddRange(result.Errors);
            }
            else if (categories == EventCategories.Informationals)
            {
                this.allEvents.AddRange(result.Informationals);
                this.informationals.AddRange(result.Informationals);
            }

            this.Success &= result.Success;

            return this;
        }

        /// <summary>
        /// Adds the specified result to this instance.
        /// </summary>
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
        /// <param name="categories">
        /// The categories of inner event contained by <paramref name="result"/> to add to this instance.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="result"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Note that <paramref name="value"/> may be <see langword="null"/> if <paramref name="result"/> does not
        /// contain a value. Because the <see cref="IFuncResult{T}"/> interface declares the value of the
        /// <see cref="IReadOnlyValueContainer{T}.Value"/> property to be undefined when
        /// <see cref="IReadOnlyValueContainer{T}.HasValue"/> is
        /// <see langword="false"/>, accessing <see cref="IReadOnlyValueContainer{T}.Value"/> could do something
        /// unexpected, like throw an exception. To avoid unexpected exceptions, a default value is used instead. If
        /// you're using the C# nullable reference feature, make sure <typeparamref name="TValue"/> is declared
        /// correctly to avoid <see langword="null"/> escaping.
        /// </remarks>
        public ActionResult<TEvent> AddResult<TValue>(
            IValueResult<TEvent, TValue> result,
            out TValue value,
            EventCategories categories = EventCategories.All)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            value = result.HasValue ? result.Value : default!;

            if (categories == EventCategories.All)
            {
                this.allEvents.AddRange(result.AllEvents);
                this.errors.AddRange(result.Errors);
                this.informationals.AddRange(result.Informationals);
            }
            else if (categories == EventCategories.Errors)
            {
                this.allEvents.AddRange(result.Errors);
                this.errors.AddRange(result.Errors);
            }
            else if (categories == EventCategories.Informationals)
            {
                this.allEvents.AddRange(result.Informationals);
                this.informationals.AddRange(result.Informationals);
            }

            this.Success &= result.Success;

            return this;
        }
    }
}
