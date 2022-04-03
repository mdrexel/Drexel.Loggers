using System;

namespace Drexel.Loggers.Results
{
    public static class ValueContainerExtensions
    {
        /// <summary>
        /// Adds the specified value to this container, throwing an exception if this container already has a value.
        /// </summary>
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

            if (!container.TryAddValue(value, out _))
            {
                throw new InvalidOperationException("The specified value container already contains a value.");
            }
        }

    }
}
