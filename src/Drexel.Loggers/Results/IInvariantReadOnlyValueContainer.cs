namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents a read-only value container with an invariant type of value.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value contained by the container.
    /// </typeparam>
    /// <remarks>
    /// Because <see langword="out"/> parameters are actually <see langword="ref"/> under the hood, a method cannot
    /// have an <see langword="out"/> on <typeparamref name="T"/> unless <typeparamref name="T"/> is invariant.
    /// </remarks>
    public interface IInvariantReadOnlyValueContainer<T> : IReadOnlyValueContainer<T>
    {
        /// <summary>
        /// Gets the value contained by this instance, if one exists.
        /// </summary>
        /// <param name="value">
        /// When this method returns, if this instance contains a value, set to the value contained by this instance.
        /// Otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was contained by this instance; otherwise, <see langword="false"/>.
        /// </returns>
        bool GetValue(out T value);
    }
}
