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

        public int MoveRight { get { return moveRight; } }
        public int MoveLeft { get { return moveLeft; } }

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
            if (pos.MoveColor != this.MoveColor)
                return false;

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

        public void DoMove(Move move)
        {
            pieces[move.From].Clear();
            SetPiece(move.To, move.After);
            for ( int i = 0; i < move.KillCount; i++)
            {
                pieces[move.GetKillSquare(i)].Clear();
            }
            ChangeMoveColor();
        }

        public void UndoMove(Move move)
        {
            pieces[move.To].Clear();
            SetPiece(move.From, move.Before);
            for ( int i = 0; i < move.KillCount; i++ )
            {
                SetPiece(move.GetKillSquare(i), move.GetKillPieceValue(i));
            }
            ChangeMoveColor();
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

        public void SetPiece(int square, Piece.PieceValue piece)
        {
            switch (piece)
            {
                case Piece.PieceValue.WhiteMan : pieces[square].SetWhiteMan();  break;
                case Piece.PieceValue.WhiteKing: pieces[square].SetWhiteKing(); break;
                case Piece.PieceValue.BlackMan : pieces[square].SetBlackMan();  break;
                case Piece.PieceValue.BlackKing: pieces[square].SetBlackKing(); break;
                default: break;
            }
        }

        public int GetSquareNumber(int index)
        {
            return map[index];
        }

        /// <summary>
        /// Преобразует внутренний номер squareNum в реальный номер поля, 
        /// который используется в нотации для записи партий, нумерация идет от 1 до N
        /// </summary>
        /// <param name="squareNum"></param>
        /// <returns></returns>
        public int GetBoardNumber(int squareNum)
        {
            for ( int i = 0; i < map.Length; i++)
            {
                if (map[i] == squareNum)
                    return i + 1;
            }
            return 0;
        }

        public bool IsPromoteSquare(int square, Team color)
        {
            if (color == Team.White)
            {
                if ((square > minPromoSquareForWhite) && (square < maxPromoSquareForWhite))
                    return true;
            }
            else
            {
                if ((square > minPromoSquareForBlack) && (square < maxPromoSquareForBlack))
                    return true;
            }
            return false;
        }

        private void Init()        
        {
            squareCount = size + height/2 + width + 1;
            pieces = new Piece[squareCount];
            moveRight = Height / 2;
            moveLeft = moveRight + 1;
            for ( int i = 0; i < squareCount; i++)
                pieces[i] = new Piece();

            map = new int[size];
            InitMap();
            SetPromoSquares();
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
        
        private void SetPromoSquares()
        {
            maxPromoSquareForBlack = map[Size-1] + 1;
            minPromoSquareForBlack = maxPromoSquareForBlack - (width/2 + 1);
            minPromoSquareForWhite = map[0] - 1;
            maxPromoSquareForWhite = minPromoSquareForWhite + (width/2 + 1);
        }

        private void ChangeMoveColor()
        {
            if (moveColor == Team.White)
                moveColor = Team.Black;
            else
                moveColor = Team.White;
        }

        private int height;
        private int width;
        private int size;
        private int squareCount;
        private int piecesPerSide;
        private Team moveColor;

        private int moveRight;
        private int moveLeft;

        private int minPromoSquareForWhite;
        private int maxPromoSquareForWhite;
        private int minPromoSquareForBlack;
        private int maxPromoSquareForBlack;

        private Piece[] pieces;
        private int[] map;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
