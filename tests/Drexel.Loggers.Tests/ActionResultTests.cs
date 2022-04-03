using System.Linq;
using Drexel.Loggers.Events;
using Drexel.Loggers.Results;
using Drexel.Loggers.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public sealed class ActionResultTests
    {
        private static readonly LogEventTemplate template =
            new LogEventTemplate(
                new EventCode(
                    new EventCodeGroup(ushort.MaxValue, "Test Group"),
                    ushort.MaxValue,
                    "Test Event"),
                "Test message"!,
                "Test reason",
                new EventSuggestions()
                {
                    "Test suggestion 1"!,
                    "Test suggestion 2"!,
                    "Test suggestion 3"!,
                });

        [TestMethod]
        public void ActionResult_Ctor_Succeeds()
        {
            ActionResult result = new ActionResult();

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ActionResult_Ctor_ExplicitlyUnsuccessful_Succeeds()
        {
            ActionResult result = new ActionResult(isUnsuccessful: true);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ActionResult_AddError_Succeeds()
        {
            ILogEvent @event = template.Create();

            ActionResult result = new ActionResult();

            Assert.IsTrue(result.Success);
            Assert.AreSame(result, result.AddError(@event));
            Assert.IsFalse(result.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, result.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, result.Errors.Select(x => x.Event));
            Assert.AreEqual(0, result.Informationals.Count);
        }
    }
}
