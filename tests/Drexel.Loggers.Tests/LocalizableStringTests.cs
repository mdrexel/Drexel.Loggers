using System;
using System.Collections.Generic;
using System.Globalization;
using Drexel.Loggers.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public sealed class LocalizableStringTests
    {
        private static readonly CultureInfo French = CultureInfo.GetCultureInfoByIetfLanguageTag("fr");
        private static readonly CultureInfo German = CultureInfo.GetCultureInfoByIetfLanguageTag("de");
        private static readonly CultureInfo Japanese = CultureInfo.GetCultureInfoByIetfLanguageTag("jp");

        public static IEnumerable<object?[]> EqualityOperatorCases
        {
            get
            {
                yield return new object?[] { null, null };
                foreach (object?[] @case in EqualsAreEqualCases)
                {
                    yield return @case;
                }
            }
        }

        public static IEnumerable<object?[]> InequalityOperatorCases
        {
            get
            {
                yield return new object?[] { null, new LocalizableStringImpl("foo") };
                foreach (object?[] @case in EqualsAreNotEqualCases)
                {
                    yield return @case;
                }
            }
        }
        public static IEnumerable<object?[]> EqualsAreEqualCases
        {
            get
            {
                yield return new object?[] { new LocalizableStringImpl("foo"), new LocalizableStringImpl("foo") };
                yield return new object?[]
                {
                    new LocalizableStringImpl(
                        new Dictionary<CultureInfo, string>()
                        {
                            [French] = "Foo",
                        },
                        French),
                    new LocalizableStringImpl(
                        new Dictionary<CultureInfo, string>()
                        {
                            [Japanese] = "Foo",
                        },
                        Japanese),
                };
            }
        }

        public static IEnumerable<object?[]> EqualsAreNotEqualCases
        {
            get
            {
                yield return new object?[] { new LocalizableStringImpl("foo"), null };
                yield return new object?[] { new LocalizableStringImpl("foo"), new LocalizableStringImpl("bar") };
                yield return new object?[]
                {
                    new LocalizableStringImpl(
                        new Dictionary<CultureInfo, string>()
                        {
                            [French] = "Foo",
                        },
                        French),
                    new LocalizableStringImpl(
                        new Dictionary<CultureInfo, string>()
                        {
                            [Japanese] = "Bar",
                        },
                        Japanese),
                };
            }
        }

        [TestMethod]
        public void LocalizableString_Ctor_Invariant_Succeeds()
        {
            const string expectedMessage = "Hello";

            LocalizableStringImpl actual = new LocalizableStringImpl(expectedMessage);

            Assert.AreEqual(expectedMessage, actual.Localizations[CultureInfo.InvariantCulture]);
        }

        [TestMethod]
        public void LocalizableString_Ctor_Invariant_Null_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new LocalizableStringImpl(invariant: null!));
            Assert.AreEqual("invariant", exception.ParamName);
        }

        [TestMethod]
        public void LocalizableString_Ctor_Preferred_Succeeds()
        {
            const string expectedMessage = "Bonjour";
            CultureInfo expectedCulture = French;

            LocalizableStringImpl actual = new LocalizableStringImpl(expectedMessage, expectedCulture);

            Assert.AreEqual(expectedMessage, actual.Localizations[expectedCulture]);
        }

        [TestMethod]
        public void LocalizableString_Ctor_Preferred_NullMessage_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new LocalizableStringImpl(localization: null!, German));
            Assert.AreEqual("localization", exception.ParamName);
        }

        [TestMethod]
        public void LocalizableString_Ctor_Preferred_NullCulture_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new LocalizableStringImpl(localization: "Hello", culture: null!));
            Assert.AreEqual("culture", exception.ParamName);
        }

        [TestMethod]
        public void LocalizableString_Ctor_Localizations_Succeeds()
        {
            Dictionary<CultureInfo, string> expectedMessages =
                new Dictionary<CultureInfo, string>()
                {
                    [French] = "Bonjour",
                    [German] = "Hallo",
                    [Japanese] = "こんにちは",
                };

            LocalizableStringImpl actual = new LocalizableStringImpl(expectedMessages, French);

            Assert.That.Equivalent(expectedMessages, actual.Localizations);
        }

        [TestMethod]
        public void LocalizableString_Ctor_Localizations_Null_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new LocalizableStringImpl(localizations: null!, preferredCulture: null!));
            Assert.AreEqual("localizations", exception.ParamName);
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualityOperatorCases))]
        public void LocalizableString_EqualityOperator_AreEqual_Succeeds(
            LocalizableStringImpl? left,
            LocalizableStringImpl? right)
        {
            Assert.IsTrue(left == right);
        }

        [DataTestMethod]
        [DynamicData(nameof(InequalityOperatorCases))]
        public void LocalizableString_EqualityOperator_AreNotEqual_Succeeds(
            LocalizableStringImpl? left,
            LocalizableStringImpl? right)
        {
            Assert.IsFalse(left == right);
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualityOperatorCases))]
        public void LocalizableString_InequalityOperator_AreEqual_Succeeds(
            LocalizableStringImpl? left,
            LocalizableStringImpl? right)
        {
            Assert.IsFalse(left != right);
        }

        [DataTestMethod]
        [DynamicData(nameof(InequalityOperatorCases))]
        public void LocalizableString_InequalityOperator_AreNotEqual_Succeeds(
            LocalizableStringImpl? left,
            LocalizableStringImpl? right)
        {
            Assert.IsTrue(left!= right);
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreEqualCases))]
        public void LocalizableString_Equals_Object_AreEqual_Succeeds(
            LocalizableStringImpl left,
            object? right)
        {
            Assert.IsTrue(left.Equals(right));
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreNotEqualCases))]
        public void LocalizableString_Equals_Object_AreNotEqual_Succeeds(
            LocalizableStringImpl left,
            object? right)
        {
            Assert.IsFalse(left.Equals(right));
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreEqualCases))]
        public void LocalizableString_Equals_LocalizableString_AreEqual_Succeeds(
            LocalizableStringImpl left,
            LocalizableStringImpl? right)
        {
            Assert.IsTrue(left.Equals(right));
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreNotEqualCases))]
        public void LocalizableString_Equals_LocalizableString_AreNotEqual_Succeeds(
            LocalizableStringImpl left,
            LocalizableStringImpl? right)
        {
            Assert.IsFalse(left.Equals(right));
        }

        [TestMethod]
        public void LocalizableString_Equals_Culture_NullOther_Succeeds()
        {
            LocalizableStringImpl left = new LocalizableStringImpl("foo");

            Assert.IsFalse(left.Equals(null, null));
        }

        [TestMethod]
        public void LocalizableString_Equals_Culture_NullCulture_Succeeds()
        {
            LocalizableStringImpl left = new LocalizableStringImpl("Foo", French);
            LocalizableStringImpl right = new LocalizableStringImpl("Foo", German);

            Assert.IsTrue(left.Equals(right, null));
        }

        [TestMethod]
        public void LocalizableString_Equals_Culture_Fallback_Succeeds()
        {
            LocalizableStringImpl left = new LocalizableStringImpl("Foo", French);
            LocalizableStringImpl right = new LocalizableStringImpl("Foo", German);

            Assert.IsTrue(left.Equals(right, Japanese));
        }

        [TestMethod]
        public void LocalizableString_Equals_Culture_AreEqual_Succeeds()
        {
            LocalizableStringImpl left =
                new LocalizableStringImpl(
                    new Dictionary<CultureInfo, string>()
                    {
                        [French] = "foo",
                        [German] = "bar",
                    },
                    French);
            LocalizableStringImpl right =
                new LocalizableStringImpl(
                    new Dictionary<CultureInfo, string>()
                    {
                        [French] = "foo",
                        [German] = "bar",
                    },
                    French);

            Assert.IsTrue(left.Equals(right, German));
        }

        [TestMethod]
        public void LocalizableString_GetHashCode_Succeeds()
        {
            const string message = "Foo";

            LocalizableStringImpl eventMessage = new LocalizableStringImpl(message);

            Assert.AreEqual(message.GetHashCode(), eventMessage.GetHashCode());
        }

        [TestMethod]
        public void LocalizableString_GetHashCode_NoLocalizations_Succeeds()
        {
            LocalizableStringImpl eventMessage = new LocalizableStringImpl(
                new Dictionary<CultureInfo, string>(),
                CultureInfo.InvariantCulture);

            Assert.AreEqual(0, eventMessage.GetHashCode());
        }

        [DataTestMethod]
        [DataRow(nameof(EqualsAreEqualCases))]
        public void LocalizableString_GetHashCode_FollowsEqualityRules(
            LocalizableStringImpl left,
            LocalizableStringImpl right)
        {
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}
