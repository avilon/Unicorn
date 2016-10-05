using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unicorn;

namespace UnitTests
{
    [TestClass]
    public class TestFen
    {
        [TestMethod]
        public void SimpleParsing()
        {
            Fen fen = new Fen();
            Assert.IsNotNull(fen);
            Assert.AreEqual(0, fen.Count);

            fen.Parse("W:W49,50:B1,2.");
            Assert.AreEqual(4, fen.Count);
            Assert.AreEqual(Team.White, fen.StartColor);
        }
    }
}
