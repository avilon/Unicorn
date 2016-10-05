using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    /// <summary>
    /// Находит в текущей позиции все возможные ходы и помещает их в список
    /// </summary>
    public class MoveGen
    {
        public MoveGen(Position p)
        {
            position = p;
            move = new Move();
            dirs = new int[4];
            dirs[0] = position.MoveLeft;
            dirs[1] = position.MoveRight;
            dirs[2] = -dirs[0];
            dirs[3] = -dirs[1];
            promoteImmediate = false;
        }

        public void Generate(MoveList ml)
        {
            list = ml;
            list.Clear();
            GenerateCaptures();
            if (list.Count == 0)
                GenerateSilentMoves();
        }

        private void GenerateCaptures()
        {
            for ( int i = 0; i < position.Size; i++)
            {
                if (!position[i].IsEmpty)
                {
                    int square = position.GetSquareNumber(i);
                    Piece piece = position[i];
                    if (position.MoveColor == Team.White)
                    {
                        if (piece.IsWhite)
                        {
                            if (piece.IsMan)
                            {
                                TryWhiteManCapture(square);
                            }
                            else
                            {
                                TryWhiteKingCapture(square);
                            }
                        }
                    }
                    if (position.MoveColor == Team.Black)
                    {
                        if (piece.IsBlack)
                        {
                            if (piece.IsMan)
                            {
                                TryBlackManCapture(square);
                            }
                            else
                            {
                                TryBlackKingCapture(square);
                            }
                        }
                    }
                }
            }
        }
        
        private void TryWhiteManCapture(int square)
        {
            foreach (int t in dirs)
            {
                int next = square + t;
                if (position.GetPiece(next).IsBlack)
                {
                    int dest = next + t;
                    if (position.GetPiece(dest).IsEmpty)
                    {
                        move = new Move
                        {
                            From = square,
                            Before = Piece.PieceValue.WhiteMan
                        };

                        move.AddKillPiece(new PieceLocation(next, position.GetPiece(next).Value), 1);
                        position.GetPiece(next).ChangeColor();
                        if (position.IsPromoteSquare(dest, Team.White))
                        {
                            if (promoteImmediate)
                            {
                                FindWhitePromoCapture(dest, t, 1, move);
                            }
                            else
                            {
                                FindNextWhiteManCapture(dest, t, 1, move);
                            }
                        }
                        else
                        {
                            FindNextWhiteManCapture(dest, t, 1, move);
                        }

                        position.GetPiece(next).ChangeColor();
                    }
                }
            }
        }


        private void TryBlackManCapture(int square)
        {
            foreach (int dir in dirs)
            {
                int next = square + dir;
                if (position.GetPiece(next).IsWhite)
                {
                    int dest = next + dir;
                    if (position.GetPiece(dest).IsEmpty)
                    {
                        move = new Move
                        {
                            From = square,
                            Before = Piece.PieceValue.BlackMan
                        };

                        move.AddKillPiece(new PieceLocation(next, position.GetPiece(next).Value), 1);
                        position.GetPiece(next).ChangeColor();
                        if (position.IsPromoteSquare(dest, Team.Black))
                        {
                            if (promoteImmediate)
                            {
                                FindBlackPromoCapture(dest, dir, 1, move);
                            }
                            else
                            {
                                FindNextBlackManCapture(dest, dir, 1, move);
                            }
                        }
                        else
                        {
                            FindNextBlackManCapture(dest, dir, 1, move);
                        }

                        position.GetPiece(next).ChangeColor();
                    }
                }
            }
        }

        private void FindNextWhiteManCapture(int square, int fromDir, int deep, Move move)
        {
            bool found = false;
            foreach (int dir in dirs)
            {
                if (dir != -fromDir)
                {
                    int next = square + dir;
                    if (position.GetPiece(next).IsBlack)
                    {
                        int dest = next + dir;
                        if (position.GetPiece(dest).IsEmpty)
                        {
                            found = true;
                            move.AddKillPiece(new PieceLocation(next, position.GetPiece(next).Value), deep + 1);
                            position.GetPiece(next).ChangeColor();
                            if (position.IsPromoteSquare(dest, Team.White))
                            {
                                if (promoteImmediate)
                                {
                                    FindWhitePromoCapture(dest, dir, deep+1, move);
                                }
                                else
                                {
                                    FindNextWhiteManCapture(dest, dir, deep + 1, move);
                                }
                            }
                            else
                            {
                                FindWhitePromoCapture(dest, dir, deep+1, move);
                            }
                            position.GetPiece(next).ChangeColor();
                        }
                    }
                }
            }

            if (!found)
            {
                move.To = square;
                if (position.IsPromoteSquare(square, Team.White))
                    move.After = Piece.PieceValue.WhiteKing;
                else
                    move.After = Piece.PieceValue.WhiteMan;
                list.Add(move);
            }
        }

        private void FindWhitePromoCapture(int square, int fromDir, int deep, Move move)
        {

        }

        private void FindNextBlackManCapture(int square, int fromDir, int deep, Move move)
        {
            
        }

        private void FindBlackPromoCapture(int square, int fromDir, int deep, Move move)
        {
            
        }

        private void TryWhiteKingCapture(int square)
        {

        }
        
        private void TryBlackKingCapture(int square)
        {

        }

        private void GenerateSilentMoves()
        {
            move.Reset();
            for (int i = 0; i < position.Size; i++)
            {
                if (!position[i].IsEmpty)
                {
                    int square = position.GetSquareNumber(i);
                    Piece piece = position[i];
                    if (position.MoveColor == Team.White)
                    {
                        if (piece.IsWhite)
                        {
                            if ( piece.IsMan)
                            {
                                CheckWhiteManMove(square);
                            }
                            else
                            {
                                CheckWhiteKingMove(square);
                            }
                        }
                    }
                    if (position.MoveColor == Team.Black)
                    {
                        if (piece.IsBlack)
                        {
                            if (piece.IsMan)
                            {
                                CheckBlackManMove(square);
                            }
                            else
                            {
                                CheckBlackKingMove(square);
                            }
                        }
                    }
                }
            }
        }

        private void CheckWhiteManMove(int square)
        {
            int next = square - position.MoveLeft;
            if (position.GetPiece(next).IsEmpty)
            {
                move.From = square;
                move.To = next;
                move.Before = Piece.PieceValue.WhiteMan;
                if (position.IsPromoteSquare(next, Team.White))
                    move.After = Piece.PieceValue.WhiteKing;
                else
                    move.After = Piece.PieceValue.WhiteMan;

                list.Add(move);
            }
            next = square - position.MoveRight;
            if (position.GetPiece(next).IsEmpty)
            {
                move.From = square;
                move.To = next;
                move.Before = Piece.PieceValue.WhiteMan;
                if (position.IsPromoteSquare(next, Team.White))
                    move.After = Piece.PieceValue.WhiteKing;
                else
                    move.After = Piece.PieceValue.WhiteMan;

                list.Add(move);
            }
        }

        private void CheckBlackManMove(int square)
        {
            int next = square + position.MoveLeft;
            if (position.GetPiece(next).IsEmpty)
            {
                move.From = square;
                move.To = next;
                move.Before = Piece.PieceValue.BlackMan;
                if (position.IsPromoteSquare(next, Team.Black))
                    move.After = Piece.PieceValue.BlackKing;
                else
                    move.After = Piece.PieceValue.BlackMan;

                list.Add(move);
            }
            next = square + position.MoveRight;
            if (position.GetPiece(next).IsEmpty)
            {
                move.From = square;
                move.To = next;
                move.Before = Piece.PieceValue.BlackMan;
                if (position.IsPromoteSquare(next, Team.Black))
                    move.After = Piece.PieceValue.BlackKing;
                else
                    move.After = Piece.PieceValue.BlackMan;

                list.Add(move);
            }
        }

        private void CheckWhiteKingMove(int square)
        {
            CheckFreeSquareForKing(square, position.MoveLeft, Piece.PieceValue.WhiteKing);
            CheckFreeSquareForKing(square, position.MoveRight, Piece.PieceValue.WhiteKing);
            CheckFreeSquareForKing(square, -position.MoveLeft, Piece.PieceValue.WhiteKing);
            CheckFreeSquareForKing(square, -position.MoveRight, Piece.PieceValue.WhiteKing);
        }
        private void CheckBlackKingMove(int square)
        {
            CheckFreeSquareForKing(square, position.MoveLeft, Piece.PieceValue.BlackKing);
            CheckFreeSquareForKing(square, position.MoveRight, Piece.PieceValue.BlackKing);
            CheckFreeSquareForKing(square, -position.MoveLeft, Piece.PieceValue.BlackKing);
            CheckFreeSquareForKing(square, -position.MoveRight, Piece.PieceValue.BlackKing);
        }

        private void CheckFreeSquareForKing(int startSquare, int dir, Piece.PieceValue piece)
        {
            int next = startSquare + dir;
            while (position.GetPiece(next).IsEmpty)
            {
                move.From = startSquare;
                move.To = next;
                move.Before = piece;
                move.After = piece;
                list.Add(move);

                next += dir;
            }
        }

        private bool promoteImmediate;
        private MoveList list;
        private Position position;
        private Move move;
        private int[] dirs;
    }
}
