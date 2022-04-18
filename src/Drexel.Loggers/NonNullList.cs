using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static System.FormattableString;

namespace Drexel.Loggers
{
    /// <summary>
    /// Represents a list where all items are guaranteed to be non-<see langword="null"/>.
    /// </summary>
    /// <typeparam name="TItem">
    /// The type of item contained by this list.
    /// </typeparam>
    /// <typeparam name="TDerived">
    /// The derived type.
    /// </typeparam>
    [DebuggerDisplay("Count = {Count,nq}")]
    public abstract class NonNullList<TItem, TDerived> : IReadOnlyList<TItem>
        where TDerived : NonNullList<TItem, TDerived>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        private protected NonNullList()
        {
            this.Items = new List<TItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The initial capacity of the collection.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="capacity"/> is less than zero.
        /// </exception>
        private protected NonNullList(int capacity)
        {
            this.Items = new List<TItem>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        /// <param name="itemParams">
        /// The items that this list should contain.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="itemParams"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when a value contained by <paramref name="itemParams"/> is <see langword="null"/>.
        /// </exception>
        private protected NonNullList(params TItem[] itemParams)
            : this(itemParams, itemParams?.Length ?? 0, nameof(itemParams))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        /// <param name="collection">
        /// The items that this list should contain.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="collection"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when a value contained by <paramref name="collection"/> is <see langword="null"/>.
        /// </exception>
        private protected NonNullList(IReadOnlyCollection<TItem> collection)
            : this(collection, collection?.Count ?? 0, nameof(collection))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        /// <param name="enumerable">
        /// The items that this list should contain.
        /// </param>
        /// <param name="capacity">
        /// The initial capacity of the collection.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="enumerable"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when a value contained by <paramref name="enumerable"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="capacity"/> is less than zero.
        /// </exception>
        private protected NonNullList(IEnumerable<TItem> enumerable, int capacity = 0)
            : this(enumerable, capacity, nameof(enumerable))
        {
        }

        private NonNullList(IEnumerable<TItem> enumerable, int capacity, string enumerableParamName)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(enumerableParamName);
            }

            this.Items = new List<TItem>(capacity);
            using (IEnumerator<TItem> enumerator = enumerable.GetEnumerator())
            {
                for (int counter = 0; enumerator.MoveNext(); counter++)
                {
                    if (enumerator.Current is null)
                    {
                        throw new ArgumentException(
                            Invariant($"Item at index {counter} is null."),
                            enumerableParamName);
                    }

                    this.Items.Add(enumerator.Current);
                }
            }
        }

        /// <inheritdoc/>
        public TItem this[int index] => this.Items[index];

        /// <inheritdoc/>
        public int Count => this.Items.Count;

        /// <summary>
        /// Gets this instance as the derived type.
        /// </summary>
        protected abstract TDerived AsDerived { get; }

        /// <summary>
        /// Gets a mutable list of the items contained by this instance.
        /// </summary>
        protected List<TItem> Items { get; }

        /// <summary>
        /// Adds the specified item to this collection.
        /// </summary>
        /// <param name="item">
        /// The item to add.
        /// </param>
        /// <returns>
        /// This instance.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="item"/> is <see langword="null"/>.
        /// </exception>
        public TDerived Add(TItem item)
        {
            this.Items.Add(item ?? throw new ArgumentNullException(nameof(item)));
            return this.AsDerived;
        }

        /// <inheritdoc/>
        public IEnumerator<TItem> GetEnumerator() => this.Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
