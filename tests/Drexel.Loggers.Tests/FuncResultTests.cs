using System;
using System.Linq;
using Drexel.Loggers.Events;
using Drexel.Loggers.Results;
using Drexel.Loggers.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public sealed class FuncResultTests
    {
        [TestMethod]
        public void FuncResult_Ctor_Succeeds()
        {
            FuncResult<string?> result = new FuncResult<string?>();

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void FuncResult_Ctor_ExplicitlyUnsuccessful_Succeeds()
        {
            FuncResult<string?> result = new FuncResult<string?>(isUnsuccessful: true);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void FuncResult_Implicit_Bool_Successful_Succeeds()
        {
            FuncResult<string?> result = new FuncResult<string?>();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FuncResult_Implicit_Bool_Unsuccessful_Succeeds()
        {
            FuncResult<string?> result = new FuncResult<string?>(isUnsuccessful: true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FuncResult_Not_Successful_Succeeds()
        {
            FuncResult<string?> result = new FuncResult<string?>();

            Assert.IsFalse(!result);
        }

        [TestMethod]
        public void FuncResult_Not_Unsuccessful_Succeeds()
        {
            FuncResult<string?> result = new FuncResult<string?>(isUnsuccessful: true);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        public void FuncResult_AddError_Succeeds()
        {
            ILogEvent @event = TestEvents.Template.Create();

            FuncResult<string?> result = new FuncResult<string?>();

            Assert.IsTrue(result.Success);
            Assert.AreSame(result, result.AddError(@event));
            Assert.IsFalse(result.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, result.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, result.Errors.Select(x => x.Event));
            Assert.AreEqual(0, result.Informationals.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FuncResult_AddError_Null_ThrowsArgumentNull()
        {
            FuncResult<string?> result = new FuncResult<string?>();
            result.AddError(null!);
        }

        [TestMethod]
        public void FuncResult_AddInformational_Succeeds()
        {
            ILogEvent @event = TestEvents.Template.Create();

            FuncResult<string?> result = new FuncResult<string?>();

            Assert.IsTrue(result.Success);
            Assert.AreSame(result, result.AddInformational(@event));
            Assert.IsTrue(result.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, result.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, result.Informationals.Select(x => x.Event));
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FuncResult_AddInformational_Null_ThrowsArgumentNull()
        {
            FuncResult<string?> result = new FuncResult<string?>();
            result.AddInformational(null!);
        }

        [TestMethod]
        public void FuncResult_AddResult_IActionResult_ImplicitlyUnsuccessfulResult_Succeeds()
        {
            ILogEvent @event = TestEvents.Template.Create();

            ActionResult inner = new ActionResult();
            inner.AddError(@event);

            FuncResult<string?> outer = new FuncResult<string?>();

            Assert.IsTrue(outer.Success);
            Assert.AreSame(outer, outer.AddResult(inner));
            Assert.IsFalse(outer.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, outer.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, outer.Errors.Select(x => x.Event));
            Assert.AreEqual(0, outer.Informationals.Count);
        }

        [TestMethod]
        public void FuncResult_AddResult_IActionResult_ExplicitlyUnsuccessfulResult_Succeeds()
        {
            ActionResult inner = new ActionResult(true);

            FuncResult<string?> outer = new FuncResult<string?>();

            Assert.IsTrue(outer.Success);
            Assert.AreSame(outer, outer.AddResult(inner));
            Assert.IsFalse(outer.Success);

            Assert.AreEqual(0, outer.AllEvents.Count);
            Assert.AreEqual(0, outer.Errors.Count);
            Assert.AreEqual(0, outer.Informationals.Count);
        }

        [TestMethod]
        public void FuncResult_AddResult_IActionResult_SuccessfulResult_Succeeds()
        {
            ILogEvent @event = TestEvents.Template.Create();

            ActionResult inner = new ActionResult();
            inner.AddInformational(@event);

            FuncResult<string?> outer = new FuncResult<string?>();

            Assert.IsTrue(outer.Success);
            Assert.AreSame(outer, outer.AddResult(inner));
            Assert.IsTrue(outer.Success);

            Assert.That.Equal(new ILogEvent[] { @event }, outer.AllEvents.Select(x => x.Event));
            Assert.That.Equal(new ILogEvent[] { @event }, outer.Informationals.Select(x => x.Event));
            Assert.AreEqual(0, outer.Errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FuncResult_AddResult_IFuncResult_Null_ThrowsArgumentNull()
        {
            FuncResult<string?> result = new FuncResult<string?>();
            result.AddResult(null!);
        }
    }
}
