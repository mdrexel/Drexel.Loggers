using System;
using System.Collections.Generic;
using Drexel.Loggers.Tests.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Drexel.Loggers.Tests
{
    [TestClass]
    public sealed class NonNullListTests
    {
        [TestMethod]
        public void NonNullList_Ctor_NoParameters_Succeeds()
        {
            NonNullListImpl<string> list = new NonNullListImpl<string>();

            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void NonNullList_Ctor_Capacity_Succeeds()
        {
            const int Capacity = 3;

            NonNullListImpl<string> list = new NonNullListImpl<string>(Capacity);

            Assert.AreEqual(Capacity, list.Capacity);
        }

        [TestMethod]
        public void NonNullList_Ctor_Capacity_LessThanZero_ThrowsArgumentOutOfRange()
        {
            ArgumentOutOfRangeException exception = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new NonNullListImpl<string>(capacity: -1));

            Assert.AreEqual("capacity", exception.ParamName);
        }

        [TestMethod]
        public void NonNullList_Ctor_Params_Succeeds()
        {
            NonNullListImpl<string> list = new NonNullListImpl<string>("foo", "bar", "baz");

            Assert.That.Equal(
                new string[] { "foo", "bar", "baz" },
                list);
        }

        [TestMethod]
        public void NonNullList_Ctor_Params_Null_ThrowsArgumentNull()
        {
            string[] @params = null!;
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(
                () => new NonNullListImpl<string>(itemParams: @params));

            Assert.AreEqual("itemParams", exception.ParamName);
        }

        [TestMethod]
        public void NonNullList_Ctor_Params_NullItem_ThrowsArgument()
        {
            string[] @params = new string[] { "foo", "bar", "baz", null!, "bazinga" };

            ArgumentException exception = Assert.ThrowsException<ArgumentException>(
                () => new NonNullListImpl<string>(itemParams: @params));

            Assert.AreEqual("itemParams", exception.ParamName);
            StringAssert.StartsWith(exception.Message, "Item at index 3 is null.");
        }

        [TestMethod]
        public void NonNullList_Ctor_Collection_Succeeds()
        {
            IReadOnlyCollection<string> expected = new string[] { "Foo", "Bar", "Baz" };

            NonNullListImpl<string> list = new NonNullListImpl<string>(expected);

            Assert.That.Equal(expected, list);
        }

        [TestMethod]
        public void NonNullList_Ctor_Collection_Null_ThrowsArgumentNull()
        {
            IReadOnlyCollection<string> collection = null!;
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(
                () => new NonNullListImpl<string>(collection));

            Assert.AreEqual("collection", exception.ParamName);
        }

        [TestMethod]
        public void NonNullList_Ctor_Collection_NullItem_ThrowsArgument()
        {
            IReadOnlyCollection<string> collection = new string[] { "foo", "bar", "baz", null!, "bazinga" };

            ArgumentException exception = Assert.ThrowsException<ArgumentException>(
                () => new NonNullListImpl<string>(collection));

            Assert.AreEqual("collection", exception.ParamName);
            StringAssert.StartsWith(exception.Message, "Item at index 3 is null.");
        }

        [TestMethod]
        public void NonNullList_Ctor_Enumerable_Succeeds()
        {
            string[] expected = new string[] { "foo", "bar", "baz" };

            NonNullListImpl<string> list = new NonNullListImpl<string>(expected);

            Assert.That.Equal(expected, list);
        }

        [TestMethod]
        public void NonNullList_Ctor_Enumerable_Capacity_Succeeds()
        {
            const int Capacity = 12;
            string[] expected = new string[] { "foo", "bar", "baz" };

            NonNullListImpl<string> list = new NonNullListImpl<string>(expected, Capacity);

            Assert.AreEqual(Capacity, list.Capacity);
            Assert.That.Equal(expected, list);
        }

        [TestMethod]
        public void NonNullList_Ctor_Enumerable_Null_ThrowsArgumentNull()
        {
            IEnumerable<string> enumerable = null!;
            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(
                () => new NonNullListImpl<string>(enumerable));

            Assert.AreEqual("enumerable", exception.ParamName);
        }

        [TestMethod]
        public void NonNullList_Ctor_Enumerable_NullItem_ThrowsArgument()
        {
            IEnumerable<string> enumerable = new string[] { "foo", "bar", "baz", null!, "bazinga" };

            ArgumentException exception = Assert.ThrowsException<ArgumentException>(
                () => new NonNullListImpl<string>(enumerable));

            Assert.AreEqual("enumerable", exception.ParamName);
            StringAssert.StartsWith(exception.Message, "Item at index 3 is null.");
        }

        [TestMethod]
        public void NonNullList_Ctor_Enumerable_Capacity_LessThanZero_ThrowsArgumentOutOfRange()
        {
            ArgumentOutOfRangeException exception = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => new NonNullListImpl<string>(enumerable: new string[] { "foo" }, capacity: -1));

            Assert.AreEqual("capacity", exception.ParamName);
        }

        [TestMethod]
        public void NonNullList_Add_Succeeds()
        {
            NonNullListImpl<string> list = new NonNullListImpl<string>();
            NonNullListImpl<string> result = list.Add("foo").Add("bar").Add("baz");

            Assert.AreSame(list, result);
            Assert.That.Equal(new string[] { "foo", "bar", "baz" }, list);
        }

        [TestMethod]
        public void NonNullList_Add_Null_ThrowsArgumentNull()
        {
            NonNullListImpl<string> list = new NonNullListImpl<string>();

            ArgumentNullException exception = Assert.ThrowsException<ArgumentNullException>(
                () => list.Add(null!));

            Assert.AreEqual("item", exception.ParamName);
        }

        [TestMethod]
        public void NonNullList_Count_Succeeds()
        {
            NonNullListImpl<string> list = new NonNullListImpl<string>("foo", "bar");

            Assert.AreEqual(2, list.Count);

            list.Add("baz");

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void NonNullList_Indexer_Succeeds()
        {
            string[] expected = new string[] { "foo", "bar", "baz", "beep", "boop" };

            NonNullListImpl<string> list = new NonNullListImpl<string>(expected);

            for (int counter = 0; counter < expected.Length; counter++)
            {
                Assert.AreEqual(expected[counter], list[counter]);
            }
        }
    }
}
