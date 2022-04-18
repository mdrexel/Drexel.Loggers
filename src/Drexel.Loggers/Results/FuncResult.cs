using System;
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
    /// When an instance of <see cref="FuncResult{T}"/> is initialized, the value contained by this result will be set
    /// to <see langword="default"/>, which is <see langword="null"/> for any <typeparamref name="T"/> that is a
    /// <see langword="class"/>. Because <typeparamref name="T"/> could be a <see langword="struct"/>, the
    /// interface must declare the value to be non-nullable. This means that, if you do not specify
    /// <typeparamref name="T"/> to be nullable, you must make sure you always populate this property before
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

        public static implicit operator bool(FuncResult<TValue> result) => result.Success;

        public static bool operator !(FuncResult<TValue> result) => !result.Success;

        public bool HasValue => this.value.HasValue;

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

        /// <inheritdoc cref="ActionResult.AddResult(IActionResult)"/>
        public new FuncResult<TValue> AddResult(IActionResult result)
        {
            base.AddResult(result);
            return this;
        }

        /// <inheritdoc cref="ActionResult.AddResult{T}(IFuncResult{T}, out T)"/>
        /// <remarks>
        /// Note that <paramref name="value"/> may be <see langword="null"/> if <paramref name="result"/> does not
        /// contain a value. Because the <see cref="IFuncResult{T}"/> interface declares the value of the
        /// <see cref="IFuncResult{T}.Value"/> property to be undefined when <see cref="IFuncResult{T}.HasValue"/> is
        /// <see langword="false"/>, accessing <see cref="IFuncResult{T}.Value"/> could do something unexpected, like
        /// throw an exception. To avoid unexpected exceptions, a default value is used instead. If you're using the
        /// C# nullable reference feature, make sure <typeparamref name="T"/> is declared correctly to avoid
        /// <see langword="null"/> escaping.
        /// </remarks>
        public new FuncResult<TValue> AddResult<T>(IFuncResult<T> result, out T value)
        {
            base.AddResult(result, out value);
            return this;
        }

        public bool GetValue(out TValue value) =>
            this.value.GetValue(out value);

        public bool RemoveValue() =>
            this.value.RemoveValue();

        public bool RemoveValue(out TValue value) =>
            this.value.RemoveValue(out value);

        public bool SetValue(TValue newValue) =>
            this.value.SetValue(newValue);

        public bool SetValue(TValue newValue, out TValue oldValue) =>
            this.value.SetValue(newValue, out oldValue);

        public bool TryAddValue(TValue value) =>
            this.value.TryAddValue(value);

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
    /// <typeparamref name="T"/> that is a <see langword="class"/>. Because <typeparamref name="T"/> could be a
    /// <see langword="struct"/>, the interface must declare the value to be non-nullable. This means that, if you do
    /// not specify <typeparamref name="T"/> to be nullable, you must make sure you always populate this property
    /// before returning the result object, or else you may unexpectedly return a value of <see langword="null"/>.
    /// </remarks>
    public sealed class FuncResult<TEvent, TValue> :
        ActionResult<TEvent>,
        IValueResult<TEvent, TValue>,
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

        public static implicit operator bool(FuncResult<TEvent, TValue> result) => result.Success;

        public static bool operator !(FuncResult<TEvent, TValue> result) => !result.Success;

        public bool HasValue => this.value.HasValue;

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

        /// <inheritdoc cref="ActionResult{TEvent}.AddResult(ITryResult{TEvent})"/>
        public new FuncResult<TEvent, TValue> AddResult(ITryResult<TEvent> result)
        {
            base.AddResult(result);
            return this;
        }

        /// <inheritdoc cref="ActionResult{TEvent}.AddResult{TValue}(IValueResult{TEvent, TValue}, out TValue)"/>
        public new FuncResult<TEvent, TValue> AddResult<TOtherValue>(
            IValueResult<TEvent, TOtherValue> result,
            out TOtherValue value)
        {
            base.AddResult(result, out value);
            return this;
        }

        public bool GetValue(out TValue value) =>
            this.value.GetValue(out value);

        public bool RemoveValue() =>
            this.value.RemoveValue();

        public bool RemoveValue(out TValue value) =>
            this.value.RemoveValue(out value);

        public bool SetValue(TValue newValue) =>
            this.value.SetValue(newValue);

        public bool SetValue(TValue newValue, out TValue oldValue) =>
            this.value.SetValue(newValue, out oldValue);

        public bool TryAddValue(TValue value) =>
            this.value.TryAddValue(value);

        public bool TryAddValue(TValue value, out TValue currentValue) =>
            this.value.TryAddValue(value, out currentValue);
    }
}
