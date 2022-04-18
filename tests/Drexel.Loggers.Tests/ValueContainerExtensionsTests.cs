using System;
using Drexel.Loggers.Results;
using Drexel.Loggers.Tests.Common.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public class ValueContainerExtensionsTests
    {
        [TestMethod]
        public void ValueContainerExtension_AddValue_NullContainer_ThrowsArgumentNull()
        {
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(
                () => ValueContainerExtensions.AddValue<int>(null!, 12));

            Assert.AreEqual("container", exception.ParamName);
        }

        [TestMethod]
        public void ValueContainerExtension_AddValue_Succeeds()
        {
            const string expected = "foo bar baz";

            ValueContainer<string?> container = new ValueContainer<string?>();

            ValueContainerExtensions.AddValue(container, expected);

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }

        [TestMethod]
        public void ValueContainerExtension_AddValue_AlreadyHasValue_ThrowsInvalidOperation()
        {
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            ValueContainerExtensions.AddValue(container, expected);
            Assert.ThrowsException<InvalidOperationException>(
                () => ValueContainerExtensions.AddValue(container, "bar"));

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }

        [TestMethod]
        public void ValueContainerExtension_GetValue_NullContainer_ThrowsArgumentNull()
        {
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(
                () => ValueContainerExtensions.GetValue<int>(null!, out _));

            Assert.AreEqual("container", exception.ParamName);
        }

        [TestMethod]
        public void ValueContainerExtensions_GetValue_HasValue_Succeeds()
        {
            const int expected = 12;

            ValueContainer<int> container = new ValueContainer<int>();
            container.SetValue(expected);

            Assert.IsTrue(ValueContainerExtensions.GetValue(container, out int actual));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ValueContainerExtensions_GetValue_HasNoValue_Succeeds()
        {
            MockValueContainer<int> container =
                new MockValueContainer<int>()
                {
                    ValueGetter = () => throw new InternalTestFailureException(),
                };

            Assert.IsFalse(ValueContainerExtensions.GetValue(container, out _));
        }
    }
}
