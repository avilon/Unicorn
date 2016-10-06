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

        public void ClearPosition()
        {
            position.Clear();
        }

        public void FillPosition()
        {
            position.Fill();
        }

        public void SetupPosition(string pos)
        {
            position.Setup(pos);
        }

        public void SetPiece(int number)
        {

        }
        public void DoMove(int from, int to)
        {

        }

        public bool IsLegalMove(int from, int to, out string res)
        {
            res = "";
            int cnt = 0;
            int ndx = -1;
            moveGen.Generate(moveList);
            if (moveList.Count > 0)
            {
                for (int i = 0; i < moveList.Count; i++)
                {
                    int f = position.GetBoardNumber(moveList[i].From);
                    int t = position.GetBoardNumber(moveList[i].To);

                    if (from != 0)
                    {
                        if (f == from)
                        {
                            if (to != 0)
                            {
                                if (t == to)
                                {
                                    cnt++;
                                    ndx = i;
                                }
                            }
                            else 
                            {
                                cnt++;
                                ndx = i;
                            }
                        }
                    }
                    else
                    {
                        if (t == to)
                        {
                            cnt++;
                            ndx = i;
                        }
                    }
                }
                if (cnt == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private Position position;
        private MoveList moveList;
        private MoveGen moveGen;
    }
}
