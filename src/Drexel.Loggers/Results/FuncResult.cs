﻿using Drexel.Loggers.Events;

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
    /// When an instance of <see cref="FuncResult{T}"/> is initialized, the value contained by this result will be set
    /// to <see langword="default"/>, which is <see langword="null"/> for any <typeparamref name="TValue"/> that is a
    /// <see langword="class"/>. Because <typeparamref name="TValue"/> could be a <see langword="struct"/>, the
    /// interface must declare the value to be non-nullable. This means that, if you do not specify
    /// <typeparamref name="TValue"/> to be nullable, you must make sure you always populate this property before
    /// returning the result object, or else you may unexpectedly return a value of <see langword="null"/>.
    /// </remarks>
    public sealed class FuncResult<TValue> :
        ActionResult,
        IFuncResult<TValue>,
        IValueContainer<TValue>
    {
        private readonly ValueContainer<TValue> value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncResult{T}"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public FuncResult(bool isUnsuccessful = false)
            : base(isUnsuccessful)
        {
            this.value = new ValueContainer<TValue>();

            this.Success = !isUnsuccessful;
        }

        /// <inheritdoc/>
        public static implicit operator bool(FuncResult<TValue> result) => result.Success;

        /// <inheritdoc/>
        public static bool operator !(FuncResult<TValue> result) => !result.Success;

        /// <inheritdoc/>
        public bool HasValue => this.value.HasValue;

        /// <inheritdoc/>
        public TValue Value => this.value.Value;

        /// <inheritdoc cref="ActionResult.AddError(ILogEvent)"/>
        public new FuncResult<TValue> AddError(ILogEvent error)
        {
            base.AddError(error);
            return this;
        }

        /// <inheritdoc cref="ActionResult.AddInformational(ILogEvent)"/>
        public new FuncResult<TValue> AddInformational(ILogEvent informational)
        {
            base.AddInformational(informational);
            return this;
        }

        /// <inheritdoc cref="ActionResult.AddResult(IActionResult, EventCategories)"/>
        public new FuncResult<TValue> AddResult(
            IActionResult result,
            EventCategories categories = EventCategories.All)
        {
            base.AddResult(result, categories);
            return this;
        }

        /// <inheritdoc cref="ActionResult.AddResult{T}(IFuncResult{T}, out T, EventCategories)"/>
        public new FuncResult<TValue> AddResult<T>(
            IFuncResult<T> result,
            out T value,
            EventCategories categories = EventCategories.All)
        {
            base.AddResult(result, out value, categories);
            return this;
        }

        /// <inheritdoc/>
        public bool GetValue(out TValue value) =>
            this.value.GetValue(out value);

        /// <inheritdoc/>
        public bool RemoveValue() =>
            this.value.RemoveValue();

        /// <inheritdoc/>
        public bool RemoveValue(out TValue value) =>
            this.value.RemoveValue(out value);

        /// <inheritdoc/>
        public bool SetValue(TValue newValue) =>
            this.value.SetValue(newValue);

        /// <inheritdoc/>
        public bool SetValue(TValue newValue, out TValue oldValue) =>
            this.value.SetValue(newValue, out oldValue);

        /// <inheritdoc/>
        public bool TryAddValue(TValue value) =>
            this.value.TryAddValue(value);

        /// <inheritdoc/>
        public bool TryAddValue(TValue value, out TValue currentValue) =>
            this.value.TryAddValue(value, out currentValue);
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
    /// When an instance of <see cref="FuncResult{TEvent, TValue}"/> is initialized, the value contained by this
    /// result will be set to <see langword="default"/>, which is <see langword="null"/> for any
    /// <typeparamref name="TValue"/> that is a <see langword="class"/>. Because <typeparamref name="TValue"/> could be
    /// a <see langword="struct"/>, the interface must declare the value to be non-nullable. This means that, if you do
    /// not specify <typeparamref name="TValue"/> to be nullable, you must make sure you always populate this property
    /// before returning the result object, or else you may unexpectedly return a value of <see langword="null"/>.
    /// </remarks>
    public sealed class FuncResult<TEvent, TValue> :
        ActionResult<TEvent>,
        IFuncResult<TEvent, TValue>,
        IValueContainer<TValue>
        where TEvent : ILogEvent
    {
        private readonly ValueContainer<TValue> value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncResult{TEvent, TValue}"/> class.
        /// </summary>
        /// <param name="isUnsuccessful">
        /// A value indicating whether this result should be considered unsuccessful even if it contains no errors.
        /// <see langword="true"/> if this result should be considered unsuccessful even if it contains no errors;
        /// otherwise, <see langword="false"/>. Note that a value of <see langword="false"/> means the result will
        /// still be unsuccessful <b>if the result contains any errors</b>.
        /// </param>
        public FuncResult(bool isUnsuccessful = false)
            : base(isUnsuccessful)
        {
            this.value = new ValueContainer<TValue>();
            this.Success = !isUnsuccessful;
        }

        /// <inheritdoc/>
        public static implicit operator bool(FuncResult<TEvent, TValue> result) => result.Success;

        /// <inheritdoc/>
        public static bool operator !(FuncResult<TEvent, TValue> result) => !result.Success;

        /// <inheritdoc/>
        public bool HasValue => this.value.HasValue;

        /// <inheritdoc/>
        public TValue Value => this.value.Value;

        /// <inheritdoc cref="ActionResult{TEvent}.AddError(TEvent)"/>
        public new FuncResult<TEvent, TValue> AddError(TEvent error)
        {
            base.AddError(error);
            return this;
        }

        /// <inheritdoc cref="ActionResult{TEvent}.AddInformational(TEvent)"/>
        public new FuncResult<TEvent, TValue> AddInformational(TEvent informational)
        {
            base.AddInformational(informational);
            return this;
        }

        /// <inheritdoc cref="ActionResult{TEvent}.AddResult(ITryResult{TEvent}, EventCategories)"/>
        public new FuncResult<TEvent, TValue> AddResult(
            ITryResult<TEvent> result,
            EventCategories categories = EventCategories.All)
        {
            base.AddResult(result, categories);
            return this;
        }

        /// <inheritdoc cref="ActionResult{TEvent}.AddResult{TValue}(IFuncResult{TEvent, TValue}, out TValue, EventCategories)"/>
        public new FuncResult<TEvent, TValue> AddResult<TOtherValue>(
            IFuncResult<TEvent, TOtherValue> result,
            out TOtherValue value,
            EventCategories categories = EventCategories.All)
        {
            base.AddResult(result, out value, categories);
            return this;
        }

        /// <inheritdoc/>
        public bool GetValue(out TValue value) =>
            this.value.GetValue(out value);

        /// <inheritdoc/>
        public bool RemoveValue() =>
            this.value.RemoveValue();

        /// <inheritdoc/>
        public bool RemoveValue(out TValue value) =>
            this.value.RemoveValue(out value);

        /// <inheritdoc/>
        public bool SetValue(TValue newValue) =>
            this.value.SetValue(newValue);

        /// <inheritdoc/>
        public bool SetValue(TValue newValue, out TValue oldValue) =>
            this.value.SetValue(newValue, out oldValue);

        /// <inheritdoc/>
        public bool TryAddValue(TValue value) =>
            this.value.TryAddValue(value);

        /// <inheritdoc/>
        public bool TryAddValue(TValue value, out TValue currentValue) =>
            this.value.TryAddValue(value, out currentValue);
    }
}
