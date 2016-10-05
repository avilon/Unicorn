using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    public class Piece : ICloneable
    {
        public enum PieceValue
        {
            Empty,
            WhiteMan,
            WhiteKing,
            BlackMan,
            BlackKing,
            Edge
        }

        public Piece()
        {
            value = PieceValue.Edge;
        }

        public PieceValue Value { get { return value; } }

        #region Boolean properties
        
        public bool IsWhiteMan { get { return (value == PieceValue.WhiteMan); } }
        public bool IsWhiteKing { get { return (value == PieceValue.WhiteKing); } }
        public bool IsBlackMan { get { return (value == PieceValue.BlackMan); } }
        public bool IsBlackKing { get { return (value == PieceValue.BlackKing); } }
        public bool IsEmpty { get { return (value == PieceValue.Empty); } }
        public bool IsMan { get { return ((value == PieceValue.WhiteMan) || (value == PieceValue.BlackMan)); } }
        public bool IsKing { get { return ((value == PieceValue.WhiteKing) || (value == PieceValue.BlackKing)); } }
        public bool IsWhite { get { return ((value == PieceValue.WhiteMan) || (value == PieceValue.WhiteKing)); } }
        public bool IsBlack { get { return ((value == PieceValue.BlackMan) || (value == PieceValue.BlackKing)); } }
        
        #endregion Boolean properties

        #region Set piece value

        public void Clear()
        {
            value = PieceValue.Empty;
        }

        public void SetWhiteMan()
        {
            value = PieceValue.WhiteMan;
        }
        public void SetWhiteKing()
        {
            value = PieceValue.WhiteKing;
        }
        public void SetBlackMan()
        {
            value = PieceValue.BlackMan;
        }
        public void SetBlackKing()
        {
            value = PieceValue.BlackKing;
        }
        
        #endregion Set piece value

        public void ChangeColor()
        {
            if (value == PieceValue.WhiteMan)
            {
                value = PieceValue.BlackMan;
                return;
            }
            if (value == PieceValue.WhiteKing)
            {
                value = PieceValue.BlackKing;
                return;
            }
            if (value == PieceValue.BlackMan)
            {
                value = PieceValue.WhiteMan;
                return;
            }
            if (value == PieceValue.BlackKing)
            {
                value = PieceValue.WhiteKing;
                return;
            }
        }

        public void Promote()
        {
            if (value == PieceValue.WhiteMan)
            {
                value = PieceValue.WhiteKing;
                return;
            }
            if (value == PieceValue.BlackMan)
            {
                value = PieceValue.BlackKing;
                return;
            }
        }

        private PieceValue value;

        public object Clone()
        {
            Piece piece = new Piece();
            piece.value = this.value;
            return piece;
        }
    }
}
