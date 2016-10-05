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
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("W:WK46:B1.");
            gen.Generate(list);
            Assert.AreEqual(9, list.Count);
            pos.Setup("W:WK5:B1.");
            gen.Generate(list);
            Assert.AreEqual(9, list.Count);
            pos.Setup("W:WK5, K46:B1.");
            gen.Generate(list);
            Assert.AreEqual(16, list.Count);
        }

        [TestMethod]
        public void TestWhiteManCaptures()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("W:W46:B41.");
            gen.Generate(list);
            Assert.AreEqual(1, list.Count);

            pos.Setup("W:W46,47:B41.");
            gen.Generate(list);
            Assert.AreEqual(2, list.Count);

            pos.Setup("W:W36,46,47:B41.");
            gen.Generate(list);
            Assert.AreEqual(1, list.Count);
            if (list.Count == 1)
            {
                Move move = list[0];
                Assert.AreEqual(1, move.KillCount);
            }
        }
    }
}
