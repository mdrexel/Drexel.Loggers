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
        [TestMethod]
        public void EventMessage_Ctor_Invariant_Succeeds()
        {
            const string expectedMessage = "This is a test message.";

            EventMessage actual = new EventMessage(expectedMessage);

            Assert.AreEqual(expectedMessage, actual.Localizations[CultureInfo.InvariantCulture]);
        }

        [TestMethod]
        public void EventMessage_Ctor_Invariant_Null_ThrowsArgumentNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new EventMessage(invariant: null!));
        }

        [TestMethod]
        public void EventMessage_Ctor_Localizations_Succeeds()
        {
            Dictionary<CultureInfo, string> expectedMessages =
                new Dictionary<CultureInfo, string>()
                {
                    [CultureInfo.GetCultureInfoByIetfLanguageTag("de")] = "Hallo",
                    [CultureInfo.GetCultureInfoByIetfLanguageTag("fr")] = "Bonjour",
                    [CultureInfo.GetCultureInfoByIetfLanguageTag("jp")] = "こんにちは",
                };

            EventMessage actual = new EventMessage(expectedMessages);

            Assert.That.Equivalent(expectedMessages, actual.Localizations);
        }

        [TestMethod]
        public void EventMessage_Ctor_Localizations_Null_ThrowsArgumentNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new EventMessage(localizations: null!));
        }
    }
}
