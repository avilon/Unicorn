using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unicorn;

namespace UnitTests
{
    [TestClass]
    public class TestMove
    {
        [TestMethod]
        public void TestToStringSilent()
        {
            Move move = new Move();
            move.From = 32;
            move.To = 28;
            Assert.AreEqual("32-28", move.ToString());
        }

        [TestMethod]
        public void TestToStringCapture()
        {

        }
    }
}
