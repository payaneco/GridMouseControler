using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GridMouseControler;

namespace UnitTestProject1
{
    [TestClass]
    public class KeyMapTest
    {
        [TestMethod]
        public void TestInitialize()
        {
            var handy = new HandyMap();
            Assert.AreEqual(3, handy.Division.X);
            Assert.AreEqual(3, handy.Division.Y);
            Assert.AreEqual("w", handy.GetKey(0, 0));
            Assert.AreEqual("e", handy.GetKey(1, 0));
            Assert.AreEqual("f", handy.GetKey(2, 1));
            Assert.AreEqual("v", handy.GetKey(2, 2));
            var full = new FullMap();
            Assert.AreEqual(5, full.Division.X);
            Assert.AreEqual(5, full.Division.Y);
            Assert.AreEqual("q", full.GetKey(0, 0));
            Assert.AreEqual(";", full.GetKey(4, 4));
        }
    }
}
