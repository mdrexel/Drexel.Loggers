using System;
using System.Collections;
using System.Collections.Generic;
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
    public abstract class NonNullList<TItem, TDerived> : IReadOnlyList<TItem>
        where TDerived : NonNullList<TItem, TDerived>
    {
        private readonly List<TItem> items;

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        private protected NonNullList()
        {
            this.items = new List<TItem>();
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
            this.items = new List<TItem>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        /// <param name="items">
        /// The items that this list should contain.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="items"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when a value contained by <paramref name="items"/> is <see langword="null"/>.
        /// </exception>
        private protected NonNullList(IReadOnlyCollection<TItem> items)
            : this(items, items?.Count ?? 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NonNullList{TItem, TDerived}"/> class.
        /// </summary>
        /// <param name="items">
        /// The items that this list should contain.
        /// </param>
        /// <param name="capacity">
        /// The initial capacity of the collection.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="items"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when a value contained by <paramref name="items"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="capacity"/> is less than zero.
        /// </exception>
        private protected NonNullList(IEnumerable<TItem> items, int capacity = 0)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = new List<TItem>(capacity);
            using (IEnumerator<TItem> enumerator = items.GetEnumerator())
            {
                for (int counter = 0; enumerator.MoveNext(); counter++)
                {
                    if (enumerator.Current is null)
                    {
                        throw new ArgumentException(Invariant($"Item at index {counter} is null."), nameof(items));
                    }

                    this.items.Add(enumerator.Current);
                }
            }
        }

        public TItem this[int index] => this.items[index];

        public int Count => this.items.Count;

        protected abstract TDerived AsDerived { get; }

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
            this.items.Add(item ?? throw new ArgumentNullException(nameof(item)));
            return this.AsDerived;
        }

        public IEnumerator<TItem> GetEnumerator() => this.items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
