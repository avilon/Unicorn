using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Unicorn
{
    /// <summary>
    /// Парсит позицию в формате FEN. 
    /// Определяет, где какие фигуры стоят и чья в данной позиции очередь хода
    /// </summary>
    public class Fen
    {
        public int Count
        {
            get { return pieces.Count; }
        }
        public Team StartColor { get { return startColor; } }

        public void Parse(string pos)
        {
            pieces.Clear();
            string[] parts = pos.Replace('.', ' ').Split(':');
            if (parts.Length != 3)
            {
                logger.Error("Неверный формат FEN: " + pos);
                return;
            }
            string partColor = parts[0].Trim();
            if (partColor.Contains('W'))
            {
                startColor = Team.White;
            }
            else
            {
                startColor = Team.Black;
            }
            ReadPosition(parts[1], Team.White);
            ReadPosition(parts[2], Team.Black);
        }

        public int GetSquare(int index)
        {
            return pieces.ElementAt(index).Key;
        }
        public Piece GetPiece(int index)
        {
            return pieces.ElementAt(index).Value;
        }
        private void ReadPosition(string pos, Team color)
        {
            try
            {
                string[] numbers = pos.Replace('B', ' ').Replace('W', ' ').Trim().Split(',');

                foreach (string s in numbers)
                {
                    Piece p = new Piece();
                    if (color == Team.White)
                        p.SetWhiteMan();
                    else
                        p.SetBlackMan();

                    if (s.Contains('K'))
                        p.Promote();
                    string sq = s.Replace('K', ' ').Trim();
                    int n = int.Parse(sq);
                    pieces.Add(n, p);
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private Team startColor;
        private Dictionary<int, Piece> pieces = new Dictionary<int,Piece>();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
