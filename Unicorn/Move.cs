using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    /// <summary>
    /// Описывает ход шашечной фигуры
    /// </summary>
    public class Move : ICloneable
    {
        public Move() 
        {
            kills = new PieceLocation[MAX_KILL_COUNT];
            killCount = 0;
        }

        /// <summary>
        /// Номер поля, с которого пошла фигура
        /// </summary>
        public int From { get; set; }
        
        /// <summary>
        /// Номер поля, на которое пришла фигура
        /// </summary>
        public int To { get; set; }

        /// <summary>
        /// Какая фигура начинает ход
        /// </summary>
        public Piece.PieceValue After { get; set; }
        
        /// <summary>
        /// Какая фигура заканчивает ход ( простая может превратится в дамку )
        /// </summary>
        public Piece.PieceValue Before { get; set; }
        
        /// <summary>
        /// Количество сбитых после хода фигур противника
        /// </summary>
        public int KillCount { get { return killCount; } }

        public void Reset()
        {
            From = 0;
            To = 0;
            Before = Piece.PieceValue.Empty;
            After = Piece.PieceValue.Empty;
            killCount = 0;
        }

        public void AddKillPiece(PieceLocation ploc, int index)
        {
            kills[index - 1] = ploc;
            killCount = index;
        }

        private int killCount;
        private PieceLocation[] kills;

        private static int MAX_KILL_COUNT = 32;

        public object Clone()
        {
            Move move = new Move();
            move.From = this.From;
            move.To = this.To;
            move.Before = this.Before;
            move.After = this.After;
            move.killCount = this.KillCount;
            if ( this.killCount > 0 )
            {
                for (int i = 0; i < this.killCount; i++)
                {
                    move.AddKillPiece(this.kills[i], i + 1);
                }
            }

            return move;
        }
    }
}
