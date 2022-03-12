using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.FormattableString;

namespace Drexel.Loggers.Tests
{
    internal static class AssertExtensions
    {
        /// <summary>
        /// When <paramref name="expected"/> is <see langword="null"/>, tests that <paramref name="actual"/> is
        /// <see langword="null"/>. Otherwise, enumerates <paramref name="expected"/> and <paramref name="actual"/>,
        /// and tests that each pair of items is equal according to <paramref name="comparer"/>; then, tests that
        /// <paramref name="actual"/> contains no additional items.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item contained by <paramref name="expected"/> and <paramref name="actual"/>.
        /// </typeparam>
        /// <param name="assert">
        /// Not used; required for extension method syntax.
        /// </param>
        /// <param name="expected">
        /// The expected enumerable.
        /// </param>
        /// <param name="actual">
        /// The actual enumerable.
        /// </param>
        /// <param name="comparer">
        /// When non-<see langword="null"/>, the comparer to use when comparing items. Otherwise, the default comparer
        /// will be used.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown when an assert performed by this method fails.
        /// </exception>
        public static void Equal<T>(
            this Assert assert,
            IEnumerable<T> expected,
            IEnumerable<T> actual,
            IEqualityComparer<T>? comparer = null)
        {
            if (expected is null)
            {
                Assert.IsNull(actual);
                return;
            }
            else
            {
                Assert.IsNotNull(actual);
            }

            comparer ??= EqualityComparer<T>.Default;

            using (IEnumerator<T> expectedEnumerator = expected.GetEnumerator())
            using (IEnumerator<T> actualEnumerator = actual.GetEnumerator())
            {
                int index;
                for (index = 0; expectedEnumerator.MoveNext(); index++)
                {
                    Assert.IsTrue(
                        actualEnumerator.MoveNext(),
                        Invariant($"Actual enumerator contained fewer items than expected enumerator. Length of actual: {index}"));
                    Assert.IsTrue(
                        comparer.Equals(expectedEnumerator.Current, actualEnumerator.Current),
                        Invariant($"Item at index {0} was not equal. Expected: {expectedEnumerator.Current}, Actual: {actualEnumerator.Current}"));
                }

                Assert.IsFalse(
                    actualEnumerator.MoveNext(),
                    Invariant($"Actual enumerator contained more items than expected enumerator. Length of expected: {index}, Last item in actual: {actualEnumerator.Current}"));
            }
        }

        /// <summary>
        /// When <paramref name="expected"/> is <see langword="null"/>, tests that <paramref name="actual"/> is
        /// <see langword="null"/>. Otherwise, enumerates <paramref name="expected"/> and <paramref name="actual"/>,
        /// and tests that all items in <paramref name="expected"/> are contained by <paramref name="actual"/>
        /// according to the equality implementation of <paramref name="comparer"/>, and that <paramref name="actual"/>
        /// contains no additional items.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item contained by <paramref name="expected"/> and <paramref name="actual"/>.
        /// </typeparam>
        /// <param name="assert">
        /// Not used; required for extension method syntax.
        /// </param>
        /// <param name="expected">
        /// The expected enumerable.
        /// </param>
        /// <param name="actual">
        /// The actual enumerable.
        /// </param>
        /// <param name="comparer">
        /// When non-<see langword="null"/>, the comparer to use when comparing items. Otherwise, the default comparer
        /// will be used.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown when an assert performed by this method fails.
        /// </exception>
        public static void Equivalent<T>(
            this Assert assert,
            IEnumerable<T> expected,
            IEnumerable<T> actual,
            IEqualityComparer<T>? comparer = null)
        {
            if (expected is null)
            {
                Assert.IsNull(actual);
                return;
            }
            else
            {
                Assert.IsNotNull(actual);
            }

            comparer ??= EqualityComparer<T>.Default;

            IReadOnlyList<T> expectedList = expected.ToList();
            IReadOnlyList<T> actualList = actual.ToList();

            Assert.AreEqual(
                expectedList.Count,
                actualList.Count,
                "Expected and actual enumerables are of differing lengths.");

            BitArray alive = new BitArray(expectedList.Count, true);
            for (int expectedIndex = 0; expectedIndex < expectedList.Count; expectedIndex++)
            {
                T expectedItem = expectedList[expectedIndex];
                bool found = false;
                for (int actualIndex = 0; !found && actualIndex < actualList.Count; actualIndex++)
                {
                    if (alive[actualIndex])
                    {
                        T actualItem = actualList[actualIndex];
                        if (comparer.Equals(expectedItem, actualItem))
                        {
                            alive[actualIndex] = false;
                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    Assert.Fail(Invariant($"Expected item at index {expectedIndex} was not contained by actual enumerable. Expected item: {expectedItem}"));
                }
            }

            using (IEnumerator<T> expectedEnumerator = expected.GetEnumerator())
            using (IEnumerator<T> actualEnumerator = actual.GetEnumerator())
            {
                int index;
                for (index = 0; expectedEnumerator.MoveNext(); index++)
                {
                    Assert.IsTrue(
                        actualEnumerator.MoveNext(),
                        Invariant($"Actual enumerator contained fewer items than expected enumerator. Length of actual: {index}"));
                    Assert.IsTrue(
                        comparer.Equals(expectedEnumerator.Current, actualEnumerator.Current),
                        Invariant($"Item at index {0} was not equal. Expected: {expectedEnumerator.Current}, Actual: {actualEnumerator.Current}"));
                }

                Assert.IsFalse(
                    actualEnumerator.MoveNext(),
                    Invariant($"Actual enumerator contained more items than expected enumerator. Length of expected: {index}, Last item in actual: {actualEnumerator.Current}"));
            }
        }
    }
}
