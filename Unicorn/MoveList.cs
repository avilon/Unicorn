using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    public class MoveList
    {
        public MoveList()
        {
            moves = new List<Move>();
        }
        public void Clear()
        {
            moves.Clear();
            maxLength = 0;
        }
        public void Add(Move move)
        {
            if ( move.KillCount > maxLength)
            {
                moves.Clear();
                maxLength = move.KillCount;
            }
            moves.Add((Move)move.Clone());
        }

        public int Count { get { return moves.Count; } }
        private List<Move> moves;
        private int maxLength;
    }
}
