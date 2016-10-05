using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;
using System.Collections;

namespace Unicorn
{
    /// <summary>
    /// Описание шашечной позиции
    /// </summary>
    public class Position : IEnumerable
    {
        public Position()
        {
            logger.Debug("Create default position");
            height = 10;
            width = 10;
            size = 50;
            piecesPerSide = 20;
            Init();
            Fill();
            moveColor = Team.White;
        }

        /// <summary>
        /// Высота доски
        /// </summary>
        public int Height { get { return height; } }
        /// <summary>
        /// Шиина доски
        /// </summary>
        public int Width { get { return width; } }
        /// <summary>
        /// Количество клеток, по которым могут ходить шашки
        /// </summary>
        public int Size { get { return size; } }
        /// <summary>
        /// Количество фигур у каждой из сторон в начальной расстановке
        /// </summary>
        public int PiecesPerSide { get { return piecesPerSide; } }

        /// <summary>
        /// Цвет фигур, у которых очередь хода
        /// </summary>
        public Team MoveColor { get { return moveColor; } }

        public Piece this[int index]
        {
            get { return pieces[map[index]]; }
        }

        public IEnumerator GetEnumerator()
        {
            return pieces.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position))
                return false;

            Position pos = (Position)obj;
            for (int i = 0; i < Size; i++)
            {
                if (this.pieces[map[i]].Value != pos[i].Value)
                    return false;
            }
            return true;
        }

        public void Clear()
        {
            for (int i = 0; i < Size; i++)
                pieces[map.ElementAt(i)].Clear();
        }

        public void Fill()
        {
            Clear();
            for ( int i = 0; i < piecesPerSide; i++)
            {
                pieces[map[i]].SetBlackMan();
            }
            for ( int i = Size - 1; i > (Size - 1 - piecesPerSide); i--)
            {
                pieces[map[i]].SetWhiteMan();
            }
        }

        public void Setup(string pos)
        {
            Fen fen = new Fen();
            fen.Parse(pos);
            Clear();
            moveColor = fen.StartColor;
            for ( int i = 0; i < fen.Count; i++)
            {
                int square = fen.GetSquare(i);
                Piece p = fen.GetPiece(i);
                SetPiece(square - 1, p);
            }
        }

        public Piece GetPiece(int square)
        {
            return pieces[square];
        }

        public void SetPiece(int square, Piece piece)
        {
            if (piece.IsWhite)
            {
                if (piece.IsMan)
                {
                    pieces[map[square]].SetWhiteMan();
                    return;
                }
                if (piece.IsKing)
                {
                    pieces[map[square]].SetWhiteKing();
                    return;
                }
            }
            if (piece.IsBlack)
            {
                if (piece.IsMan)
                {
                    pieces[map[square]].SetBlackMan();
                    return;
                }
                if (piece.IsKing)
                {
                    pieces[map[square]].SetBlackKing();
                    return;
                }
            }            
        }
        private void Init()        
        {
            squareCount = size + height/2 + width + 1;
            pieces = new Piece[squareCount];
            for ( int i = 0; i < squareCount; i++)
                pieces[i] = new Piece();

            map = new int[size];
            InitMap();    
        }

        private void InitMap()
        {
            int n = Width/2 + 1;
            int nn = n + Width;
            int cnt = Height / 2;
            int p = 0;

            for ( int i = 0; i < cnt; i++ ) {
                for ( int j = n; j < nn; j++ ) {
                    map[p++] = j;
                }
                n = nn + 1;
                nn = n + Width;
            }
        }
        
        private int height;
        private int width;
        private int size;
        private int squareCount;
        private int piecesPerSide;
        private Team moveColor;

        private Piece[] pieces;
        private int[] map;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
