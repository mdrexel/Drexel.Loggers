namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Represents a read-only value container.
    /// </summary>
    /// <typeparam name="T">
    /// The type of value contained by the container.
    /// </typeparam>
    public interface IReadOnlyValueContainer<out T>
    {
        /// <summary>
        /// Gets a value indicating whether this instance has a value.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if this container has a value; otherwise, <see langword="false"/>.
        /// </value>
        bool HasValue { get; }

        /// <summary>
        /// Gets the value contained by this instance, if one exists. Otherwise, undefined.
        /// </summary>
        T Value { get; }
    }
}
