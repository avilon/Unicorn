using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unicorn;

namespace UnitTests
{
    [TestClass]
    public class TestGenerator
    {
        [TestMethod]
        public void TestCreate()
        {
            Position pos = new Position();
            MoveList moveList = new MoveList();
            MoveGen moveGen = new MoveGen(pos);
            moveGen.Generate(moveList);
        }

        [TestMethod]
        public void TestWhiteManSilent()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos)
;
            pos.Setup("W:W50:B5.");
            gen.Generate(list);
            Assert.AreEqual(2, list.Count);

            pos.Setup("W:W46,50:B5.");
            gen.Generate(list);
            Assert.AreEqual(3, list.Count);

            pos.Fill();
            gen.Generate(list);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        public void TestWhiteKingSilent()
        {

        }
    }
}
