﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unicorn;

namespace UnitTests
{
    [TestClass]
    public class TestPosition
    {
        [TestMethod]
        public void TestCreate()
        {
            Position pos = new Position();
            Assert.IsNotNull(pos);
            Assert.AreEqual(10, pos.Height);
            Assert.AreEqual(10, pos.Width);
            Assert.AreEqual(50, pos.Size);
        }

        [TestMethod]
        public void TestFill()
        {
            Position pos = new Position();
            for (int i = 0; i < pos.PiecesPerSide; i++ )
                Assert.AreEqual(Piece.PieceValue.BlackMan, pos[i].Value);
            for ( int i = pos.Size-1; i > (pos.Size - 1 - pos.PiecesPerSide); i--)
                Assert.AreEqual(Piece.PieceValue.WhiteMan, pos[i].Value);
        }

        [TestMethod]
        public void TestSetup()
        {
            Position pos = new Position();
            pos.Setup("W:W49,50:B1,2.");
            Assert.AreEqual(Team.White, pos.MoveColor);
            Assert.IsTrue(pos[0].IsBlack);
            Assert.IsTrue(pos[0].IsMan);
            Assert.IsTrue(pos[1].IsBlack);
            Assert.IsTrue(pos[1].IsMan);

            Assert.IsTrue(pos[48].IsWhite);
            Assert.IsTrue(pos[48].IsMan);
            Assert.IsTrue(pos[49].IsWhite);
            Assert.IsTrue(pos[49].IsMan);
        }

        [TestMethod]
        public void TestPromoSquares()
        {
            Position pos = new Position();
            Assert.IsTrue(pos.IsPromoteSquare(6, Team.White));
            Assert.IsTrue(pos.IsPromoteSquare(7, Team.White));
            Assert.IsTrue(pos.IsPromoteSquare(8, Team.White));
            Assert.IsTrue(pos.IsPromoteSquare(9, Team.White));
            Assert.IsTrue(pos.IsPromoteSquare(10, Team.White));

            Assert.IsFalse(pos.IsPromoteSquare(5, Team.White));
            Assert.IsFalse(pos.IsPromoteSquare(11, Team.White));

            Assert.IsTrue(pos.IsPromoteSquare(55, Team.Black));
            Assert.IsTrue(pos.IsPromoteSquare(56, Team.Black));
            Assert.IsTrue(pos.IsPromoteSquare(57, Team.Black));
            Assert.IsTrue(pos.IsPromoteSquare(58, Team.Black));
            Assert.IsTrue(pos.IsPromoteSquare(59, Team.Black));

            Assert.IsFalse(pos.IsPromoteSquare(54, Team.Black));
            Assert.IsFalse(pos.IsPromoteSquare(60, Team.Black));

        }

        [TestMethod]
        public void TestEquals()
        {
            Position first = new Position();
            Position second = new Position();
            first.Fill();
            second.Fill();
            Assert.IsTrue(first.Equals(second));
            second.Setup("W:W46,47:B1,2.");
            Assert.IsFalse(first.Equals(second));
            first.Setup("W:W46,47:B1,2.");
            Assert.IsTrue(first.Equals(second));

            second.Setup("B:W46,47:B1,2.");
            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void TestDoMove()
        {
            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            pos.Setup("W:W45:B1.");
            Assert.AreEqual(Piece.PieceValue.WhiteMan, pos[44].Value);
            Assert.AreEqual(Piece.PieceValue.Empty, pos[39].Value);

            gen.Generate(list);
            Assert.AreEqual(1, list.Count);
            pos.DoMove(list[0]);
            Assert.AreEqual(Piece.PieceValue.Empty, pos[44].Value);
            Assert.AreEqual(Piece.PieceValue.WhiteMan, pos[39].Value);
        }

        [TestMethod]
        public void TestDoUndoSilentMove()
        {
            Position etalon = new Position();

            Position pos = new Position();
            MoveList list = new MoveList();
            MoveGen gen = new MoveGen(pos);

            etalon.Setup("W:W45:B1.");
            pos.Setup("W:W45:B1.");
            Assert.IsTrue(pos.Equals(etalon));

            gen.Generate(list);
            Assert.AreEqual(1, list.Count);
            pos.DoMove(list[0]);
            pos.UndoMove(list[0]);
            Assert.IsTrue(pos.Equals(etalon));

            etalon.Setup("W:WK46:B1.");
            pos.Setup("W:WK46:B1.");
            Assert.IsTrue(pos.Equals(etalon));
            gen.Generate(list);
            for ( int i = 0; i < list.Count; i++)
            {
                pos.DoMove(list[i]);
                pos.UndoMove(list[i]);
            }
            Assert.IsTrue(pos.Equals(etalon));

            etalon.Setup("W:WK46,K47,K48,K49:B1.");
            pos.Setup("W:WK46,K47,K48,K49:B1.");
            Assert.IsTrue(pos.Equals(etalon));
            gen.Generate(list);
            for (int i = 0; i < list.Count; i++)
            {
                pos.DoMove(list[i]);
                pos.UndoMove(list[i]);
            }
            Assert.IsTrue(pos.Equals(etalon));

            etalon.Setup("B:WK50:BK1,K2,K3,K4,K5.");
            pos.Setup("B:WK50:BK1,K2,K3,K4,K5.");
            Assert.IsTrue(pos.Equals(etalon));
            gen.Generate(list);
            for (int i = 0; i < list.Count; i++)
            {
                pos.DoMove(list[i]);
                pos.UndoMove(list[i]);
            }
            Assert.IsTrue(pos.Equals(etalon));

            etalon.Fill();
            pos.Fill();
            Assert.IsTrue(pos.Equals(etalon));
            gen.Generate(list);
            for (int i = 0; i < list.Count; i++)
            {
                pos.DoMove(list[i]);
                pos.UndoMove(list[i]);
            }
            Assert.IsTrue(pos.Equals(etalon));
        }

        [TestMethod]
        public void TestDoUndoCaptures()
        {

        }
    }
}
