namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents a mutable value container.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value contained by the container.
    /// </typeparam>
    public interface IValueContainer<T> : IInvariantReadOnlyValueContainer<T>
    {
        /// <summary>
        /// Removes the value contained by this instance, if one exists.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if a value was removed from this instance; otherwise, <see langword="false"/>.
        /// </returns>
        bool RemoveValue();

        /// <summary>
        /// Removes the value contained by this instance, if one exists.
        /// </summary>
        /// <param name="value">
        /// When this method returns, if this instance contained a value, set to the value previously contained by
        /// this instance. Otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was removed from this instance; otherwise, <see langword="false"/>.
        /// </returns>
        bool RemoveValue([System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out T value);

        /// <summary>
        /// Sets the value of this instance to the specified value.
        /// </summary>
        /// <param name="newValue">
        /// The value this instance should contain.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was replaced by this operation; otherwise, <see langword="false"/>.
        /// </returns>
        bool SetValue(T newValue);

        /// <summary>
        /// Sets the value of this instance to the specified value.
        /// </summary>
        /// <param name="newValue">
        /// The value this instance should contain.
        /// </param>
        /// <param name="oldValue">
        /// When this method returns, if this instance contains a value, set to the value contained by this instance.
        /// Otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was replaced by this operation; otherwise, <see langword="false"/>.
        /// </returns>
        bool SetValue(T newValue, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(false)] out T oldValue);

        /// <summary>
        /// Tries to add the specified value to this instance, doing nothing if a value already exists.
        /// </summary>
        /// <param name="value">
        /// The value to try to add.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the value was added; otherwise, <see langword="false"/>.
        /// </returns>
        bool TryAddValue(T value);

        /// <summary>
        /// Tries to add the specified value to this instance, doing nothing if a value already exists.
        /// </summary>
        /// <param name="value">
        /// The value to try to add.
        /// </param>
        /// <param name="currentValue">
        /// When this method returns, if this instance contains a value, set to the value contained by this instance.
        /// Otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the value was added; otherwise, <see langword="false"/>.
        /// </returns>
        bool TryAddValue(T value, [System.Diagnostics.CodeAnalysis.MaybeNullWhen(true)] out T currentValue);
    }
}
