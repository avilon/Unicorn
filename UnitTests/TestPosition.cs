using System;
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
    }
}
