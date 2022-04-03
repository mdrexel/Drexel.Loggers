using System.Linq;
using Drexel.Loggers.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public sealed class TryResultTests
    {
        [TestMethod]
        public void TryResult_Ctor_Succeeds()
        {
            TryResult result = new TryResult();

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void TryResult_Ctor_ExplicitlyUnsuccessful_Succeeds()
        {
            TryResult result = new TryResult(isUnsuccessful: true);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void TryResult_Foo()
        {
            // TODO: I don't like how it works right now
            // 1. Why does `TryResult<TEvent>` not inherit from `TryResult`? If I want to write code that accepts a
            //    mutable result and adds events to it, what am I supposed to do?
            // 2. Why does `ValueResult` not inherit from `TryResult`?
            // 3. You're telling me I gotta write tests for four classes that are 99% the same?
            TryResult result = new TryResult();
        }
    }
}
