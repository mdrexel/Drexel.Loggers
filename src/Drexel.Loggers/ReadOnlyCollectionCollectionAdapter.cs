using System;
using System.Collections;
using System.Collections.Generic;

namespace Drexel.Loggers
{
    internal sealed class ReadOnlyCollectionCollectionAdapter<T> : IReadOnlyCollection<T>
    {
        private readonly ICollection<T> collection;

        public ReadOnlyCollectionCollectionAdapter(ICollection<T> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public int Count => this.collection.Count;

        public IEnumerator<T> GetEnumerator() => this.collection.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.collection).GetEnumerator();
    }
}
