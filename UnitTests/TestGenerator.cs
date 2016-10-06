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

        [TestMethod]
        public void TestBlackManSilent()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("B:W46, 47:B1,2.");
            gen.Generate(list);
            Assert.AreEqual(4, list.Count);
            pos.Setup("B:W46,47, 48, 49, 50:B1,2, 3, 4, 5.");
            gen.Generate(list);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        public void TestBlackKingSilent()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("B:WK50:BK5.");
            gen.Generate(list);
            Assert.AreEqual(9, list.Count);

            pos.Setup("B:WK50:BK5,K1.");
            gen.Generate(list);
            Assert.AreEqual(18, list.Count);
        }

        [TestMethod]
        public void TestBlackManCaptures()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("B:W7,50:W1,2.");
            gen.Generate(list);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestWhitePromoCapture()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("W:W15:B10.");
            gen.Generate(list);
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(21, list[0].From);
            Assert.AreEqual(9, list[0].To);
            Assert.AreEqual(Piece.PieceValue.WhiteMan, list[0].Before);
            Assert.AreEqual(Piece.PieceValue.WhiteKing, list[0].After);
            Assert.AreEqual(1, list[0].KillCount);
            Assert.AreEqual(Piece.PieceValue.BlackMan, list[0].GetKillPieceValue(0));
            Assert.AreEqual(15, list[0].GetKillSquare(0));
        }

        [TestMethod]
        public void TestWhiteKingCapture()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("W:WK50:B44.");
            gen.Generate(list);
            Assert.AreEqual(7, list.Count);

            pos.Setup("W:WK50:B11.");
            gen.Generate(list);
            Assert.AreEqual(1, list.Count);

            pos.Setup("W:WK16,K50:B11.");
            gen.Generate(list);
            Assert.AreEqual(3, list.Count);

            pos.Setup("W:WK16,K50:B7,17.");
            gen.Generate(list);
            Assert.AreEqual(1, list.Count);
        }
    }
}
