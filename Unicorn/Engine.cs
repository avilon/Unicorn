using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn
{
    public class Engine
    {
        public Engine()
        {
            position = new Position();
            moveList = new MoveList();
            moveGen = new MoveGen(position);
        }

        public void SetupPosition(string pos)
        {
            position.Setup(pos);
        }
        public void SetPiece(int number)
        {

        }

        public bool IsLegalMove(int from, int to, out string res)
        {
            res = "";

            return false;
        }

        private Position position;
        private MoveList moveList;
        private MoveGen moveGen;
    }
}
