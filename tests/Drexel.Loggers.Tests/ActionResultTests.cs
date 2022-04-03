using System;
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
        public void ActionResult_Implicit_Bool_Successful_Succeeds()
        {
            ActionResult result = new ActionResult();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ActionResult_Implicit_Bool_Unsuccessful_Succeeds()
        {
            ActionResult result = new ActionResult(isUnsuccessful: true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ActionResult_Not_Successful_Succeeds()
        {
            ActionResult result = new ActionResult();

            Assert.IsFalse(!result);
        }

        [TestMethod]
        public void ActionResult_Not_Unsuccessful_Succeeds()
        {
            ActionResult result = new ActionResult(isUnsuccessful: true);

            Assert.IsTrue(!result);
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ActionResult_AddError_Null_ThrowsArgumentNull()
        {
            ActionResult result = new ActionResult();
            result.AddError(null!);
        }

        [TestMethod]
        public void ActionResult_AddInformational_Succeeds()
        {
            ILogEvent @event = template.Create();

            ActionResult result = new ActionResult();

            Assert.IsTrue(result.Success);
            Assert.AreSame(result, result.AddInformational(@event));
            Assert.IsTrue(result.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, result.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, result.Informationals.Select(x => x.Event));
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ActionResult_AddInformational_Null_ThrowsArgumentNull()
        {
            ActionResult result = new ActionResult();
            result.AddInformational(null!);
        }

        [TestMethod]
        public void ActionResult_AddResult_IActionResult_ImplicitlyUnsuccessfulResult_Succeeds()
        {
            ILogEvent @event = template.Create();

            ActionResult inner = new ActionResult();
            inner.AddError(@event);

            ActionResult outer = new ActionResult();

            Assert.IsTrue(outer.Success);
            Assert.AreSame(outer, outer.AddResult(inner));
            Assert.IsFalse(outer.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, outer.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, outer.Errors.Select(x => x.Event));
            Assert.AreEqual(0, outer.Informationals.Count);
        }

        [TestMethod]
        public void ActionResult_AddResult_IActionResult_ExplicitlyUnsuccessfulResult_Succeeds()
        {
            ActionResult inner = new ActionResult(true);

            ActionResult outer = new ActionResult();

            Assert.IsTrue(outer.Success);
            Assert.AreSame(outer, outer.AddResult(inner));
            Assert.IsFalse(outer.Success);

            Assert.AreEqual(0, outer.AllEvents.Count);
            Assert.AreEqual(0, outer.Errors.Count);
            Assert.AreEqual(0, outer.Informationals.Count);
        }

        [TestMethod]
        public void ActionResult_AddResult_IActionResult_SuccessfulResult_Succeeds()
        {
            ILogEvent @event = template.Create();

            ActionResult inner = new ActionResult();
            inner.AddInformational(@event);

            ActionResult outer = new ActionResult();

            Assert.IsTrue(outer.Success);
            Assert.AreSame(outer, outer.AddResult(inner));
            Assert.IsTrue(outer.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, outer.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, outer.Informationals.Select(x => x.Event));
            Assert.AreEqual(0, outer.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ActionResult_AddResult_IActionResult_Null_ThrowsArgumentNull()
        {
            ActionResult result = new ActionResult();
            result.AddResult(null!);
        }
    }
}
