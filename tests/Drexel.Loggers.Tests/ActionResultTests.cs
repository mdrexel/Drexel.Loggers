using Drexel.Loggers.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public sealed class ActionResultTests
    {
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
        public void ActionResult_Foo()
        {
            ActionResult result = new ActionResult();
        }
    }
}
