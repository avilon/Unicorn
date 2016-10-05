using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    public class PieceLocation : ICloneable
    {
        public PieceLocation(int square, Piece.PieceValue piece)
        {
            this.square = square;
            this.piece = piece;
        }

        public int Square { get { return this.square; } }
        public Piece.PieceValue Piece { get { return this.piece; } }

        private int square;
        private Piece.PieceValue piece;

        public object Clone()
        {
            return new PieceLocation(this.square, this.Piece);
        }
    }
}
