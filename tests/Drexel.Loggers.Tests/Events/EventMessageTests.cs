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
    }
}
