using System;
using System.Collections;
using System.Collections.Generic;

namespace Drexel.Loggers
{
    internal static class ReadOnlyDictionary
    {
        public static IReadOnlyDictionary<T, U> Create<T, U>(Dictionary<T, U> underlying) =>
            new ReadOnlyDictionary1<T, U>(underlying);

        public static IReadOnlyDictionary<T, U> Create<T, U>(IReadOnlyDictionary<T, U> underlying) =>
            new ReadOnlyDictionary1<T, U>(underlying);

        public static IReadOnlyDictionary<T, U> Create<T, U>(IDictionary<T, U> underlying) =>
            new ReadOnlyDictionary2<T, U>(underlying);

        private sealed class ReadOnlyDictionary1<T, U> : IReadOnlyDictionary<T, U>
        {
            private readonly IReadOnlyDictionary<T, U> dictionary;

            public ReadOnlyDictionary1(IReadOnlyDictionary<T, U> dictionary)
            {
                this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            }

            public U this[T key] => this.dictionary[key];

            public IEnumerable<T> Keys => this.dictionary.Keys;

            public IEnumerable<U> Values => this.dictionary.Values;

            public int Count => this.dictionary.Count;

            public bool ContainsKey(T key) => this.dictionary.ContainsKey(key);

            public IEnumerator<KeyValuePair<T, U>> GetEnumerator() => this.dictionary.GetEnumerator();

            public bool TryGetValue(T key, out U value) => this.dictionary.TryGetValue(key, out value);

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.dictionary).GetEnumerator();
        }

        private sealed class ReadOnlyDictionary2<T, U> : IReadOnlyDictionary<T, U>
        {
            private readonly IDictionary<T, U> dictionary;

            public ReadOnlyDictionary2(IDictionary<T, U> dictionary)
            {
                this.dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            }

            public U this[T key] => this.dictionary[key];

            public IEnumerable<T> Keys => this.dictionary.Keys;

            public IEnumerable<U> Values => this.dictionary.Values;

            public int Count => this.dictionary.Count;

            public bool ContainsKey(T key) => this.dictionary.ContainsKey(key);

            public IEnumerator<KeyValuePair<T, U>> GetEnumerator() => this.dictionary.GetEnumerator();

            public bool TryGetValue(T key, out U value) => this.dictionary.TryGetValue(key, out value);

            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.dictionary).GetEnumerator();
        }
    }
}
