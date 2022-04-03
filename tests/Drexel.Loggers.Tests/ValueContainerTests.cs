﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drexel.Loggers.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public sealed class ValueContainerTests
    {
        [TestMethod]
        public void ValueContainer_Ctor_Succeeds()
        {
            ValueContainer<string?> container = new ValueContainer<string?>();

            Assert.IsFalse(container.HasValue);
            Assert.IsNull(container.Value);
        }

        [TestMethod]
        public void ValueContainer_AddValue_Succeeds()
        {
            const string expected = "foo bar baz";

            ValueContainer<string?> container = new ValueContainer<string?>();

            container.AddValue(expected);

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }

        [TestMethod]
        public void ValueContainer_AddValue_AlreadyHasValue_ThrowsInvalidOperation()
        {
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            container.AddValue(expected);
            Assert.ThrowsException<InvalidOperationException>(() => container.AddValue("bar"));

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }

        [TestMethod]
        public void ValueContainer_TryAddValue_Succeeds()
        {
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            Assert.IsTrue(container.TryAddValue(expected, out string? currentValue));
            Assert.IsNull(currentValue);

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }

        [TestMethod]
        public void ValueContainer_TryAddValue_AlreadyHasValue_Succeeds()
        {
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            container.AddValue(expected);

            Assert.IsFalse(container.TryAddValue("bar", out string? currentValue));
            Assert.AreEqual(expected, currentValue);

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }

        [TestMethod]
        public void ValueContainer_RemoveValue_Succeeds()
        {
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            container.AddValue(expected);

            Assert.IsTrue(container.RemoveValue(out string? value));
            Assert.AreEqual(expected, value);

            Assert.IsFalse(container.HasValue);
            Assert.IsNull(container.Value);
        }

        [TestMethod]
        public void ValueContainer_RemoveValue_HasNoValue_Succeeds()
        {
            ValueContainer<string?> container = new ValueContainer<string?>();

            Assert.IsFalse(container.RemoveValue(out string? value));
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueContainer_GetValue_Succeeds()
        {
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            container.AddValue(expected);

            Assert.IsTrue(container.GetValue(out string? value));
            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        public void ValueContainer_GetValue_HasNoValue_Succeeds()
        {
            ValueContainer<string?> container = new ValueContainer<string?>();

            Assert.IsFalse(container.GetValue(out string? value));
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ValueContainer_SetValue_Succeeds()
        {
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            Assert.IsFalse(container.HasValue);
            Assert.IsNull(container.Value);

            Assert.IsFalse(container.SetValue(expected, out string? oldValue));
            Assert.IsNull(oldValue);

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }

        [TestMethod]
        public void ValueContainer_SetValue_AlreadyHasValue_Overwrites()
        {
            const string initial = "bar";
            const string expected = "foo";

            ValueContainer<string?> container = new ValueContainer<string?>();

            container.AddValue(initial);

            Assert.IsTrue(container.SetValue(expected, out string? oldValue));
            Assert.AreEqual(initial, oldValue);

            Assert.IsTrue(container.HasValue);
            Assert.AreEqual(expected, container.Value);
        }
    }
}
