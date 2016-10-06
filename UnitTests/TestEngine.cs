using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unicorn;

namespace UnitTests
{
    [TestClass]
    public class TestEngine
    {
        [TestMethod]
        public void TestCreate()
        {
            Engine engine = new Engine();
            Assert.IsNotNull(engine);
        }
        
        [TestMethod]
        public void TestCheckLegalMove()
        {
            string ret = "";
            Engine engine = new Engine();
            engine.SetupPosition("W:W50:B1.");

            Assert.IsTrue(engine.IsLegalMove(50, 45, out ret));
            Assert.IsTrue(engine.IsLegalMove(50, 44, out ret));
            Assert.IsFalse(engine.IsLegalMove(44, 50, out ret));

            engine.SetupPosition("W:W46:B1.");
            Assert.IsTrue(engine.IsLegalMove(46, 0, out ret));
            Assert.IsTrue(engine.IsLegalMove(0, 41, out ret));

            engine.FillPosition();
            Assert.IsTrue(engine.IsLegalMove(31, 26, out ret));
            Assert.IsTrue(engine.IsLegalMove(31, 27, out ret));
            Assert.IsTrue(engine.IsLegalMove(32, 27, out ret));
            Assert.IsTrue(engine.IsLegalMove(32, 28, out ret));
            Assert.IsTrue(engine.IsLegalMove(33, 28, out ret));
            Assert.IsTrue(engine.IsLegalMove(33, 29, out ret));
            Assert.IsTrue(engine.IsLegalMove(34, 29, out ret));
            Assert.IsTrue(engine.IsLegalMove(34, 30, out ret));
            Assert.IsTrue(engine.IsLegalMove(35, 30, out ret));
        }
    }
}
