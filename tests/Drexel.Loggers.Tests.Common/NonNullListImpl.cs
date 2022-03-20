using System.Collections.Generic;

namespace Drexel.Loggers.Tests.Common
{
    /// <summary>
    /// An implementation of the <see cref="NonNullList{TItem, TDerived}"/> abstract class.
    /// </summary>
    public sealed class NonNullListImpl<T> : NonNullList<T, NonNullListImpl<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullListImpl{T}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public NonNullListImpl()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullListImpl{T}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public NonNullListImpl(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullListImpl{T}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public NonNullListImpl(params T[] itemParams)
            : base(itemParams)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullListImpl{T}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public NonNullListImpl(IReadOnlyCollection<T> collection)
            : base(collection)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullListImpl{T}"/> class.
        /// </summary>
        /// <inheritdoc/>
        public NonNullListImpl(IEnumerable<T> enumerable, int capacity = 0)
            : base(enumerable, capacity)
        {
        }

        /// <summary>
        /// Gets the capacity of the underlying list.
        /// </summary>
        public int Capacity => this.Items.Capacity;

        protected override NonNullListImpl<T> AsDerived => this;
    }
}
