using System;
using System.Collections.Generic;
using System.Globalization;
using Drexel.Loggers.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests.Events
{
    [TestClass]
    public sealed class EventMessageTests
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
                yield return new object?[] { null, new EventMessage("foo") };
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
                yield return new object?[] { new EventMessage("foo"), new EventMessage("foo") };
                yield return new object?[]
                {
                    new EventMessage(
                        new Dictionary<CultureInfo, string>()
                        {
                            [French] = "Foo",
                        },
                        French),
                    new EventMessage(
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
                yield return new object?[] { new EventMessage("foo"), null };
                yield return new object?[] { new EventMessage("foo"), new EventMessage("bar") };
                yield return new object?[]
                {
                    new EventMessage(
                        new Dictionary<CultureInfo, string>()
                        {
                            [French] = "Foo",
                        },
                        French),
                    new EventMessage(
                        new Dictionary<CultureInfo, string>()
                        {
                            [Japanese] = "Bar",
                        },
                        Japanese),
                };
            }
        }

        [TestMethod]
        public void EventMessage_Ctor_Invariant_Succeeds()
        {
            const string expectedMessage = "Hello";

            EventMessage actual = new EventMessage(expectedMessage);

            Assert.AreEqual(expectedMessage, actual.Localizations[CultureInfo.InvariantCulture]);
        }

        [TestMethod]
        public void EventMessage_Ctor_Invariant_Null_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new EventMessage(invariant: null!));
            Assert.AreEqual("invariant", exception.ParamName);
        }

        [TestMethod]
        public void EventMessage_Ctor_Preferred_Succeeds()
        {
            const string expectedMessage = "Bonjour";
            CultureInfo expectedCulture = French;

            EventMessage actual = new EventMessage(expectedMessage, expectedCulture);

            Assert.AreEqual(expectedMessage, actual.Localizations[expectedCulture]);
        }

        [TestMethod]
        public void EventMessage_Ctor_Preferred_NullMessage_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new EventMessage(message: null!, German));
            Assert.AreEqual("message", exception.ParamName);
        }

        [TestMethod]
        public void EventMessage_Ctor_Preferred_NullCulture_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new EventMessage(message: "Hello", culture: null!));
            Assert.AreEqual("culture", exception.ParamName);
        }

        [TestMethod]
        public void EventMessage_Ctor_Localizations_Succeeds()
        {
            Dictionary<CultureInfo, string> expectedMessages =
                new Dictionary<CultureInfo, string>()
                {
                    [French] = "Bonjour",
                    [German] = "Hallo",
                    [Japanese] = "こんにちは",
                };

            EventMessage actual = new EventMessage(expectedMessages);

            Assert.That.Equivalent(expectedMessages, actual.Localizations);
        }

        [TestMethod]
        public void EventMessage_Ctor_Localizations_Null_ThrowsArgumentNull()
        {
            ArgumentNullException exception =
                Assert.ThrowsException<ArgumentNullException>(
                    () => new EventMessage(localizations: null!));
            Assert.AreEqual("localizations", exception.ParamName);
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualityOperatorCases))]
        public void EventMessage_EqualityOperator_AreEqual_Succeeds(EventMessage? left, EventMessage? right)
        {
            Assert.IsTrue(left == right);
        }

        [DataTestMethod]
        [DynamicData(nameof(InequalityOperatorCases))]
        public void EventMEssage_EqualityOperator_AreNotEqual_Succeeds(EventMessage? left, EventMessage? right)
        {
            Assert.IsFalse(left == right);
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualityOperatorCases))]
        public void EventMessage_InequalityOperator_AreEqual_Succeeds(EventMessage? left, EventMessage? right)
        {
            Assert.IsFalse(left != right);
        }

        [DataTestMethod]
        [DynamicData(nameof(InequalityOperatorCases))]
        public void EventMessage_InequalityOperator_AreNotEqual_Succeeds(EventMessage? left, EventMessage? right)
        {
            Assert.IsTrue(left!= right);
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreEqualCases))]
        public void EventMessage_Equals_Object_AreEqual_Succeeds(EventMessage left, object? right)
        {
            Assert.IsTrue(left.Equals(right));
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreNotEqualCases))]
        public void EventMessage_Equals_Object_AreNotEqual_Succeeds(EventMessage left, object? right)
        {
            Assert.IsFalse(left.Equals(right));
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreEqualCases))]
        public void EventMessage_Equals_EventMessage_AreEqual_Succeeds(EventMessage left, EventMessage? right)
        {
            Assert.IsTrue(left.Equals(right));
        }

        [DataTestMethod]
        [DynamicData(nameof(EqualsAreNotEqualCases))]
        public void EventMessage_Equals_EventMessage_AreNotEqual_Succeeds(EventMessage left, EventMessage? right)
        {
            Assert.IsFalse(left.Equals(right));
        }

        [TestMethod]
        public void EventMessage_Equals_Culture_NullOther_Succeeds()
        {
            EventMessage left = new EventMessage("foo");

            Assert.IsFalse(left.Equals(null, null));
        }

        [TestMethod]
        public void EventMessage_Equals_Culture_NullCulture_Succeeds()
        {
            EventMessage left = new EventMessage("Foo", French);
            EventMessage right = new EventMessage("Foo", German);

            Assert.IsTrue(left.Equals(right, null));
        }

        [TestMethod]
        public void EventMEssage_Equals_Culture_Fallback_Succeeds()
        {
            EventMessage left = new EventMessage("Foo", French);
            EventMessage right = new EventMessage("Foo", German);

            Assert.IsTrue(left.Equals(right, Japanese));
        }

        [TestMethod]
        public void EventMessage_Equals_Culture_AreEqual_Succeeds()
        {
            EventMessage left =
                new EventMessage(
                    new Dictionary<CultureInfo, string>()
                    {
                        [French] = "foo",
                        [German] = "bar",
                    },
                    French);
            EventMessage right =
                new EventMessage(
                    new Dictionary<CultureInfo, string>()
                    {
                        [French] = "foo",
                        [German] = "bar",
                    },
                    French);

            Assert.IsTrue(left.Equals(right, German));
        }

        [TestMethod]
        public void EventMessage_GetHashCode_Succeeds()
        {
            const string message = "Foo";

            EventMessage eventMessage = new EventMessage(message);

            Assert.AreEqual(message.GetHashCode(), eventMessage.GetHashCode());
        }

        [TestMethod]
        public void EventMessage_GetHashCode_NoLocalizations_Succeeds()
        {
            EventMessage eventMessage = new EventMessage(new Dictionary<CultureInfo, string>());

            Assert.AreEqual(0, eventMessage.GetHashCode());
        }

        [DataTestMethod]
        [DataRow(nameof(EqualsAreEqualCases))]
        public void EventMessage_GetHashCode_FollowsEqualityRules(EventMessage left, EventMessage right)
        {
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}
