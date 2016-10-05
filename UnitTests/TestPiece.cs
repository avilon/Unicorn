using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Unicorn;

namespace UnitTests
{
    [TestClass]
    public class TestPiece
    {
        [TestMethod]
        public void TestCreate()
        {
            Piece piece = new Piece();
            Assert.AreEqual(Piece.PieceValue.Edge, piece.Value);
        }

        [TestMethod]
        public void TestSetMethods()
        {
            Piece piece = new Piece();
            Assert.AreEqual(Piece.PieceValue.Edge, piece.Value);

            piece.SetWhiteMan();
            Assert.AreEqual(Piece.PieceValue.WhiteMan, piece.Value);
            piece.SetWhiteKing();
            Assert.AreEqual(Piece.PieceValue.WhiteKing, piece.Value);
            piece.SetBlackMan();
            Assert.AreEqual(Piece.PieceValue.BlackMan, piece.Value);
            piece.SetBlackKing();
            Assert.AreEqual(Piece.PieceValue.BlackKing, piece.Value);
        }

        [TestMethod]
        public void TestChangeColor()
        {
            Piece wm = new Piece();
            Piece bm = new Piece();
            wm.SetWhiteMan();
            Assert.AreEqual(Piece.PieceValue.WhiteMan, wm.Value);
            wm.Promote();
            Assert.AreEqual(Piece.PieceValue.WhiteKing, wm.Value);
            wm.Promote();
            Assert.AreEqual(Piece.PieceValue.WhiteKing, wm.Value);

            bm.SetBlackMan();
            Assert.AreEqual(Piece.PieceValue.BlackMan, bm.Value);
            bm.Promote();
            Assert.AreEqual(Piece.PieceValue.BlackKing, bm.Value);
            bm.Promote();
            Assert.AreEqual(Piece.PieceValue.BlackKing, bm.Value);
        }

        [TestMethod]
        public void TestIsMan()
        {

        }
    }
}
