using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    public class PieceLocation : ICloneable
    {
        public PieceLocation(int square, Piece piece)
        {
            this.square = square;
            this.piece = (Piece)piece.Clone();
        }

        public int Square { get { return this.square; } }
        public Piece Piece { get { return this.piece; } }

        private int square;
        private Piece piece;

        public object Clone()
        {
            return new PieceLocation(this.square, (Piece)this.piece.Clone());
        }
    }
}
