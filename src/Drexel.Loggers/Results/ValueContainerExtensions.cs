using System;

namespace Drexel.Loggers.Results
{
    /// <summary>
    /// Extension methods for value containers.
    /// </summary>
    public static class ValueContainerExtensions
    {
        /// <summary>
        /// Adds the specified value to this container, throwing an exception if this container already has a value.
        /// </summary>
        /// <typeparam name="T">
        /// The type of value contained by the container.
        /// </typeparam>
        /// <param name="container">
        /// The container to add the value to.
        /// </param>
        /// <param name="value">
        /// The value to add.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="container"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when this container already has a value.
        /// </exception>
        public static void AddValue<T>(this IValueContainer<T> container, T value)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (!container.TryAddValue(value))
            {
                throw new InvalidOperationException("The specified value container already contains a value.");
            }
        }

        /// <summary>
        /// Gets the value contained by <paramref name="container"/>, if one exists.
        /// </summary>
        /// <typeparam name="T">
        /// The type of value contained by the container.
        /// </typeparam>
        /// <param name="container">
        /// The container to get the value from.
        /// </param>
        /// <param name="value">
        /// When this method returns, if <paramref name="container"/> contains a value, set to the value contained by
        /// <paramref name="container"/>. Otherwise, undefined.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if a value was contained by <paramref name="container"/>; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="container"/> is <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This method is not thread-safe. If a value is added to <paramref name="container"/> while this method is
        /// executing, this method will incorrectly report that <paramref name="container"/> does not contain a value.
        /// Similarly, if a value is removed from <paramref name="container"/> while this method is executing, this
        /// method will illegally access the <see cref="IReadOnlyValueContainer{T}.Value"/> property, causing undefined
        /// behavior. If thread safety is important to you, use the
        /// <see cref="IInvariantReadOnlyValueContainer{T}.GetValue(out T)"/> method with a thread-safe implementation
        /// instead if possible.
        /// </remarks>
        public static bool GetValue<T>(this IReadOnlyValueContainer<T> container, out T value)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            if (container.HasValue)
            {
                value = container.Value;

                // Re-retrieve the HasValue property 
                return container.HasValue;
            }
            else
            {
                value = default!;
                return false;
            }
        }
    }
}
